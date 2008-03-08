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
        protected IList<IObjectFactory> objectFactories = new List<IObjectFactory>();
        protected IDictionary<string, ObjectConfiguration> namedConfigurations = new Dictionary<string, ObjectConfiguration>();
        protected IDictionary<Type, ObjectConfiguration> typedConfigurations = new Dictionary<Type, ObjectConfiguration>();

        protected Context()
        {
        }

        public T CreateObject<T>(params object[] args)
        {
            T res = (T)Activator.CreateInstance(typeof(T), args);
            return res;
        }

        public T GetObject<T>(string objectId)
        {
            if (namedConfigurations.ContainsKey(objectId))
            {
                ObjectConfiguration config = namedConfigurations[objectId];
                object res = config.FactoryDelegate();
                return (T)res;
            }

            throw new Exception(string.Format("Named configuraton '{0}' was not found", objectId));
        }

        public T GetObject<T>(Type objectType)
        {
            if (typedConfigurations.ContainsKey(objectType))
            {
                ObjectConfiguration config = typedConfigurations[objectType];
                object res = config.FactoryDelegate();
                return (T)res;
            }

            throw new Exception(string.Format("Typed configuraton '{0}' was not found", objectType.Name));
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

        public void RegisterObjectFactory(IObjectFactory factory)
        {
            factory.Context = this;
            objectFactories.Add(factory);

            foreach (MethodInfo method in factory.GetType().GetMethods())
            {
                FactoryMethodAttribute attrib = method.GetCustomAttributes(typeof(FactoryMethodAttribute), true).FirstOrDefault() as FactoryMethodAttribute;
                
                if (attrib == null)
                    continue; //not a factory method

                Type objectType = method.ReturnType;

                if (attrib.ObjectId != null)
                {
                    Func<object> factoryDelegate = CreateMethodFactoryDelegate(factory, method);
                    RegisterObjectFactoryMethod(attrib.ObjectId, factoryDelegate, attrib.InstanceMode);
                }
                else if (attrib.ObjectType != null)
                {
                    Func<object> factoryDelegate = CreateMethodFactoryDelegate(factory, method);
                    RegisterObjectFactoryMethod(attrib.ObjectType, factoryDelegate, attrib.InstanceMode);
                }
                else if (method.Name.StartsWith("Get"))
                {
                    string objectId = method.Name.Substring(3);
                    Func<object> factoryDelegate = CreateMethodFactoryDelegate(factory, method);
                    RegisterObjectFactoryMethod(objectId, factoryDelegate, attrib.InstanceMode);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        private Func<object> CreateMethodFactoryDelegate(IObjectFactory factory, MethodInfo method)
        {
            ConstantExpression instance = Expression.Constant(factory);
            MethodCallExpression call = Expression.Call(instance, method);
            Expression body = Expression.TypeAs(call, typeof(object));
            LambdaExpression lambda = Expression.Lambda(typeof(Func<object>), body);
            Func<object> factoryDelegate = (Func<object>)lambda.Compile();
            return factoryDelegate;
        }

        public void RegisterObjectFactoryMethod(Type objectType, Func<object> factoryDelegate, ObjectInstanceMode instanceMode)
        {
            ObjectConfiguration config = CreateObjectConfiguration(factoryDelegate, instanceMode);
            typedConfigurations.Add(objectType, config);
        }

        public void RegisterObjectFactoryMethod(string objectId, Func<object> factoryDelegate, ObjectInstanceMode instanceMode)
        {
            ObjectConfiguration config = CreateObjectConfiguration(factoryDelegate, instanceMode);
            namedConfigurations.Add(objectId, config);
        }

        private static ObjectConfiguration CreateObjectConfiguration(Func<object> factoryDelegate, ObjectInstanceMode instanceMode)
        {
            ObjectConfiguration config = new ObjectConfiguration();
            config.FactoryDelegate = factoryDelegate;
            config.InstanceMode = instanceMode;
            return config;
        }
    }
}
