using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;

namespace GenerationStudio.Forms.Docking
{
    public class DockingForm : DockContent
    {
        protected Control content;
        protected ToolStripContainer Container;
        protected Control oldParent;

        public DockingForm()
        {
            HideOnClose = true;
            base.DockAreas = DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.Document | DockAreas.Float;
        }

        public virtual void SetContent(Control content, string title)
        {
            this.HideOnClose = true;
            this.content = content;
            this.oldParent = content.Parent;
            this.Controls.Clear();
            content.Parent = this;
            content.Dock = DockStyle.Fill;
            this.Text = title;
            content.Visible = true;
        }
    }
}
