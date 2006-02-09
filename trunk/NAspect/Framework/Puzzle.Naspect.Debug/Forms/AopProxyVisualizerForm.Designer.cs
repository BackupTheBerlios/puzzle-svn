namespace Puzzle.Naspect.Debug.Forms
{
    partial class AopProxyVisualizerForm
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
            this.lstMethods = new System.Windows.Forms.ListBox();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.lstInterceptors = new System.Windows.Forms.ListBox();
            this.lblMethodName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picInterceptors = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInterceptors)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstMethods
            // 
            this.lstMethods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMethods.FormattingEnabled = true;
            this.lstMethods.IntegralHeight = false;
            this.lstMethods.Location = new System.Drawing.Point(10, 42);
            this.lstMethods.Name = "lstMethods";
            this.lstMethods.Size = new System.Drawing.Size(225, 294);
            this.lstMethods.TabIndex = 0;
            this.lstMethods.SelectedIndexChanged += new System.EventHandler(this.lstMethods_SelectedIndexChanged);
            // 
            // lblTypeName
            // 
            this.lblTypeName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTypeName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTypeName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTypeName.Location = new System.Drawing.Point(10, 10);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(225, 32);
            this.lblTypeName.TabIndex = 1;
            this.lblTypeName.Text = "label1";
            this.lblTypeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstInterceptors
            // 
            this.lstInterceptors.FormattingEnabled = true;
            this.lstInterceptors.Location = new System.Drawing.Point(898, 219);
            this.lstInterceptors.Name = "lstInterceptors";
            this.lstInterceptors.Size = new System.Drawing.Size(71, 43);
            this.lstInterceptors.TabIndex = 2;
            // 
            // lblMethodName
            // 
            this.lblMethodName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblMethodName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMethodName.Location = new System.Drawing.Point(898, 187);
            this.lblMethodName.Name = "lblMethodName";
            this.lblMethodName.Size = new System.Drawing.Size(71, 32);
            this.lblMethodName.TabIndex = 3;
            this.lblMethodName.Text = "label1";
            this.lblMethodName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picInterceptors);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 340);
            this.panel1.TabIndex = 4;
            // 
            // picInterceptors
            // 
            this.picInterceptors.Image = global::Puzzle.Naspect.Debug.Properties.Resources.interceptor;
            this.picInterceptors.Location = new System.Drawing.Point(3, 3);
            this.picInterceptors.Name = "picInterceptors";
            this.picInterceptors.Size = new System.Drawing.Size(330, 462);
            this.picInterceptors.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picInterceptors.TabIndex = 0;
            this.picInterceptors.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstMethods);
            this.splitContainer1.Panel1.Controls.Add(this.lblTypeName);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Size = new System.Drawing.Size(795, 346);
            this.splitContainer1.SplitterDistance = 245;
            this.splitContainer1.TabIndex = 5;
            // 
            // AopProxyVisualizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 346);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblMethodName);
            this.Controls.Add(this.lstInterceptors);
            this.Name = "AopProxyVisualizerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AopProxyVisualizerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AopProxyVisualizerForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInterceptors)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstMethods;
        private System.Windows.Forms.Label lblTypeName;
        private System.Windows.Forms.ListBox lstInterceptors;
        private System.Windows.Forms.Label lblMethodName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picInterceptors;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}