using System;
namespace Mojo
{
    public interface IContext 
    {
        //hide
        ContextState State { get; }
        void SubstituteType<T, S>();

        T CreateObject<T>(params object[] args);

        T GetObject<T>(); 
        T GetObject<T>(string factoryId);

        void ConfigureObject<T>(string configId, T item);
        void ConfigureObject<T>(object item);

        void RegisterObject<T>(object item);
        void RegisterObject<T>(string objectId, T item);
    }

    public interface IContext<TEMPLATE> : IContext
    {
        TEMPLATE Template { get; }
    }
}
