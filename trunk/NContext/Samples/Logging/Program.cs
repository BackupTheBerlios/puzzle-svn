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

            //get an object from the container
            Car volvo = ctx.GetTemplate<DemoTemplate>().Volvo();
           
            //see if the logging works
            volvo.Drive();

            //examine the injected logger on the volvo
            Console.WriteLine("The logger attached to volvo is: {0}", volvo.Logger.GetType().Name);

            //identity test
            Car volvoAgain = ctx.GetTemplate<DemoTemplate>().Volvo();
            if (volvo == volvoAgain)
                Console.WriteLine("volvo and volvoAgain are the same object");
            else
                Console.WriteLine("volvo and volvoAgain are not the same object");


            //get a crazy car (instancemode = PerCall = a new instance each time)
            Car crazyCar1 = ctx.GetTemplate<DemoTemplate>().CrazyCar();
            crazyCar1.Drive();

            if (volvo.Logger == crazyCar1.Logger)
                Console.WriteLine("volvo and crazyCar1 uses the same logger");
            else
                Console.WriteLine("volvo and crazyCar1 do not use the same logger");

            Console.ReadLine();
        }
    }
}
