using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Linq;

namespace Puzzle.NPersist.Framework
{
    public static class IContextExtensions
    {
        public static ITable<T> Repository<T> (this IContext context)
        {
            Table<T> list = new Table<T>();
            ITable ilinqList = (ITable)list;
            ilinqList.Init (context,null);

            return list;
        }

        public static ITable<T> Repository<T>(this IContext context,LoadSpan<T> loadSpan)
        {            
            Table<T> list = new Table<T>();
            ITable ilinqList = (ITable)list;
            ilinqList.Init(context,loadSpan);

            return list;
        }
    }
}
