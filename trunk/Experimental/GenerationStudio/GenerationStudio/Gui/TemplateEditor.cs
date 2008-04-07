using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Puzzle.SourceCode;

namespace GenerationStudio.Gui
{
    public partial class TemplateEditor : UserControl
    {
        public TemplateEditor()
        {
            InitializeComponent();
        }

        private void TemplateEditor_Load(object sender, EventArgs e)
        {
            SyntaxLoader sl = new SyntaxLoader();
            Language lang = sl.LoadXML(Properties.Resources.CSharpTemplate);
            SyntaxBox.Document.Parser.Init(lang);            
        }
    }
}
