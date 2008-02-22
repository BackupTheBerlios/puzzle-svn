using System;
using System.Collections.Generic;
using System.Text;

namespace MyDM
{
    public abstract class OrderDetail
    {
        private double quantity;

        public virtual double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        private Product product;

        public virtual Product Product
        {
            get { return product; }
            set { product = value; }
        }

        private Order order;
        public virtual Order Order
        {
            get { return order; }
            set { order = value; }
        }
        
    }
}
