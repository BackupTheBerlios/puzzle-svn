using System;
using System.Collections;
using NUnit.Framework;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Persistence;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Samples.Northwind.Domain;
using Puzzle.NPersist.Framework.EventArguments;
using Puzzle.NPersist.Framework.Delegates;

namespace Puzzle.NPersist.Tests.Northwind.Basic
{
	[TestFixture()]
	public class IdentityManagementTests : TestBase
	{
		public IdentityManagementTests()
		{
		}

		[Test]
		public void TestGetAquiredIdentityEvent()
		{
			string tmpProdId = ""; 
			using (IContext context = GetContext() )
			{
				hashAquiredSourceAssignedIdentities.Clear();
				context.AquiredSourceAssignedIdentity += new AquiredSourceAssignedIdentityEventHandler(Context_CountOnAquiredSourceAssignedIdentity) ;

				ITransaction tx = context.BeginTransaction();
				try
				{
					Product product = (Product) context.CreateObject(typeof(Product));

					product.ProductName = "TestProduct";

					context.ValidateBeforeCommit = false;

					IObjectManager om = context.ObjectManager;

					tmpProdId = om.GetObjectIdentity(product);

					Assert.AreEqual(36, tmpProdId.Length);

					Assert.AreEqual(0, hashAquiredSourceAssignedIdentities.Count);

					context.Commit();

					Assert.AreEqual(1, hashAquiredSourceAssignedIdentities.Count);
					Assert.IsTrue(hashAquiredSourceAssignedIdentities.ContainsKey(product));

					context.DeleteObject(product);

					context.Commit();

					tx.Commit();
				}
				catch (Exception ex)
				{
					tx.Rollback();
					throw ex;
				}
			}
		}

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


		[Test]
		public void TestRevertTemporaryIdentity()
		{
			DeleteTestCustomer("TESTX");

			string tmpProdId = ""; 
			using (IContext context = GetContext() )
			{
				ITransaction tx = context.BeginTransaction();
				try
				{
					Product product = (Product) context.CreateObject(typeof(Product));
					Customer customer = (Customer) context.CreateObject("TESTX", typeof(Customer));

					//customer.CompanyName = "TestCompany";
					product.ProductName = "TestProduct";

					context.ValidateBeforeCommit = false;

					IObjectManager om = context.ObjectManager;

					tmpProdId = om.GetObjectIdentity(product);

					Assert.AreEqual(36, tmpProdId.Length);

					try
					{
						context.Commit();

						throw new Exception("We should not reach this place!");
						//tx.Commit();
					}
					catch (Exception ex2)
					{
						Assert.AreEqual(tmpProdId, om.GetObjectIdentity(product));

						tx.Rollback();
					}
				}
				catch (Exception ex)
				{
					tx.Rollback();
					throw ex;
				}
			}
		}


		[Test]
		public void TestRevertTemporaryIdentityOnReference()
		{
			DeleteTestCustomer("TESTC");

			string tmpProdId = ""; 
			string tmpOrderId = ""; 
			string tmpDetailId = ""; 
			using (IContext context = GetContext() )
			{
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
					//product.ProductName = "TestProduct";

					context.ValidateBeforeCommit = false;

					IObjectManager om = context.ObjectManager;

					tmpProdId = om.GetObjectIdentity(product);
					tmpOrderId = om.GetObjectIdentity(order);
					tmpDetailId = om.GetObjectIdentity(orderDetail);

					Assert.AreEqual(36, tmpProdId.Length);

					try
					{
						context.Commit();

						throw new Exception("We should not reach this place!");
						//tx.Commit();
					}
					catch (Exception ex2)
					{
						Assert.AreEqual(tmpProdId, om.GetObjectIdentity(product));
						Assert.AreEqual(tmpOrderId, om.GetObjectIdentity(order));
						Assert.AreEqual(tmpDetailId, om.GetObjectIdentity(orderDetail));

						tx.Rollback();
					}
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
