using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;
using System.Collections;
using Puzzle.NAspect.Framework.ConfigurationElements;
using Puzzle.NAspect.Visualization.Nodes;
using Puzzle.NAspect.Visualization.PropertyHolders;
using Puzzle.NAspect.Visualization.Presentation;

namespace Puzzle.NAspect.Visualization
{
    public partial class VisualizeProjectForm : Form
    {
        public VisualizeProjectForm()
        {
            InitializeComponent();
        }

        #region Private fields

        private PresentationModel model = null;

        private AspectMatcher aspectMatcher = new AspectMatcher();
        private PointcutMatcher pointcutMatcher = new PointcutMatcher();

        private IList assemblies = new ArrayList();

        private object selected = null;
        private object selectedParent = null;

        #endregion

        #region Setup

        private void SetupProject()
        {            
            IEngine engine = EngineFactory.FromFile(configPathTextBox.Text, true);
            model = PresentationModelManager.CreatePresentationModel(engine);
        }

        private void SetupProjectTreeView()
        {
            try
            {
                Assembly asm = Assembly.LoadFile(assemblyPathTextBox.Text);                
                assemblies.Add(asm);

                SetupProject();

                TreeViewManager.SetupProjectTreeView(treeView1, assemblies, model, aspectMatcher, pointcutMatcher);
                TreeViewManager.SetupAspectTreeView(treeView2, assemblies, model, aspectMatcher, pointcutMatcher);
            }
            catch (Exception ex)
            {
                ;
            }
        }

        #endregion

        #region Misc

        private void button1_Click(object sender, EventArgs e)
        {
            SetupProjectTreeView();
        }

        #endregion

        #region Refresh

        private void RefreshAll()
        {
            RefreshTreeViews();
        }

        private void RefreshTreeViews()
        {
            TreeViewManager.RefreshTreeView(treeView1);
            TreeViewManager.RefreshTreeView(treeView2);
        }

        #endregion

        #region Selecting

        private void SelectNode(TreeViewEventArgs e)
        {
            AspectNode aspectNode = e.Node as AspectNode;
            if (aspectNode != null)
                propertyGrid1.SelectedObject = new AspectProperties(aspectNode.Aspect);

            AspectTargetNode aspectTargetNode = e.Node as AspectTargetNode;
            if (aspectTargetNode != null)
                propertyGrid1.SelectedObject = new AspectTargetProperties(aspectTargetNode.Target);

            MixinNode mixinNode = e.Node as MixinNode;
            if (mixinNode != null)
                propertyGrid1.SelectedObject = new MixinProperties(mixinNode.Mixin);

            PointcutNode pointcutNode = e.Node as PointcutNode;
            if (pointcutNode != null)
                propertyGrid1.SelectedObject = new PointcutProperties(pointcutNode.Pointcut);

            PointcutTargetNode pointcutTargetNode = e.Node as PointcutTargetNode;
            if (pointcutTargetNode != null)
                propertyGrid1.SelectedObject = new PointcutTargetProperties(pointcutTargetNode.Target);

            InterceptorNode interceptorNode = e.Node as InterceptorNode;
            if (interceptorNode != null)
                propertyGrid1.SelectedObject = new InterceptorProperties(interceptorNode.Interceptor);

            TypeNode typeNode = e.Node as TypeNode;
            if (typeNode != null)
                propertyGrid1.SelectedObject = new TypeProperties(typeNode.Type);

        }

        #endregion

        #region Actions

        private void RemoveAspectTarget(PresentationAspectTarget aspectTarget)
        {
            aspectTarget.Aspect.Targets.Remove(aspectTarget);
            RefreshAll();
        }

        private void AddAspectTarget(PresentationAspect aspect)
        {
            AspectTarget aspectTarget = new AspectTarget("", AspectTargetType.Signature);
            aspect.Targets.Add(aspectTarget);
            RefreshAll();
        }

        #endregion

        #region EventHandlers

        #region PropertyGrid

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            RefreshAll();
        }

        #endregion

        #region TreeViews

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectNode(e);
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectNode(e);
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            TreeViewMouseUp(treeView1, e);
        }

        private void treeView2_MouseUp(object sender, MouseEventArgs e)
        {
            TreeViewMouseUp(treeView1, e);
        }

        #endregion

        #region Context Menus

        private void removeAspectTargetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PresentationAspectTarget aspectTarget = selected as PresentationAspectTarget;
            if (aspectTarget != null)
            {
                RemoveAspectTarget(aspectTarget);
            }
        }

        private void addTargetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PresentationAspect aspect = selected as PresentationAspect;
            if (aspect != null)
            {
                AddAspectTarget(aspect);
            }
        }

        #endregion

        #endregion

        #region Event Helpers

        #region TreeView

        private void TreeViewMouseUp(TreeView treeView, MouseEventArgs e)
        {
            TreeNode onNode = treeView.GetNodeAt(new Point(e.X, e.Y));

            if (onNode != null)
            {
                if (treeView.SelectedNode != onNode)
                    treeView.SelectedNode = onNode;

                if (e.Button == MouseButtons.Right)
                {
                    NodeBase nodeBase = onNode as NodeBase;
                    selected = nodeBase.Object;

                    if (onNode.Parent != null)
                    {
                        NodeBase nodeBaseParent = onNode.Parent as NodeBase;
                        selectedParent = nodeBaseParent.Object;
                    }

                    if (onNode is AspectTargetNode)
                        aspectTargetContextMenuStrip.Show(treeView, new Point(e.X, e.Y));

                    if (onNode is AspectNode)
                        aspectContextMenuStrip.Show(treeView, new Point(e.X, e.Y));

                }
            }
            else
            {
                selected = null;
            }

        }

        #endregion


        #endregion
    }
}