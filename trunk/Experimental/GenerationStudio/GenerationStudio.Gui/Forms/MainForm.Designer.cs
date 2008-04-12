﻿namespace GenerationStudio.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node0");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ProjectTree = new System.Windows.Forms.TreeView();
            this.Icons = new System.Windows.Forms.ImageList(this.components);
            this.ProjectTopPanel = new System.Windows.Forms.Panel();
            this.ProjectToolStrip = new System.Windows.Forms.ToolStrip();
            this.ElementProperties = new System.Windows.Forms.PropertyGrid();
            this.ErrorPanel = new System.Windows.Forms.Panel();
            this.ErrorGrid = new System.Windows.Forms.DataGridView();
            this.OwnerTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OwnerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuViewSolutionExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuViewErrorList = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuViewProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.DockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.BoldFont = new System.Windows.Forms.Label();
            this.FontPanel = new System.Windows.Forms.Panel();
            this.NormalFont = new System.Windows.Forms.Label();
            this.ItalicFont = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ProjectPanel = new System.Windows.Forms.Panel();
            this.PropertyPanel = new System.Windows.Forms.Panel();
            this.DesignTimeContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileSaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshProjectTreeButton = new System.Windows.Forms.ToolStripButton();
            this.ProjectTopPanel.SuspendLayout();
            this.ProjectToolStrip.SuspendLayout();
            this.ErrorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGrid)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.FontPanel.SuspendLayout();
            this.ProjectPanel.SuspendLayout();
            this.PropertyPanel.SuspendLayout();
            this.DesignTimeContainer.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProjectTree
            // 
            this.ProjectTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectTree.HideSelection = false;
            this.ProjectTree.ImageIndex = 0;
            this.ProjectTree.ImageList = this.Icons;
            this.ProjectTree.LabelEdit = true;
            this.ProjectTree.Location = new System.Drawing.Point(0, 26);
            this.ProjectTree.Name = "ProjectTree";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Node0";
            this.ProjectTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.ProjectTree.SelectedImageIndex = 0;
            this.ProjectTree.ShowNodeToolTips = true;
            this.ProjectTree.Size = new System.Drawing.Size(171, 117);
            this.ProjectTree.TabIndex = 3;
            this.ProjectTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.ProjectTree_AfterLabelEdit);
            this.ProjectTree.DoubleClick += new System.EventHandler(this.ProjectTree_DoubleClick);
            this.ProjectTree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trvProject_MouseUp);
            this.ProjectTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvProject_AfterSelect);
            this.ProjectTree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProjectTree_KeyPress);
            this.ProjectTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProjectTree_KeyDown);
            // 
            // Icons
            // 
            this.Icons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.Icons.ImageSize = new System.Drawing.Size(16, 16);
            this.Icons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ProjectTopPanel
            // 
            this.ProjectTopPanel.Controls.Add(this.ProjectToolStrip);
            this.ProjectTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProjectTopPanel.Location = new System.Drawing.Point(0, 0);
            this.ProjectTopPanel.Name = "ProjectTopPanel";
            this.ProjectTopPanel.Size = new System.Drawing.Size(171, 26);
            this.ProjectTopPanel.TabIndex = 4;
            // 
            // ProjectToolStrip
            // 
            this.ProjectToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshProjectTreeButton});
            this.ProjectToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ProjectToolStrip.Name = "ProjectToolStrip";
            this.ProjectToolStrip.Size = new System.Drawing.Size(171, 25);
            this.ProjectToolStrip.TabIndex = 1;
            // 
            // ElementProperties
            // 
            this.ElementProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementProperties.Location = new System.Drawing.Point(0, 0);
            this.ElementProperties.Name = "ElementProperties";
            this.ElementProperties.Size = new System.Drawing.Size(171, 143);
            this.ElementProperties.TabIndex = 3;
            // 
            // ErrorPanel
            // 
            this.ErrorPanel.BackColor = System.Drawing.Color.Transparent;
            this.ErrorPanel.Controls.Add(this.ErrorGrid);
            this.ErrorPanel.Location = new System.Drawing.Point(3, 3);
            this.ErrorPanel.Name = "ErrorPanel";
            this.ErrorPanel.Size = new System.Drawing.Size(171, 143);
            this.ErrorPanel.TabIndex = 2;
            // 
            // ErrorGrid
            // 
            this.ErrorGrid.AllowUserToAddRows = false;
            this.ErrorGrid.AllowUserToDeleteRows = false;
            this.ErrorGrid.AllowUserToResizeRows = false;
            this.ErrorGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ErrorGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ErrorGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ErrorGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ErrorGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ErrorGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OwnerTypeColumn,
            this.OwnerColumn,
            this.MessageColumn});
            this.ErrorGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorGrid.GridColor = System.Drawing.SystemColors.Window;
            this.ErrorGrid.Location = new System.Drawing.Point(0, 0);
            this.ErrorGrid.Name = "ErrorGrid";
            this.ErrorGrid.ReadOnly = true;
            this.ErrorGrid.RowHeadersVisible = false;
            this.ErrorGrid.RowTemplate.Height = 18;
            this.ErrorGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ErrorGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ErrorGrid.Size = new System.Drawing.Size(171, 143);
            this.ErrorGrid.TabIndex = 0;
            this.ErrorGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ErrorGrid_CellDoubleClick);
            // 
            // OwnerTypeColumn
            // 
            this.OwnerTypeColumn.DataPropertyName = "OwnerType";
            this.OwnerTypeColumn.HeaderText = "Type";
            this.OwnerTypeColumn.Name = "OwnerTypeColumn";
            this.OwnerTypeColumn.ReadOnly = true;
            // 
            // OwnerColumn
            // 
            this.OwnerColumn.DataPropertyName = "Owner";
            this.OwnerColumn.HeaderText = "Element";
            this.OwnerColumn.Name = "OwnerColumn";
            this.OwnerColumn.ReadOnly = true;
            // 
            // MessageColumn
            // 
            this.MessageColumn.DataPropertyName = "Message";
            this.MessageColumn.HeaderText = "Message";
            this.MessageColumn.Name = "MessageColumn";
            this.MessageColumn.ReadOnly = true;
            // 
            // ProjectContextMenu
            // 
            this.ProjectContextMenu.Name = "mnuProjectContext";
            this.ProjectContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1033, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.MainMenuFileOpenProject,
            this.toolStripMenuItem1,
            this.MainMenuFileSaveProject});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(108, 6);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuViewSolutionExplorer,
            this.MainMenuViewErrorList,
            this.MainMenuViewProperties});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // MainMenuViewSolutionExplorer
            // 
            this.MainMenuViewSolutionExplorer.Name = "MainMenuViewSolutionExplorer";
            this.MainMenuViewSolutionExplorer.Size = new System.Drawing.Size(166, 22);
            this.MainMenuViewSolutionExplorer.Text = "Solution Explorer";
            // 
            // MainMenuViewErrorList
            // 
            this.MainMenuViewErrorList.Name = "MainMenuViewErrorList";
            this.MainMenuViewErrorList.Size = new System.Drawing.Size(166, 22);
            this.MainMenuViewErrorList.Text = "Error List";
            // 
            // MainMenuViewProperties
            // 
            this.MainMenuViewProperties.Name = "MainMenuViewProperties";
            this.MainMenuViewProperties.Size = new System.Drawing.Size(166, 22);
            this.MainMenuViewProperties.Text = "Properties";
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(1035, 700);
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 555);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.StatusBar.Size = new System.Drawing.Size(1033, 22);
            this.StatusBar.TabIndex = 0;
            // 
            // DockPanel
            // 
            this.DockPanel.ActiveAutoHideContent = null;
            this.DockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockPanel.Location = new System.Drawing.Point(0, 0);
            this.DockPanel.Name = "DockPanel";
            this.DockPanel.Size = new System.Drawing.Size(1033, 363);
            this.DockPanel.TabIndex = 0;
            // 
            // BoldFont
            // 
            this.BoldFont.AutoSize = true;
            this.BoldFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoldFont.Location = new System.Drawing.Point(16, 12);
            this.BoldFont.Name = "BoldFont";
            this.BoldFont.Size = new System.Drawing.Size(32, 13);
            this.BoldFont.TabIndex = 1;
            this.BoldFont.Text = "Bold";
            // 
            // FontPanel
            // 
            this.FontPanel.BackColor = System.Drawing.Color.Fuchsia;
            this.FontPanel.Controls.Add(this.NormalFont);
            this.FontPanel.Controls.Add(this.ItalicFont);
            this.FontPanel.Controls.Add(this.BoldFont);
            this.FontPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FontPanel.Location = new System.Drawing.Point(0, 363);
            this.FontPanel.Name = "FontPanel";
            this.FontPanel.Size = new System.Drawing.Size(1033, 34);
            this.FontPanel.TabIndex = 2;
            this.FontPanel.Visible = false;
            // 
            // NormalFont
            // 
            this.NormalFont.AutoSize = true;
            this.NormalFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NormalFont.Location = new System.Drawing.Point(104, 12);
            this.NormalFont.Name = "NormalFont";
            this.NormalFont.Size = new System.Drawing.Size(40, 13);
            this.NormalFont.TabIndex = 3;
            this.NormalFont.Text = "Normal";
            // 
            // ItalicFont
            // 
            this.ItalicFont.AutoSize = true;
            this.ItalicFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItalicFont.Location = new System.Drawing.Point(63, 12);
            this.ItalicFont.Name = "ItalicFont";
            this.ItalicFont.Size = new System.Drawing.Size(29, 13);
            this.ItalicFont.TabIndex = 2;
            this.ItalicFont.Text = "Italic";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // ProjectPanel
            // 
            this.ProjectPanel.BackColor = System.Drawing.Color.Transparent;
            this.ProjectPanel.Controls.Add(this.ProjectTree);
            this.ProjectPanel.Controls.Add(this.ProjectTopPanel);
            this.ProjectPanel.Location = new System.Drawing.Point(357, 3);
            this.ProjectPanel.Name = "ProjectPanel";
            this.ProjectPanel.Size = new System.Drawing.Size(171, 143);
            this.ProjectPanel.TabIndex = 1;
            // 
            // PropertyPanel
            // 
            this.PropertyPanel.BackColor = System.Drawing.Color.Transparent;
            this.PropertyPanel.Controls.Add(this.ElementProperties);
            this.PropertyPanel.Location = new System.Drawing.Point(180, 3);
            this.PropertyPanel.Name = "PropertyPanel";
            this.PropertyPanel.Size = new System.Drawing.Size(171, 143);
            this.PropertyPanel.TabIndex = 1;
            // 
            // DesignTimeContainer
            // 
            this.DesignTimeContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.DesignTimeContainer.Controls.Add(this.ErrorPanel);
            this.DesignTimeContainer.Controls.Add(this.PropertyPanel);
            this.DesignTimeContainer.Controls.Add(this.ProjectPanel);
            this.DesignTimeContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DesignTimeContainer.Location = new System.Drawing.Point(0, 397);
            this.DesignTimeContainer.Name = "DesignTimeContainer";
            this.DesignTimeContainer.Size = new System.Drawing.Size(1033, 158);
            this.DesignTimeContainer.TabIndex = 3;
            this.DesignTimeContainer.Visible = false;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(1033, 25);
            this.MainToolStrip.TabIndex = 5;
            this.MainToolStrip.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::GenerationStudio.Properties.Resources.newproject;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // MainMenuFileOpenProject
            // 
            this.MainMenuFileOpenProject.Image = global::GenerationStudio.Properties.Resources.open;
            this.MainMenuFileOpenProject.Name = "MainMenuFileOpenProject";
            this.MainMenuFileOpenProject.Size = new System.Drawing.Size(111, 22);
            this.MainMenuFileOpenProject.Text = "&Open";
            this.MainMenuFileOpenProject.Click += new System.EventHandler(this.MainMenuFileOpenProject_Click);
            // 
            // MainMenuFileSaveProject
            // 
            this.MainMenuFileSaveProject.Image = global::GenerationStudio.Properties.Resources.save;
            this.MainMenuFileSaveProject.Name = "MainMenuFileSaveProject";
            this.MainMenuFileSaveProject.Size = new System.Drawing.Size(111, 22);
            this.MainMenuFileSaveProject.Text = "&Save";
            this.MainMenuFileSaveProject.Click += new System.EventHandler(this.MainMenuFileSaveProject_Click);
            // 
            // RefreshProjectTreeButton
            // 
            this.RefreshProjectTreeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshProjectTreeButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshProjectTreeButton.Image")));
            this.RefreshProjectTreeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshProjectTreeButton.Name = "RefreshProjectTreeButton";
            this.RefreshProjectTreeButton.Size = new System.Drawing.Size(23, 22);
            this.RefreshProjectTreeButton.Text = "toolStripButton1";
            this.RefreshProjectTreeButton.ToolTipText = "Refresh project tree";
            this.RefreshProjectTreeButton.Click += new System.EventHandler(this.RefreshProjectTreeButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 577);
            this.Controls.Add(this.MainToolStrip);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.DockPanel);
            this.Controls.Add(this.FontPanel);
            this.Controls.Add(this.DesignTimeContainer);
            this.Controls.Add(this.StatusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Caramel - Code Generator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ProjectTopPanel.ResumeLayout(false);
            this.ProjectTopPanel.PerformLayout();
            this.ProjectToolStrip.ResumeLayout(false);
            this.ProjectToolStrip.PerformLayout();
            this.ErrorPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGrid)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.FontPanel.ResumeLayout(false);
            this.FontPanel.PerformLayout();
            this.ProjectPanel.ResumeLayout(false);
            this.PropertyPanel.ResumeLayout(false);
            this.DesignTimeContainer.ResumeLayout(false);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ProjectTree;
        private System.Windows.Forms.PropertyGrid ElementProperties;
        private System.Windows.Forms.ContextMenuStrip ProjectContextMenu;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileOpenProject;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileSaveProject;
        private System.Windows.Forms.ImageList Icons;
        private System.Windows.Forms.Panel FontPanel;
        private System.Windows.Forms.Label NormalFont;
        private System.Windows.Forms.Label ItalicFont;
        private System.Windows.Forms.Label BoldFont;
        private System.Windows.Forms.Panel ProjectTopPanel;
        private System.Windows.Forms.ToolStrip ProjectToolStrip;
        private System.Windows.Forms.ToolStripButton RefreshProjectTreeButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Panel ErrorPanel;
        private System.Windows.Forms.DataGridView ErrorGrid;
        private System.Windows.Forms.Panel ProjectPanel;
        private System.Windows.Forms.Panel PropertyPanel;
        private System.Windows.Forms.FlowLayoutPanel DesignTimeContainer;
        private WeifenLuo.WinFormsUI.Docking.DockPanel DockPanel;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenuViewSolutionExplorer;
        private System.Windows.Forms.ToolStripMenuItem MainMenuViewErrorList;
        private System.Windows.Forms.ToolStripMenuItem MainMenuViewProperties;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn OwnerTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OwnerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessageColumn;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}

