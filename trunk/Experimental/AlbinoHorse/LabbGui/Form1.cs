using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AlbinoHorse.Model;

using System.Collections;
using AlbinoHorse.Layout;
using System.Drawing.Drawing2D;

namespace LabbGui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    Hashtable classLookup = new Hashtable();
            //    IDomainMap dm = DomainMap.Load(openFileDialog1.FileName);

            //    foreach (IClassMap classMap in dm.ClassMaps)
            //    {
            //        UmlType umlClass = new UmlType();
            //        umlClass.TypeName = classMap.Name;

            //        foreach (IPropertyMap propertyMap in classMap.PropertyMaps)
            //        {
            //            UmlProperty umlProperty = new UmlProperty();
            //            umlProperty.Name = propertyMap.Name;
            //            umlProperty.Type = propertyMap.DataType;
            //            umlClass.Properties.Add(umlProperty);
            //        }
            //        umlClass.Bounds = new Rectangle(0, 0, 200, 10);
            //        umlClass.Expanded = true;
            //        umlDesigner1.Diagram.Shapes.Add(umlClass);
            //        classLookup.Add(umlClass.TypeName, umlClass);
            //    }

            //    foreach (IClassMap classMap in dm.ClassMaps)
            //    {
            //        foreach (IPropertyMap propertyMap in classMap.PropertyMaps)
            //        {
            //            if (propertyMap.ReferenceType != Puzzle.NPersist.Framework.Enumerations.ReferenceType.None)
            //            {
            //                UmlType start = (UmlType)classLookup[classMap.Name];
            //                UmlType end = (UmlType)classLookup[propertyMap.DataType];
            //                UmlConnection umlConnection = new UmlConnection();
            //                umlConnection.Start = start;
            //                umlConnection.End = end;
            //                umlDesigner1.Diagram.Shapes.Add(umlConnection);
            //            }
            //        }
            //    }
            //}

            umlDesigner1.AutoLayout();

            UmlType person = new UmlType();
            person.Bounds = new Rectangle(1 * 21, 1 * 21, 7 * 21, 2 * 21);
            person.TypeName = "Person";
            umlDesigner1.Diagram.Shapes.Add(person);

            UmlType employee = new UmlType();
            employee.Bounds = new Rectangle(3 * 21, 6 * 21, 7 * 21, 2 * 21);
            employee.Expanded = true;
            UmlProperty prop = new UmlProperty();
            prop.Name = "Name";
            prop.Type = "System.String";
            employee.Properties.Members.Add(prop);




            prop = new UmlProperty();
            prop.Name = "Age";
            prop.Type = "System.Int32";
            employee.Properties.Members.Add(prop);
            employee.InheritsType = person;

            employee.TypeName = "Employee";
            umlDesigner1.Diagram.Shapes.Add(employee);

            UmlConnection connection1 = new UmlConnection();
            connection1.Start = employee;
            connection1.End = person;
            umlDesigner1.Diagram.Shapes.Add(connection1);

            //employee.BorderPen = new Pen(Color.Black, 1);
            //employee.BorderPen.DashCap = DashCap.Round;
            //employee.BorderPen.DashStyle = DashStyle.Dash;

            //person.BorderPen = new Pen(Color.Gray, 2);
            //person.BorderPen.DashCap = DashCap.Round;
            //person.BorderPen.DashStyle = DashStyle.Dash;
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            umlDesigner1.AutoLayout();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = toolStripComboBox1.SelectedItem.ToString ();
            if (text == "")
                return;

            int zoom = Convert.ToInt32(text);
            double dzoom = (double)zoom / 100;
            umlDesigner1.Zoom = dzoom;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            UmlType newClass = new UmlType();
            newClass.Bounds = new Rectangle(1 * 21, 1 * 21, 7 * 21, 2 * 21);
            newClass.TypeName = "SomeClass";
            umlDesigner1.Diagram.Shapes.Add(newClass);
            umlDesigner1.Refresh();
        }
    }
}