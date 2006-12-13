//Thanks to Harm Neervens for these tests!

using System;
using NUnit.Framework;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Samples.Northwind.Domain;

namespace Puzzle.NPersist.Tests.Northwind.Basic
{
	/// <summary>
	/// Summary description for ListPropertyTests.
	/// </summary>
	[TestFixture()]
	public class ListPropertyTests : TestBase
	{
		public ListPropertyTests()
		{
		}

		[Test()]
		public void TestListLoadingBeforeCommit()
		{
			using (IContext context = GetContext() )
			{
				context.SetConnectionString(ContextFactory.NormalNWConnectionString);

				Customer customer = (Customer) context.GetObjectById("ALFKI", typeof(Customer));

				Assert.IsNotNull(customer);

				int orderCount = customer.Orders.Count;
			
				Assert.AreEqual(6, orderCount);
			}
		}

		[Test()]
		public void TestListLoadingAfterCommit()
		{
			using (IContext context = GetContext() )
			{
				context.SetConnectionString(ContextFactory.NormalNWConnectionString);

				Customer customer = (Customer) context.GetObjectById("ALFKI", typeof(Customer));

				Assert.IsNotNull(customer);

				if (customer.CompanyName.EndsWith("test"))
					customer.CompanyName = customer.CompanyName.Replace("test", "");
				else
					customer.CompanyName = customer.CompanyName + "test";

				Console.WriteLine(context.GetPropertyStatus(customer, "Orders"));

				context.Commit();

				Console.WriteLine(context.GetPropertyStatus(customer, "Orders"));

				int orderCount = customer.Orders.Count;
			
				Assert.AreEqual(6, orderCount);
			}
		}

	}
}
