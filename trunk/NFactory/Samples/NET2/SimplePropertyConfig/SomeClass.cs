using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SimplePropertyConfig
{
    public class SomeClass
    {
        #region Property AStringProperty
        private string aStringProperty;
        public virtual string AStringProperty
        {
            get
            {
                return this.aStringProperty;
            }
            set
            {
                this.aStringProperty = value;
            }
        }
        #endregion

        #region Property ADateTimeProperty
        private DateTime aDateTimeProperty;
        public virtual DateTime ADateTimeProperty
        {
            get
            {
                return this.aDateTimeProperty;
            }
            set
            {
                this.aDateTimeProperty = value;
            }
        }
        #endregion

        #region Property SomeListProperty
        private IList someListProperty;
        public virtual IList SomeListProperty
        {
            get
            {
                return this.someListProperty;
            }
            set
            {
                this.someListProperty = value;
            }
        }
        #endregion
    }
}
