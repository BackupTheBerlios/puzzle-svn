using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenerationStudio.Gui
{
    public partial class TableDataView : UserControl
    {
        public TableDataView()
        {
            InitializeComponent();
        }

        public DataTable Data { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Grid.DataSource = Data;
        }
    }
}
