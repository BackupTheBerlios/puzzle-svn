using System;
using System.Collections.Generic;
using System.Text;
using System.Query;


namespace Puzzle.NPersist.Linq
{
    
    
    public interface ILinqList<T> : IList<T> 
    {
        bool IsLoaded {get;set;}
        bool IsDirty {get;set;}
        LinqQuery<T> Query {get;}        

        ILinqList<T> Clone();
    }
}
