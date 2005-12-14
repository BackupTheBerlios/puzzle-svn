using System;
using NUnit.Framework;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Samples.Northwind.Domain;

namespace Puzzle.NPersist.Tests.Northwind.Basic
{
	/// <summary>
	/// Summary description for TestLazyLoading.
	/// </summary>
	[TestFixture()]
	public class LazyLoadingTests : TestBase
	{

		#region TestIdentityMapOnReferenceProperty

		/// <summary>
		/// Make sure that traversing reference properties will cause
		/// the proper lazy loading behavior
		/// </summary>
		[Test()]
		public virtual void TestLazyLoadingOnReferenceProperty()
		{
			using (IContext context = GetContext() )
			{
				//Ask the context to fetch the employee with id = 1
				Employee employee = (Employee) context.GetObjectById(1, typeof(Employee));

				//Assert that the context didn't return a null value
				Assert.IsNotNull(employee);

				//Assert that the employee has the id we asked for
				Assert.AreEqual(1, employee.Id);

				//Assert that the employee object has been fully loaded
				Assert.AreEqual(ObjectStatus.Clean, context.GetObjectStatus(employee));

				//Read what is hopefully the same instance of the boss via the
				//ReportsTo reference property
				Employee boss = employee.ReportsTo ;

				//Assert that the context didn't return a null value
				Assert.IsNotNull(boss);

				//Assert that the boss has the id we asked for
				Assert.AreEqual(2, boss.Id);

				//Assert that the boss object has only been lazy loaded
				//(it has an id but none of its other properties are
				//loaded with values from the database yes)
				Assert.AreEqual(ObjectStatus.NotLoaded, context.GetObjectStatus(boss));

				//Read a property of the lazy loaded boss object, causing the object
				//to be loaded with values from the database
				Assert.IsTrue(boss.FirstName.Length > 0);

				//Assert that the boss object has now been fully loaded
				Assert.AreEqual(ObjectStatus.Clean, context.GetObjectStatus(boss));

			}
		}

		#endregion

		#region TestLazyLoadingOnReferenceListProperty

		/// <summary>
		/// Make sure that traversing reference list properties will cause
		/// the proper lazy loading behavior
		/// </summary>
		[Test()]
		public virtual void TestLazyLoadingOnReferenceListProperty()
		{
			using (IContext context = GetContext() )
			{
				//Ask the context to fetch the employee with id = 2
				Employee boss = (Employee) context.GetObjectById(2, typeof(Employee));

				//Assert that the context didn't return a null value
				Assert.IsNotNull(boss);

				//Assert that the employee has the id we asked for
				Assert.AreEqual(2, boss.Id);

				//Assert that the employee object has been fully loaded
				Assert.AreEqual(ObjectStatus.Clean, context.GetObjectStatus(boss));

				//Assert that the Employees property of the employee object has not been loaded yet
				Assert.AreEqual(PropertyStatus.NotLoaded, context.GetPropertyStatus(boss, "Employees"));

				//Read the Employees property, causing the property
				//to be loaded with values from the database
				Assert.IsTrue(boss.Employees.Count > 0);

				//Assert that the Employees property of the employee object has not been loaded yet
				Assert.AreEqual(PropertyStatus.Clean, context.GetPropertyStatus(boss, "Employees"));
			}
		}

		#endregion

	}
}
