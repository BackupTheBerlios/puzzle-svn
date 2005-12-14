using System;
using System.Reflection;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Samples.Northwind.Domain;

namespace Puzzle.NPersist.Tests.Northwind
{
	/// <summary>
	/// Summary description for TestBase.
	/// </summary>
	public abstract class TestBase
	{

		public virtual IContext GetContext()
		{
			return ContextFactory.CreateContext() ;
		}
	}
}
