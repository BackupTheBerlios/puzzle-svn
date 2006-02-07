using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization.Elements
{
    [Serializable]
    public class VizConstructor : VizMethodBase
    {
        public override string ToString()
        {
            return string.Format(".ctor ({0})", GetParamTypes());
        }

        public override string GetCallSample()
        {
            return string.Format("{0} MyObject = engine.CreateProxy<{0}> ({1})", this.OwnerType.BaseName , this.GetParamTypes());
        }

        public override string GetProxyText()
        {
            return string.Format("Constructor {0} ({1})", this.OwnerType.Name, this.GetParamTypes());
        }

        public override string GetRealText()
        {
            return string.Format("Constructor {0} ({1})", this.OwnerType.BaseName, this.GetParamTypes());
        }
    }
}
