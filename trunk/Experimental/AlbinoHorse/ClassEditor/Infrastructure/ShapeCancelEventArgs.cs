using System;
using System.Collections.Generic;
using System.Text;
using AlbinoHorse.Model;

namespace AlbinoHorse.Infrastructure
{
    public class ShapeCancelEventArgs
    {
        public Shape Shape { get; set; }
        public bool Cancel { get; set; }
    }
}
