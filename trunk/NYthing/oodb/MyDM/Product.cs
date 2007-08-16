using System;
using System.Collections.Generic;
using System.Text;

namespace MyDM
{
    public abstract class Product
    {
        private int productNumber;

        public virtual int ProductNumber
        {
            get { return productNumber; }
            set { productNumber = value; }
        }

        private string name;

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double price;

        public virtual double Price
        {
            get { return price; }
            set { price = value; }
        }
        
    }
}
