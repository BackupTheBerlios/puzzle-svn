using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Puzzle.NContext.Framework
{
    public static class RunnableEngine
    {
        public static void RunRunnable(IRunnable runnable)
        {
            Thread thread = new Thread(runnable.Run);
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Lowest;

            thread.Start();
        }
    }
}
