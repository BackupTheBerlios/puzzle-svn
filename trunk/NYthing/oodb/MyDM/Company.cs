using System;
using System.Collections.Generic;
using System.Text;

namespace MyDM
{
    public abstract class Company
    {
        private string name;
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        private IList<Employee> employees;
        public virtual IList<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }
    }
}
