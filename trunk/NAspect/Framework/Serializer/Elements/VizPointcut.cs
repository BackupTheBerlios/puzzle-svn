using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization.Elements
{
    [Serializable]
    public class VizPointcut
    {
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
}
