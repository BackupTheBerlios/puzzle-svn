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
    }
}
