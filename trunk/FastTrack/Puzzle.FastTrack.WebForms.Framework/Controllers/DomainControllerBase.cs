using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Puzzle.FastTrack.WebForms.Framework.Controllers
{
    public abstract class DomainControllerBase : IDomainController 
    {
        public DomainControllerBase()
        {
            string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DomainAssembly"];
            domainAssembly = Assembly.LoadFile(assemblyName);
        }

        private Assembly domainAssembly;
        public virtual Assembly DomainAssembly
        {
            get { return domainAssembly; }
        }
	
        private object selectedObject;
        public virtual object SelectedObject
        {
            get { return selectedObject; }
            set { selectedObject = value; }
        }

        public abstract object GetObjectByIdentity(object identity, Type type);

        public abstract object CreateObject(Type type);

        public virtual void SaveObject()
        {
            SaveObject(this.selectedObject);
        }

        public abstract void SaveObject(object obj);

        public virtual void DeleteObject()
        {
            DeleteObject(this.SelectedObject);
        }

        public abstract void DeleteObject(object obj);

        public virtual object GetPropertyValue(string propertyName)
        {
            return GetPropertyValue(this.selectedObject, propertyName);
        }

        public virtual object GetPropertyValue(object obj, string propertyName)
        {
            if (propertyName != null && propertyName != "")
            {
                if (obj != null)
                {
                    PropertyInfo property = obj.GetType().GetProperty(propertyName);
                    if (property != null)
                    {
                        MethodInfo getter = property.GetGetMethod();
                        if (getter != null)
                        {
                            return getter.Invoke(obj, new object[] { });
                        }
                    }
                }
            }

            return null;
        }

        public virtual void SetPropertyValue(string propertyName, object value)
        {
            SetPropertyValue(this.selectedObject, propertyName, value);
        }

        public virtual void SetPropertyValue(object obj, string propertyName, object value)
        {
            if (propertyName != null && propertyName != "")
            {
                if (obj != null)
                {
                    PropertyInfo property = obj.GetType().GetProperty(propertyName);
                    if (property != null)
                    {
                        MethodInfo setter = property.GetSetMethod();
                        if (setter != null)
                        {
                            setter.Invoke(obj, new object[] { value });
                        }
                    }
                }
            }
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            ;
        }

        #endregion
    }
}
