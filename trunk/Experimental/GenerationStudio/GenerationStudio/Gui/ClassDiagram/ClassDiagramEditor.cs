using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AlbinoHorse.Model;
using GenerationStudio.AppCore;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public partial class ClassDiagramEditor : UserControl
    {
        public ClassDiagramEditor()
        {
            InitializeComponent();
            Engine.NotifyChange += new EventHandler(Engine_NotifyChange);
        }

        void Engine_NotifyChange(object sender, EventArgs e)
        {
            UmlDesigner.Refresh();
        }

        private void UmlToolbox_DoubleClick(object sender, EventArgs e)
        {
            if (UmlToolbox.SelectedItem.ToString() == "Class")
            {
                AddClass();
            }
        }

        private void AddClass()
        {
            UmlType newClass = new UmlType();
            newClass.Bounds = new Rectangle(1 * 21, 1 * 21, 7 * 21, 2 * 21);
            newClass.DataSource.TypeName = "SomeClass";

            UmlDesigner.Diagram.Shapes.Add(newClass);
            UmlDesigner.Refresh();
        }

        private void UmlDesigner_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                ClassElement element = (ClassElement)Engine.DragDropElement;
                UmlType t = new UmlType();
                UmlTypeData data = new UmlTypeData();
                data.Owner = element;
                t.DataSource = data;
                t.Bounds = new Rectangle(1 * 21, 1 * 21, 7 * 21, 2 * 21);
                t.Expanded = true;
                UmlDesigner.Diagram.Shapes.Add(t);
                UmlDesigner.Refresh();
            }
        }

        private void UmlDesigner_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
