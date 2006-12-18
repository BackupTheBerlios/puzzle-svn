using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Standard;

namespace KumoUnitTests
{
    [DirtyTracked]
    public class DirtyTrackedClass
    {
        #region Property SomeProp 
        private string someProp;
        public string SomeProp
        {
            get
            {
                return this.someProp;
            }
            [MakeDirty]
            set
            {
                this.someProp = value;
            }
        }                        
        #endregion

        #region Property SomeIntProp 
        private int someIntProp;
        public int SomeIntProp
        {
            get
            {
                return this.someIntProp;
            }
            [MakeDirty]
            set
            {
                this.someIntProp = value;
            }
        }                        
        #endregion

        [ClearDirty]
        public void Save()
        { 
        }
    }
}
