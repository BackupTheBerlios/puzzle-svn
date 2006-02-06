using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization
{
    [Serializable]
    public class SerializedProxy
    {
        #region Property TypeName 
        private string typeName;
        public string TypeName
        {
            get
            {
                return this.typeName;
            }
            set
            {
                this.typeName = value;
            }
        }                        
        #endregion
    }
}
