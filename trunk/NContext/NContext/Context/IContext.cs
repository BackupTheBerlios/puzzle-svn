using System;
namespace Puzzle.NContext.Framework
{
    public interface IContext
    {
        T CreateObject<T>(params object[] args);

        T GetObject<T>(Type configType);
        T GetObject<T>(); //objectType == T
        T GetObject<T>(string configId);
        T GetObject<T>(FactoryDelegate<T> factoryMethod);

        void ConfigureObject<T>(string configId, T item);
        void ConfigureObject<T>(Type configType, T item);
        void ConfigureObject<T>(ConfigureDelegate<T> configMethod, T item);

        void RegisterObject<T>(Type objectType, T item);
        void RegisterObject<T>(string objectId, T item);

        void RegisterObjectFactoryMethod(Type objectType, FactoryDelegate<object> factoryMethod, ObjectInstanceMode instanceMode);
        void RegisterObjectFactoryMethod(string objectId, FactoryDelegate<object> factoryMethod, ObjectInstanceMode instanceMode);

        void RegisterObjectFactory(IObjectFactory factory);


    }
}
