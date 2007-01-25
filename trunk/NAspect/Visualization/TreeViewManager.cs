using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Puzzle.NAspect.Framework;
using System.Collections;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Visualization.Nodes;
using Puzzle.NAspect.Visualization.Sorting;
using Puzzle.NAspect.Visualization.Presentation;

namespace Puzzle.NAspect.Visualization
{
    public class TreeViewManager
    {

        public static void SetupProjectTreeView(TreeView treeView, IList assemblies, PresentationModel model, AspectMatcher aspectMatcher, PointcutMatcher pointcutMatcher)
        {
            treeView.Nodes.Clear();

            ArrayList sortedAssemblies = new ArrayList(assemblies);
            sortedAssemblies.Sort(new AssemblyComparer());
            foreach (Assembly asm in sortedAssemblies)
                treeView.Nodes.Add(new AssemblyNode(asm, model, aspectMatcher, pointcutMatcher));
        }

        public static void SetupAspectTreeView(TreeView treeView, IList assemblies, PresentationModel model, AspectMatcher aspectMatcher, PointcutMatcher pointcutMatcher)
        {
            treeView.Nodes.Clear();

            foreach (IGenericAspect aspect in model.Aspects)
                treeView.Nodes.Add(new AspectNode(aspect));
        }

        public static void RefreshTreeView(TreeView treeView)
        {
            treeView.BeginUpdate(); 
            foreach (NodeBase node in treeView.Nodes)
                node.Refresh();
            treeView.EndUpdate();
        }

        internal static NodeBase FindNodeByObject(TreeNodeCollection treeNodeCollection, object obj)
        {
            foreach (NodeBase node in treeNodeCollection)
                if (node.Object == obj)
                    return node;
            return null;
        }
    }
}
