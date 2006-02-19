using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NFactory.Framework;

namespace SimplePropertyConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = ApplicationContext.Configure();
            SomeClass myObject = container.GetObject<SomeClass>("MyObject");

            Console.WriteLine("AStringProperty = {0}",myObject.AStringProperty);
            Console.WriteLine("ADateTimeProperty = {0}", myObject.ADateTimeProperty);

            //lets see whats in the list property
            foreach (object item in myObject.SomeListProperty)
            {
                Console.WriteLine("element = {0}", item);
            }

            Console.ReadLine();
        }
    }
}
