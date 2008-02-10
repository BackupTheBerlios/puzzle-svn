using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;


namespace Puzzle.NPersist.Framework.Linq
{
    public interface ITable
    {
        void Init(IContext context,ILoadSpan loadSpan);       
    }
    
    public interface ITable<T> : IList<T> 
    {
        bool IsLoaded {get;set;}
        bool IsDirty {get;set;}
        LinqQuery<T> Query {get;}        

        ITable<T> Clone();
    }
}
