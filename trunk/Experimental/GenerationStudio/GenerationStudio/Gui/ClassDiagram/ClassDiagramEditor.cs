﻿using System;
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

            if (UmlToolbox.SelectedItem.ToString() == "Association")
            {
                UmlDesigner.BeginDrawRelation(EndDrawAssociation);
            }

            if (UmlToolbox.SelectedItem.ToString() == "Inheritance")
            {
                UmlDesigner.BeginDrawRelation(EndDrawInheritance);
            }
        }

        private void EndDrawAssociation(Shape start, Shape end)
        {
            if (start == null || end == null)
                return;

        }

        private void EndDrawInheritance(Shape start, Shape end)
        {
            if (start == null || end == null)
                return;

            if (start is UmlClass)
            {                
                UmlClass startClass = start as UmlClass;
                ClassElement startElement = (startClass.DataSource as UmlClassData).Owner.Type as ClassElement;

                if (end is UmlClass)
                {
                    UmlClass endClass = end as UmlClass;
                    ClassElement endElement = (endClass.DataSource as UmlClassData).Owner.Type as ClassElement;

                    startElement.Inherits = endElement.Name;
                }

                if (end is UmlInterface)
                {
                    UmlInterface endInterface = end as UmlInterface;
                    InterfaceElement endElement = (endInterface.DataSource as UmlInterfaceData).Owner.Type as InterfaceElement;

                    bool exists = false;

                    foreach (var existingImpl in startElement.GetChildren<ImplementationElement>())
                    {
                        if (existingImpl.InterfaceName == endElement.Name)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        ImplementationElement implementation = new ImplementationElement();
                        implementation.InterfaceName = endElement.Name;
                        startElement.AddChild(implementation);
                    }
                }
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
                    if (Engine.DragDropElement is EnumElement )
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
            diagramElement.Width = 21 * 7;
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

            if (diagramElement.Type is EnumElement)
            {
                t = new UmlEnum();
                UmlEnumData data = new UmlEnumData();
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
            foreach (var diagramElement in ClassDiagramNode.GetChildren<ClassDiagramMemberElement>())
            {
                if (diagramElement is ClassDiagramTypeElement)
                    AddUmlTypeFromTypeElement(diagramElement as ClassDiagramTypeElement);
                if (diagramElement is ClassDiagramCommentElement)
                    AddUmlComment(diagramElement as ClassDiagramCommentElement);
            }
            UmlDesigner.Refresh();
        }

        private void AddUmlComment(ClassDiagramCommentElement diagramElement)
        {
            UmlComment comment = new UmlComment();
            UmlCommentData data = new UmlCommentData();
            data.Owner = diagramElement;
            comment.DataSource = data;
            UmlDesigner.Diagram.Shapes.Add(comment);
        }

        private void ZoomLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            double zoomLevel = double.Parse(ZoomLevelComboBox.Text) / 100;
            UmlDesigner.Zoom = zoomLevel;
        }

        private void UmlToolbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
