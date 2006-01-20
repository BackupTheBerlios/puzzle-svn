using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Puzzle.NPersist.Framework;
using QuickStart.Domain;

namespace QuickStart.WinGui
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox firstNameTextBox;
		private System.Windows.Forms.TextBox lastNameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button createAuthorButton;
		private System.Windows.Forms.TextBox idTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button fetchAuthorButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitializeContext();
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
				if (context != null)
				{
					context.Dispose();
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
			this.label1 = new System.Windows.Forms.Label();
			this.firstNameTextBox = new System.Windows.Forms.TextBox();
			this.lastNameTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.createAuthorButton = new System.Windows.Forms.Button();
			this.idTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.fetchAuthorButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "First Name";
			// 
			// firstNameTextBox
			// 
			this.firstNameTextBox.Location = new System.Drawing.Point(16, 32);
			this.firstNameTextBox.Name = "firstNameTextBox";
			this.firstNameTextBox.TabIndex = 1;
			this.firstNameTextBox.Text = "";
			// 
			// lastNameTextBox
			// 
			this.lastNameTextBox.Location = new System.Drawing.Point(16, 80);
			this.lastNameTextBox.Name = "lastNameTextBox";
			this.lastNameTextBox.TabIndex = 3;
			this.lastNameTextBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 64);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Last Name";
			// 
			// createAuthorButton
			// 
			this.createAuthorButton.Location = new System.Drawing.Point(16, 112);
			this.createAuthorButton.Name = "createAuthorButton";
			this.createAuthorButton.Size = new System.Drawing.Size(104, 23);
			this.createAuthorButton.TabIndex = 4;
			this.createAuthorButton.Text = "Create Author";
			this.createAuthorButton.Click += new System.EventHandler(this.createAuthorButton_Click);
			// 
			// idTextBox
			// 
			this.idTextBox.Location = new System.Drawing.Point(16, 176);
			this.idTextBox.Name = "idTextBox";
			this.idTextBox.TabIndex = 5;
			this.idTextBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 160);
			this.label3.Name = "label3";
			this.label3.TabIndex = 6;
			this.label3.Text = "Id";
			// 
			// fetchAuthorButton
			// 
			this.fetchAuthorButton.Location = new System.Drawing.Point(16, 208);
			this.fetchAuthorButton.Name = "fetchAuthorButton";
			this.fetchAuthorButton.Size = new System.Drawing.Size(104, 23);
			this.fetchAuthorButton.TabIndex = 7;
			this.fetchAuthorButton.Text = "Fetch Author";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(768, 542);
			this.Controls.Add(this.fetchAuthorButton);
			this.Controls.Add(this.idTextBox);
			this.Controls.Add(this.createAuthorButton);
			this.Controls.Add(this.lastNameTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.firstNameTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
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

		private string mapPath = @"C:\Berlioz\Puzzle\NPersist\Documentation\QuickStart\code\QuickStart.npersist";
		private string connectionString = "SERVER=(local);UID=sa;PWD=;DATABASE=QuickStart";

		private IContext context;

		private void InitializeContext()
		{
			context = new Context(mapPath); 
			context.SetConnectionString(connectionString);
		}

		private void createAuthorButton_Click(object sender, System.EventArgs e)
		{
			context.SetConnectionString 
			Author author = AuthorServices.CreateAuthor(
				context, 
				firstNameTextBox.Text, 
				lastNameTextBox.Text);

			idTextBox.Text = author.Id.ToString();
		}
	}
}
