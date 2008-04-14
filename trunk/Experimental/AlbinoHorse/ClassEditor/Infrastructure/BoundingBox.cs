using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AlbinoHorse.Infrastructure
{
    public class BoundingBox
    {
        #region Property Bounds 
        private Rectangle bounds;
        public Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }
            set
            {
                this.bounds = value;
            }
        }                        
        #endregion

        #region Property Target
        private object target;
        public object Target
        {
            get
            {
                return this.target;
            }
            set
            {
                this.target = value;
            }
        }
        #endregion

        //#region Property Section
        //private string section;
        //public string Section
        //{
        //    get
        //    {
        //        return this.section;
        //    }
        //    set
        //    {
        //        this.section = value;
        //    }
        //}
        //#endregion

        #region Property Data
        private object data;
        public object Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }
        #endregion
    }
}
