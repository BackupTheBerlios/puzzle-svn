using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Puzzle.NContext.Framework
{
    public partial class Context : IContext
    {
        protected IList<IObjectInitializer> objectFactories = new List<IObjectInitializer>();
        
        protected IDictionary<string, ObjectFactoryInfo> namedObjectFactories = new Dictionary<string, ObjectFactoryInfo>();
        protected IDictionary<Type, ObjectFactoryInfo> typedObjectFactories = new Dictionary<Type, ObjectFactoryInfo>();

        protected IDictionary<string, ObjectConfigurationInfo> namedObjectConfigurations = new Dictionary<string, ObjectConfigurationInfo>();
        protected IDictionary<Type, ObjectConfigurationInfo> typedObjectConfigurations = new Dictionary<Type, ObjectConfigurationInfo>();

        protected IDictionary<Type, Type> typeSubstitutes = new Dictionary<Type, Type>();
        
        protected Context()
        {
        }

        public virtual void SubstituteType<T, S>()
        {
            typeSubstitutes.Add(typeof(T), typeof(S));
        }
        
        public T GetObject<T>(Func<T> factoryMethod)
        {
            MethodInfo method = factoryMethod.Method;
            IObjectInitializer factory = factoryMethod.Target as IObjectInitializer;
            if (factory == null)
                throw new Exception(string.Format("The method does not belong to an IObjectFactory"));

            FactoryMethodAttribute attrib = method.GetCustomAttributes(typeof(FactoryMethodAttribute), true).FirstOrDefault() as FactoryMethodAttribute;

            if (!objectFactories.Contains(factory))
                RegisterObjectFactory(factory);

            if (attrib == null)
                throw new Exception("Method is not a factory method");

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
            if (!typeSubstitutes.TryGetValue(typeof(T), out typeToCreate))
                typeToCreate = typeof(T);

            T res = (T)Activator.CreateInstance(typeToCreate, args);
            return res;
        }

        public T GetObject<T>(string factoryId)
        {
            if (namedObjectFactories.ContainsKey(factoryId))
            {
                ObjectFactoryInfo config = namedObjectFactories[factoryId];
                object res = config.FactoryDelegate();
                return (T)res;
            }

            throw new Exception(string.Format("Named factory '{0}' was not found", factoryId));
        }

        public T GetObject<T>(Type factoryType)
        {
            if (typedObjectFactories.ContainsKey(factoryType))
            {
                ObjectFactoryInfo config = typedObjectFactories[factoryType];
                object res = config.FactoryDelegate();
                return (T)res;
            }

            throw new Exception(string.Format("Typed factory '{0}' was not found", factoryType.Name));
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
                throw new ArgumentException("Parameter 'item' must be assignable to parameter 'objectType'", "item");

        }

        public void RegisterObjectFactory(IObjectInitializer factory)
        {
            factory.Context = this;
            objectFactories.Add(factory);

            foreach (MethodInfo method in factory.GetType().GetMethods())
            {
                FactoryMethodAttribute attrib = method.GetCustomAttributes(typeof(FactoryMethodAttribute), true).FirstOrDefault() as FactoryMethodAttribute;
                
                if (attrib == null)
                    continue; //not a factory method

                RegisterObjectFactoryMethod(factory, method, attrib);
            }

            foreach (MethodInfo method in factory.GetType().GetMethods())
            {
                ConfigureMethodAttribute attrib = method.GetCustomAttributes(typeof(ConfigureMethodAttribute), true).FirstOrDefault() as ConfigureMethodAttribute;

                if (attrib == null)
                    continue; //not a configuration method

                RegisterObjectConfigurationMethod(factory, method, attrib);
            }
        }

        public void RegisterObjectFactoryMethod(Type objectType, FactoryDelegate factoryDelegate, InstanceMode instanceMode)
        {
            ObjectFactoryInfo factory = CreateObjectFactory(factoryDelegate, instanceMode);
            typedObjectFactories.Add(objectType, factory);
        }

        public void RegisterObjectFactoryMethod(string objectId, FactoryDelegate factoryDelegate, InstanceMode instanceMode)
        {
            ObjectFactoryInfo factory = CreateObjectFactory(factoryDelegate, instanceMode);
            namedObjectFactories.Add(objectId, factory);
        }

        public void ConfigureObject<T>(string configId, T item)
        {
            if (namedObjectConfigurations.ContainsKey(configId))
            {
                ObjectConfigurationInfo config = namedObjectConfigurations[configId];
                config.ConfigureDelegate(item);
                return;
            }

            throw new Exception(string.Format("Named configuration '{0}' was not found", configId));
        }

        public void ConfigureObject<T>(Type configType, T item)
        {
            if (typedObjectConfigurations.ContainsKey(configType))
            {
                ObjectConfigurationInfo config = typedObjectConfigurations[configType];
                config.ConfigureDelegate(item);
                return;
            }

            throw new Exception(string.Format("Typed configuration '{0}' was not found", configType.Name));
        }

        public void ConfigureObject<T>(ConfigureDelegate configMethod, T item)
        {
            MethodInfo method = configMethod.Method;
            IObjectInitializer factory = configMethod.Target as IObjectInitializer;
            if (factory == null)
                throw new Exception(string.Format("The method does not belong to an IObjectFactory"));

            ConfigureMethodAttribute attrib = method.GetCustomAttributes(typeof(ConfigureMethodAttribute), true).FirstOrDefault() as ConfigureMethodAttribute;

            if (!objectFactories.Contains(factory))
                RegisterObjectFactory(factory);

            if (attrib == null)
                throw new Exception("Method is not a factory method");

            if (attrib.ConfigId != null)
            {
                ConfigureObject<T>(attrib.ConfigId,item);
            }
            else if (attrib.DefaultForType != null)
            {
                ConfigureObject<T>(attrib.DefaultForType,item);
            }
            else
            {
                string configId = method.Name;
                ConfigureObject<T>(configId, item);
            }
        }

        private void RegisterObjectConfigurationMethod(IObjectInitializer factory, MethodInfo method, ConfigureMethodAttribute attrib)
        {
            Type objectType = method.ReturnType;

            if (method.GetParameters().Length != 1)
                throw new NotSupportedException("Configuration methods must have only 1 parameter");

            if (attrib.ConfigId != null)
            {
                ConfigureDelegate configDelegate = CreateMethodConfigurationDelegate(factory, method);
                RegisterObjectConfigurationMethod(attrib.ConfigId, configDelegate);
            }
            else if (attrib.DefaultForType != null)
            {
                ConfigureDelegate configDelegate = CreateMethodConfigurationDelegate(factory, method);
                RegisterObjectConfigurationMethod(attrib.DefaultForType, configDelegate);
            }
            else
            {
                string objectId = method.Name;
                ConfigureDelegate configDelegate = CreateMethodConfigurationDelegate(factory, method);
                RegisterObjectConfigurationMethod(objectId, configDelegate);
            }
        }

        private ConfigureDelegate CreateMethodConfigurationDelegate(IObjectInitializer factory, MethodInfo method)
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

        private void RegisterObjectFactoryMethod(IObjectInitializer factory, MethodInfo method, FactoryMethodAttribute attrib)
        {
            Type objectType = method.ReturnType;

            if (method.GetParameters().Length != 0)
                throw new NotSupportedException("Factory methods may not have any parameters");

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

        private FactoryDelegate CreateMethodFactoryDelegate(IObjectInitializer factory, MethodInfo method)
        {
            ConstantExpression instance = Expression.Constant(factory);
            MethodCallExpression call = Expression.Call(instance, method);
            Expression body = Expression.TypeAs(call, typeof(object));
            LambdaExpression lambda = Expression.Lambda(typeof(FactoryDelegate), body);
            FactoryDelegate factoryDelegate = (FactoryDelegate)lambda.Compile();
            return factoryDelegate;
        }

        private static ObjectFactoryInfo CreateObjectFactory(FactoryDelegate factoryDelegate, InstanceMode instanceMode)
        {
            ObjectFactoryInfo factory = new ObjectFactoryInfo();
            factory.FactoryDelegate = factoryDelegate;
            factory.InstanceMode = instanceMode;
            return factory;
        }

        public void RegisterObjectConfigurationMethod(string configId, ConfigureDelegate configMethod)
        {
            ObjectConfigurationInfo config = CreateObjectConfiguration(configMethod);
            namedObjectConfigurations.Add(configId, config);
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
            typedObjectConfigurations.Add(configType, config);
        }
    }
}
