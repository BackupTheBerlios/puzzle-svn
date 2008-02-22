using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using Puzzle.SideFX.Framework;

namespace Puzzle.FastForward.Framework.Service
{
    public abstract class ObjectServiceBase : IObjectService
    {
        public ObjectServiceBase(IEngine engine)
        {
            this.engine = engine;
        }

        protected IEngine engine;

        #region IObjectService Members

        public virtual void SetProperty(object obj, string propertyName, object value)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Setting property {0} of object {1}.{2} to {3}",
                    propertyName,
                    GetTypeName(obj),
                    GetIdentity(obj),
                    value.ToString()));

            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null)
                property.SetValue(obj, value, new object[] { });
        }

        public virtual string GetTypeName(object obj)
        {
            return GetTypeName(obj.GetType());
        }

        public virtual string GetTypeName(Type type)
        {
            return type.ToString();
        }

        public abstract object GetIdentity(object obj);

        public virtual object GetProperty(object obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null)
                return property.GetValue(obj, new object[] { });
            return null;
        }

        public abstract object CreateObject(Type type);

        public abstract void DeleteObject(object obj);

        public abstract Type GetTypeByName(string className);

        public abstract IList GetObjects(Type type, IDictionary<string, object> match);

        public abstract IList GetObjects(Type type, string where);

        public abstract void Commit();

        public abstract void Abort();

        public abstract bool IsNull(object obj, string propertyName);


        #endregion
    }
}
