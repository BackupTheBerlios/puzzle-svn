using System;


namespace Puzzle.NPersist.Samples.Northwind.Domain
{
    public enum EmployeeStatus
    {
        Hired = 0,
        Fired = 1,
        Interviewed = 2
    }

    public class Employee
    {
        private System.Int32 m_Id;
        private System.String m_Address;
        private System.DateTime m_BirthDate;
        private System.String m_City;
        private System.String m_Country;
        private System.Collections.IList m_Employees;
        private System.String m_Extension;
        private System.String m_FirstName;
        private System.DateTime m_HireDate;
        private System.String m_HomePhone;
        private System.String m_LastName;
        private System.String m_Notes;
        private System.Collections.IList m_Orders;
        private System.Byte[] m_Photo;
        private System.String m_PhotoPath;
        private System.String m_PostalCode;
        private System.String m_Region;
        private Employee m_ReportsTo;
        private System.Collections.IList m_Territories;
        private System.String m_Title;
        private System.String m_TitleOfCourtesy;

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        private EmployeeStatus status = EmployeeStatus.Interviewed;
        public virtual EmployeeStatus Status
        {
            get { return status; }
            set { status = value; }
        }	

        private bool testBool;
        public virtual bool TestBool
        {
            get { return testBool; }
            set { testBool = value; }
        }

	    public virtual System.Int32 Id
        {
            get
            {
                return m_Id;
            }
        }

	    public virtual System.String Address
        {
            get
            {
                return m_Address;
            }
            set
            {
                m_Address = value;
            }
        }

		public virtual System.DateTime BirthDate
        {
            get
            {
                return m_BirthDate;
            }
            set
            {
                m_BirthDate = value;
            }
        }

		public virtual System.String City
        {
            get
            {
                return m_City;
            }
            set
            {
                m_City = value;
            }
        }

        public virtual System.String Country
        {
            get
            {
                return m_Country;
            }
            set
            {
                m_Country = value;
            }
        }

		public virtual System.Collections.IList Employees
        {
            get
            {
                return m_Employees;
            }
            set
            {
                m_Employees = value;
            }
        }

		public virtual System.String Extension
        {
            get
            {
                return m_Extension;
            }
            set
            {
                m_Extension = value;
            }
        }

		public virtual System.String FirstName
        {
            get
            {
                return m_FirstName;
            }
            set
            {
                m_FirstName = value;
            }
        }

		public virtual System.DateTime HireDate
        {
            get
            {
                return m_HireDate;
            }
            set
            {
                m_HireDate = value;
            }
        }

		public virtual System.String HomePhone
        {
            get
            {
                return m_HomePhone;
            }
            set
            {
                m_HomePhone = value;
            }
        }

		public virtual System.String LastName
        {
            get
            {
                return m_LastName;
            }
            set
            {
                m_LastName = value;
            }
        }

		public virtual System.String Notes
        {
            get
            {
                return m_Notes;
            }
            set
            {
                m_Notes = value;
            }
        }

		public virtual System.Collections.IList Orders
        {
            get
            {
                return m_Orders;
            }
            set
            {
                m_Orders = value;
            }
        }

        public virtual System.Byte[] Photo
        {
            get
            {
                return m_Photo;
            }
            set
            {
                m_Photo = value;
            }
        }

		public virtual System.String PhotoPath
        {
            get
            {
                return m_PhotoPath;
            }
            set
            {
                m_PhotoPath = value;
            }
        }

		public virtual System.String PostalCode
        {
            get
            {
                return m_PostalCode;
            }
            set
            {
                m_PostalCode = value;
            }
        }

		public virtual System.String Region
        {
            get
            {
                return m_Region;
            }
            set
            {
                m_Region = value;
            }
        }

		public virtual Employee ReportsTo
        {
            get
            {
                return m_ReportsTo;
            }
            set
            {
                m_ReportsTo = value;
            }
        }

		public virtual System.Collections.IList Territories
        {
            get
            {
                return m_Territories;
            }
            set
            {
                m_Territories = value;
            }
        }

		public virtual System.String Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                m_Title = value;
            }
        }

		public virtual System.String TitleOfCourtesy
        {
            get
            {
                return m_TitleOfCourtesy;
            }
            set
            {
                m_TitleOfCourtesy = value;
            }
        }
    }
}
