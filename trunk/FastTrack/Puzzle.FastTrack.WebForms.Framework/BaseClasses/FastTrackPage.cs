using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Puzzle.NPersist.Framework;
using System.Reflection;
using Puzzle.FastTrack.WebForms.Framework.Controllers;
using Puzzle.FastTrack.WebForms.Framework.Interfaces;
using Puzzle.FastTrack.WebForms.Framework.Factories;

namespace Puzzle.FastTrack.WebForms.Framework
{
    public class FastTrackPage : Page
    {
        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            domainController = ControllerFactory.CreateDomainController();

            //if (Request["Type"] != null && Request["Id"] != null)
            //{
                string typeString = Request["Type"];
                string id = Request["Id"];

                if (typeString == null)
                    typeString = "Puzzle.NPersist.Samples.Northwind.Domain.Employee";

                if (id == null)
                    id = "1";
                
                Type type = domainController.DomainAssembly.GetType(typeString);

                SelectedObject = GetObjectByIdentity(id, type);
            //}

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


        #endregion

    }
}
