using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;

namespace Puzzle.NPersist.Linq.Context
{
    public static class IContextExtensions
    {
        public static ILinqList<T> Repository<T> (this IContext context)
        {
            /*
            LinqQuery<T> query = new LinqQuery<T>();
            query.Context = context;
             */

            LinqList<T> list = new LinqList<T>();
            list.Query = new LinqQuery<T>();
            list.Query.Context = context;


            return list;
        }

        //public static IList<T> GetObjects<T>(this IContext context,LinqQuery<T> query)
        //{
        //    string npath = query.ToNPath();
        //    return context.GetObjectsByNPath <T>(npath);
        //}
    }
}
