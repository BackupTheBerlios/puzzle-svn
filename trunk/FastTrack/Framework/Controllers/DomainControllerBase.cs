using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using Puzzle.FastTrack.Framework.Filtering;

namespace Puzzle.FastTrack.Framework.Controllers
{
    public abstract class DomainControllerBase : IDomainController 
    {
        public DomainControllerBase()
        {
            string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DomainAssembly"];
            if (!string.IsNullOrEmpty(assemblyName))
                domainAssembly = Assembly.Load(assemblyName);
        }

        private Assembly domainAssembly;
        public virtual Assembly DomainAssembly
        {
            get { return domainAssembly; }
            set { domainAssembly = value; }
        }


        private IDictionary<Type, ITypeController> typeControllers = new Dictionary<Type, ITypeController>();

        public virtual void RegisterTypeController(string typeName, ITypeController typeController)
        {
            Type type = GetTypeFromTypeName(typeName);
            RegisterTypeController(type, typeController);
        }
            
        public virtual void RegisterTypeController(Type type, ITypeController typeController)
        {
            type = GetTypeFromType(type);
            typeController.DomainController = this;
            typeControllers[type] = typeController;
        }

        public ITypeController GetTypeController(Type type)
        {
            type = GetTypeFromType(type);
            if (HasTypeController(type))
                return typeControllers[type];
            return null;
        }

        public bool HasTypeController(Type type)
        {
            type = GetTypeFromType(type);
            return typeControllers.ContainsKey(type);
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

        public virtual object CreateObject(Type type)
        {
            ITypeController typeController = GetTypeController(type);
            if (typeController != null)
                return typeController.CreateObject(type);
            return ExecuteCreateObject(type);
        }

        public abstract object ExecuteCreateObject(Type type);

        public virtual void SaveObject()
        {
            SaveObject(this.selectedObject);
        }

        public virtual void SaveObject(object obj)
        {
            ITypeController typeController = GetTypeController(obj.GetType());
            if (typeController != null)
                typeController.SaveObject(obj);
            else
                ExecuteSaveObject(obj);
        }

        public abstract void ExecuteSaveObject(object obj);

        public virtual void DeleteObject()
        {
            DeleteObject(this.SelectedObject);
        }

        public virtual void DeleteObject(object obj)
        {
            ITypeController typeController = GetTypeController(obj.GetType());
            if (typeController != null)
                typeController.DeleteObject(obj);
            else
                ExecuteDeleteObject(obj);
        }

        public abstract void ExecuteDeleteObject(object obj);

        public virtual bool IsListProperty(string propertyName)
        {
            if (this.selectedObject != null)
                return IsListProperty(this.SelectedObject, propertyName);
            else if (this.selectedType != null)
                return IsListProperty(this.SelectedType, propertyName);
            return false;
        }

        public virtual bool IsListProperty(object obj, string propertyName)
        {
            return IsListProperty(obj.GetType(), propertyName);
        }

        public virtual bool IsListProperty(Type type, string propertyName)
        {
            PropertyInfo property = type.GetProperty(propertyName);
            if (property != null)
                return (typeof(IList).IsAssignableFrom(property.PropertyType));
            return false;
        }

        #region IsReadOnlyProperty

        public virtual bool IsReadOnlyProperty(string propertyName)
        {
            if (this.selectedObject != null)
                return IsReadOnlyProperty(this.SelectedObject, propertyName);
            else if (this.selectedType != null)
                return IsReadOnlyProperty(this.SelectedType, propertyName);
            return false;
        }

        public virtual bool IsReadOnlyProperty(object obj, string propertyName)
        {
            return IsReadOnlyProperty(obj.GetType(), propertyName); 
        }

        public virtual bool IsReadOnlyProperty(Type type, string propertyName)
        {
            PropertyInfo property = type.GetProperty(propertyName);
            if (property != null)
                return (property.GetSetMethod() == null);
            return false;
        }

        #endregion

        public virtual Type GetListPropertyItemType(string propertyName)
        {
            return GetListPropertyItemType(this.selectedObject, propertyName);
        }

        public abstract Type GetListPropertyItemType(object obj, string propertyName);

        public virtual bool IsNullableProperty(string propertyName)
        {
            if (this.selectedObject != null)
                return IsNullableProperty(this.SelectedObject, propertyName);
            else if (this.selectedType != null)
                return IsNullableProperty(this.SelectedType, propertyName);
            return false;
        }

        public virtual bool IsNullableProperty(object obj, string propertyName)
        {
            return IsNullableProperty(obj.GetType(), propertyName);
        }

        public abstract bool IsNullableProperty(Type type, string propertyName);

        public virtual bool GetPropertyNullStatus(string propertyName)
        {
            return GetPropertyNullStatus(this.selectedObject, propertyName);
        }

        public abstract bool GetPropertyNullStatus(object obj, string propertyName);

        public virtual void SetPropertyNullStatus(string propertyName, bool isNull)
        {
            SetPropertyNullStatus(this.selectedObject, propertyName, isNull);
        }

        public abstract void SetPropertyNullStatus(object obj, string propertyName, bool isNull);

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

        public virtual IList GetTypeNames()
        {
            return new ArrayList();
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
