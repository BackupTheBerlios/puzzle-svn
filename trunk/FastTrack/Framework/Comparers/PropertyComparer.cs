using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;

namespace Puzzle.FastTrack.Framework.Comparers
{
    public class PropertyComparer : IComparer 
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            PropertyInfo prop1 = x as PropertyInfo;
            PropertyInfo prop2 = y as PropertyInfo;
            if (prop1 == null || prop2 == null)
                throw new Exception("Can only compare instances of PropertyInfo");

            return prop1.Name.CompareTo(prop2.Name);
        }

        #endregion
    }
}
