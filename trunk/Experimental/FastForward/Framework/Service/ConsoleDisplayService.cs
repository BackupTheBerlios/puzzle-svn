using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Puzzle.SideFX.Framework;
using System.Collections;

namespace Puzzle.FastForward.Framework.Service
{
    public class ConsoleDisplayService : IDisplayService
    {
        public ConsoleDisplayService(IEngine engine)
        {
            this.engine = engine;
        }

        protected IEngine engine;

        #region IDisplayService Members

        public virtual void Display(IList objects)
        {
            foreach (object obj in objects)
                Display(obj);
        }

        public virtual void Display(object obj)
        {
            string output = engine.GetService<IRenderService>().Render(obj, false).ToString();
            Console.WriteLine(output);
        }

        public virtual void List(IList objects)
        {
            foreach (object obj in objects)
                List(obj);
        }

        public virtual void List(object obj)
        {
            string output = engine.GetService<IRenderService>().Render(obj, true).ToString();
            Console.WriteLine(output);
        }

        #endregion

    }
}
