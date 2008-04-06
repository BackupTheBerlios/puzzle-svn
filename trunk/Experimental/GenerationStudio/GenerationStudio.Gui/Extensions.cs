using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Elements;
using System.Windows.Forms;

namespace GenerationStudio
{
    public static class GuiExtensions
    {
        public static Element GetElement(this TreeNode node)
        {
            return node.Tag as Element;
        }
    }
}
