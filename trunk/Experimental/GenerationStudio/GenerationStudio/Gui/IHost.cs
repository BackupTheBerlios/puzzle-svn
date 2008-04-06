using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public interface IHost
    {
        void ShowEditor(Control editor);
        T GetEditor<T>(Element owner, string name) where T : Control, new();
        void RefreshProjectTree();
    }
}
