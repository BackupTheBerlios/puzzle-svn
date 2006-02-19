using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NFactory.Framework;

namespace SimpleCtorConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = ApplicationContext.Configure();
            SomeClass myObject = container.GetObject<SomeClass>("MyObject");
            //this is a simple ctor injection sample
            //see SomeClass.cs for more info

            Console.ReadLine();
        }
    }
}
