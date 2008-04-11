using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GenerationStudio.Elements;
using GenerationStudio.Forms.Docking;
using WeifenLuo.WinFormsUI.Docking;

namespace GenerationStudio.Gui
{
    public partial class MainForm : IHost
    {
        private Dictionary<Element, Dictionary<string, Control>> elementEditors = new Dictionary<Element, Dictionary<string, Control>>();

        public T GetEditor<T>(Element owner,string name) where T:Control,new()
        {
            if (!elementEditors.ContainsKey(owner))
                elementEditors.Add(owner, new Dictionary<string, Control>());

            if (!elementEditors[owner].ContainsKey(name))
                elementEditors[owner].Add(name, new T());

            T editor = (T)elementEditors[owner][name];
            return editor;
        }

        public void ShowEditor(Control editor)
        {

            DockingForm form = new DockingForm();
            form.SetContent(editor, "Blah");
            form.Show(DockPanel);
            MainMenu.SendToBack();
        }

        public void RefreshProjectTree()
        {
            FillTreeView();
        }
    }
}
