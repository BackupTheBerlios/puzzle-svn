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

        protected Context()
        {
        }
        
        public T GetObject<T>(FactoryDelegate<T> factoryMethod)
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
            else if (attrib.DefaultForType != null)
            {
                return GetObject<T>(attrib.DefaultForType);
            }
            else
            {
                string factoryId = method.Name;
                return GetObject<T>(factoryId);
            }
        }


        public T CreateObject<T>(params object[] args)
        {
            T res = (T)Activator.CreateInstance(typeof(T), args);
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

            throw new Exception(string.Format("Named configuraton '{0}' was not found", factoryId));
        }

        public T GetObject<T>(Type factoryType)
        {
            if (typedObjectFactories.ContainsKey(factoryType))
            {
                ObjectFactoryInfo config = typedObjectFactories[factoryType];
                object res = config.FactoryDelegate();
                return (T)res;
            }

            throw new Exception(string.Format("Typed configuraton '{0}' was not found", factoryType.Name));
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
        }

        private void RegisterObjectFactoryMethod(IObjectInitializer factory, MethodInfo method, FactoryMethodAttribute attrib)
        {
            Type objectType = method.ReturnType;

            if (method.GetParameters().Length > 0)
                throw new NotSupportedException("Factory methods may not have any parameters");

            if (attrib.FactoryId != null)
            {
                FactoryDelegate<object> factoryDelegate = CreateMethodFactoryDelegate(factory, method);
                RegisterObjectFactoryMethod(attrib.FactoryId, factoryDelegate, attrib.InstanceMode);
            }
            else if (attrib.DefaultForType != null)
            {
                FactoryDelegate<object> factoryDelegate = CreateMethodFactoryDelegate(factory, method);
                RegisterObjectFactoryMethod(attrib.DefaultForType, factoryDelegate, attrib.InstanceMode);
            }
            else
            {
                string objectId = method.Name;
                FactoryDelegate<object> factoryDelegate = CreateMethodFactoryDelegate(factory, method);
                RegisterObjectFactoryMethod(objectId, factoryDelegate, attrib.InstanceMode);
            }
        }

        private FactoryDelegate<object> CreateMethodFactoryDelegate(IObjectInitializer factory, MethodInfo method)
        {
            ConstantExpression instance = Expression.Constant(factory);
            MethodCallExpression call = Expression.Call(instance, method);
            Expression body = Expression.TypeAs(call, typeof(object));
            LambdaExpression lambda = Expression.Lambda(typeof(FactoryDelegate<object>), body);
            FactoryDelegate<object> factoryDelegate = (FactoryDelegate<object>)lambda.Compile();
            return factoryDelegate;
        }

        public void RegisterObjectFactoryMethod(Type objectType, FactoryDelegate<object> factoryDelegate, ObjectInstanceMode instanceMode)
        {
            ObjectFactoryInfo config = CreateObjectConfiguration(factoryDelegate, instanceMode);
            typedObjectFactories.Add(objectType, config);
        }

        public void RegisterObjectFactoryMethod(string objectId, FactoryDelegate<object> factoryDelegate, ObjectInstanceMode instanceMode)
        {
            ObjectFactoryInfo config = CreateObjectConfiguration(factoryDelegate, instanceMode);
            namedObjectFactories.Add(objectId, config);
        }

        private static ObjectFactoryInfo CreateObjectConfiguration(FactoryDelegate<object> factoryDelegate, ObjectInstanceMode instanceMode)
        {
            ObjectFactoryInfo config = new ObjectFactoryInfo();
            config.FactoryDelegate = factoryDelegate;
            config.InstanceMode = instanceMode;
            return config;
        }

        public void ConfigureObject<T>(string configId, T item)
        {
            throw new NotImplementedException();
        }

        public void ConfigureObject<T>(Type configType, T item)
        {
            throw new NotImplementedException();
        }

        public void ConfigureObject<T>(ConfigureDelegate<T> configMethod, T item)
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
    }
}
