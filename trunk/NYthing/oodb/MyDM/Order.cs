using System;
using System.Collections.Generic;
using System.Text;

namespace MyDM
{
    public abstract class Order
    {
        private Employee seller;

        public virtual Employee Seller
        {
            get { return seller; }
            set { seller = value; }
        }

        private DateTime orderDate;

        public virtual DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }


        private List<OrderDetail> details;

        public virtual List<OrderDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
        
    }
}
