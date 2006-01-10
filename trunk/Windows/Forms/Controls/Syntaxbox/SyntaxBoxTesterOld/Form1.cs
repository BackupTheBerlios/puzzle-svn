using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SyntaxBoxTesterOld
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Puzzle.Windows.Forms.SyntaxBoxControl syntaxBoxControl1;
		private Puzzle.SourceCode.SyntaxDocument syntaxDocument1;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.syntaxBoxControl1 = new Puzzle.Windows.Forms.SyntaxBoxControl();
			this.syntaxDocument1 = new Puzzle.SourceCode.SyntaxDocument(this.components);
			this.SuspendLayout();
			// 
			// syntaxBoxControl1
			// 
			this.syntaxBoxControl1.ActiveView = Puzzle.Windows.Forms.SyntaxBox.ActiveView.BottomRight;
			this.syntaxBoxControl1.AutoListPosition = null;
			this.syntaxBoxControl1.AutoListSelectedText = "";
			this.syntaxBoxControl1.AutoListVisible = false;
			this.syntaxBoxControl1.CopyAsRTF = false;
			this.syntaxBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.syntaxBoxControl1.Document = this.syntaxDocument1;
			this.syntaxBoxControl1.FontName = "V";
			this.syntaxBoxControl1.InfoTipCount = 1;
			this.syntaxBoxControl1.InfoTipPosition = null;
			this.syntaxBoxControl1.InfoTipSelectedIndex = 0;
			this.syntaxBoxControl1.InfoTipVisible = false;
			this.syntaxBoxControl1.Location = new System.Drawing.Point(0, 0);
			this.syntaxBoxControl1.LockCursorUpdate = false;
			this.syntaxBoxControl1.Name = "syntaxBoxControl1";
			this.syntaxBoxControl1.Size = new System.Drawing.Size(480, 334);
			this.syntaxBoxControl1.SmoothScroll = false;
			this.syntaxBoxControl1.SplitviewH = -4;
			this.syntaxBoxControl1.SplitviewV = -4;
			this.syntaxBoxControl1.TabGuideColor = System.Drawing.Color.FromArgb(((System.Byte)(244)), ((System.Byte)(243)), ((System.Byte)(234)));
			this.syntaxBoxControl1.TabIndex = 0;
			this.syntaxBoxControl1.Text = "syntaxBoxControl1";
			this.syntaxBoxControl1.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			// 
			// syntaxDocument1
			// 
			this.syntaxDocument1.Lines = new string[] {
														  "using System;\r",
														  "using System.Drawing;\r",
														  "using System.Collections;\r",
														  "using System.ComponentModel;\r",
														  "using System.Windows.Forms;\r",
														  "using System.Data;\r",
														  "\r",
														  "namespace SyntaxBoxTesterOld\r",
														  "{\r",
														  "\t/// <summary>\r",
														  "\t/// Summary description for Form1.\r",
														  "\t/// </summary>\r",
														  "\tpublic class Form1 : System.Windows.Forms.Form\r",
														  "\t{\r",
														  "\t\tprivate Puzzle.Windows.Forms.SyntaxBoxControl syntaxBoxControl1;\r",
														  "\t\tprivate Puzzle.SourceCode.SyntaxDocument syntaxDocument1;\r",
														  "\t\tprivate System.ComponentModel.IContainer components;\r",
														  "\r",
														  "\t\tpublic Form1()\r",
														  "\t\t{\r",
														  "\t\t\t//\r",
														  "\t\t\t// Required for Windows Form Designer support\r",
														  "\t\t\t//\r",
														  "\t\t\tInitializeComponent();\r",
														  "\r",
														  "\t\t\t//\r",
														  "\t\t\t// TODO: Add any constructor code after InitializeComponent call\r",
														  "\t\t\t//\r",
														  "\t\t}\r",
														  "\r",
														  "\t\t/// <summary>\r",
														  "\t\t/// Clean up any resources being used.\r",
														  "\t\t/// </summary>\r",
														  "\t\tprotected override void Dispose( bool disposing )\r",
														  "\t\t{\r",
														  "\t\t\tif( disposing )\r",
														  "\t\t\t{\r",
														  "\t\t\t\tif (components != null) \r",
														  "\t\t\t\t{\r",
														  "\t\t\t\t\tcomponents.Dispose();\r",
														  "\t\t\t\t}\r",
														  "\t\t\t}\r",
														  "\t\t\tbase.Dispose( disposing );\r",
														  "\t\t}\r",
														  "\r",
														  "\t\t#region Windows Form Designer generated code\r",
														  "\t\t/// <summary>\r",
														  "\t\t/// Required method for Designer support - do not modify\r",
														  "\t\t/// the contents of this method with the code editor.\r",
														  "\t\t/// </summary>\r",
														  "\t\tprivate void InitializeComponent()\r",
														  "\t\t{\r",
														  "\t\t\tthis.components = new System.ComponentModel.Container();\r",
														  "\t\t\tthis.syntaxBoxControl1 = new Puzzle.Windows.Forms.SyntaxBoxControl();\r",
														  "\t\t\tthis.syntaxDocument1 = new Puzzle.SourceCode.SyntaxDocument(this.components);\r" +
														  "",
														  "\t\t\tthis.SuspendLayout();\r",
														  "\t\t\t// \r",
														  "\t\t\t// syntaxBoxControl1\r",
														  "\t\t\t// \r",
														  "\t\t\tthis.syntaxBoxControl1.ActiveView = Puzzle.Windows.Forms.SyntaxBox.ActiveView." +
														  "BottomRight;\r",
														  "\t\t\tthis.syntaxBoxControl1.AutoListPosition = null;\r",
														  "\t\t\tthis.syntaxBoxControl1.AutoListSelectedText = \"\";\r",
														  "\t\t\tthis.syntaxBoxControl1.AutoListVisible = false;\r",
														  "\t\t\tthis.syntaxBoxControl1.CopyAsRTF = false;\r",
														  "\t\t\tthis.syntaxBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;\r",
														  "\t\t\tthis.syntaxBoxControl1.Document = this.syntaxDocument1;\r",
														  "\t\t\tthis.syntaxBoxControl1.FontName = \"V\";\r",
														  "\t\t\tthis.syntaxBoxControl1.InfoTipCount = 1;\r",
														  "\t\t\tthis.syntaxBoxControl1.InfoTipPosition = null;\r",
														  "\t\t\tthis.syntaxBoxControl1.InfoTipSelectedIndex = 0;\r",
														  "\t\t\tthis.syntaxBoxControl1.InfoTipVisible = false;\r",
														  "\t\t\tthis.syntaxBoxControl1.Location = new System.Drawing.Point(0, 0);\r",
														  "\t\t\tthis.syntaxBoxControl1.LockCursorUpdate = false;\r",
														  "\t\t\tthis.syntaxBoxControl1.Name = \"syntaxBoxControl1\";\r",
														  "\t\t\tthis.syntaxBoxControl1.Size = new System.Drawing.Size(480, 334);\r",
														  "\t\t\tthis.syntaxBoxControl1.SmoothScroll = false;\r",
														  "\t\t\tthis.syntaxBoxControl1.SplitviewH = -4;\r",
														  "\t\t\tthis.syntaxBoxControl1.SplitviewV = -4;\r",
														  "\t\t\tthis.syntaxBoxControl1.TabGuideColor = System.Drawing.Color.FromArgb(((System." +
														  "Byte)(244)), ((System.Byte)(243)), ((System.Byte)(234)));\r",
														  "\t\t\tthis.syntaxBoxControl1.TabIndex = 0;\r",
														  "\t\t\tthis.syntaxBoxControl1.Text = \"syntaxBoxControl1\";\r",
														  "\t\t\tthis.syntaxBoxControl1.WhitespaceColor = System.Drawing.SystemColors.ControlDa" +
														  "rk;\r",
														  "\t\t\t// \r",
														  "\t\t\t// syntaxDocument1\r",
														  "\t\t\t// \r",
														  "\t\t\tthis.syntaxDocument1.Lines = new string[] {\r",
														  "\t\t\t\t\t\t\t\t\t\t\t\t\t\t  \"\"};\r",
														  "\t\t\tthis.syntaxDocument1.MaxUndoBufferSize = 1000;\r",
														  "\t\t\tthis.syntaxDocument1.Modified = false;\r",
														  "\t\t\tthis.syntaxDocument1.UndoStep = 0;\r",
														  "\t\t\t// \r",
														  "\t\t\t// Form1\r",
														  "\t\t\t// \r",
														  "\t\t\tthis.AutoScaleBaseSize = new System.Drawing.Size(5, 13);\r",
														  "\t\t\tthis.ClientSize = new System.Drawing.Size(480, 334);\r",
														  "\t\t\tthis.Controls.Add(this.syntaxBoxControl1);\r",
														  "\t\t\tthis.Name = \"Form1\";\r",
														  "\t\t\tthis.Text = \"Form1\";\r",
														  "\t\t\tthis.ResumeLayout(false);\r",
														  "\r",
														  "\t\t}\r",
														  "\t\t#endregion\r",
														  "\r",
														  "\t\t/// <summary>\r",
														  "\t\t/// The main entry point for the application.\r",
														  "\t\t/// </summary>\r",
														  "\t\t[STAThread]\r",
														  "\t\tstatic void Main() \r",
														  "\t\t{\r",
														  "\t\t\tApplication.Run(new Form1());\r",
														  "\t\t}\r",
														  "\t}\r",
														  "}"};
			this.syntaxDocument1.MaxUndoBufferSize = 1000;
			this.syntaxDocument1.Modified = false;
			this.syntaxDocument1.UndoStep = 0;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 334);
			this.Controls.Add(this.syntaxBoxControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
	}
}
