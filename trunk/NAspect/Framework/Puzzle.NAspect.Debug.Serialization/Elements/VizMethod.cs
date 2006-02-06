using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization.Elements
{
    [Serializable]
    public class VizMethodBase
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

        #region Property Parameters
        private List<VizParameter> parameters = new List<VizParameter> ();
        public virtual List<VizParameter> Parameters
        {
            get
            {
                return this.parameters;
            }
        }
        #endregion
       
        #region Property Interceptors
        private List<VizInterceptor> interceptors = new List<VizInterceptor> ();
        public virtual List<VizInterceptor> Interceptors
        {
            get
            {
                return this.interceptors;
            }
        }
        #endregion
    }

    [Serializable]
    public class VizMethod : VizMethodBase
    {
        #region Property ReturnType
        private string returnType;
        public virtual string ReturnType
        {
            get
            {
                return this.returnType;
            }
            set
            {
                this.returnType = value;
            }
        }
        #endregion

        //owner mixin
        #region Property Mixin
        private VizMixin mixin;
        public virtual VizMixin Mixin
        {
            get
            {
                return this.mixin;
            }
            set
            {
                this.mixin = value;
            }
        }
        #endregion
    }
}
