using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization.Elements
{
    /// <summary>
    /// DTO for the VS.NET 2005 debugger visualiser
    /// </summary>
    [Serializable]
    public enum VizParameterDirection
    {
        Val,
        Ref,        
        Out,
    }

    /// <summary>
    /// DTO for the VS.NET 2005 debugger visualiser
    /// </summary>
    [Serializable]
    public class VizParameter
    {
        #region Property Name
        private string name;
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        #endregion

        #region Property ParameterTypeName
        private string parameterTypeName;
        /// <summary>
        /// 
        /// </summary>
        public virtual string ParameterTypeName
        {
            get
            {
                return this.parameterTypeName;
            }
            set
            {
                this.parameterTypeName = value;
            }
        }
        #endregion

        #region Property Direction
        private VizParameterDirection direction;
        /// <summary>
        /// 
        /// </summary>
        public virtual VizParameterDirection Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
            }
        }
        #endregion
    }
}
