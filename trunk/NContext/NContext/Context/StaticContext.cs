using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;

namespace Puzzle.NContext.Framework
{
    public static class Context
    {
        public static Context<T> Configure<T>() where T : ITemplate , new ()
        {
            Context<T> context = new Context<T> ();
            return context;
        }
    }
}
