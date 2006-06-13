using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization.Elements
{
    /// <summary>
    /// DTO for the VS.NET 2005 debugger visualiser
    /// </summary>
    [Serializable]
    public class VizConstructor : VizMethodBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(".ctor ({0})", GetParamTypes());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetCallSample()
        {
            return string.Format("{0} My{2}Obj = engine.CreateProxy<{0}> ({1})", this.OwnerType.BaseName , this.GetParamTypes(),this.OwnerType.BaseName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetProxyText()
        {
            return string.Format("Constructor {0} ({1})", this.OwnerType.Name, this.GetParamTypes());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetRealText()
        {
            return string.Format("Constructor {0} ({1})", this.OwnerType.BaseName, this.GetParamTypes());
        }
    }
}
