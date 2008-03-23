using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puzzle.NContext.Framework;
using Logging.Classes;

namespace Logging.Factories
{
    public class DemoFactory : ObjectInitializerBase
    {

        //register an object on the name "Volvo"
        //custom names can also be used
        //but default is the method name
        /// <summary>
        /// Creates a nice Volvo for you
        /// Intellisense test
        /// </summary>
        /// <returns></returns>
        [FactoryMethod(InstanceMode.PerContext)]
        public Car Volvo()
        {
            Car car = CreateObject<Car>();
            car.NumberOfWheels = 4;
            car.Name = "Volvo V70";
            return car;
        }

        //register an object that will
        //change each time you fetch it from the context
        private Random rnd = new Random();
        private int crazyId = 0;
        [FactoryMethod(InstanceMode.PerCall)]
        public Car CrazyCar()
        {
            Car car = CreateObject<Car>();
            car.NumberOfWheels = rnd.Next(3, 10);
            car.Name = "CrazyCar" + crazyId.ToString();
            crazyId++;
            return car;
        }

        //create a concrete logger and register it as the
        //default object for ILogger
        //only one instance per context should be created
        [FactoryMethod(FactoryType.DefaultForType, InstanceMode.PerContext)]
        public ILogger Logger()
        {
            MyLogger logger = CreateObject<MyLogger>();
            return logger;
        }

        //configure _all_ objects that implement ILoggable
        [ConfigurationMethod(ConfigurationType.AppliesToAll)]
        public void ConfigureLoggable(ILoggable loggable)
        {
            //attach the default implementation of ILogger 
            //to the loggable object
            loggable.Logger = GetObject<ILogger>();
        }
    }
}
