using System;

namespace Labb
{
	public class MyStaticFactory
	{
		public static SomeClass CreateSomeClass(int abc)
		{
			return new SomeClass(abc) ;
		}
	}
}
