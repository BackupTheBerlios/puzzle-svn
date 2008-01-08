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

		//Note: This test changes the connection to the ordinary Northwind 
		//database which it assumes has a state where the ALFKI customer has
		//six orders...yes, this is an ugly "special, special" test. Don't ever do
		//anything like this and pretend you didn't see this. Just comment out the test
		//when it doesn't run on your machine rather than giving your poor ALFKI customer
		//six orders...
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

		//Note: This test changes the connection to the ordinary Northwind 
		//database which it assumes has a state where the ALFKI customer has
		//six orders...yes, this is an ugly "special, special" test. Don't ever do
		//anything like this and pretend you didn't see this. Just comment out the test
		//when it doesn't run on your machine rather than giving your poor ALFKI customer
		//six orders...
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
