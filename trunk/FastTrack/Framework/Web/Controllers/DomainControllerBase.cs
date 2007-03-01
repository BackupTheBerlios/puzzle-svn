using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using Puzzle.FastTrack.Framework.Web.Filtering;

namespace Puzzle.FastTrack.Framework.Web.Controllers
{
    public abstract class DomainControllerBase : IDomainController 
    {
        public DomainControllerBase()
        {
            string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DomainAssembly"];
            domainAssembly = Assembly.Load(assemblyName);
        }

        private Assembly domainAssembly;
        public virtual Assembly DomainAssembly
        {
            get { return domainAssembly; }
        }
	
        private Type selectedType;
        public virtual Type SelectedType
        {
            get { return selectedType; }
            set { selectedType = value; }
        }

        private object selectedObject;
        public virtual object SelectedObject
        {
            get { return selectedObject; }
            set { selectedObject = value; }
        }

        private IList selectedObjects;
        public virtual IList SelectedObjects
        {
            get { return selectedObjects; }
            set { selectedObjects = value; }
        }

        private string selectedPropertyName;
        public virtual string SelectedPropertyName
        {
            get { return selectedPropertyName; }
            set { selectedPropertyName = value; }
        }

        public abstract object GetObjectByIdentity(object identity, Type type);

        public abstract string GetObjectIdentity(object obj);

        public abstract IList GetObjectsOfType(Type type);

        public abstract IList GetObjectsOfType(Type type, Filter filter);

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

        public virtual string GetObjectName()
        {
            return GetObjectName(this.selectedObject);
        }

        public virtual string GetObjectName(object obj)
        {
            return obj.ToString();
        }

        public virtual Type GetTypeFromType()
        {
            return GetTypeFromType(this.selectedObject.GetType());
        }

        public virtual Type GetTypeFromType(Type type)
        {
            return type;
        }

        public virtual string GetTypeNameFromType()
        {
            return GetTypeNameFromType(this.selectedObject.GetType());
        }

        public virtual string GetTypeNameFromType(Type type)
        {
            return type.Name;
        }

        public virtual Type GetTypeFromTypeName(string typeName)
        {
            return DomainAssembly.GetType(typeName);
        }


        #region IDisposable Members

        public virtual void Dispose()
        {
            ;
        }

        #endregion
    }
}
