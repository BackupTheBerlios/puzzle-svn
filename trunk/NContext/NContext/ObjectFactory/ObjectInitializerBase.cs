﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Puzzle.NContext.Framework
{
    public class ObjectInitializerBase : IObjectInitializer
    {
        public virtual IContext Context { get; set; }
        public virtual T CreateObject<T>(params object[] args)
        {
            return Context.CreateObject<T>(args);
        }

        public virtual T GetObject<T>(string configId)
        {
            return Context.GetObject<T>(configId);
        }

        public virtual T GetObject<T>(Type configType)
        {
            return Context.GetObject<T>(configType);
        }

        public virtual T GetObject<T>()
        {
            return Context.GetObject<T>();
        }

        public virtual T GetObject<T>(Func<T> factoryMethod)
        {
            return Context.GetObject (factoryMethod);
        }

        public virtual void ConfigureObject<T>(string configId, T item)
        {
            Context.ConfigureObject(configId, item);
        }

        public virtual void ConfigureObject<T>(Type configType, T item)
        {
            Context.ConfigureObject(configType, item);
        }

        public virtual void ConfigureObject<T>(ConfigureDelegate configMethod, T item)
        {
            Context.ConfigureObject(configMethod, item);
        }
    }
}
