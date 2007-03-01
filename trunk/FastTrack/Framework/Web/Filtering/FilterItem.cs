using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.FastTrack.Framework.Web.Filtering
{
    public class FilterItem
    {
        private string propertyName;
        public virtual string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        private object value;
        public virtual object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private MatchCondition matchCondition = MatchCondition.Equals;
        public virtual MatchCondition MatchCondition
        {
            get { return matchCondition; }
            set { matchCondition = value; }
        }
    }
}
