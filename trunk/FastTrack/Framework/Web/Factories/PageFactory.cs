using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Puzzle.FastTrack.Framework.Web.Factories
{
    public class PageFactory
    {
        public static IList CreatePage(IList list, int pageNumber, int pageSize)
        {
            IList page = new ArrayList();

            if (list != null)
            {
                int start = pageNumber * pageSize;
                int stop = start + pageSize;

                int i = 0;
                foreach (object obj in list)
                {
                    if (i >= start)
                        page.Add(obj);

                    i++;
                    if (i >= stop)
                        break;
                }
            }

            return page;
        }
    }
}
