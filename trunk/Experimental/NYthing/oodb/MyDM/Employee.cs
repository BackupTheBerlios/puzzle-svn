using System;
using System.Collections.Generic;
using System.Text;

namespace MyDM
{
    public abstract class Employee
    {

        private Company company;

        public Employee()
        { }

        public virtual Company Company
        {
            get { return company; }
            set
            {
                company = value;
            }
        }

        private string name;

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
