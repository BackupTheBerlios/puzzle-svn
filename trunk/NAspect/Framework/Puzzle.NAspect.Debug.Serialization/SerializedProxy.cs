using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Debug.Serialization.Elements;

namespace Puzzle.NAspect.Debug.Serialization
{
    [Serializable]
    public class SerializedProxy
    {
        #region Property ProxyType
        private VizType proxyType;
        public virtual VizType ProxyType
        {
            get
            {
                return this.proxyType;
            }
            set
            {
                this.proxyType = value;
            }
        }
        #endregion
    }
}