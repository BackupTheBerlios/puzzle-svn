using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Puzzle.SourceCode;
using GenerationStudio.Elements;
using System.IO;
using GenerationStudio.AppCore;

namespace GenerationStudio.Gui
{
    public partial class TemplateEditor : UserControl
    {
        public TemplateEditor()
        {
            InitializeComponent();
            MainToolStrip.Renderer = new Office2007Renderer.Office2007Renderer();
        }

        private void TemplateEditor_Load(object sender, EventArgs e)
        {
            SyntaxLoader sl = new SyntaxLoader();
            Language lang = sl.LoadXML(Properties.Resources.CSharpTemplate);
            TemplateSyntaxBox.Document.Parser.Init(lang);

            lang = sl.LoadXML(Properties.Resources.CSharp);
            SourceSyntaxBox.Document.Parser.Init(lang);
            OutputSyntaxBox.Document.Parser.Init(lang);            
        }

        public TemplateElement Node { get; set; }

        private void MainToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = OpenDialog.FileName;
                OpenFile(fileName);
            }
        }

        public void OpenFile(string fileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
                {
                    string text = sr.ReadToEnd();
                    TemplateSyntaxBox.Document.Text = text;
                    Node.FilePath = fileName;
                    Engine.OnNotifyChange();
                }
            }
            catch
            {
                MessageBox.Show("Could not open file");
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = SaveDialog.FileName;
                SaveFileDialog(fileName);
            }
        }

        private void SaveFileDialog(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName,false, Encoding.Default))
            {
                sw.Write(TemplateSyntaxBox.Document.Text);                
                Node.FilePath = fileName;
                Engine.OnNotifyChange();
            }
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            TemplateSyntaxBox.Document.ParseAll(true);

            StringBuilder sbHeader = new StringBuilder();

            foreach (Row row in TemplateSyntaxBox.Document)
            {
                foreach (Word word in row)
                {                    
                    if (word.Segment.BlockType.Name == "CS Directive")
                    {
                        if (word.Style.Name == "CS Scope")
                            continue;

                        sbHeader.Append(word.Text);
                    }                    
                }
                if (row.EndSegment.BlockType.Name == "CS Directive")
                    sbHeader.AppendLine();
            }

            sbHeader.AppendLine("namespace Runtime.Code");
            sbHeader.AppendLine("{");
            sbHeader.AppendLine("   public class MyTemplate : ITemplate");
            sbHeader.AppendLine("   {");
            sbHeader.AppendLine("       public void Render()");
            sbHeader.AppendLine("       {");
            sbHeader.AppendLine("       }");
            sbHeader.AppendLine("   }");
            sbHeader.AppendLine("}");
            SourceSyntaxBox.Document.Text = sbHeader.ToString();

        }
    }
}
