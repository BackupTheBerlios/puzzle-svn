using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Mojo
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
