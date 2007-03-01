using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.FastTrack.Framework.Web.Filtering
{
    public class Filter
    {
        private IList<FilterItem> filterItems = new List<FilterItem>();

        public IList<FilterItem> FilterItems
        {
            get { return filterItems; }
        }
    }
}
