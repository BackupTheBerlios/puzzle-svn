using System;

namespace Labb
{
	public class MyInstanceFactory
	{
		public SomeClass CreateSomeClass(int abc,ListProppClass def)
		{
			return new SomeClass(abc,def) ;
		}
	}
}
