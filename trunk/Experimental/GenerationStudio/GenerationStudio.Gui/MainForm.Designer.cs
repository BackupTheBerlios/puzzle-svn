namespace GenerationStudio.Gui
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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node0");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ProjectTree = new System.Windows.Forms.TreeView();
            this.Icons = new System.Windows.Forms.ImageList(this.components);
            this.ProjectTopPanel = new System.Windows.Forms.Panel();
            this.ProjectToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshProjectTreeButton = new System.Windows.Forms.ToolStripButton();
            this.ElementProperties = new System.Windows.Forms.PropertyGrid();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.DocumentPanel = new System.Windows.Forms.Panel();
            this.ErrorPanel = new System.Windows.Forms.Panel();
            this.ErrorGrid = new System.Windows.Forms.DataGridView();
            this.OwnerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenuFileSaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.BoldFont = new System.Windows.Forms.Label();
            this.FontPanel = new System.Windows.Forms.Panel();
            this.NormalFont = new System.Windows.Forms.Label();
            this.ItalicFont = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.ProjectTopPanel.SuspendLayout();
            this.ProjectToolStrip.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.ErrorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGrid)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.ToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.ToolStripContainer.ContentPanel.SuspendLayout();
            this.ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.ToolStripContainer.SuspendLayout();
            this.FontPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.splitContainer1.Size = new System.Drawing.Size(742, 497);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(6, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ProjectTree);
            this.splitContainer2.Panel1.Controls.Add(this.ProjectTopPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ElementProperties);
            this.splitContainer2.Size = new System.Drawing.Size(186, 497);
            this.splitContainer2.SplitterDistance = 229;
            this.splitContainer2.TabIndex = 0;
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
            treeNode2.Name = "Node0";
            treeNode2.Text = "Node0";
            this.ProjectTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.ProjectTree.SelectedImageIndex = 0;
            this.ProjectTree.ShowNodeToolTips = true;
            this.ProjectTree.Size = new System.Drawing.Size(186, 203);
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
            this.ProjectTopPanel.Size = new System.Drawing.Size(186, 26);
            this.ProjectTopPanel.TabIndex = 4;
            // 
            // ProjectToolStrip
            // 
            this.ProjectToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshProjectTreeButton});
            this.ProjectToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ProjectToolStrip.Name = "ProjectToolStrip";
            this.ProjectToolStrip.Size = new System.Drawing.Size(186, 25);
            this.ProjectToolStrip.TabIndex = 1;
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
            // ElementProperties
            // 
            this.ElementProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementProperties.Location = new System.Drawing.Point(0, 0);
            this.ElementProperties.Name = "ElementProperties";
            this.ElementProperties.Size = new System.Drawing.Size(186, 264);
            this.ElementProperties.TabIndex = 3;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.DocumentPanel);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.ErrorPanel);
            this.splitContainer3.Size = new System.Drawing.Size(540, 497);
            this.splitContainer3.SplitterDistance = 384;
            this.splitContainer3.TabIndex = 0;
            // 
            // DocumentPanel
            // 
            this.DocumentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentPanel.Location = new System.Drawing.Point(0, 0);
            this.DocumentPanel.Name = "DocumentPanel";
            this.DocumentPanel.Size = new System.Drawing.Size(540, 384);
            this.DocumentPanel.TabIndex = 1;
            // 
            // ErrorPanel
            // 
            this.ErrorPanel.Controls.Add(this.ErrorGrid);
            this.ErrorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorPanel.Location = new System.Drawing.Point(0, 0);
            this.ErrorPanel.Name = "ErrorPanel";
            this.ErrorPanel.Size = new System.Drawing.Size(540, 109);
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
            this.ErrorGrid.Size = new System.Drawing.Size(540, 109);
            this.ErrorGrid.TabIndex = 0;
            this.ErrorGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ErrorGrid_CellDoubleClick);
            // 
            // OwnerColumn
            // 
            this.OwnerColumn.DataPropertyName = "Owner";
            this.OwnerColumn.HeaderText = "Owner";
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
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(742, 24);
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(108, 6);
            // 
            // MainMenuFileSaveProject
            // 
            this.MainMenuFileSaveProject.Image = global::GenerationStudio.Properties.Resources.save;
            this.MainMenuFileSaveProject.Name = "MainMenuFileSaveProject";
            this.MainMenuFileSaveProject.Size = new System.Drawing.Size(111, 22);
            this.MainMenuFileSaveProject.Text = "&Save";
            this.MainMenuFileSaveProject.Click += new System.EventHandler(this.MainMenuFileSaveProject_Click);
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
            // ToolStripContainer
            // 
            // 
            // ToolStripContainer.BottomToolStripPanel
            // 
            this.ToolStripContainer.BottomToolStripPanel.Controls.Add(this.StatusBar);
            // 
            // ToolStripContainer.ContentPanel
            // 
            this.ToolStripContainer.ContentPanel.Controls.Add(this.splitContainer1);
            this.ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(742, 497);
            this.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStripContainer.Location = new System.Drawing.Point(0, 34);
            this.ToolStripContainer.Name = "ToolStripContainer";
            this.ToolStripContainer.Size = new System.Drawing.Size(742, 543);
            this.ToolStripContainer.TabIndex = 0;
            this.ToolStripContainer.Text = "toolStripContainer1";
            // 
            // ToolStripContainer.TopToolStripPanel
            // 
            this.ToolStripContainer.TopToolStripPanel.Controls.Add(this.MainMenu);
            // 
            // StatusBar
            // 
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.StatusBar.Location = new System.Drawing.Point(0, 0);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(742, 22);
            this.StatusBar.TabIndex = 0;
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
            this.FontPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FontPanel.Location = new System.Drawing.Point(0, 0);
            this.FontPanel.Name = "FontPanel";
            this.FontPanel.Size = new System.Drawing.Size(742, 34);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 577);
            this.Controls.Add(this.ToolStripContainer);
            this.Controls.Add(this.FontPanel);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Caramel - Code Generator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ProjectTopPanel.ResumeLayout(false);
            this.ProjectTopPanel.PerformLayout();
            this.ProjectToolStrip.ResumeLayout(false);
            this.ProjectToolStrip.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ErrorPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGrid)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.ToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.ToolStripContainer.ContentPanel.ResumeLayout(false);
            this.ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.ToolStripContainer.TopToolStripPanel.PerformLayout();
            this.ToolStripContainer.ResumeLayout(false);
            this.ToolStripContainer.PerformLayout();
            this.FontPanel.ResumeLayout(false);
            this.FontPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView ProjectTree;
        private System.Windows.Forms.PropertyGrid ElementProperties;
        private System.Windows.Forms.ContextMenuStrip ProjectContextMenu;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripContainer ToolStripContainer;
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
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel DocumentPanel;
        private System.Windows.Forms.Panel ErrorPanel;
        private System.Windows.Forms.DataGridView ErrorGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn OwnerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessageColumn;
    }
}

