using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace GenerationStudio.Forms.Docking
{
    public class StartDockingForm : DockContent
    {
        private System.Windows.Forms.WebBrowser webBrowser1;

        public StartDockingForm()
        {
            InitializeComponent();
        }
    
        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(552, 538);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("http://rogeralsing.com/category/puzzleframework/caramel/", System.UriKind.Absolute);
            // 
            // StartDockingForm
            // 
            this.ClientSize = new System.Drawing.Size(552, 538);
            this.Controls.Add(this.webBrowser1);
            this.Name = "StartDockingForm";
            this.TabText = "Start";
            this.Text = "Start";
            this.ResumeLayout(false);

        }
    }
}
