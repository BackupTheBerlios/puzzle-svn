namespace GenerationStudio.Gui
{
    partial class ClassDiagramEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassDiagramEditor));
            this.UmlDesigner = new AlbinoHorse.Windows.Forms.UmlDesigner();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ClassButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // UmlDesigner
            // 
            this.UmlDesigner.BackColor = System.Drawing.Color.White;
            this.UmlDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UmlDesigner.GridSize = 21;
            this.UmlDesigner.Location = new System.Drawing.Point(0, 25);
            this.UmlDesigner.Name = "UmlDesigner";
            this.UmlDesigner.ShowGrid = false;
            this.UmlDesigner.Size = new System.Drawing.Size(555, 490);
            this.UmlDesigner.SnapToGrid = true;
            this.UmlDesigner.TabIndex = 0;
            this.UmlDesigner.Text = "umlDesigner1";
            this.UmlDesigner.Zoom = 1;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.SaveButton,
            this.toolStripSeparator2,
            this.ClassButton});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(555, 25);
            this.MainToolStrip.TabIndex = 4;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SaveButton
            // 
            this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(23, 22);
            this.SaveButton.Text = "toolStripButton1";
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
            // ClassButton
            // 
            this.ClassButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClassButton.Image = ((System.Drawing.Image)(resources.GetObject("ClassButton.Image")));
            this.ClassButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClassButton.Name = "ClassButton";
            this.ClassButton.Size = new System.Drawing.Size(23, 22);
            this.ClassButton.Text = "toolStripButton2";
            this.ClassButton.Click += new System.EventHandler(this.ClassButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ClassDiagramEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UmlDesigner);
            this.Controls.Add(this.MainToolStrip);
            this.Name = "ClassDiagramEditor";
            this.Size = new System.Drawing.Size(555, 515);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AlbinoHorse.Windows.Forms.UmlDesigner UmlDesigner;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ClassButton;
    }
}
