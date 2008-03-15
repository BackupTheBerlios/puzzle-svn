using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puzzle.NContext.Framework;
using Logging.Factories;
using Logging.Classes;

namespace Logging
{
    class Program
    {
        static void Main(string[] args)
        {
            IContext ctx = Context.Configure();
            ctx.RegisterObjectFactory(new DemoFactory());

            //get an object from the container
            Car volvo = ctx.GetObject<Car>("Volvo");
            
            //see if the logging works
            volvo.Drive();

            //examine the injected logger on the volvo
            Console.WriteLine("The logger attached to volvo is: {0}", volvo.Logger.GetType().Name);

            //identity test
            Car volvoAgain = ctx.GetObject<Car>("Volvo");
            if (volvo == volvoAgain)
                Console.WriteLine("volvo and volvoAgain are the same object");
            else
                Console.WriteLine("volvo and volvoAgain are not the same object");

            

            Console.ReadLine();
        }
    }
}
