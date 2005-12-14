using System;
using System.Reflection;

namespace Labb
{
	public class SomeClass
	{
		public SomeClass(int abc)
		{
			Abc = abc;	
		}


		public SomeClass(int abc,ListProppClass def)
		{
			Abc = abc;
			ListPropp = def;
		}

		#region Public Property ListProp
		private ListProppClass listPropp;
		public ListProppClass ListPropp
		{
			get
			{
				return this.listPropp;
			}
			set
			{
				this.listPropp = value;
			}
		}
		#endregion

		#region Public Property Abc
		private int abc;
		public int Abc
		{
			get
			{
				return this.abc;
			}
			set
			{
				this.abc = value;
			}
		}
		#endregion

		#region Public Property LongProp
		private long longProp;
		public long LongProp
		{
			get
			{
				return this.longProp;
			}
			set
			{
				this.longProp = value;
			}
		}
		#endregion

		#region Public Property SomeAssembly
		private Assembly someAssembly;
		public Assembly SomeAssembly
		{
			get
			{
				return this.someAssembly;
			}
			set
			{
				this.someAssembly = value;
			}
		}
		#endregion
	}
}
