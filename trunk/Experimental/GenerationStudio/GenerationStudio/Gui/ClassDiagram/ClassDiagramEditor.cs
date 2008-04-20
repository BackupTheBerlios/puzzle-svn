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

        public ClassDiagramElement ClassDiagramNode { get; set; }


        private void AddClass()
        {
            UmlInstanceType newClass = new UmlClass();
            newClass.Bounds = new Rectangle(1 * 21, 1 * 21, 7 * 21, 2 * 21);
            newClass.DataSource.TypeName = "SomeClass";

            UmlDesigner.Diagram.Shapes.Add(newClass);
            UmlDesigner.Refresh();
        }

        private void UmlDesigner_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                string data = (string)e.Data.GetData(typeof(string));
                if (data == "DragElement")
                {
                    if (Engine.DragDropElement is ClassElement)
                    {
                        CreateUmlType(e);
                    }
                    if (Engine.DragDropElement is InterfaceElement)
                    {
                        CreateUmlType(e);
                    }

                }
            }
        }



        private void CreateUmlType(DragEventArgs e)
        {
            TypeElement element = (TypeElement)Engine.DragDropElement;

            ClassDiagramTypeElement diagramElement = new ClassDiagramTypeElement();
            diagramElement.Type = element;
            diagramElement.Expanded = true;
            Point cp = UmlDesigner.PointToClient(new Point(e.X, e.Y));
            diagramElement.X = cp.X;
            diagramElement.Y = cp.Y;
            diagramElement.Width = 21 * 4;
            ClassDiagramNode.AddChild(diagramElement);


            AddUmlTypeFromTypeElement(diagramElement);

            UmlDesigner.Refresh();
        }

        private void AddUmlTypeFromTypeElement(ClassDiagramTypeElement diagramElement)
        {           
            UmlInstanceType t = null;
            if (diagramElement.Type is InterfaceElement)
            {
                t = new UmlInterface();
                UmlInterfaceData data = new UmlInterfaceData();
                data.Owner = diagramElement;
                t.DataSource = data;
            }

            if (diagramElement.Type is ClassElement)
            {
                t = new UmlClass();
                UmlClassData data = new UmlClassData();
                data.Owner = diagramElement;
                t.DataSource = data;
            }

            UmlDesigner.Diagram.Shapes.Add(t);
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

        public void LoadData()
        {
            UmlDesigner.Diagram.Shapes.Clear();
            foreach (var diagramElement in ClassDiagramNode.GetChildren<ClassDiagramTypeElement>())
            {
                AddUmlTypeFromTypeElement(diagramElement);
            }
            UmlDesigner.Refresh();
        }
    }
}
