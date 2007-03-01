using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Puzzle.FastTrack.Framework.Web.Comparers
{
    public class ObjectComparer : IComparer
    {
        public ObjectComparer(FastTrackPage page, string propertyName) : this(page, propertyName, false)
        {
        }

        public ObjectComparer(FastTrackPage page, string propertyName, bool descending)
        {
            this.page = page;
            this.propertyName = propertyName;
            this.valueComparer = new ValueComparer(page, descending);
        }

        private ValueComparer valueComparer;

        private FastTrackPage page;
        public virtual FastTrackPage Page
        {
            get { return page; }
            set { page = value; }
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

            object value1 = page.GetPropertyValue(x, propertyName);
            object value2 = page.GetPropertyValue(y, propertyName);

            return valueComparer.Compare(value1, value2);
        }

        #endregion
    }
}
