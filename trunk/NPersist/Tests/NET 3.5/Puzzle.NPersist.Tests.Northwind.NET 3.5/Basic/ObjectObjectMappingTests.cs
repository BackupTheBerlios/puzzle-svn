using System;
using System.Text;
using System.Collections;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Framework.Querying;
using Puzzle.NPersist.Samples.Northwind.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Puzzle.NPersist.Tests.Northwind.NET_3._5.Basic
{
    /// <summary>
    /// Summary description for ObjectObjectMappingTests
    /// </summary>
    [TestClass]
    public class ObjectObjectMappingTests : CrudTests
    {
        public ObjectObjectMappingTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        public override IContext GetContext()
        {
            IContext rootContext = base.GetContext();
            IContext leafContext = new Context(rootContext);
            return leafContext;
        }

        #region TestFetchEmployeeById

        /// <summary>
        /// Fetch an employee object by identity
        /// </summary>
        [TestMethod]
        public virtual void TestEarlyOptimisticConcurrency()
        {
            int bossid = EnsureBoss();
            int id = EnsureNancy(bossid);

            using (IContext context = GetContext())
            {
                //we want to fetch the employee with id = 1
                int employeeId = id;

                //Ask the context to fetch the employee
                Employee employee = (Employee)context.GetObjectById(employeeId, typeof(Employee));

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
    }
}
