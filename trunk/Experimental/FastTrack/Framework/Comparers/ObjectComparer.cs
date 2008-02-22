using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Puzzle.FastTrack.Framework.Controllers;

namespace Puzzle.FastTrack.Framework.Comparers
{
    public class ObjectComparer : IComparer
    {
        public ObjectComparer(IDomainController controller, string propertyName) : this(controller, propertyName, false)
        {
        }

        public ObjectComparer(IDomainController controller, string propertyName, bool descending)
        {
            this.controller = controller;
            this.propertyName = propertyName;
            this.valueComparer = new ValueComparer(controller, descending);
        }

        private ValueComparer valueComparer;

        private IDomainController controller;
        public virtual IDomainController Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        private string propertyName;
        public virtual string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }
	
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (propertyName == null || propertyName == null)
                return 0;

            if (x == null && y == null)
                return 0;

            if (x == null)
                return 1;

            if (y == null)
                return -1;

            if (x.GetType() != y.GetType())
                throw new Exception("Can't compare values of different types");

            object value1 = controller.GetPropertyValue(x, propertyName);
            object value2 = controller.GetPropertyValue(y, propertyName);

            return valueComparer.Compare(value1, value2);
        }

        #endregion
    }
}
