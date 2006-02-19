using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCtorConfig
{
    public class SomeClass
    {
        //default ctor
        public SomeClass()
        {
            Console.WriteLine("SomeClass() was called");
        }

        //custom ctor
        public SomeClass(int intValue)
        {
            Console.WriteLine("SomeClass(int) was called");
            Console.WriteLine("intValue = {0}",intValue);
        }

        //custom ctor
        public SomeClass(string stringValue, DateTime dateTimeValue)
        {
            Console.WriteLine("SomeClass(string,DateTime) was called");
            Console.WriteLine("stringValue = {0}", stringValue);
            Console.WriteLine("dateTimeValue = {0}", dateTimeValue);
        }
    }
}
