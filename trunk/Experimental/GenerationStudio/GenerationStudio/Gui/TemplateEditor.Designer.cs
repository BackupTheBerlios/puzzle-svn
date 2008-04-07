namespace GenerationStudio.Gui
{
    partial class TemplateEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateEditor));
            this.SyntaxBox = new Puzzle.Windows.Forms.SyntaxBoxControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SyntaxBox
            // 
            this.SyntaxBox.ActiveView = Puzzle.Windows.Forms.ActiveView.BottomRight;
            this.SyntaxBox.AutoListPosition = null;
            this.SyntaxBox.AutoListSelectedText = "a123";
            this.SyntaxBox.AutoListVisible = false;
            this.SyntaxBox.BackColor = System.Drawing.Color.White;
            this.SyntaxBox.BorderStyle = Puzzle.Windows.Forms.BorderStyle.None;
            this.SyntaxBox.CopyAsRTF = false;
            this.SyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SyntaxBox.FontName = "Courier new";
            this.SyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SyntaxBox.InfoTipCount = 1;
            this.SyntaxBox.InfoTipPosition = null;
            this.SyntaxBox.InfoTipSelectedIndex = 1;
            this.SyntaxBox.InfoTipVisible = false;
            this.SyntaxBox.Location = new System.Drawing.Point(0, 25);
            this.SyntaxBox.LockCursorUpdate = false;
            this.SyntaxBox.Name = "SyntaxBox";
            this.SyntaxBox.ShowScopeIndicator = false;
            this.SyntaxBox.Size = new System.Drawing.Size(353, 360);
            this.SyntaxBox.SmoothScroll = false;
            this.SyntaxBox.SplitviewH = -4;
            this.SyntaxBox.SplitviewV = -4;
            this.SyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.SyntaxBox.TabIndex = 4;
            this.SyntaxBox.Text = "syntaxBoxControl1";
            this.SyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(353, 25);
            this.toolStrip1.TabIndex = 3;
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
            // TemplateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SyntaxBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TemplateEditor";
            this.Size = new System.Drawing.Size(353, 385);
            this.Load += new System.EventHandler(this.TemplateEditor_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Puzzle.Windows.Forms.SyntaxBoxControl SyntaxBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;


    }
}
