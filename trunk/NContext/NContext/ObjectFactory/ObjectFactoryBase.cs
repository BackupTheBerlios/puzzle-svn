using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public class ObjectFactoryBase : IObjectFactory
    {
        public virtual IContext Context { get; set; }
        protected virtual T CreateObject<T>(params object[] args)
        {
            return Context.CreateObject<T>(args);
        }

        protected virtual T GetObject<T>(string objectId)
        {
            return Context.GetObject<T>(objectId);
        }

        protected virtual T GetObject<T>(Type objectType)
        {
            return Context.GetObject<T>(objectType);
        }

        protected virtual T GetObject<T>()
        {
            return Context.GetObject<T>();
        }
    }
}
