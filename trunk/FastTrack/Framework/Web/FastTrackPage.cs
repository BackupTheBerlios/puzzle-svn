using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Puzzle.NPersist.Framework;
using System.Reflection;
using Puzzle.FastTrack.Framework.Controllers;
using Puzzle.FastTrack.Framework.Web.Interfaces;
using Puzzle.FastTrack.Framework.Web.Factories;
using System.Collections;
using Puzzle.FastTrack.Framework.Filtering;
using Puzzle.FastTrack.Framework.Factories;

namespace Puzzle.FastTrack.Framework.Web
{
    public class FastTrackPage : Page
    {
        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            domainController = ControllerFactory.CreateDomainController();

            string className = Request["class"];
            string typeName = Request["type"];
            string id = Request["id"];
            string property = Request["property"];
            string page = Request["page"];
            string sort = Request["sort"];
            string desc = Request["desc"];
            
            if (typeName == null)
                typeName = "Employee";

            if (id == null)
                id = "10";

            if (className != null)
            {
                SelectedType = GetTypeFromTypeName(className);

                SelectedObjects = GetObjectsOfType(SelectedType);
            }

            if (typeName != null && id != null)
            {
                Type type = GetTypeFromTypeName(typeName);

                SelectedObject = GetObjectByIdentity(id, type);
            }

            SelectedPropertyName = property;

            if (page != null)
            {
                int pageNr;
                if (int.TryParse(page, out pageNr))
                    currentPage = pageNr;
            }

            SortProperty = sort;

            if (desc != null)
                descending = true;

            base.OnLoad(e);
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
        }

        #endregion

        #region Dispose

        public override void Dispose()
        {
            base.Dispose();
            if (this.domainController != null)
                this.domainController.Dispose();
        }

        #endregion

        #region Properties

        #region DomainController

        private IDomainController domainController;
        public virtual IDomainController DomainController
        {
            get { return domainController; }
            set { domainController = value; }
        }

        #endregion

        #region SelectedType

        public virtual Type SelectedType
        {
            get
            {
                if (this.domainController != null)
                    return this.domainController.SelectedType;
                return null;
            }
            set
            {
                if (this.domainController != null)
                    this.domainController.SelectedType = value;
            }
        }

        #endregion

        #region SelectedObject

        public virtual object SelectedObject
        {
            get 
            {
                if (this.domainController != null)
                    return this.domainController.SelectedObject;
                return null;
            }
            set 
            {
                if (this.domainController != null)
                    this.domainController.SelectedObject = value;
            }
        }

        #endregion

        #region SelectedObjects

        public virtual IList SelectedObjects
        {
            get
            {
                if (this.domainController != null)
                    return this.domainController.SelectedObjects;
                return null;
            }
            set
            {
                if (this.domainController != null)
                    this.domainController.SelectedObjects = value;
            }
        }

        #endregion

        #region SelectedPropertyName

        public virtual string SelectedPropertyName
        {
            get
            {
                if (this.domainController != null)
                    return this.domainController.SelectedPropertyName;
                return null;
            }
            set
            {
                if (this.domainController != null)
                    this.domainController.SelectedPropertyName = value;
            }
        }

        #endregion

        #region CurrentPage

        private int currentPage = 0;
        public virtual int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }	

        #endregion

        #region SortProperty

        private string sortProperty = "";
        public virtual string SortProperty
        {
            get { return sortProperty; }
            set { sortProperty = value; }
        }

        #endregion

        #region Descending

        private bool descending = false;
        public virtual bool Descending
        {
            get { return descending; }
            set { descending = value; }
        }

        #endregion

        #endregion

        #region Methods

        #region GetObjectByIdentity

        public object GetObjectByIdentity(object identity, Type type)
        {
            if (this.domainController != null)
                return this.domainController.GetObjectByIdentity(identity, type);
            return null;
        }

        #endregion

        #region GetObjectIdentity

        public string GetObjectIdentity(object obj)
        {
            if (this.domainController != null)
                return this.domainController.GetObjectIdentity(obj);
            return "";
        }

        #endregion

        #region GetObjectIdentity

        public IList GetObjectsOfType(Type type)
        {
            if (this.domainController != null)
                return this.domainController.GetObjectsOfType(type);
            return new ArrayList();
        }

        public IList GetObjectsOfType(Type type, Filter filter)
        {
            if (this.domainController != null)
                return this.domainController.GetObjectsOfType(type, filter);
            return new ArrayList();
        }

        #endregion

        #region CreateObject

        public object CreateObject(Type type)
        {
            if (this.domainController != null)
                return this.domainController.CreateObject(type);
            return null;
        }

        #endregion

        #region SaveObject

        public void SaveObject()
        {
            SaveControls();

            if (this.domainController != null)
                this.domainController.SaveObject();
        }

        public void SaveObject(object obj)
        {
            SaveControls();

            if (this.domainController != null)
                this.domainController.SaveObject(obj);
        }

        private void SaveControls()
        {
            foreach (Control control in this.Controls)
                SaveControls(control);
        }

        private void SaveControls(Control control)
        {
            IValueEditor editor = control as IValueEditor;
            if (editor != null)
                editor.Save();

            foreach (Control child in control.Controls)
                SaveControls(child);
        }

        #endregion

        #region DeleteObject

        public void DeleteObject()
        {
            if (this.domainController != null)
                this.domainController.DeleteObject();
        }

        public void DeleteObject(object obj)
        {
            if (this.domainController != null)
                this.domainController.DeleteObject(obj);
        }

        #endregion

        #region GetPropertyInfo

        public PropertyInfo GetPropertyInfo(string propertyName)
        {
            return GetPropertyInfo(this.SelectedObject, propertyName);
        }

        public PropertyInfo GetPropertyInfo(object obj, string propertyName)
        {
            if (obj != null)
                return obj.GetType().GetProperty(propertyName);
            return null;
        }

        #endregion

        #region IsListProperty

        public bool IsListProperty(string propertyName)
        {
            return IsListProperty(this.SelectedObject, propertyName);
        }

        public bool IsListProperty(object obj, string propertyName)
        {
            if (this.domainController != null)
                return this.domainController.IsListProperty(obj, propertyName);
            return false;
        }

        #endregion

        #region GetListPropertyItemType

        public Type GetListPropertyItemType(string propertyName)
        {
            return GetListPropertyItemType(this.SelectedObject, propertyName);
        }

        public Type GetListPropertyItemType(object obj, string propertyName)
        {
            if (this.domainController != null)
                return this.domainController.GetListPropertyItemType(obj, propertyName);
            return null;
        }

        #endregion

        #region IsNullableProperty

        public bool IsNullableProperty(string propertyName)
        {
            return IsNullableProperty(this.SelectedObject, propertyName);
        }

        public bool IsNullableProperty(object obj, string propertyName)
        {
            if (this.domainController != null)
                return this.domainController.IsNullableProperty(obj, propertyName);
            return false;
        }

        #endregion

        #region GetPropertyNullStatus

        public bool GetPropertyNullStatus(string propertyName)
        {
            return GetPropertyNullStatus(this.SelectedObject, propertyName);
        }

        public bool GetPropertyNullStatus(object obj, string propertyName)
        {
            if (this.domainController != null)
                return this.domainController.GetPropertyNullStatus(obj, propertyName);
            return false;
        }

        #endregion

        #region SetPropertyNullStatus

        public void SetPropertyNullStatus(string propertyName, bool isNull)
        {
            SetPropertyNullStatus(this.SelectedObject, propertyName, isNull);
        }

        public void SetPropertyNullStatus(object obj, string propertyName, bool isNull)
        {
            if (this.domainController != null)
                this.domainController.SetPropertyNullStatus(obj, propertyName, isNull);
        }

        #endregion

        #region GetPropertyValue

        public object GetPropertyValue(string propertyName)
        {
            if (this.domainController != null)
                return this.domainController.GetPropertyValue(propertyName);
            return null;
        }

        public object GetPropertyValue(object obj, string propertyName)
        {
            if (this.domainController != null)
                return this.domainController.GetPropertyValue(obj, propertyName);
            return null;
        }

        #endregion

        #region SetPropertyValue

        public void SetPropertyValue(string propertyName, object value)
        {
            if (this.domainController != null)
                this.domainController.SetPropertyValue(propertyName, value);
        }

        public void SetPropertyValue(object obj, string propertyName, object value)
        {
            if (this.domainController != null)
                this.domainController.SetPropertyValue(obj, propertyName, value);
        }

        #endregion

        #region GetObjectName

        public string GetObjectName()
        {
            if (this.domainController != null)
                return this.domainController.GetObjectName();
            return "";
        }

        public string GetObjectName(object obj)
        {
            if (this.domainController != null)
                return this.domainController.GetObjectName(obj);
            return "";
        }

        #endregion

        #region GetTypeFromType

        public Type GetTypeFromType()
        {
            if (this.domainController != null)
                return this.domainController.GetTypeFromType();
            return this.SelectedObject.GetType();
        }

        public Type GetTypeFromType(Type type)
        {
            if (this.domainController != null)
                return this.domainController.GetTypeFromType(type);
            return type;
        }

        #endregion

        #region GetTypeFromType

        public Type GetTypeFromTypeName(string typeName)
        {
            if (this.domainController != null)
                return this.domainController.GetTypeFromTypeName(typeName);
            return null;
        }

        #endregion

        #region GetTypeNameFromType

        public string GetTypeNameFromType()
        {
            if (this.domainController != null)
                return this.domainController.GetTypeNameFromType();
            return this.SelectedObject.GetType().Name;
        }

        public string GetTypeNameFromType(Type type)
        {
            if (this.domainController != null)
                return this.domainController.GetTypeNameFromType(type);
            return type.Name;
        }

        #endregion

        #region GetViewUrl

        public string GetViewUrl(string specified)
        {
            return GetViewUrl(this.SelectedObject, specified);
        }

        public string GetViewUrl(object obj, string specified)
        {
            if (obj == null)
                return "";

            string typeName = GetTypeNameFromType(obj.GetType());
            string id = GetObjectIdentity(obj);

            return UrlFactory.GetObjectViewUrl(typeName, id, specified);
        }

        #endregion

        #region GetEditUrl

        public string GetEditUrl(string specified)
        {
            return GetEditUrl(this.SelectedObject, specified);
        }

        public string GetEditUrl(object obj, string specified)
        {
            if (obj == null)
                return "";

            string typeName = GetTypeNameFromType(obj.GetType());
            string id = GetObjectIdentity(obj);

            return UrlFactory.GetObjectEditUrl(typeName, id, specified);
        }

        #endregion

        #region GetEditUrl

        public string GetListUrl(Type type, string specified)
        {
            return GetListUrl(type, specified, null, "");
        }

        public string GetListUrl(Type type, string specified, object obj, string propertyName)
        {
            if (type == null)
                return "";

            string className = GetTypeNameFromType(type);

            if (obj == null)
            {
                return UrlFactory.GetObjectListUrl(className, specified);
            }
            else
            {
                string typeName = GetTypeNameFromType(obj.GetType());
                string id = GetObjectIdentity(obj);

                return UrlFactory.GetObjectListUrl(className, typeName, id, propertyName, specified);
            }
        }

        #endregion

        #region GetDefaultUrl

        public string GetDefaultUrl(string specified)
        {
            return UrlFactory.GetClassListUrl(specified);
        }

        #endregion

        #endregion

    }
}
