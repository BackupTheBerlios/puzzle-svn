using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AlbinoHorse.Infrastructure;
using AlbinoHorse.Windows.Forms;

namespace AlbinoHorse.Infrastructure
{
    public class ShapeKeyEventArgs
    {
        public Keys Key;        
        public bool Redraw { get; set; }
        public UmlDesigner Sender { get; set; }
        public int GridSize { get; set; }
        public bool SnapToGrid { get; set; }
    }
}
