using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using Puzzle.SourceCode;

namespace Puzzle.Windows.Forms.SyntaxBox
{
	/// <summary>
	/// Designer for the SyntaxBoxControl
	/// </summary>
	public class SyntaxBoxDesigner : ControlDesigner
	{
		public SyntaxBoxDesigner() : base()
		{
		}

		protected ISelectionService SelectionService
		{
			get { return (ISelectionService) this.GetService(typeof (ISelectionService)); }
		}

		protected void OnActivate(object s, EventArgs e)
		{
		}

		protected virtual IDesignerHost DesignerHost
		{
			get { return (IDesignerHost) base.GetService(typeof (IDesignerHost)); }
		}

		public override void OnSetComponentDefaults()
		{
			base.OnSetComponentDefaults();
			if (DesignerHost != null)
			{
				DesignerTransaction trans = DesignerHost.CreateTransaction("Adding Syntaxdocument");
				SyntaxDocument sd = DesignerHost.CreateComponent(typeof (SyntaxDocument)) as SyntaxDocument;

				SyntaxBoxControl sb = this.Control as SyntaxBoxControl;
				sb.Document = sd;
				trans.Commit();
			}
		}
	}
}