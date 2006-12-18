using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Standard
{
    public class DirtyTrackedMixin : IDirtyTracked
    {
        #region Property IsDirty 
        private bool isDirty;
        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                this.isDirty = value;
            }
        }                        
        #endregion
    }
}
