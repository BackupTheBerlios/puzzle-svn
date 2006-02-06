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
            this.SuspendLayout();
            // 
            // lstMethods
            // 
            this.lstMethods.FormattingEnabled = true;
            this.lstMethods.Location = new System.Drawing.Point(12, 41);
            this.lstMethods.Name = "lstMethods";
            this.lstMethods.Size = new System.Drawing.Size(243, 277);
            this.lstMethods.TabIndex = 0;
            this.lstMethods.SelectedIndexChanged += new System.EventHandler(this.lstMethods_SelectedIndexChanged);
            // 
            // lblTypeName
            // 
            this.lblTypeName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTypeName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTypeName.Location = new System.Drawing.Point(12, 9);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(243, 32);
            this.lblTypeName.TabIndex = 1;
            this.lblTypeName.Text = "label1";
            this.lblTypeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstInterceptors
            // 
            this.lstInterceptors.FormattingEnabled = true;
            this.lstInterceptors.Location = new System.Drawing.Point(311, 41);
            this.lstInterceptors.Name = "lstInterceptors";
            this.lstInterceptors.Size = new System.Drawing.Size(243, 277);
            this.lstInterceptors.TabIndex = 2;
            // 
            // lblMethodName
            // 
            this.lblMethodName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblMethodName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMethodName.Location = new System.Drawing.Point(311, 9);
            this.lblMethodName.Name = "lblMethodName";
            this.lblMethodName.Size = new System.Drawing.Size(243, 32);
            this.lblMethodName.TabIndex = 3;
            this.lblMethodName.Text = "label1";
            this.lblMethodName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AopProxyVisualizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 335);
            this.Controls.Add(this.lblMethodName);
            this.Controls.Add(this.lstInterceptors);
            this.Controls.Add(this.lblTypeName);
            this.Controls.Add(this.lstMethods);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AopProxyVisualizerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AopProxyVisualizerForm";
            this.Load += new System.EventHandler(this.AopProxyVisualizerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstMethods;
        private System.Windows.Forms.Label lblTypeName;
        private System.Windows.Forms.ListBox lstInterceptors;
        private System.Windows.Forms.Label lblMethodName;
    }
}