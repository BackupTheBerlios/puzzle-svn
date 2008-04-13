using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GenerationStudio.Elements;
using GenerationStudio.AppCore;
using GenerationStudio.Attributes;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Remoting.Messaging;
using GenerationStudio.Forms.Docking;
using WeifenLuo.WinFormsUI.Docking;

namespace GenerationStudio.Gui
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private RootElement root;
        private ErrorDockingForm ErrorDockingForm = new ErrorDockingForm();
        private ProjectDockingForm ProjectDockingForm = new ProjectDockingForm();
        private PropertiesDockingForm PropertiesDockingForm = new PropertiesDockingForm();
        private StartDockingForm StartDockingForm = new StartDockingForm();
        private SummaryDockingForm SummaryDockingForm = new SummaryDockingForm();

        private void Form1_Load(object sender, EventArgs e)
        {
            //MainMenu.Renderer = new Office2007Renderer.Office2007Renderer();
            //ProjectContextMenu.Renderer = new Office2007Renderer.Office2007Renderer();
            //StatusBar.Renderer = new Office2007Renderer.Office2007Renderer();

            ErrorDockingForm.SetContent(ErrorPanel, "Error List");
            ProjectDockingForm.SetContent(ProjectPanel, "Solution Explorer");
            PropertiesDockingForm.SetContent(PropertyPanel, "Properties");
            SummaryDockingForm.SetContent(SummaryPanel, "Summary");

            ErrorDockingForm.Show(DockPanel, DockState.DockBottom);
            ProjectDockingForm.Show(DockPanel,DockState.DockLeft);
            PropertiesDockingForm.Show(ProjectDockingForm.Pane, DockAlignment.Bottom, 0.5);
            StartDockingForm.Show(DockPanel, DockState.Document);
            SummaryDockingForm.Show(DockPanel, DockState.Document);

            NewProject();
            Engine.RegisterAllElementTypes(root.GetType().Assembly);

            Engine.NotifyChange += new EventHandler(Engine_NotifyChange);

            MainToolStrip.SendToBack();
            MainMenu.SendToBack();
            
        }

        void Engine_NotifyChange(object sender, EventArgs e)
        {
            NotifyChange();
        }

        private void NotifyChange()
        {
            RefreshTreeView();
            ElementProperties.SelectedObject = ElementProperties.SelectedObject;
            ShowErrors();
        }

        private void ShowErrors()
        {
            IList<ElementError> allErrors = root.GetErrorsRecursive();
            DataTable dt = new DataTable();
            dt.Columns.Add("Image", typeof(Image));
            dt.Columns.Add("OwnerType", typeof(string));
            dt.Columns.Add("Owner", typeof(string));            
            dt.Columns.Add("Message", typeof(string));
            dt.Columns.Add("Item", typeof(Element));
            ErrorGrid.AutoGenerateColumns = false;
            foreach (ElementError error in allErrors)
            {
                dt.Rows.Add(error.Owner.GetIcon (), error.Owner.GetType().GetElementName(), error.Owner.GetDisplayName (), error.Message,error.Owner);
            }
            ErrorGrid.DataSource = dt;
        }

        private void RefreshTreeView()
        {
            UpdateNode(ProjectTree.Nodes[0]);
        }

        private void UpdateNode(TreeNode node)
        {
            Element element = (Element)node.Tag;
            if (element.GetDisplayName () != node.Text)
                node.Text = element.GetDisplayName();

            ApplyImage(node);
            ApplyErrors(node);

            foreach (TreeNode childNode in node.Nodes)
            {
                UpdateNode(childNode);
            }
        }

        private static void ApplyErrors(TreeNode node)
        {
            Element element = node.GetElement ();
            IList<ElementError> allErrors = element.GetErrorsRecursive();
            string message = "";
            foreach (ElementError error in allErrors)
            {
                message += error.Message + "\r\n";
            }
            if (message.Length > 0)
                message = message.Substring(0, message.Length - 2);

            node.ToolTipText = message;
        }

        private void FillTree(Element element,TreeNode parentNode)
        {
            TreeNode node = new TreeNode();
            node.Text = element.GetDisplayName ();
            node.Tag = element;

            ApplyImage(node);
            ApplyErrors(node);

            parentNode.Nodes.Add(node);

            element.AllChildren                
                .OrderBy(childElement => childElement.Excluded)
                .ThenBy(childElement => childElement.GetSortPriority())
                .ThenBy(childElement => childElement.GetType().Name)                
                .ThenBy(childElement => childElement.GetDisplayName ())
                .ToList()
                .ForEach(childElement => FillTree(childElement, node));

            if (element.GetDefaultExpanded())
                node.Expand ();
            else
                node.Collapse ();
        }

        private void ApplyImage(TreeNode node)
        {
            Element selectedElement = node.GetElement();

            string imageKey = selectedElement.GetIconKey();
            if (!Icons.Images.ContainsKey(imageKey))
            {
                Image img = selectedElement.GetIcon ();
                Icons.Images.Add(imageKey, img);
            }
            if (node.ImageKey != imageKey)
            {
                node.ImageKey = imageKey;
                node.SelectedImageKey = imageKey;
            }
        }

        private void trvProject_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowSelectedElement();
        }

        private void ShowSelectedElement()
        {
            TreeNode selectedNode = ProjectTree.SelectedNode;
            if (selectedNode == null)
            {
                ElementProperties.SelectedObject = null;
                return;
            }

            Element currentElement =  selectedNode.GetElement ();
            if (currentElement == null)
                return;

            ElementProperties.SelectedObject = currentElement;
            SummaryTitleLabel.Text = currentElement.GetDisplayName();

            Element pathElement = currentElement;
            string path = "";
            while (pathElement != null)
            {
                path = pathElement.GetDisplayName () + "\\" + path;
                pathElement = pathElement.Parent;
            }
            SummaryPathLabel.Text = path;
            SummaryIcon.Image = currentElement.GetIcon();


            IList<Element> allChildren = currentElement.AllChildren;
            DataTable dt = new DataTable();
            dt.Columns.Add("Image", typeof(Image));
            dt.Columns.Add("OwnerType", typeof(string));
            dt.Columns.Add("Owner", typeof(string));            
            dt.Columns.Add("Item", typeof(Element));
            SummaryGridView.AutoGenerateColumns = false;
            foreach (Element child in allChildren)
            {
                dt.Rows.Add(child.GetIcon(), child.GetType().GetElementName(), child.GetDisplayName(), child);
            }
            SummaryGridView.DataSource = dt;
        }

        private void trvProject_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                ShowProjectContextMenu(e);
        }

        private void ShowProjectContextMenu(MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            
            TreeNode selectedNode = ProjectTree.GetNodeAt(p);            

            if (selectedNode == null)
                return;

            ProjectTree.SelectedNode = selectedNode;

            Element currentElement = (Element)selectedNode.Tag;
            IList<Type> childTypes = Engine.GetChildTypes(currentElement.GetType());
            ProjectContextMenu.Items.Clear();
            ToolStripLabel addNewLabel = new ToolStripLabel("Elements:");
            addNewLabel.Font = BoldFont.Font;
            ProjectContextMenu.Items.Add (addNewLabel);
            foreach (Type childType in childTypes)
            {
                bool allowNew = true;
                AllowMultipleAttribute allowMultipleAttrib = childType.GetAttribute<AllowMultipleAttribute>();
                if (allowMultipleAttrib != null && allowMultipleAttrib.Allow == false)
                {
                    foreach (Element childElement in currentElement.AllChildren)
                    {
                        if (childElement.GetType() == childType)
                        {
                            allowNew = false;
                            break;
                        }
                    }
                }
                string itemText = string.Format("Add {0}", childType.GetElementName());
                ToolStripMenuItem item = new ToolStripMenuItem(itemText);
                item.Tag = childType;
                item.Click += new EventHandler(NewElement_Click);
                item.Enabled = allowNew;
                item.Image = childType.GetElementIcon();
                if (allowNew)
                {
                }
                else
                {
                    item.ToolTipText = "Only one instance of this item is allowed";
                }
                ProjectContextMenu.Items.Add(item);
            }


            ToolStripSeparator separator1 = new ToolStripSeparator();
            ProjectContextMenu.Items.Add(separator1);


            ToolStripLabel verbLabel = new ToolStripLabel("Verbs:");
            verbLabel.Font = BoldFont.Font;
            ProjectContextMenu.Items.Add(verbLabel);

            List<MethodInfo> elementVerbs = currentElement.GetType().GetElementVerbs();
            foreach (MethodInfo method in elementVerbs)
            {
                string verbName = method.GetVerbName();
                ToolStripMenuItem item = new ToolStripMenuItem(verbName);
                item.Tag = method;
                item.Click += new EventHandler(ElementVerb_Click);
                ProjectContextMenu.Items.Add(item);

            }
            

            ProjectContextMenu.Show(ProjectTree, e.Location);
        }

        void ElementVerb_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = ProjectTree.SelectedNode;
            Element currentElement = (Element)selectedNode.Tag;

            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            MethodInfo method = item.Tag as MethodInfo;

            InvokeVerb(currentElement, method);
        }

        private void InvokeVerb(Element currentElement, MethodInfo method)
        {
            method.Invoke(currentElement, new object[]{this});
        }
            

        private void NewElement_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = ProjectTree.SelectedNode;
            Element currentElement = (Element)selectedNode.Tag;

            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            Type childType = (Type)item.Tag;
            Element newElement = (Element)Activator.CreateInstance(childType);
            currentElement.AddChild(newElement);
            if (newElement is NamedElement)
            {
                ((NamedElement)newElement).Name = string.Format("New {0}", childType.GetElementName());
            }
            TreeNode newNode = new TreeNode(newElement.GetDisplayName());
            
            
            newNode.Tag = newElement;            
            selectedNode.Nodes.Add(newNode);
            selectedNode.Expand();
            ProjectTree.SelectedNode = newNode;
            ApplyImage(newNode);
            ApplyErrors(newNode);

            
            if (newElement is NamedElement)
            {
                newNode.BeginEdit();
            }

        }

        private void FillTreeView()
        {
            ProjectTree.Nodes.Clear();
            TreeNode rootParentNode = new TreeNode();
            FillTree(root, rootParentNode);

            ProjectTree.Nodes.Add(rootParentNode.Nodes[0]);
            
        }

        private void ProjectTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.CancelEdit)
                return;

            if (e.Label == null)
                return;

            TreeNode selectedNode = ProjectTree.SelectedNode;
            NamedElement currentElement = selectedNode.GetElement () as NamedElement;
            if (currentElement == null)
                return;

            currentElement.Name = e.Label;
        }

        private void RefreshProjectTreeButton_Click(object sender, EventArgs e)
        {
            FillTreeView();
        }

        private void ProjectTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedElement();
                
            }
        }

        private void DeleteSelectedElement()
        {
            TreeNode selectedNode = ProjectTree.SelectedNode;
            Element currentElement = selectedNode.GetElement();
            if (!currentElement.AllowDelete())
                return; //ignore

            currentElement.Parent.RemoveChild(currentElement);
            TreeNode parentNode = selectedNode.Parent;
            selectedNode.Parent.Nodes.Remove(selectedNode);
            UpdateNode(parentNode);
        }

        private void ProjectTree_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void MainMenuFileSaveProject_Click(object sender, EventArgs e)
        {

            SaveProject();

            //SerializerEngine se = new SerializerEngine();
            //using (FileStream fs = new FileStream(@"c:\labb.txt", FileMode.Create))
            //{
            //    se.Serialize(fs, root);
            //    fs.Flush();
            //}

        }

        private void SaveProject()
        {
            FileStream fs = new FileStream("c:\\productobjectsoapformatted.Data", FileMode.Create);
            BinaryFormatter sf = new BinaryFormatter();
            sf.AssemblyFormat = FormatterAssemblyStyle.Simple;
            sf.FilterLevel = TypeFilterLevel.Full;
            sf.TypeFormat = FormatterTypeStyle.TypesAlways;
            sf.Serialize(fs, root);
            fs.Close();
        }

        private void MainMenuFileOpenProject_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenProject(OpenFileDialog.FileName);
            }
        }

        private void OpenProject(string fileName)
        {
            NewProject();
            FileStream fs = new FileStream(fileName, FileMode.Open);
            BinaryFormatter sf = new BinaryFormatter();
            sf.AssemblyFormat = FormatterAssemblyStyle.Simple;
            sf.FilterLevel = TypeFilterLevel.Full;
            sf.TypeFormat = FormatterTypeStyle.TypesAlways;
            root = (RootElement)sf.Deserialize(fs);
            fs.Close();
            FillTreeView();
            NotifyChange();    
       
        }

        private void NewProject()
        {
            root = new RootElement();
            FillTreeView();
            elementEditors = new Dictionary<Element, Dictionary<string, Control>>();
        }

        private void ProjectTree_DoubleClick(object sender, EventArgs e)
        {
            TreeNode selectedNode = ProjectTree.SelectedNode;
            if (selectedNode == null)
                return;

            Element currentElement = (Element)selectedNode.Tag;

            MethodInfo defaultVerb = currentElement.GetType().GetElementDefaultVerb();
            if (defaultVerb == null)
                return; //no default verb found

            InvokeVerb(currentElement, defaultVerb);
        }

        private void ErrorGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView rowView = (DataRowView)ErrorGrid.Rows[e.RowIndex].DataBoundItem;
            DataRow row = rowView.Row;
            Element errorElement = (Element)row["Item"];

            SelectElementInProjectTree(errorElement,ProjectTree.Nodes[0]);
        }

        private void SelectElementInProjectTree(Element errorElement,TreeNode node)
        {
            if (node.GetElement() == errorElement)
            {
                ProjectTree.SelectedNode = node;
                return;
            }
            foreach (TreeNode childNode in node.Nodes)
            {
                SelectElementInProjectTree(errorElement, childNode);
            }
        }       
    }
}
