using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Puzzle.Windows.Forms.PopUpContainer;

namespace Puzzle.Windows.Forms
{
	/// <summary>
	/// Summary description for PupUpContainer.
	/// </summary>
	public class PopUpContainerControl : Panel
	{
		private Point _Pos;
		private Control _RealParent = null;
		private PopUpContainerForm _ParentForm;
		private bool _IsActive = false;

		public event EventHandler BeforeShowPopUp = null;

		protected virtual void OnBeforeShowPopUp()
		{
			if (BeforeShowPopUp != null)
				BeforeShowPopUp(this, EventArgs.Empty);
		}

		public bool IsActive
		{
			get { return _IsActive; }
		}

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public PopUpContainerControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
			this.Visible = false;

		}

		public void ShowPopUp()
		{
			if (_IsActive)
				return;

			_IsActive = true;


			_ParentForm = new PopUpContainerForm(this);


			_ParentForm.Size = new Size(this.Width, this.Height);
			Point FormPos = _ParentForm.Location = this.PointToScreen(new Point(0, 0));
			if (this.ShowBorder)
			{
				_ParentForm.FormBorderStyle = FormBorderStyle.FixedDialog;
				FormPos = _ParentForm.Location = this.PointToScreen(new Point(0, 0));
				FormPos.X -= 2;
				_ParentForm.Size = new Size(this.Width + 8, this.Height + 4);
			}

			NativeMethods.SetWindowLong(_ParentForm.Handle,
			                            NativeMethods.GWL_STYLE,
			                            NativeMethods.WS_CHILD);

			_ParentForm.TopLevel = true;
			_ParentForm.BringToFront();
			if (this.TopLevelControl is Form)
			{
				_ParentForm.Owner = (Form) this.TopLevelControl;
			}

			_RealParent = this.Parent;

			_Pos = this.Location;


			_ParentForm.Controls.Add(this);
			this.Location = new Point(0, 0);
			this.Visible = true;
			_ParentForm.TopMost = true;

			_ParentForm.Deactivate += new EventHandler(this.Form_Leave);


			_ParentForm.Location = FormPos;
			_ParentForm.Closed += new EventHandler(this.Form_Leave);
			_ParentForm.BeforeAnimate();
			OnBeforeShowPopUp();
//			if (this.ShowBorder)
//				_ParentForm.FormBorderStyle = FormBorderStyle.FixedDialog;

			_ParentForm.Show();
			this.Controls[0].Select();
			this.Controls[0].Focus();
			_ParentForm.BeginAnimate();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		public void StartEventHandlers()
		{
			if (_ParentForm != null)
				_ParentForm.StartEventHandlers();
		}

		private void Form_Leave(object s, EventArgs e)
		{
			HidePopUp();
		}

		public void HidePopUp()
		{
			if (!_IsActive)
				return;
			_IsActive = false;
			this.Dock = DockStyle.None;
			this.Visible = false;
			_RealParent.Controls.Add(this);
			this.Location = _Pos;
			_ParentForm.Dispose();
			Application.DoEvents();
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// PupUpContainer
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {});
			this.Name = "PupUpContainer";
			this.Size = new System.Drawing.Size(208, 232);
			this.ResumeLayout(false);

		}

		#endregion

		#region PUBLIC PROPERTY SHOWBORDER

		private bool _ShowBorder = false;

		public bool ShowBorder
		{
			get { return _ShowBorder; }
			set { _ShowBorder = value; }
		}

		#endregion
	}
}