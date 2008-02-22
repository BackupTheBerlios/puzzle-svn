using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.FastForward.Framework;
using Puzzle.FastForward.Framework.Service;

namespace Puzzle.FastForward.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            while (input != "exit")
            {
                Execute(input);
                input = Console.ReadLine();
            }
        }

        private static void Execute(string text)
        {
            IEngine engine = new FastForwardEngine(); 
            engine.Execute(text);
        }
    }
}
