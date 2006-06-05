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
            return new LinqQuery<T>();
        }
    }
}
