namespace Puzzle.NAspect.Visualization
{
    partial class VisualizeProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualizeProjectForm));
            this.assemblyPathTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.configPathTextBox = new System.Windows.Forms.TextBox();
            this.aspectAssemblyPathTextBox = new System.Windows.Forms.TextBox();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.aspectTargetContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeAspectTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aspectContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aspectTargetContextMenuStrip.SuspendLayout();
            this.aspectContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // assemblyPathTextBox
            // 
            this.assemblyPathTextBox.Location = new System.Drawing.Point(12, 12);
            this.assemblyPathTextBox.Name = "assemblyPathTextBox";
            this.assemblyPathTextBox.Size = new System.Drawing.Size(458, 20);
            this.assemblyPathTextBox.TabIndex = 0;
            this.assemblyPathTextBox.Text = "C:\\Berlioz\\Puzzle\\NPersist\\Framework\\bin\\Debug\\Puzzle.NCore.Framework.dll";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(487, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(12, 167);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(285, 389);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "assembly.gif");
            this.imageList1.Images.SetKeyName(1, "class.gif");
            this.imageList1.Images.SetKeyName(2, "method2.gif");
            this.imageList1.Images.SetKeyName(3, "property.gif");
            this.imageList1.Images.SetKeyName(4, "aspect2.gif");
            this.imageList1.Images.SetKeyName(5, "mixin2.gif");
            this.imageList1.Images.SetKeyName(6, "pointcut2.gif");
            this.imageList1.Images.SetKeyName(7, "interceptor.gif");
            this.imageList1.Images.SetKeyName(8, "target2.gif");
            this.imageList1.Images.SetKeyName(9, "aspect_on_class.gif");
            this.imageList1.Images.SetKeyName(10, "intercepted_method.gif");
            // 
            // configPathTextBox
            // 
            this.configPathTextBox.Location = new System.Drawing.Point(12, 64);
            this.configPathTextBox.Name = "configPathTextBox";
            this.configPathTextBox.Size = new System.Drawing.Size(457, 20);
            this.configPathTextBox.TabIndex = 3;
            this.configPathTextBox.Text = "C:\\Berlioz\\Puzzle\\NAspect\\Visualization\\test.config";
            // 
            // aspectAssemblyPathTextBox
            // 
            this.aspectAssemblyPathTextBox.Location = new System.Drawing.Point(12, 38);
            this.aspectAssemblyPathTextBox.Name = "aspectAssemblyPathTextBox";
            this.aspectAssemblyPathTextBox.Size = new System.Drawing.Size(457, 20);
            this.aspectAssemblyPathTextBox.TabIndex = 4;
            this.aspectAssemblyPathTextBox.Text = "C:\\Berlioz\\Puzzle\\NAspect\\Samples\\CacheSample.NET 2.0\\bin\\Debug\\CacheSample.exe";
            // 
            // treeView2
            // 
            this.treeView2.ImageIndex = 0;
            this.treeView2.ImageList = this.imageList1;
            this.treeView2.Location = new System.Drawing.Point(314, 167);
            this.treeView2.Name = "treeView2";
            this.treeView2.SelectedImageIndex = 0;
            this.treeView2.Size = new System.Drawing.Size(285, 389);
            this.treeView2.TabIndex = 5;
            this.treeView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterSelect);
            this.treeView2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView2_MouseUp);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(831, 167);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(256, 389);
            this.propertyGrid1.TabIndex = 7;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // aspectTargetContextMenuStrip
            // 
            this.aspectTargetContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeAspectTargetToolStripMenuItem});
            this.aspectTargetContextMenuStrip.Name = "aspectTargetContextMenuStrip";
            this.aspectTargetContextMenuStrip.Size = new System.Drawing.Size(125, 26);
            // 
            // removeAspectTargetToolStripMenuItem
            // 
            this.removeAspectTargetToolStripMenuItem.Name = "removeAspectTargetToolStripMenuItem";
            this.removeAspectTargetToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.removeAspectTargetToolStripMenuItem.Text = "Remove";
            this.removeAspectTargetToolStripMenuItem.Click += new System.EventHandler(this.removeAspectTargetToolStripMenuItem_Click);
            // 
            // aspectContextMenuStrip
            // 
            this.aspectContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem});
            this.aspectContextMenuStrip.Name = "aspectContextMenuStrip";
            this.aspectContextMenuStrip.Size = new System.Drawing.Size(105, 26);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTargetToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // addTargetToolStripMenuItem
            // 
            this.addTargetToolStripMenuItem.Name = "addTargetToolStripMenuItem";
            this.addTargetToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addTargetToolStripMenuItem.Text = "Target";
            this.addTargetToolStripMenuItem.Click += new System.EventHandler(this.addTargetToolStripMenuItem_Click);
            // 
            // VisualizeProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 568);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.treeView2);
            this.Controls.Add(this.aspectAssemblyPathTextBox);
            this.Controls.Add(this.configPathTextBox);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.assemblyPathTextBox);
            this.Name = "VisualizeProjectForm";
            this.Text = "VisualizeProjectForm";
            this.aspectTargetContextMenuStrip.ResumeLayout(false);
            this.aspectContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox assemblyPathTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox configPathTextBox;
        private System.Windows.Forms.TextBox aspectAssemblyPathTextBox;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ContextMenuStrip aspectTargetContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem removeAspectTargetToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip aspectContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTargetToolStripMenuItem;
    }
}