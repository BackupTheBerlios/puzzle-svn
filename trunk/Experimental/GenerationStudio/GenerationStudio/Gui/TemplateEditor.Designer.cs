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
            this.TemplateSyntaxBox = new Puzzle.Windows.Forms.SyntaxBoxControl();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.OpenButton = new System.Windows.Forms.ToolStripButton();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExecuteButton = new System.Windows.Forms.ToolStripButton();
            this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.TemplateTabPage = new System.Windows.Forms.TabPage();
            this.CompiledTabPage = new System.Windows.Forms.TabPage();
            this.OutputTabPage = new System.Windows.Forms.TabPage();
            this.SourceSyntaxBox = new Puzzle.Windows.Forms.SyntaxBoxControl();
            this.OutputSyntaxBox = new Puzzle.Windows.Forms.SyntaxBoxControl();
            this.MainToolStrip.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.TemplateTabPage.SuspendLayout();
            this.CompiledTabPage.SuspendLayout();
            this.OutputTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // TemplateSyntaxBox
            // 
            this.TemplateSyntaxBox.ActiveView = Puzzle.Windows.Forms.ActiveView.BottomRight;
            this.TemplateSyntaxBox.AutoListPosition = null;
            this.TemplateSyntaxBox.AutoListSelectedText = "a123";
            this.TemplateSyntaxBox.AutoListVisible = false;
            this.TemplateSyntaxBox.BackColor = System.Drawing.Color.White;
            this.TemplateSyntaxBox.BorderStyle = Puzzle.Windows.Forms.BorderStyle.None;
            this.TemplateSyntaxBox.CopyAsRTF = false;
            this.TemplateSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TemplateSyntaxBox.FontName = "Courier new";
            this.TemplateSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TemplateSyntaxBox.InfoTipCount = 1;
            this.TemplateSyntaxBox.InfoTipPosition = null;
            this.TemplateSyntaxBox.InfoTipSelectedIndex = 1;
            this.TemplateSyntaxBox.InfoTipVisible = false;
            this.TemplateSyntaxBox.Location = new System.Drawing.Point(3, 3);
            this.TemplateSyntaxBox.LockCursorUpdate = false;
            this.TemplateSyntaxBox.Name = "TemplateSyntaxBox";
            this.TemplateSyntaxBox.ShowScopeIndicator = false;
            this.TemplateSyntaxBox.Size = new System.Drawing.Size(339, 328);
            this.TemplateSyntaxBox.SmoothScroll = false;
            this.TemplateSyntaxBox.SplitviewH = -4;
            this.TemplateSyntaxBox.SplitviewV = -4;
            this.TemplateSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.TemplateSyntaxBox.TabIndex = 4;
            this.TemplateSyntaxBox.Text = "syntaxBoxControl1";
            this.TemplateSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenButton,
            this.SaveButton,
            this.toolStripSeparator1,
            this.ExecuteButton});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(353, 25);
            this.MainToolStrip.TabIndex = 3;
            this.MainToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MainToolStrip_ItemClicked);
            // 
            // OpenButton
            // 
            this.OpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenButton.Image")));
            this.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(23, 22);
            this.OpenButton.Text = "toolStripButton2";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(23, 22);
            this.SaveButton.Text = "toolStripButton1";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExecuteButton.Image = ((System.Drawing.Image)(resources.GetObject("ExecuteButton.Image")));
            this.ExecuteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(23, 22);
            this.ExecuteButton.Text = "toolStripButton1";
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // OpenDialog
            // 
            this.OpenDialog.FileName = "openFileDialog1";
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.TemplateTabPage);
            this.Tabs.Controls.Add(this.CompiledTabPage);
            this.Tabs.Controls.Add(this.OutputTabPage);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 25);
            this.Tabs.Multiline = true;
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(353, 360);
            this.Tabs.TabIndex = 5;
            // 
            // TemplateTabPage
            // 
            this.TemplateTabPage.Controls.Add(this.TemplateSyntaxBox);
            this.TemplateTabPage.Location = new System.Drawing.Point(4, 22);
            this.TemplateTabPage.Name = "TemplateTabPage";
            this.TemplateTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TemplateTabPage.Size = new System.Drawing.Size(345, 334);
            this.TemplateTabPage.TabIndex = 0;
            this.TemplateTabPage.Text = "Template";
            this.TemplateTabPage.UseVisualStyleBackColor = true;
            // 
            // CompiledTabPage
            // 
            this.CompiledTabPage.Controls.Add(this.SourceSyntaxBox);
            this.CompiledTabPage.Location = new System.Drawing.Point(4, 22);
            this.CompiledTabPage.Name = "CompiledTabPage";
            this.CompiledTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.CompiledTabPage.Size = new System.Drawing.Size(345, 334);
            this.CompiledTabPage.TabIndex = 1;
            this.CompiledTabPage.Text = "Source";
            this.CompiledTabPage.UseVisualStyleBackColor = true;
            // 
            // OutputTabPage
            // 
            this.OutputTabPage.Controls.Add(this.OutputSyntaxBox);
            this.OutputTabPage.Location = new System.Drawing.Point(4, 22);
            this.OutputTabPage.Name = "OutputTabPage";
            this.OutputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.OutputTabPage.Size = new System.Drawing.Size(345, 334);
            this.OutputTabPage.TabIndex = 2;
            this.OutputTabPage.Text = "Output";
            this.OutputTabPage.UseVisualStyleBackColor = true;
            // 
            // SourceSyntaxBox
            // 
            this.SourceSyntaxBox.ActiveView = Puzzle.Windows.Forms.ActiveView.BottomRight;
            this.SourceSyntaxBox.AutoListPosition = null;
            this.SourceSyntaxBox.AutoListSelectedText = "a123";
            this.SourceSyntaxBox.AutoListVisible = false;
            this.SourceSyntaxBox.BackColor = System.Drawing.Color.White;
            this.SourceSyntaxBox.BorderStyle = Puzzle.Windows.Forms.BorderStyle.None;
            this.SourceSyntaxBox.CopyAsRTF = false;
            this.SourceSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceSyntaxBox.FontName = "Courier new";
            this.SourceSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SourceSyntaxBox.InfoTipCount = 1;
            this.SourceSyntaxBox.InfoTipPosition = null;
            this.SourceSyntaxBox.InfoTipSelectedIndex = 1;
            this.SourceSyntaxBox.InfoTipVisible = false;
            this.SourceSyntaxBox.Location = new System.Drawing.Point(3, 3);
            this.SourceSyntaxBox.LockCursorUpdate = false;
            this.SourceSyntaxBox.Name = "SourceSyntaxBox";
            this.SourceSyntaxBox.ShowScopeIndicator = false;
            this.SourceSyntaxBox.Size = new System.Drawing.Size(339, 328);
            this.SourceSyntaxBox.SmoothScroll = false;
            this.SourceSyntaxBox.SplitviewH = -4;
            this.SourceSyntaxBox.SplitviewV = -4;
            this.SourceSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.SourceSyntaxBox.TabIndex = 5;
            this.SourceSyntaxBox.Text = "syntaxBoxControl1";
            this.SourceSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // OutputSyntaxBox
            // 
            this.OutputSyntaxBox.ActiveView = Puzzle.Windows.Forms.ActiveView.BottomRight;
            this.OutputSyntaxBox.AutoListPosition = null;
            this.OutputSyntaxBox.AutoListSelectedText = "a123";
            this.OutputSyntaxBox.AutoListVisible = false;
            this.OutputSyntaxBox.BackColor = System.Drawing.Color.White;
            this.OutputSyntaxBox.BorderStyle = Puzzle.Windows.Forms.BorderStyle.None;
            this.OutputSyntaxBox.CopyAsRTF = false;
            this.OutputSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputSyntaxBox.FontName = "Courier new";
            this.OutputSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OutputSyntaxBox.InfoTipCount = 1;
            this.OutputSyntaxBox.InfoTipPosition = null;
            this.OutputSyntaxBox.InfoTipSelectedIndex = 1;
            this.OutputSyntaxBox.InfoTipVisible = false;
            this.OutputSyntaxBox.Location = new System.Drawing.Point(3, 3);
            this.OutputSyntaxBox.LockCursorUpdate = false;
            this.OutputSyntaxBox.Name = "OutputSyntaxBox";
            this.OutputSyntaxBox.ShowScopeIndicator = false;
            this.OutputSyntaxBox.Size = new System.Drawing.Size(339, 328);
            this.OutputSyntaxBox.SmoothScroll = false;
            this.OutputSyntaxBox.SplitviewH = -4;
            this.OutputSyntaxBox.SplitviewV = -4;
            this.OutputSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.OutputSyntaxBox.TabIndex = 5;
            this.OutputSyntaxBox.Text = "syntaxBoxControl1";
            this.OutputSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // TemplateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.MainToolStrip);
            this.Name = "TemplateEditor";
            this.Size = new System.Drawing.Size(353, 385);
            this.Load += new System.EventHandler(this.TemplateEditor_Load);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.Tabs.ResumeLayout(false);
            this.TemplateTabPage.ResumeLayout(false);
            this.CompiledTabPage.ResumeLayout(false);
            this.OutputTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Puzzle.Windows.Forms.SyntaxBoxControl TemplateSyntaxBox;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.ToolStripButton OpenButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ExecuteButton;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
        private System.Windows.Forms.SaveFileDialog SaveDialog;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage TemplateTabPage;
        private System.Windows.Forms.TabPage CompiledTabPage;
        private System.Windows.Forms.TabPage OutputTabPage;
        private Puzzle.Windows.Forms.SyntaxBoxControl SourceSyntaxBox;
        private Puzzle.Windows.Forms.SyntaxBoxControl OutputSyntaxBox;


    }
}
