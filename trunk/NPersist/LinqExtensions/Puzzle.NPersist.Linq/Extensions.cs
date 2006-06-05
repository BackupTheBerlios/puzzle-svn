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
        



        public static LinqQuery<T> Where<T>(this LinqQuery<T> source, Expression<Func<T, bool>> predicate) {           
            source.WhereClause = "where " + LinqToNPathConverter.ConvertToString(predicate);

            return source;
        }

        public static LinqQuery<T> Select<T, S>(this LinqQuery<T> source, Expression<Func<T, S>> selector) {       
            if (selector.Body is NewExpression)
            {
                LinqToNPathConverter.CreateLoadspan((NewExpression)selector.Body,source);
            }

            if (selector.Body is MemberInitExpression)
            {
                LinqToNPathConverter.CreateLoadspan((MemberInitExpression)selector.Body,source);
            }

            return source;
        }  
  
        public static LinqQuery<T> OrderBy<T, K>(this LinqQuery<T> source, Expression<Func<T, K>> keySelector) {
            source.OrderByClause ="order by " + LinqToNPathConverter.ConvertToString (keySelector);
            return source;
        }

        public static LinqQuery<T> OrderByDescending<T, K>(this LinqQuery<T> source, Expression<Func<T, K>> keySelector) {
           
            source.OrderByClause ="order by " + LinqToNPathConverter.ConvertToString (keySelector) + " desc";
            return source;
        }

        public static LinqQuery<T> ThenBy<T, K>(this LinqQuery<T> source, Expression<Func<T, K>> keySelector) {
             source.OrderByClause +=", " + LinqToNPathConverter.ConvertToString (keySelector);
            return source;
        }

        public static LinqQuery<T> ThenByDescending<T, K>(this LinqQuery<T> source, Expression<Func<T, K>> keySelector) {
            source.OrderByClause +=", " + LinqToNPathConverter.ConvertToString (keySelector) + " desc";
            return source;
        }
    }    
}

