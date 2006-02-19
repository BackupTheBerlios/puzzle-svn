using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedConfig
{
    public class Some
    {
        #region Property Property
        private Property property = new Property ();
        public virtual Property Property
        {
            get
            {
                return this.property;
            }
        }
        #endregion
    }

    public class Property
    {
        #region Property Path
        private string path;
        public virtual string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }
        #endregion
    }

    
}
