using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Puzzle.NAspect.Debug.Serialization;
using Puzzle.NAspect.Debug.Serialization.Elements;
using System.Drawing.Drawing2D;

namespace Puzzle.Naspect.Debug.Forms
{
    public partial class AopProxyVisualizerForm : Form
    {
        #region Property Proxy
        private SerializedProxy proxy;
        public virtual SerializedProxy Proxy
        {
            get
            {
                return this.proxy;
            }
            set
            {
                this.proxy = value;
            }
        }
        #endregion

        public AopProxyVisualizerForm()
        {
            InitializeComponent();
        }

        private void AopProxyVisualizerForm_Load(object sender, EventArgs e)
        {
            SetupData();
        }

        private void SetupData()
        {
            lblTypeName.Text = proxy.ProxyType.Name;
            foreach (VizMethodBase method in Proxy.ProxyType.Methods)
            {
                lstMethods.Items.Add(method);
            }
            lstMethods.SelectedIndex = 0;
        }

        private void lstMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMethods.SelectedIndex == -1)
                return;

            VizMethodBase vizMethod = (VizMethodBase)lstMethods.SelectedItem;

            SelectMethod(vizMethod);
        }

        private void SelectMethod(VizMethodBase vizMethod)
        {
            lblMethodName.Text = vizMethod.ToString ();
            lstInterceptors.Items.Clear();
            foreach (VizInterceptor interceptor in vizMethod.Interceptors)
            {
                lstInterceptors.Items.Add(interceptor);
            }

            Bitmap bmp = new Bitmap(500, 230 + 70 * vizMethod.Interceptors.Count);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen pen = new Pen (Brushes.Silver,5f);
            pen.EndCap = LineCap.ArrowAnchor;

            int bottom = 230 + 70 * vizMethod.Interceptors.Count-70;
            g.DrawLine(pen, 100, 70, 100, bottom);
            g.DrawLine(pen, 410, bottom, 410, 70);

            DrawConsumer(vizMethod, g);

            DrawProxy(vizMethod, g);

            int y = 170;
            foreach (VizInterceptor vizInterceptor in vizMethod.Interceptors)
            {
                DrawInterceptor(vizInterceptor, g, y);
                y += 70;
            }

            DrawBase(vizMethod, g,y);

            picInterceptors.Image = bmp;
            g.Dispose();
        }

        private void DrawBase(VizMethodBase vizMethod, Graphics g,int y)
        {
            Rectangle consumerBounds = new Rectangle(30, 00 + y, 450, 30);
            DrawBox(consumerBounds, g, Color.White, Color.FromArgb(255, 230, 210));
            DrawString(consumerBounds.X + 30, consumerBounds.Y + 10, vizMethod.GetRealText(), g);
        }

        private void DrawInterceptor(VizInterceptor vizInterceptor, Graphics g, int y)
        {
            Rectangle consumerBounds = new Rectangle(30, y, 450, 30);
            DrawBox(consumerBounds, g, Color.White, Color.FromArgb(230, 210, 255));
            DrawString(consumerBounds.X + 30, consumerBounds.Y + 10, vizInterceptor.TypeName, g);
        }

        private void DrawProxy(VizMethodBase vizMethod, Graphics g)
        {
            Rectangle consumerBounds = new Rectangle(30, 30 + 70, 450, 30);
            DrawBox(consumerBounds, g, Color.White, Color.FromArgb(255, 210, 230));
            DrawString(consumerBounds.X + 30, consumerBounds.Y + 10, vizMethod.GetProxyText(), g);
        }

        private void DrawConsumer(VizMethodBase vizMethod, Graphics g)
        {
            Rectangle consumerBounds = new Rectangle(30, 30, 450, 30);
            DrawBox(consumerBounds, g, Color.White, Color.FromArgb(210, 255, 230));
            DrawString(consumerBounds.X + 30, consumerBounds.Y + 10, vizMethod.GetCallSample(), g);
        }

        private void DrawString(int x, int y, string text, Graphics g)
        {
            Font f = new Font ("Verdana",7f,FontStyle.Regular);
            g.DrawString(text, f, Brushes.Black, x, y);
        }

        private void DrawStringBold(int x, int y, string text, Graphics g)
        {
            Font f = new Font("Verdana", 7f, FontStyle.Bold);
            g.DrawString(text, f, Brushes.Black, x, y);
        }

        private void DrawBox(Rectangle bounds, Graphics g, Color startColor, Color endColor)
        {
            Rectangle shadow = bounds;
            shadow.Offset (3,3);
            g.FillRectangle(Brushes.LightGray, shadow);
            LinearGradientBrush bgbrush = new LinearGradientBrush(bounds, startColor, endColor, 0, false);
            g.FillRectangle(bgbrush, bounds);
            g.DrawRectangle(Pens.DarkGray, bounds);
        }
    }
}