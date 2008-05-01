using System;
using System.Linq.Expressions;
namespace Puzzle.NContext.Framework
{
    public interface IContext
    {
        IContext ParentContext { get; set; }
        //hide
        ContextState State { get; }
        void SubstituteType<T, S>();

        T CreateObject<T>(params object[] args);

        T GetObject<T>(Type factoryType);
        T GetObject<T>(); //objectType == T
        T GetObject<T>(string factoryId);

        F GetTemplate<F>() where F : ITemplate;

        void ConfigureObject<T>(string configId, T item);
        void ConfigureObject<T>(Type configType, T item);

        void RegisterObject<T>(Type objectType, T item);
        void RegisterObject<T>(string objectId, T item);

        void RegisterTemplate<F>() where F:ITemplate;
        void RegisterTemplate(Type templateType);
    }
}
