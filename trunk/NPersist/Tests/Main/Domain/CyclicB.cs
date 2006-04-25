using System;

namespace Puzzle.NPersist.Tests.Main
{
	/// <summary>
	/// Summary description for CyclicB.
	/// </summary>
	public class CyclicB
	{
		public CyclicB()
		{
		}

		#region Property  Id
		
		private int id;
		
		public int Id
		{
			get { return this.id; }
			set { this.id = value; }
		}
		
		#endregion

		#region Property  CyclicA 
		
		private CyclicA cyclicA ;
		
		public CyclicA CyclicA 
		{
			get { return this.cyclicA ; }
			set { this.cyclicA  = value; }
		}
		
		#endregion

		#region Property  SomeText
		
		private string someText;
		
		public string SomeText
		{
			get { return this.someText; }
			set { this.someText = value; }
		}
		
		#endregion
	}
}
