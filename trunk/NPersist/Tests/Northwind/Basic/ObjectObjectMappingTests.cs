using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Persistence;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Framework.Querying;
using Puzzle.NPersist.Samples.Northwind.Domain;
using Puzzle.NPersist.Framework.EventArguments;
using Puzzle.NPersist.Framework.Delegates;

namespace Puzzle.NPersist.Tests.Northwind.Basic
{
	/// <summary>
	/// Summary description for ObjectObjectMappingTests.
	/// </summary>
	[TestFixture()]
	public class ObjectObjectMappingTests : CrudTests
	{
		public override IContext GetContext()
		{
			IContext rootContext = base.GetContext ();
			IContext leafContext = new Context(rootContext);
			return leafContext;
		}

		#region TestFetchEmployeeById

		/// <summary>
		/// Fetch an employee object by identity
		/// </summary>
		[Test()]
		public virtual void TestEarlyOptimisticConcurrency()
		{
			int bossid = EnsureBoss();
			int id = EnsureNancy(bossid);

			using (IContext context = GetContext() )
			{
				//we want to fetch the employee with id = 1
				int employeeId = id;

				//Ask the context to fetch the employee
				Employee employee = (Employee) context.GetObjectById(employeeId, typeof(Employee));

				//Assert that the context didn't return a null value
				Assert.IsNotNull(employee);

				//Assert that the object is marked as clean
				Assert.AreEqual(ObjectStatus.Clean, context.ObjectManager.GetObjectStatus(employee));

				Assert.AreEqual(PropertyStatus.Clean, context.ObjectManager.GetPropertyStatus(employee, "Id"));
				Assert.AreEqual(PropertyStatus.Clean, context.ObjectManager.GetPropertyStatus(employee, "FirstName"));
				Assert.AreEqual(PropertyStatus.Clean, context.ObjectManager.GetPropertyStatus(employee, "LastName"));

				//Assert that the employee has the id we asked for
				Assert.AreEqual(employeeId, employee.Id);

				//Assert that the employee has the name Nancy Davolio
				//(She should, in a standard northwind setup)
				Assert.AreEqual("Nancy", employee.FirstName);
				Assert.AreEqual("Davolio", employee.LastName);			

			}
		}

		#endregion

		public virtual void TestCreateAndDeleteEmployeeNotRecursive()
		{
			using (IContext context = GetContext() )
			{
				context.AlwaysCommitRecursive = false;

				//Encapsulate our whole test in a transaction,
				//so that any changes to the database are rolled back in case
				//of a test failure
				ITransaction tx = context.BeginTransaction();

				try
				{
					//Ask the context to create the new employee
					Employee employee = (Employee) context.CreateObject(typeof(Employee));

					//Assert that the context didn't return a null value
					Assert.IsNotNull(employee);

					//Set the properties of the new employee object
					employee.FirstName = "Mats";
					employee.LastName = "Helander";
					employee.HireDate = DateTime.Now;

					//Ask the context to insert our new employee into the database
					context.Commit() ;

					//The employee has an Id mapping to an autoincreasing column.
					//Make sure the employee object has been updated with its new identity
					Assert.IsTrue(employee.Id < 1, "Employee was given identity!");

					//Make sure that the row was inserted into the database...
					//To do this we resort to some good old sql...
					string sql = "Select Count(*) From Employees Where EmployeeId = " + employee.Id.ToString() + 
						" And FirstName = 'Mats'" +
						" And LastName = 'Helander'";

					//Execute the sql statement via the context (so that it is in the same transaction...that helps ;-))
					int result = (int) context.SqlExecutor.ExecuteScalar(sql);

					//Make sure that the query for our new row returned exactly zero hits
					Assert.AreEqual(0, result, "Row not inserted!");


					((ObjectPersistenceEngine) context.PersistenceEngine).SourceContext.Commit();


					Assert.IsTrue(employee.Id > 0, "Employee was not given identity!");

					//Execute the sql statement via the context (so that it is in the same transaction...that helps ;-))
					result = (int) context.SqlExecutor.ExecuteScalar(sql);

					//Make sure that the query for our new row returned exactly one hit
					Assert.AreEqual(1, result, "Row not inserted!");

					//Assert that the properties have the correct values
					Assert.AreEqual("Mats", employee.FirstName, "FirstName");
					Assert.AreEqual("Helander", employee.LastName, "LastName");			

					//Ask the context to delete our employee
					context.DeleteObject(employee);

					//Ask the context to remove the employee from the database again
					context.Commit();

					//Execute the sql statement again
					result = (int) context.SqlExecutor.ExecuteScalar(sql);

					//Make sure that the query for our new row returns no hits
					Assert.AreEqual(0, result, "Row not deleted!");

					//The whole test went fine! Commit the transaction!
					tx.Commit();
				}
				catch (Exception ex)
				{
					//Something went wrong!
					//Rollback the transaction and retheow the exception
					tx.Rollback();

					throw ex;
				}
			}
		}

		
		//
//		#region TestFetchAndUpdateEmployee
//
//		/// <summary>
//		/// Fetch an employee object by identity
//		/// </summary>
//		[Test()]
//		public virtual void TestFetchAndUpdateEmployee()
//		{
//			using (IContext context = GetContext() )
//			{
//				//we want to fetch the employee with id = 10
//				int employeeId = 10;
//
//				//Ask the context to fetch the employee
//				Employee employee = (Employee) context.GetObjectById(employeeId, typeof(Employee));
//
//				//Assert that the context didn't return a null value
//				Assert.IsNotNull(employee);
//
//				//Assert that the object is marked as clean
//				Assert.AreEqual(ObjectStatus.Clean, context.ObjectManager.GetObjectStatus(employee));
//
//				Assert.AreEqual(PropertyStatus.Clean, context.ObjectManager.GetPropertyStatus(employee, "Id"));
//				Assert.AreEqual(PropertyStatus.Clean, context.ObjectManager.GetPropertyStatus(employee, "FirstName"));
//				Assert.AreEqual(PropertyStatus.Clean, context.ObjectManager.GetPropertyStatus(employee, "LastName"));
//
//				//Assert that the employee has the id we asked for
//				Assert.AreEqual(employeeId, employee.Id);
//
//				//Assert that the employee has the name Nancy Davolio
//				//(She should, in a standard northwind setup)
//				Assert.AreEqual("Mats", employee.FirstName);
//				Assert.AreEqual("Helander", employee.LastName);			
//
//				employee.FirstName = "Lars";
//				employee.LastName = "Nilsson";
//
//				context.Commit() ;
//			}
//		}
//
//		#endregion


		[Test]
		public void TestGetAquiredIdentityEventForDetail()
		{
			DeleteTestCustomer("TESTC");

			string tmpProdId = ""; 
			using (IContext context = GetContext() )
			{
				hashAquiredSourceAssignedIdentities.Clear();
				context.AquiredSourceAssignedIdentity += new AquiredSourceAssignedIdentityEventHandler(Context_CountOnAquiredSourceAssignedIdentity) ;

				ITransaction tx = context.BeginTransaction();
				try
				{
					Customer customer = (Customer) context.CreateObject("TESTC", typeof(Customer));
					Order order = (Order) context.CreateObject(typeof(Order));
					Product product = (Product) context.CreateObject(typeof(Product));
					OrderDetail orderDetail = (OrderDetail) context.CreateObject(typeof(OrderDetail));
					order.Customer = customer;
					orderDetail.Order = order;
					orderDetail.Product = product;

					orderDetail.Discount = 0;
					orderDetail.Quantity = 1;
					orderDetail.UnitPrice = 10;

					customer.CompanyName = "TestCompany";
					product.ProductName = "TestProduct";

					context.ValidateBeforeCommit = false;

					IObjectManager om = context.ObjectManager;

					tmpProdId = om.GetObjectIdentity(product);

					Assert.AreEqual(36, tmpProdId.Length);

					Assert.AreEqual(0, hashAquiredSourceAssignedIdentities.Count);

					context.Commit();

					Assert.AreEqual(3, hashAquiredSourceAssignedIdentities.Count);
					Assert.IsTrue(hashAquiredSourceAssignedIdentities.ContainsKey(product));
					Assert.IsTrue(hashAquiredSourceAssignedIdentities.ContainsKey(order));
					Assert.IsTrue(hashAquiredSourceAssignedIdentities.ContainsKey(orderDetail));

					tx.Commit();
				}
				catch (Exception ex)
				{
					tx.Rollback();
					throw ex;
				}
			}
		}

		private void DeleteTestCustomer(string id)
		{
			using (IContext context = GetContext() )
			{
				Customer customer = (Customer) context.TryGetObjectById(id, typeof(Customer));

				if (customer != null)
				{
					foreach (Order order in new ArrayList(customer.Orders))
					{
						foreach (OrderDetail orderDetail in new ArrayList(order.OrderDetails))
						{
							context.DeleteObject(orderDetail.Product);
							context.DeleteObject(orderDetail);
						}
						context.DeleteObject(order);
					}

					context.DeleteObject(customer);
					context.Commit();
				}
			}			
		}

		private Hashtable hashAquiredSourceAssignedIdentities = new Hashtable();

		private void Context_CountOnAquiredSourceAssignedIdentity(object sender, ObjectEventArgs e)
		{
			hashAquiredSourceAssignedIdentities[e.EventObject] = e.EventObject;
		}
		
	}
}
