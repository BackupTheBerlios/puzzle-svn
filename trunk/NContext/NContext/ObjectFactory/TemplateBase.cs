using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Mojo
{
    public class Template : ITemplate
    {
        IContext IContextBound.Context
        {
            get;
            set;
        }

        private IContext Context
        {
            get
            {
                return ((IContextBound)this).Context;
            }
        }

        void ITemplate.Initialize()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
        }

        protected virtual T CreateObject<T>(params object[] args)
        {
            return Context.CreateObject<T>(args);
        }

        protected virtual T GetObject<T>(string configId)
        {
            return Context.GetObject<T>(configId);
        }

        protected virtual T GetObject<T>()
        {
            return Context.GetObject<T>();
        }

        protected virtual void ConfigureObject<T>(string configId, T item)
        {
            Context.ConfigureObject(configId, item);
        }

        protected virtual void ConfigureObject<T>(object item)
        {
            Context.ConfigureObject<T>(item);
        }
    }
}
