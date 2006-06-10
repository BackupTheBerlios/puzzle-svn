using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;

namespace Puzzle.NPersist.Linq
{
    public static class IContextExtensions
    {
        public static ITable<T> Repository<T> (this IContext context)
        {
            Table<T> list = new Table<T>();
            ITable ilinqList = (ITable)list;
            ilinqList.AttachContext (context);

            return list;
        }
    }
}
