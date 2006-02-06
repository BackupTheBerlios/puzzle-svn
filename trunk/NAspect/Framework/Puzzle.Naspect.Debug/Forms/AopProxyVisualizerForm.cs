using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Puzzle.NAspect.Debug.Serialization;
using Puzzle.NAspect.Debug.Serialization.Elements;

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
        }
    }
}