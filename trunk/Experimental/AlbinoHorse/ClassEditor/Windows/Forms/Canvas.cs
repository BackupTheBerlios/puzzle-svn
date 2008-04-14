using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AlbinoHorse.Windows.Forms
{
    public partial class Canvas : UserControl
    {
        public event EventHandler CanvasScroll;
        protected virtual void OnCanvasScroll(EventArgs e)
        {
            if (CanvasScroll != null)
                CanvasScroll(this, e);
        }

        public Canvas()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            this.Paint += new PaintEventHandler(Canvas_Paint);
        }

        private int oldScrollX;
        private int oldScrollY;
        void Canvas_Paint(object sender, PaintEventArgs e)
        {
            CheckScroll();
        }

        private void CheckScroll()
        {
            if (AutoScrollPosition.X != oldScrollX || AutoScrollPosition.Y != oldScrollY)
            {
                oldScrollX = AutoScrollPosition.X;
                oldScrollY = AutoScrollPosition.Y;
                OnCanvasScroll(EventArgs.Empty);
            }
        }
    }
}
