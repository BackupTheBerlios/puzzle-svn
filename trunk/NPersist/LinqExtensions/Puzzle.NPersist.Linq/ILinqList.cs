using System;
using System.Collections.Generic;
using System.Text;
using System.Query;
using Puzzle.NPersist.Framework;


namespace Puzzle.NPersist.Linq
{
    public interface ILinqList
    {
        void AttachContext(IContext context);

    }
    
    public interface ILinqList<T> : IList<T> 
    {
        bool IsLoaded {get;set;}
        bool IsDirty {get;set;}
        LinqQuery<T> Query {get;}        

        ILinqList<T> Clone();
    }
}
