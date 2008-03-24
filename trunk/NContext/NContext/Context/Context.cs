using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Puzzle.NContext.Framework
{
    public partial class Context :  IContext
    {
        protected ContextState state = new ContextState();

        protected Context()
        {            
        }

        public virtual IContext ParentContext {get;set;}

        public virtual void SubstituteType<T, S>()
        {
            if (!typeof(T).IsAssignableFrom(typeof(S)))
                throw ExceptionHelper.TypeSubstitutionException<T,S>();

            state.TypeSubstitutes.Add(typeof(T), typeof(S));
        }

        //public T GetObject<T, F>(Expression<Func<F,Func<T>>> factoryMethod) where F : IObjectInitializer
        //{
        //    LambdaExpression lambda = factoryMethod as LambdaExpression;
        //    UnaryExpression u = lambda.Body as UnaryExpression;
        //    MethodCallExpression call = u.Operand as MethodCallExpression;
        //    ConstantExpression methodExpression = call.Arguments[2] as ConstantExpression;
        //    MethodInfo method = methodExpression.Value as MethodInfo;
        //    return GetObjectFromMethodInfo<T>(method);
        //}

        public F Template<F>() where F : ITemplate
        {
            foreach (ITemplate initializer in state.Templates)
            {
                if (initializer is F)
                    return (F)initializer;
            }

            throw new Exception("Factory type was not found");
        }

        public T GetObject<T>(Func<T> factoryMethod)
        {
            ITemplate template = factoryMethod.Target as ITemplate;
            if (template == null)
                throw new Exception(string.Format("The method does not belong to an IObjectFactory"));

            if (!state.Templates.Contains(template))
                throw new Exception(string.Format("The template is not registered in the Context"));

            MethodInfo method = factoryMethod.Method;
            return GetObjectFromMethodInfo<T>(method);
        }

        private T GetObjectFromMethodInfo<T>(MethodInfo method)
        {
            FactoryMethodAttribute attrib = method.GetCustomAttributes(typeof(FactoryMethodAttribute), true).FirstOrDefault() as FactoryMethodAttribute;
            if (attrib == null)
                throw ExceptionHelper.NotFactoryMethodException();

            if (attrib.FactoryId != null)
            {
                return GetObject<T>(attrib.FactoryId);
            }
            else if (attrib.RegisterAs == FactoryType.DefaultForType)
            {
                Type defaultType = method.ReturnType;
                return GetObject<T>(defaultType);
            }
            else
            {
                string factoryId = method.Name;
                return GetObject<T>(factoryId);
            }
        }

        public T CreateObject<T>(params object[] args)
        {
            Type typeToCreate = null;
            if (!state.TypeSubstitutes.TryGetValue(typeof(T), out typeToCreate))
                typeToCreate = typeof(T);

            T res = (T)Activator.CreateInstance(typeToCreate, args);

            //assign countextbound objects to this context
            if (res is IContextBound)
            {
                ((IContextBound)res).Context = this;
            }

            ConfigureObjectWithTemplates(res);
            return res;
        }

        private void ClearPerGraphCache()
        {
            state.namedPerGraphObjects.Clear();
            state.typedPerGraphObjects.Clear();
        }

        private bool inGraphCall = false;
        public T GetObject<T>(string factoryId)
        {
            bool inGraphStack = inGraphCall;
            try
            {
                inGraphCall = true;
                T res = InternalGetObject<T>(factoryId);
                return res;
            }
            finally
            {
                inGraphCall = inGraphStack;
                if (!inGraphCall)
                    ClearPerGraphCache();
            }
        }

        private object syncRoot = new object();
        private T InternalGetObject<T>(string factoryId)
        {
            lock (syncRoot)
            {
                if (state.NamedObjectFactories.ContainsKey(factoryId))
                {
                    ObjectFactoryInfo config = state.NamedObjectFactories[factoryId];
                    VerifyInstanceModeIntegrity(config);

                    state.configStack.Push(config);
                    try
                    {
                        if (config.InstanceMode == InstanceMode.PerContext && state.namedPerContextObjects.ContainsKey(factoryId))
                            return (T)state.namedPerContextObjects[factoryId];

                        if (config.InstanceMode == InstanceMode.PerGraph && state.namedPerGraphObjects.ContainsKey(factoryId))
                            return (T)state.namedPerGraphObjects[factoryId];

                        if (config.InstanceMode == InstanceMode.PerThread && state.namedPerThreadObjects.ContainsKey(factoryId))
                            return (T)state.namedPerThreadObjects[factoryId];

                        object res = config.FactoryDelegate();
                        ConfigureObjectWithTemplates((T)res);

                        if (res is IRunnable)
                            RunnableEngine.RunRunnable(res as IRunnable);

                        if (config.InstanceMode == InstanceMode.PerContext)
                            state.namedPerContextObjects.Add(factoryId, res);

                        if (config.InstanceMode == InstanceMode.PerGraph)
                            state.namedPerGraphObjects.Add(factoryId, res);

                        if (config.InstanceMode == InstanceMode.PerThread)
                            state.namedPerThreadObjects.Add(factoryId, res);

                        return (T)res;
                    }
                    finally
                    {
                        state.configStack.Pop();
                    }
                }
            }

            if (ParentContext != null)
                return ParentContext.GetObject<T>(factoryId);
            else
                throw ExceptionHelper.NamedFactoryNotFoundException(factoryId);
        }

        public T GetObject<T>(Type factoryType)
        {
            bool inGraphStack = inGraphCall;
            try
            {
                inGraphCall = true;
                T res = InternalGetObject<T>(factoryType);
                return res;
            }
            finally
            {
                inGraphCall = inGraphStack;
                if (!inGraphCall)
                    ClearPerGraphCache();
            }
        }

        private T InternalGetObject<T>(Type factoryType)
        {
            lock (syncRoot)
            {
                if (state.TypedObjectFactories.ContainsKey(factoryType))
                {
                    ObjectFactoryInfo config = state.TypedObjectFactories[factoryType];
                    VerifyInstanceModeIntegrity(config);
                    state.configStack.Push(config);

                    try
                    {
                        if (config.InstanceMode == InstanceMode.PerContext && state.typedPerContextObjects.ContainsKey(factoryType))
                            return (T)state.typedPerContextObjects[factoryType];

                        if (config.InstanceMode == InstanceMode.PerGraph && state.typedPerGraphObjects.ContainsKey(factoryType))
                            return (T)state.typedPerGraphObjects[factoryType];

                        if (config.InstanceMode == InstanceMode.PerThread && state.typedPerThreadObjects.ContainsKey(factoryType))
                            return (T)state.typedPerThreadObjects[factoryType];

                        object res = config.FactoryDelegate();
                        ConfigureObjectWithTemplates((T)res);

                        if (res is IRunnable)
                            RunnableEngine.RunRunnable(res as IRunnable);

                        if (config.InstanceMode == InstanceMode.PerContext)
                            state.typedPerContextObjects.Add(factoryType, res);

                        if (config.InstanceMode == InstanceMode.PerGraph)
                            state.typedPerGraphObjects.Add(factoryType, res);

                        if (config.InstanceMode == InstanceMode.PerThread)
                            state.typedPerThreadObjects.Add(factoryType, res);

                        return (T)res;
                    }
                    finally
                    {
                        state.configStack.Pop();
                    }
                }
            }

            if (ParentContext != null)
                return ParentContext.GetObject<T>(factoryType);
            else
                throw ExceptionHelper.TypedFactoryNotFoundException(factoryType);
        }

        private void VerifyInstanceModeIntegrity(ObjectFactoryInfo nextConfig)
        {
            if (state.configStack.Count == 0)
                return;

            //ObjectFactoryInfo prevConfig = state.configStack.Peek();

            //if (prevConfig.InstanceMode > nextConfig.InstanceMode)
            //    throw new Exception(string.Format("Object '{0}' with InstanceMode '{1}' is referencing object '{2}' with InstanceMode '{3}'", prevConfig.DisplayName,prevConfig.InstanceMode,nextConfig.DisplayName ,nextConfig.InstanceMode));
        }



        public T GetObject<T>()
        {
            return GetObject<T>(typeof(T));
        }

        public void RegisterObject<T>(string objectId, T item)
        {

        }

        public void RegisterObject<T>(Type objectType, T item)
        {
            if (!objectType.IsAssignableFrom(typeof(T)))
                throw ExceptionHelper.RegisterObjectTypeMismatchException();

        }



        public void RegisterTemplate<F>() where F:ITemplate
        {
            RegisterTemplate(typeof(F));
        }

        public void RegisterObjectFactoryMethod(Type objectType, FactoryDelegate factoryDelegate, InstanceMode instanceMode)
        {
            ObjectFactoryInfo factory = CreateObjectFactory("DefaultFor:" + objectType.Name,factoryDelegate, instanceMode);
            state.TypedObjectFactories.Add(objectType, factory);
        }

        public void RegisterObjectFactoryMethod(string factoryId, FactoryDelegate factoryDelegate, InstanceMode instanceMode)
        {
            ObjectFactoryInfo factory = CreateObjectFactory(factoryId,factoryDelegate, instanceMode);
            state.NamedObjectFactories.Add(factoryId, factory);
        }

        public void ConfigureObject<T>(string configId, T item)
        {
            if (state.NamedObjectConfigurations.ContainsKey(configId))
            {
                ObjectConfigurationInfo config = state.NamedObjectConfigurations[configId];
                config.ConfigureDelegate(item);
                ConfigureObjectWithTemplates(item);
                return;
            }

            throw ExceptionHelper.NamedConfigurationNotFoundException(configId);
        }

 

        public void ConfigureObject<T>(Type configType, T item)
        {
            if (state.TypedObjectConfigurations.ContainsKey(configType))
            {
                ObjectConfigurationInfo config = state.TypedObjectConfigurations[configType];
                config.ConfigureDelegate(item);
                ConfigureObjectWithTemplates(item);
                return;
            }

            throw ExceptionHelper.TypedConfigurationNotFoundException(configType);
        }

        protected virtual void ConfigureObjectWithTemplates<T>(T item)
        {
            foreach (var entry in state.ApplyToAllObjectConfigurations)
            {
                if (entry.Key.IsAssignableFrom(typeof(T)))
                {
                    entry.Value.ConfigureDelegate(item);
                }
            }
        }

        public void ConfigureObject<T>(ConfigureDelegate configMethod, T item)
        {
            MethodInfo method = configMethod.Method;
            ITemplate template = configMethod.Target as ITemplate;
            if (template == null)
                throw new Exception(string.Format("The method does not belong to an IObjectFactory"));

            ConfigurationMethodAttribute attrib = method.GetCustomAttributes(typeof(ConfigurationMethodAttribute), true).FirstOrDefault() as ConfigurationMethodAttribute;

            if (!state.Templates.Contains(template))
                throw new Exception(string.Format("The template is not registered in the Context")); ;

            if (attrib == null)
                throw ExceptionHelper.NotConfigurationMethod();

            if (attrib.ConfigId != null)
            {
                ConfigureObject<T>(attrib.ConfigId,item);
            }
            else if (attrib.RegisterAs == ConfigurationType.DefaultForType)
            {
                Type defaultType = method.GetParameters().First().ParameterType;
                ConfigureObject<T>(defaultType, item);
            }
            else
            {
                string configId = method.Name;
                ConfigureObject<T>(configId, item);
            }
        }



        private void RegisterObjectConfigurationMethod(ITemplate factory, MethodInfo method, ConfigurationMethodAttribute attrib)
        {
            Type objectType = method.ReturnType;

            if (method.GetParameters().Length != 1)
                throw new NotSupportedException("Configuration methods must have only 1 parameter");

            if (attrib.ConfigId != null)
            {
                ConfigureDelegate configDelegate = CreateMethodConfigurationDelegate(factory, method);
                RegisterObjectConfigurationMethod(attrib.ConfigId, configDelegate);
            }
            else if (attrib.RegisterAs == ConfigurationType.DefaultForType)
            {
                Type defaultType = method.GetParameters().First().ParameterType;
                ConfigureDelegate configDelegate = CreateMethodConfigurationDelegate(factory, method);
                RegisterObjectConfigurationMethod(defaultType, configDelegate);
            }
            else if (attrib.RegisterAs == ConfigurationType.AppliesToAll)
            {
                Type defaultType = method.GetParameters().First().ParameterType;
                ConfigureDelegate configDelegate = CreateMethodConfigurationDelegate(factory, method);
                RegisterObjectTemplateConfigurationMethod(defaultType, configDelegate);
                
            }
            else
            {
                string objectId = method.Name;
                ConfigureDelegate configDelegate = CreateMethodConfigurationDelegate(factory, method);
                RegisterObjectConfigurationMethod(objectId, configDelegate);
            }
        }

        private ConfigureDelegate CreateMethodConfigurationDelegate(ITemplate factory, MethodInfo method)
        {
            ParameterInfo param = method.GetParameters().First();
            ParameterExpression paramExpression = Expression.Parameter(typeof(object), "arg");
            Expression castExpression = Expression.TypeAs(paramExpression, param.ParameterType);
            Expression factoryInstanceExpression = Expression.Constant(factory);
            Expression callExpression = Expression.Call(factoryInstanceExpression, method,castExpression);
            LambdaExpression lambda = Expression.Lambda(typeof(ConfigureDelegate), callExpression, paramExpression);
            ConfigureDelegate del = (ConfigureDelegate)lambda.Compile();
            return del;
        }

        private void RegisterObjectFactoryMethod(ITemplate factory, MethodInfo method, FactoryMethodAttribute attrib)
        {
            Type objectType = method.ReturnType;

            if (method.GetParameters().Length != 0)
                throw ExceptionHelper.FactoryParameterCountException();

            if (attrib.FactoryId != null)
            {
                FactoryDelegate factoryDelegate = CreateMethodFactoryDelegate(factory, method);
                RegisterObjectFactoryMethod(attrib.FactoryId, factoryDelegate, attrib.InstanceMode);
            }
            else if (attrib.RegisterAs == FactoryType.DefaultForType)
            {
                FactoryDelegate factoryDelegate = CreateMethodFactoryDelegate(factory, method);
                Type defaultType = method.ReturnType;
                RegisterObjectFactoryMethod(defaultType, factoryDelegate, attrib.InstanceMode);
            }
            else
            {
                string objectId = method.Name;
                FactoryDelegate factoryDelegate = CreateMethodFactoryDelegate(factory, method);
                RegisterObjectFactoryMethod(objectId, factoryDelegate, attrib.InstanceMode);
            }
        }

        private FactoryDelegate CreateMethodFactoryDelegate(ITemplate factory, MethodInfo method)
        {
            ConstantExpression instance = Expression.Constant(factory);
            MethodCallExpression call = Expression.Call(instance, method);
            Expression body = Expression.TypeAs(call, typeof(object));
            LambdaExpression lambda = Expression.Lambda(typeof(FactoryDelegate), body);
            FactoryDelegate factoryDelegate = (FactoryDelegate)lambda.Compile();
            return factoryDelegate;
        }

        private ObjectFactoryInfo CreateObjectFactory(string displayName,FactoryDelegate factoryDelegate, InstanceMode instanceMode)
        {
            ObjectFactoryInfo factory = new ObjectFactoryInfo();
            factory.FactoryDelegate = factoryDelegate;
            factory.InstanceMode = instanceMode;
            factory.DisplayName = displayName;
            return factory;
        }

        public void RegisterObjectConfigurationMethod(string configId, ConfigureDelegate configMethod)
        {
            ObjectConfigurationInfo config = CreateObjectConfiguration(configMethod);
            state.NamedObjectConfigurations.Add(configId, config);
        }

        private ObjectConfigurationInfo CreateObjectConfiguration(ConfigureDelegate configMethod)
        {
            ObjectConfigurationInfo config = new ObjectConfigurationInfo();
            config.ConfigureDelegate = configMethod;
            return config;
        }

        public void RegisterObjectConfigurationMethod(Type configType, ConfigureDelegate configMethod)
        {
            ObjectConfigurationInfo config = CreateObjectConfiguration(configMethod);
            state.TypedObjectConfigurations.Add(configType, config);
        }

        public void RegisterObjectTemplateConfigurationMethod(Type configType, ConfigureDelegate configMethod)
        {
            ObjectConfigurationInfo config = CreateObjectConfiguration(configMethod);
            state.ApplyToAllObjectConfigurations.Add(configType, config);
        }

        public void RegisterTemplate(Type templateType)
        {
            ITemplate template = (ITemplate)state.aopEngine.CreateProxy(templateType);

            template.Context = this;
            template.Initialize();
            state.Templates.Add(template);

            foreach (MethodInfo method in template.GetType().GetMethods())
            {
                FactoryMethodAttribute attrib = method.GetCustomAttributes(typeof(FactoryMethodAttribute), true).FirstOrDefault() as FactoryMethodAttribute;

                if (attrib == null)
                    continue; //not a factory method

                if (!method.IsVirtual)
                {
                    throw new Exception(string.Format("Factory method '{0}.{1}' must be marked as virtual", template.GetType().Name, method.Name));
                }

                RegisterObjectFactoryMethod(template, method, attrib);
            }

            foreach (MethodInfo method in template.GetType().GetMethods())
            {
                ConfigurationMethodAttribute attrib = method.GetCustomAttributes(typeof(ConfigurationMethodAttribute), true).FirstOrDefault() as ConfigurationMethodAttribute;

                if (attrib == null)
                    continue; //not a configuration method

                RegisterObjectConfigurationMethod(template, method, attrib);
            }
        }
    }
}
