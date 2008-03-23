using System;
using System.Linq.Expressions;
namespace Puzzle.NContext.Framework
{
    public interface IContext
    {
        IContext ParentContext { get; set; }

        void SubstituteType<T, S>();
        T CreateObject<T>(params object[] args);

        T GetObject<T>(Type factoryType);
        T GetObject<T>(); //objectType == T
        T GetObject<T>(string factoryId);
        T GetObject<T>(Func<T> factoryMethod);
        T GetObject<T, F>(Expression<Func<F, T>> factoryMethod) where F : IObjectInitializer;

        void ConfigureObject<T>(string configId, T item);
        void ConfigureObject<T>(Type configType, T item);
        void ConfigureObject<T>(ConfigureDelegate configMethod, T item);

        void RegisterObject<T>(Type objectType, T item);
        void RegisterObject<T>(string objectId, T item);

        void RegisterObjectFactoryMethod(Type factoryType, FactoryDelegate factoryMethod, InstanceMode instanceMode);
        void RegisterObjectFactoryMethod(string factoryId, FactoryDelegate factoryMethod, InstanceMode instanceMode);

        void RegisterObjectConfigurationMethod(string configId, ConfigureDelegate configMethod);
        void RegisterObjectConfigurationMethod(Type configType, ConfigureDelegate configMethod);

        void RegisterObjectFactory(IObjectInitializer factory);
    }
}
