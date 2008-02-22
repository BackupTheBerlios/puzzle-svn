using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Puzzle.SideFX.Framework.Tests
{
    [TestFixture]
    public class ExecutionTests
    {
        [Test]
        public void DoIt()
        {
            string text = "create class Employee (base = Person, table=employees)";
            IEngine engine = new Engine();
            LoggingService loggingService = new LoggingService();
            engine.RegisterService<ILoggingService>(loggingService);
            CommandLoggingExecutor executor = new CommandLoggingExecutor();
            engine.RegisterExecutor(executor);
            engine.Execute(text);
            Assert.AreEqual("create class Employee = (base = Person, table = employees, ) " + Environment.NewLine, loggingService.Output);
        }
    }
}
