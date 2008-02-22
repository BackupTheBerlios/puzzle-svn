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


        private IList<OrderDetail> details;
      
        //[InverseOf(typeof(OrderDetail),"Order")]
        public virtual IList<OrderDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
        
    }
}
