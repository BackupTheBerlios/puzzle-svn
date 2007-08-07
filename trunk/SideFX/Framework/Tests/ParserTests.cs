using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Puzzle.SideFX.Framework.Parsing;

namespace Puzzle.SideFX.Framework.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void CanParseText()
        {
            string test = "create class Employee (base = Person, table=employees)";
            IList<Command> commands = Parser.Parse(test);
            string s = "";
            foreach (Command command in commands)
                s += command.ToString();
            s = s + "";
        }
    }
}
