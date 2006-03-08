using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization.Elements
{
    [Serializable]
    public enum VizParameterDirection
    {
        Val,
        Ref,        
        Out,
    }

    [Serializable]
    public class VizParameter
    {
        #region Property Name
        private string name;
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
