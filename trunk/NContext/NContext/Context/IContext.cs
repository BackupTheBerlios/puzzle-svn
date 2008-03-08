using System;
namespace Puzzle.NContext.Framework
{
    public interface IContext
    {
        T CreateObject<T>(params object[] args);

        T GetObject<T>(Type objectType);
        T GetObject<T>(); //objectType == T

        T GetObject<T>(string objectId);
        void RegisterObject<T>(Type objectType, T item);
        void RegisterObject<T>(string objectId, T item);

        void RegisterObjectFactoryMethod(Type objectType, Func<object> factoryMethod, ObjectInstanceMode instanceMode);
        void RegisterObjectFactoryMethod(string objectId, Func<object> factoryMethod, ObjectInstanceMode instanceMode);

        void RegisterObjectFactory(IObjectFactory factory);


    }
}
