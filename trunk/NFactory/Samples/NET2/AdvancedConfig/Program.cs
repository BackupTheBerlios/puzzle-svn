using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NFactory.Framework;

namespace AdvancedConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = ApplicationContext.Configure();
            SomeClass myObject = container.GetObject<SomeClass>("MyObject");

            myObject.DoStuff();

            Console.WriteLine("MyObject.Some.Property.Path = {0}", myObject.Some.Property.Path);


            //get MyObject again from the container            
            SomeClass myObject2 = container.GetObject<SomeClass>("MyObject");

            if (myObject == myObject2)
                Console.WriteLine("myObject and myObject2 is the same instance");
            else
                Console.WriteLine("myObject and myObject2 are two different instances"); //<- because its marked as PerGraph in the config


            if (myObject.LogManager == myObject2.LogManager)
                Console.WriteLine("myObject.LogManager and myObject2.LogManager is the same instance"); //<- because its marked as PerContainer in the config
            else
                Console.WriteLine("myObject.LogManager and myObject2.LogManager are two different instances"); 


            Console.ReadLine();
        }
    }
}
