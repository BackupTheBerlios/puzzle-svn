using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GenerationStudio.Forms.Docking
{
    public class DocumentForm : DockingForm
    {
        public System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.Panel BorderPanel;
    
        public DocumentForm()
        {
            InitializeComponent();           
        }

        private void InitializeComponent()
        {
            this.BorderPanel = new System.Windows.Forms.Panel();
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.BorderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BorderPanel
            // 
            this.BorderPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BorderPanel.Controls.Add(this.ContentPanel);
            this.BorderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BorderPanel.Location = new System.Drawing.Point(3, 3);
            this.BorderPanel.Name = "BorderPanel";
            this.BorderPanel.Padding = new System.Windows.Forms.Padding(1);
            this.BorderPanel.Size = new System.Drawing.Size(286, 260);
            this.BorderPanel.TabIndex = 0;
            // 
            // ContentPanel
            // 
            this.ContentPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(1, 1);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(284, 258);
            this.ContentPanel.TabIndex = 1;
            // 
            // DocumentForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.BorderPanel);
            this.Name = "DocumentForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.BorderPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public override void SetContent(Control content, string title)
        {
            this.HideOnClose = true;
            this.content = content;
            this.oldParent = content.Parent;
            this.ContentPanel.Controls.Clear();
            content.Parent = this.ContentPanel;
            content.Dock = DockStyle.Fill;
            this.Text = title;
            content.Visible = true;
        }
    }
}
