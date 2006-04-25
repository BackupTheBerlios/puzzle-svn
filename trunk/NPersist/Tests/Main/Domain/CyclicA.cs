using System;

namespace Puzzle.NPersist.Tests.Main
{
	/// <summary>
	/// Summary description for CyclicA.
	/// </summary>
	public class CyclicA
	{
		public CyclicA()
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
		
		private CyclicB cyclicB ;
		
		public CyclicB CyclicB 
		{
			get { return this.cyclicB ; }
			set { this.cyclicB  = value; }
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
