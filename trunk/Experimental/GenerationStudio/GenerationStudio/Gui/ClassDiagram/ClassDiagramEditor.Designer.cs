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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.UmlToolbox = new System.Windows.Forms.ListBox();
            this.UmlDesigner = new AlbinoHorse.Windows.Forms.UmlDesigner();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.UmlToolbox);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Panel2.Controls.Add(this.UmlDesigner);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.splitContainer1.Size = new System.Drawing.Size(555, 515);
            this.splitContainer1.SplitterDistance = 90;
            this.splitContainer1.TabIndex = 5;
            // 
            // UmlToolbox
            // 
            this.UmlToolbox.BackColor = System.Drawing.SystemColors.Control;
            this.UmlToolbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UmlToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UmlToolbox.FormattingEnabled = true;
            this.UmlToolbox.Items.AddRange(new object[] {
            "Class",
            "Interface",
            "Enum",
            "Comment"});
            this.UmlToolbox.Location = new System.Drawing.Point(5, 5);
            this.UmlToolbox.Name = "UmlToolbox";
            this.UmlToolbox.Size = new System.Drawing.Size(80, 494);
            this.UmlToolbox.TabIndex = 0;
            this.UmlToolbox.DoubleClick += new System.EventHandler(this.UmlToolbox_DoubleClick);
            // 
            // UmlDesigner
            // 
            this.UmlDesigner.AllowDrop = true;
            this.UmlDesigner.BackColor = System.Drawing.Color.White;
            this.UmlDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UmlDesigner.GridSize = 21;
            this.UmlDesigner.Location = new System.Drawing.Point(1, 0);
            this.UmlDesigner.Name = "UmlDesigner";
            this.UmlDesigner.ShowGrid = false;
            this.UmlDesigner.Size = new System.Drawing.Size(460, 515);
            this.UmlDesigner.SnapToGrid = true;
            this.UmlDesigner.TabIndex = 0;
            this.UmlDesigner.Text = "umlDesigner1";
            this.UmlDesigner.Zoom = 1;
            this.UmlDesigner.DragOver += new System.Windows.Forms.DragEventHandler(this.UmlDesigner_DragOver);
            this.UmlDesigner.DragDrop += new System.Windows.Forms.DragEventHandler(this.UmlDesigner_DragDrop);
            // 
            // ClassDiagramEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ClassDiagramEditor";
            this.Size = new System.Drawing.Size(555, 515);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AlbinoHorse.Windows.Forms.UmlDesigner UmlDesigner;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox UmlToolbox;
    }
}
