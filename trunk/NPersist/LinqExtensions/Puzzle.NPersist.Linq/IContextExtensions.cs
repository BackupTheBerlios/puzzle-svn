using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;

namespace Puzzle.NPersist.Linq
{
    public static class IContextExtensions
    {
        public static LinqQuery<T> Repository<T> (this IContext context)
        {
            LinqQuery<T> query = new LinqQuery<T>();
            query.Context = context;

            return query;
        }

        public static IList<T> GetObjects<T>(this IContext context,LinqQuery<T> query)
        {
            string npath = query.ToNPath();
            return context.GetObjectsByNPath <T>(npath);
        }
    }
}
