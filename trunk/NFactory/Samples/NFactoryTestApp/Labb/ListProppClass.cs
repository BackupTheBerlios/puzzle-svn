using System;
using System.Collections;

namespace Labb
{
	public class ListProppClass
	{

		#region Public Property SomeStringProp
		private string someStringProp;
		public string SomeStringProp
		{
			get
			{
				return this.someStringProp;
			}
			set
			{
				this.someStringProp = value;
			}
		}
		#endregion

		#region Public Property Items
		private ArrayList items ;
		public ArrayList Items
		{
			get
			{
				return this.items;
			}
			set
			{
				this.items = value;
			}
		}
		#endregion
	}
}
