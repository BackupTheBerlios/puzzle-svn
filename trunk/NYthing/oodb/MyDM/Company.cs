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

        private List<Employee> employees;
        public virtual List<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }
    }
}
