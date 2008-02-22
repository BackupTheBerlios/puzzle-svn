using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Puzzle.FastForward.Framework.Service
{
    public interface IDisplayService
    {
        void Display(IList objects);

        void Display(object obj);

        void List(IList objects);

        void List(object obj);
    }
}
