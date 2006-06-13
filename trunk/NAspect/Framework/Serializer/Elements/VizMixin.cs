using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization.Elements
{
    /// <summary>
    /// DTO for the VS.NET 2005 debugger visualiser
    /// </summary>
    [Serializable]
    public class VizMixin
    {
        #region Property TypeName
        private string typeName;
        /// <summary>
        /// 
        /// </summary>
        public virtual string TypeName
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

        #region Property FullTypeName
        private string fullTypeName;
        /// <summary>
        /// 
        /// </summary>
        public virtual string FullTypeName
        {
            get
            {
                return this.fullTypeName;
            }
            set
            {
                this.fullTypeName = value;
            }
        }
        #endregion

    }
}
