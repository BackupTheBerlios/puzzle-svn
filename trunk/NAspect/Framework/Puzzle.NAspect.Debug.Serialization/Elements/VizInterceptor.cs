using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization.Elements
{
    public enum VizInterceptorType
    {
        Before,
        After,
        Around,
    }

    [Serializable]
    public class VizInterceptor
    {
        #region Property TypeName
        private string typeName;
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

        #region Property InterceptorType
        private VizInterceptorType interceptorType;
        public virtual VizInterceptorType InterceptorType
        {
            get
            {
                return this.interceptorType;
            }
            set
            {
                this.interceptorType = value;
            }
        }
        #endregion

        public override string ToString()
        {
            return string.Format("{0} : {1}", TypeName, InterceptorType);
        }
    }
}
