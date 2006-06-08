using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Expressions;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Querying;
using Puzzle.NPath.Framework.CodeDom;

namespace Puzzle.NPersist.Linq
{
    public delegate T Func<T>();
    public delegate T Func<A0, T>(A0 arg0);
    public delegate T Func<A0, A1, T>(A0 arg0, A1 arg1);
    public delegate T Func<A0, A1, A2, T>(A0 arg0, A1 arg1, A2 arg2);
    public delegate T Func<A0, A1, A2, A3, T>(A0 arg0, A1 arg1, A2 arg2, A3 arg3);

    public static class Sequence
    {
        public static ILinqList<T> Where<T>(this IList source, Expression<Func<T, bool>> predicate) 
        {           
           //source.WhereClause = "where " + LinqToNPathConverter.ConvertToString(predicate);
          
            LinqList<T> list = new LinqList<T>();

            return list;
        }

        public static ILinqList<T> Where<T>(this IList<T> source, Expression<Func<T, bool>> predicate) 
        {           
           //source.WhereClause = "where " + LinqToNPathConverter.ConvertToString(predicate);

            LinqList<T> list = new LinqList<T>();

            return list;
        }

        public static ILinqList<T> Where<T>(this ILinqList<T> source, Expression<Func<T, bool>> predicate) 
        {   
            source = source.Clone ();

            source.IsDirty = true;
    
            string whereClause = LinqToNPathConverter.ConvertToString(predicate);

            if ( source.Query.WhereClause != "" ) 
            {
                source.Query.WhereClause += string.Format (" and ({0})",whereClause);
            }
            else
            {
                source.Query.WhereClause = string.Format ("where ({0})",whereClause);
            }

            return source;
        }

        public static ILinqList<T> Select<T, S>(this ILinqList<T> source, Expression<Func<T, S>> selector) 
        {       
            source = source.Clone ();

            source.IsDirty = true;

            if (selector.Body is NewExpression)
            {
                LinqToNPathConverter.CreateLoadspan((NewExpression)selector.Body,source.Query);
            }

            if (selector.Body is MemberInitExpression)
            {
                LinqToNPathConverter.CreateLoadspan((MemberInitExpression)selector.Body,source.Query);
            }

            return source;
        }  
  
        public static ILinqList<T> OrderBy<T, K>(this ILinqList<T> source, Expression<Func<T, K>> keySelector) 
        {
            source = source.Clone ();

            source.IsDirty = true;

            source.Query.OrderByClause ="order by " + LinqToNPathConverter.ConvertToString (keySelector);
            return source;
        }

        public static ILinqList<T> OrderByDescending<T, K>(this ILinqList<T> source, Expression<Func<T, K>> keySelector) 
        {
            source = source.Clone ();

            source.IsDirty = true;

            source.Query.OrderByClause ="order by " + LinqToNPathConverter.ConvertToString (keySelector) + " desc";
            return source;
        }

        public static ILinqList<T> ThenBy<T, K>(this ILinqList<T> source, Expression<Func<T, K>> keySelector) 
        {
            source = source.Clone ();

            source.IsDirty = true;

            source.Query.OrderByClause +=", " + LinqToNPathConverter.ConvertToString (keySelector);
            return source;
        }

        public static ILinqList<T> ThenByDescending<T, K>(this ILinqList<T> source, Expression<Func<T, K>> keySelector) 
        {
            source = source.Clone ();

            source.IsDirty = true;

            source.Query.OrderByClause +=", " + LinqToNPathConverter.ConvertToString (keySelector) + " desc";
            return source;
        }
    }    
}

