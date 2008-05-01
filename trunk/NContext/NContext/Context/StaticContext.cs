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
        public static IContext<T> Configure<T>() where T : ITemplate 
        {
            Context<T> context = new Context<T> ();
            return context;
        }

        public static IContext<T> Configure<T,I>() where T : ITemplate
        {
            Context<T> context = new Context<T>(typeof(I));
            return context;
        }
    }
}
