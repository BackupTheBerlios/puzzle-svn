using System;
using Labb;
using Puzzle.NFactory.Framework;

namespace NFactoryTestApp
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			
			
			IContainer c = ApplicationContext.Configure() ;
			
			SomeClass some1 = (SomeClass)c.GetObject("MySomeClass");
			SomeClass some2 = (SomeClass)c.GetObject("MySomeClass");

			if (some1 == some2)
				Console.WriteLine("same") ;
			else
				Console.WriteLine("not same") ;

			if (some1.ListPropp == some2.ListPropp)
				Console.WriteLine("same") ;
			else
				Console.WriteLine("not same") ;

			ListProppClass listpropp = (ListProppClass)c.GetObject("MyListProppClass");
			Console.WriteLine(listpropp.SomeStringProp) ;

			Console.ReadLine() ;
		}
	}
}
