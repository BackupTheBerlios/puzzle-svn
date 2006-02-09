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

        #region Property MayBreakFlow 
        private bool mayBreakFlow;
        public bool MayBreakFlow
        {
            get
            {
                return this.mayBreakFlow;
            }
            set
            {
                this.mayBreakFlow = value;
            }
        }                        
        #endregion

        #region Property IsOptional 
        private bool isRequired;
        public bool IsRequired
        {
            get
            {
                return this.isRequired;
            }
            set
            {
                this.isRequired = value;
            }
        }                        
        #endregion
        
        #region Property ThrowsExceptionTypes 
        private List<string> throwsExceptionTypes = new List<string> ();
        public List<string> ThrowsExceptionTypes
        {
            get
            {
                return this.throwsExceptionTypes;
            }
        }                        
        #endregion

        public override string ToString()
        {
            return string.Format("{0} : {1}", TypeName, InterceptorType);
        }
    }
}
