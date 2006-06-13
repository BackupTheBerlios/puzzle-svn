using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization.Elements
{
    /// <summary>
    /// DTO for the VS.NET 2005 debugger visualiser
    /// </summary>
    [Serializable]
    public class VizMethodBase
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

        #region Property Parameters
        private List<VizParameter> parameters = new List<VizParameter> ();
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        public virtual List<VizInterceptor> Interceptors
        {
            get
            {
                return this.interceptors;
            }
        }
        #endregion

        #region Property OwnerType
        private VizType ownerType;
        /// <summary>
        /// 
        /// </summary>
        public virtual VizType OwnerType
        {
            get
            {
                return this.ownerType;
            }
            set
            {
                this.ownerType = value;
            }
        }
        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetProxyText()
        {
            return "hello";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetRealText()
        {
            return "hello";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetCallSample()
        {
            return "hello";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetParamTypes()
        {
            string paramString = "";
            foreach (VizParameter parameter in Parameters)
            {
                paramString += parameter.ParameterTypeName + ",";
            }
            if (paramString.Length > 0)
                paramString = paramString.Substring(0,paramString.Length - 1);

            return paramString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// DTO for the VS.NET 2005 debugger visualiser
    /// </summary>
    [Serializable]
    public class VizMethod : VizMethodBase
    {
        #region Property ReturnType
        private string returnType;
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetProxyText()
        {
            return string.Format("{0}.{1} ({2})",this.OwnerType.Name,this.Name, this.GetParamTypes());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetRealText()
        {
            if (this.Mixin == null)
                return string.Format("{0}.{1} ({2})", this.OwnerType.BaseName, this.Name, this.GetParamTypes());
            else
                return string.Format("{0}.{1} ({2})", Mixin.TypeName, this.Name, this.GetParamTypes());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{1} ({2}) : {0}", ReturnType, Name, GetParamTypes ());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetCallSample()
        {
            return string.Format("My{0}Obj.{1} ({2})",this.OwnerType.BaseName, this.Name, this.GetParamTypes());
        }

    }
}
