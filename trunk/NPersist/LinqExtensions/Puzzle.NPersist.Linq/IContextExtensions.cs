using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;

namespace Puzzle.NPersist.Linq
{
    public static class IContextExtensions
    {
        public static ILinqList<T> Repository<T> (this IContext context)
        {
            LinqList<T> list = new LinqList<T>();
            ILinqList ilinqList = (ILinqList)list;
            ilinqList.AttachContext (context);

            return list;
        }
    }
}
