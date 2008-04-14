using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public class UmlProperty : UmlTypeMember
    {
        #region Property Type 
        private string type;
        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }                        
        #endregion
    }
}
