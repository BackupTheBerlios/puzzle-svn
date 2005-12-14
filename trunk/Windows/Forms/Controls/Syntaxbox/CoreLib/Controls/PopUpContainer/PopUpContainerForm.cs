using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Puzzle.Windows.Forms.PopUpContainer
{
	/// <summary>
	/// Summary description for PopUpContainerForm.
	/// </summary>
	public class PopUpContainerForm : Form
	{
		private IContainer components;
		private NativeSubclasser ns = null;
		private bool _Start = false;

		public PopUpContainerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

		}

		#region From Flat Controls implementation

		public bool m_Start = false;
		private Control m_Parent = null;
		private Form m_ParentForm = null;
		private Timer tmrFocus;
		private Form m_MdiParent = null;

		public PopUpContainerForm(Control parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			if (parent.Parent != null)
			{
				m_Parent = parent;
				m_ParentForm = m_Parent.FindForm();

				if (Form.ActiveForm != null && Form.ActiveForm.IsMdiContainer)
				{
					m_MdiParent = Form.ActiveForm;

					m_MdiParent.LostFocus += new EventHandler(this.Kill);
					m_MdiParent.MouseDown += new MouseEventHandler(this.MouseKill);
					m_MdiParent.Move += new EventHandler(this.Kill);
				}

				m_ParentForm.LostFocus += new EventHandler(this.Kill);
				m_ParentForm.MouseDown += new MouseEventHandler(this.MouseKill);
				m_ParentForm.Move += new EventHandler(this.Kill);
				m_ParentForm.Deactivate += new EventHandler(this.Kill);
				m_ParentForm.Leave += new EventHandler(this.Kill);
				m_ParentForm.Resize += new EventHandler(this.Kill);


				if (!object.ReferenceEquals(m_Parent.Parent, m_ParentForm))
				{
					m_Parent.Parent.MouseDown += new MouseEventHandler(this.MouseKill);
				}

				ns = new NativeSubclasser(m_ParentForm);
				ns.Message += new NativeMessageHandler(this.Parent_WndProc);
			}
		}

		private void Parent_WndProc(object o, NativeMessageArgs e)
		{
			if (e.Message.Msg == (int) WindowMessage.WM_PARENTNOTIFY)
			{
				if (_Start)
				{
					this.CLOSE();
				}
			}
		}

		private void MouseKill(object sender, MouseEventArgs e)
		{
			this.CLOSE();
		}

		private void Kill(object sender, EventArgs e)
		{
			if (_Start)
				this.CLOSE();
		}

		#endregion

		private void CLOSE()
		{
			this.Close();
			if (ns != null)
			{
				ns.Message -= new NativeMessageHandler(this.Parent_WndProc);
				//	ns.Detatch ();
				ns = null;
			}
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

//		protected override CreateParams CreateParams
//		{
//			get
//			{
//				CreateParams createParams = base.CreateParams;
//				createParams.ExStyle = createParams.ExStyle | 128;
//				if ( Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major > 5 )
//				{
//					createParams.ExStyle = createParams.ExStyle | 134217728;
//				}
//				createParams.ClassStyle = createParams.ClassStyle | 2048;
//				return createParams;
//			}
//		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tmrFocus = new System.Windows.Forms.Timer(this.components);
			// 
			// tmrFocus
			// 
			this.tmrFocus.Interval = 10;
			this.tmrFocus.Tick += new System.EventHandler(this.tmrFocus_Tick);
			// 
			// PopUpContainerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(202, 170);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(1, 1);
			this.Name = "PopUpContainerForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

		}

		#endregion

		private int max = 0;

		private void tmrFocus_Tick(object sender, EventArgs e)
		{
			int i = this.Height;
			i += 10;
			if (i > max)
				i = max;
			this.Height = i;

			if (i == max)
				tmrFocus.Enabled = false;
		}

		public void BeforeAnimate()
		{
			max = this.Height;
			this.Height = 0;
		}

		public void BeginAnimate()
		{
			this.tmrFocus.Enabled = true;
		}

		public void StartEventHandlers()
		{
			_Start = true;
		}

//		protected override void WndProc(ref Message m)
//		{				
//			if(m.Msg == (int)Puzzle.Windows.WindowMessage.WM_MOUSEACTIVATE)
//			{
//				m.Result = (IntPtr)3; //ma_mouseactivate
//				return;
//			}
//
//			base.WndProc(ref m);			
//		}
//
//		protected override CreateParams CreateParams 
//		{
//			get
//			{
//				CreateParams cp = base.CreateParams;
//				cp.ExStyle |= 0x00000008;  //ws_ex_topmost
//				return cp;
//			}
//		}
	}
}