using System;
using System.Configuration;
using System.Xml;

using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Extensions;

//TODO: Fix References

namespace Puzzle.NAspect.Extensions.Examples.Logging
{
		class Class1
		{
			/// <summary>
			/// The main entry point for the application.
			/// </summary>
			[STAThread]
			static void Main(string[] args)
			{
				log4net.Config.XmlConfigurator.Configure();

				Engine c = NAspectApplicationContext.Configure(ConfigurationSettings.AppSettings["naspect.config"]);

				// Create an instance of "LogAoPTarget" through the aop container
				Calculator calc = c.CreateProxy(typeof (Calculator)) as Calculator;

				int nSum = 0;
				int nSub = 0;
				int nMul = calc.CalculateA(7, 5, ref nSum, out nSub);

				calc.Sum(7, 5);
				calc.Sub(7, 5);
				calc.Mul(7, 5);
				calc.Div(7, 5);
			}
		}
}
