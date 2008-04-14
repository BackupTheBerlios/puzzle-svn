using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public class UmlMethod : UmlTypeMember
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
    }
}
