using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Puzzle.NContext.Framework
{


    public partial class Context<TEMPLATE> : IContext<TEMPLATE> where TEMPLATE : ITemplate 
    {
        protected ContextState state = new ContextState();
        protected TEMPLATE template;

        public ContextState State
        {
            get { return state; }
        }

        public Context(Type implementationType)
        {
            RegisterTemplate(implementationType);
        }
        public Context()
        {
            RegisterTemplate(typeof(TEMPLATE));
        }


        public virtual void SubstituteType<T, S>()
        {
            if (!typeof(T).IsAssignableFrom(typeof(S)))
                throw ExceptionHelper.TypeSubstitutionException<T,S>();

            state.TypeSubstitutes.Add(typeof(T), typeof(S));
        }

        public TEMPLATE Template
        {
            get
            {
                return template;
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
                //if something blows up make sure to clear the graph cache
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
                    try
                    {
                        //get from context cache
                        if (config.InstanceMode == InstanceMode.PerContext && state.namedPerContextObjects.ContainsKey(factoryId))
                            return (T)state.namedPerContextObjects[factoryId];

                        //get from graph cache
                        if (config.InstanceMode == InstanceMode.PerGraph && state.namedPerGraphObjects.ContainsKey(factoryId))
                            return (T)state.namedPerGraphObjects[factoryId];

                        //get from thread cache
                        if (config.InstanceMode == InstanceMode.PerThread && state.namedPerThreadObjects.ContainsKey(factoryId))
                            return (T)state.namedPerThreadObjects[factoryId];

                        object res = config.FactoryDelegate();

                        if (res is IRunnable)
                            RunnableEngine.RunRunnable(res as IRunnable);

                        //add to context cache
                        if (config.InstanceMode == InstanceMode.PerContext)
                            state.namedPerContextObjects.Add(factoryId, res);

                        //add to graph cache
                        if (config.InstanceMode == InstanceMode.PerGraph)
                            state.namedPerGraphObjects.Add(factoryId, res);

                        //add to thread cache
                        if (config.InstanceMode == InstanceMode.PerThread)
                            state.namedPerThreadObjects.Add(factoryId, res);

                        return (T)res;
                    }
                    finally
                    {
                    }
                }
            }

            throw ExceptionHelper.NamedFactoryNotFoundException(factoryId);
        }

        public T GetObject<T>()
        {
            bool inGraphStack = inGraphCall;
            try
            {
                inGraphCall = true;
                T res = InternalGetObject<T>();
                return res;
            }
            finally
            {
                inGraphCall = inGraphStack;
                if (!inGraphCall)
                    ClearPerGraphCache();
            }
        }

        private T InternalGetObject<T>()
        {
            lock (syncRoot)
            {
                if (state.TypedObjectFactories.ContainsKey(typeof(T)))
                {
                    ObjectFactoryInfo config = state.TypedObjectFactories[typeof(T)];
                    object res = config.FactoryDelegate();
                    return (T)res;
                }
            }

            throw ExceptionHelper.TypedFactoryNotFoundException(typeof(T));
        }

        public void RegisterObject<T>(string objectId, T item)
        {
            //TODO: add code
        }

        public void RegisterObject<T>(object item)
        {            
        }

        private void RegisterObjectFactoryMethod(Type objectType, FactoryDelegate factoryDelegate, InstanceMode instanceMode)
        {
            ObjectFactoryInfo factory = CreateObjectFactory("DefaultFor:" + objectType.Name,factoryDelegate, instanceMode);
            state.TypedObjectFactories.Add(objectType, factory);

        }

        private void RegisterObjectFactoryMethod(string factoryId, FactoryDelegate factoryDelegate, InstanceMode instanceMode)
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

 

        public void ConfigureObject<T>(object item)
        {
            if (state.TypedObjectConfigurations.ContainsKey(typeof(T)))
            {
                ObjectConfigurationInfo config = state.TypedObjectConfigurations[typeof(T)];
                config.ConfigureDelegate(item);
                ConfigureObjectWithTemplates(item);
                return;
            }

            throw ExceptionHelper.TypedConfigurationNotFoundException(typeof(T));
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

        private void RegisterObjectConfigurationMethod(ITemplate factory, MethodInfo method, ConfigurationMethodAttribute attrib)
        {
            Type objectType = method.ReturnType;

            if (method.GetParameters().Length != 1)
                throw new NotSupportedException("Configuration methods must have only 1 parameter");

            ConfigureDelegate configDelegate = CreateMethodConfigurationDelegate(factory, method);
            Type defaultType = method.GetParameters().First().ParameterType;

            if (attrib.ConfigId != null)
            {                
                RegisterObjectConfigurationMethod(attrib.ConfigId, configDelegate);
            }
            else if (attrib.RegisterAs == ConfigurationType.DefaultForType)
            {               
                RegisterObjectConfigurationMethod(defaultType, configDelegate);
            }
            else if (attrib.RegisterAs == ConfigurationType.AppliesToAll)
            {
                RegisterObjectTemplateConfigurationMethod(defaultType, configDelegate);
            }
            else
            {
                string objectId = method.Name;
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

        private void RegisterObjectConfigurationMethod(Type configType, ConfigureDelegate configMethod)
        {
            ObjectConfigurationInfo config = CreateObjectConfiguration(configMethod);
            state.TypedObjectConfigurations.Add(configType, config);
        }

        private void RegisterObjectTemplateConfigurationMethod(Type configType, ConfigureDelegate configMethod)
        {
            ObjectConfigurationInfo config = CreateObjectConfiguration(configMethod);
            state.ApplyToAllObjectConfigurations.Add(configType, config);
        }

        private void RegisterTemplate(Type templateType)
        {
            ITemplate template = (ITemplate)state.aopEngine.CreateProxy(templateType);

            template.Context = this;
            template.Initialize();
            this.template = (TEMPLATE)template;

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
