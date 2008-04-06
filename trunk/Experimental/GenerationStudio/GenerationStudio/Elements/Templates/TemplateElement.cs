using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;
using System.Windows.Forms;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    public enum TemplateLanguage
    {
        CSharp,
        VBNet,
    }

    [Serializable]
    [ElementParent(typeof(RootElement))]
    [ElementName("Template")]
    [ElementIcon("GenerationStudio.Images.template.gif")]
    public class TemplateElement : NamedElement
    {
        public string FilePath { get; set; }
        public TemplateLanguage Language { get; set; }

        [ElementVerb("Edit template",Default=true)]
        public void Edit(IHost host)
        {
            TemplateEditor editor = host.GetEditor<TemplateEditor>(this, "Edit template");
            host.ShowEditor(editor);
        }
    }
}
