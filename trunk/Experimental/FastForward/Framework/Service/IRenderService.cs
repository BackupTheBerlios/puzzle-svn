using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.FastForward.Framework.Service
{
    public interface IRenderService
    {
        object Render(object obj, bool list);
    }
}
