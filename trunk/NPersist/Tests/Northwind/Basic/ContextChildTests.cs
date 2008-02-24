using System;
using NUnit.Framework;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Samples.Northwind.Domain;

namespace Puzzle.NPersist.Tests.Northwind.Basic
{
	/// <summary>
	/// Summary description for ContextChildTests.
	/// </summary>
	[TestFixture]
	public class ContextChildTests : TestBase
	{
		public ContextChildTests()
		{
		}

		[Test]
		public void ShouldFindContextOnEntity()
		{
			using (IContext context = GetContext())
			{
				Employee employee = (Employee) context.CreateObject(typeof(Employee));
				
				string id = context.ObjectManager.GetObjectIdentity(employee);
				Employee employee2 = (Employee) context.GetObjectById(id, typeof(Employee));

				Assert.IsNotNull(employee2);

			}

		}
	}
}
