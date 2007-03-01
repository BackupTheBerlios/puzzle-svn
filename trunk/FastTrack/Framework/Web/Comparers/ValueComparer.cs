using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Puzzle.FastTrack.Framework.Web.Comparers
{
    public class ValueComparer : IComparer
    {
        public ValueComparer(FastTrackPage page) : this(page, false)
        {
        }

        public ValueComparer(FastTrackPage page, bool descending)
        {
            this.page = page;
            this.descending = descending;
        }

        private FastTrackPage page;
        public virtual FastTrackPage Page
        {
            get { return page; }
            set { page = value; }
        }

        private bool descending;
        public virtual bool Descending
        {
            get { return descending; }
            set { descending = value; }
        }
	
        #region IComparer Members

        public int Compare(object x, object y)
        {
            int result = DoCompare(x, y);
            if (descending)
            {
                if (result > 0)
                    result = -1;
                else if (result < 0)
                    result = 1;
            }
            return result;
        }

        #endregion

        public int DoCompare(object x, object y)
        {
            if (x == null && y == null)
                return 0;

            if (x == null)
                return 1;

            if (y == null)
                return -1;

            if (x.GetType() != y.GetType())
                throw new Exception("Can't compare values of different types");

            Type type = x.GetType();

            if (typeof(IList).IsAssignableFrom(type))
            {
                return ((IList)x).Count.CompareTo(((IList)y).Count);
            }
            else
            {
                if (type.IsEnum)
                    return (x.ToString()).CompareTo(y.ToString());

                else if (type.IsPrimitive)
                {
                    if (type == typeof(bool))
                        return ((bool)x).CompareTo((bool)y);

                    else if (type == typeof(Int16))
                        return ((Int16)x).CompareTo((Int16)y);

                    else if (type == typeof(Int32))
                        return ((Int32)x).CompareTo((Int32)y);

                    else if (type == typeof(Int64))
                        return ((Int64)x).CompareTo((Int64)y);

                    else if (type == typeof(Byte))
                        return ((Byte)x).CompareTo((Byte)y);
                }
                else if (type.IsValueType)
                {
                    if (type == typeof(DateTime))
                        return ((DateTime)x).CompareTo((DateTime)y);

                    else if (type == typeof(Decimal))
                        return ((Decimal)x).CompareTo((Decimal)y);
                }
                else if (type.IsClass)
                {
                    if (type == typeof(string))
                        return ((string)x).CompareTo((string)y);

                    else if (type == typeof(byte[]))
                        ; //editor = new DateTimeEditor(property.Name);
                    
                    else
                    {
                        string name1 = page.GetObjectName(x);
                        string name2 = page.GetObjectName(y);
                        return name1.CompareTo(name2);
                    }
                }

                return 0;
            }
        }
    }
}
