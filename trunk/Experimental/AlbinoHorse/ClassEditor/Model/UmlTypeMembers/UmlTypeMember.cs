using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public class UmlTypeMember 
    {
        public IUmlTypeMemberData DataSource { get; set; }

        public string Name
        {
            get
            {
                return DataSource.Name;
            }

            set
            {
                DataSource.Name = value;
            }
        }
    }
}
