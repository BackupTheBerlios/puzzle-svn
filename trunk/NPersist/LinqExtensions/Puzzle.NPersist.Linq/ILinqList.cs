using System;
using System.Collections.Generic;
using System.Text;
using System.Query;
using Puzzle.NPersist.Framework;


namespace Puzzle.NPersist.Linq
{
    public interface ITable
    {
        void AttachContext(IContext context);

    }
    
    public interface ITable<T> : IList<T> 
    {
        bool IsLoaded {get;set;}
        bool IsDirty {get;set;}
        LinqQuery<T> Query {get;}        

        ITable<T> Clone();
    }
}
