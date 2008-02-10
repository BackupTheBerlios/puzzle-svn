using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Puzzle.NPersist.Framework;
using NPersistLinqTests.DM;
using Puzzle.NPersist.Linq;

namespace NPersistLinqTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class LinqToNPathTests
    {

        public LinqToNPathTests()
        {
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

        [TestMethod]
        public void WorldFirstNPathLinqTest()
        {
            Context ctx = null;

            var res = from cust in ctx.Repository(new LoadSpan<Customer> ("Name","Email"))
                      where cust.Address.StreetName == "abc123"
                      select cust;

            string expected = "select Name, Email from Customer where ((Address.StreetName = \"abc123\"))";
            string actual = res.Query.ToNPath();

            Assert.AreEqual<string>(expected, actual);                                  
        }

        [TestMethod]
        public void WherePropertyPathTest()
        {
            Context ctx = null;

            var res = from cust in ctx.Repository<Customer>()
                      where cust.Address.StreetName == "abc123"
                      select cust;

            string expected = "select * from Customer where ((Address.StreetName = \"abc123\"))";
            string actual = res.Query.ToNPath();

            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void LoadSpanTest()
        {
            Context ctx = null;

            var res = from cust in ctx.Repository(new LoadSpan<Customer> ("Name","Email","Address.StreetName"))                      
                      select cust;

            string expected = "select Name, Email, Address.StreetName from Customer";
            string actual = res.Query.ToNPath();

            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void OrderByTest()
        {
            Context ctx = null;

            var res = from cust in ctx.Repository<Customer>()
                      orderby cust.Name, cust.Address.StreetName
                      select cust;

            string expected = "select * from Customer order by Name, Address.StreetName";
            string actual = res.Query.ToNPath();

            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void ListPropCountTest()
        {
            Context ctx = null;

            var res = from cust in ctx.Repository<Customer>()
                      where cust.Orders.Count == 1
                      select cust;

            string expected = "select * from Customer where ((Orders.Count() = 1))";
            string actual = res.Query.ToNPath();

            Assert.AreEqual<string>(expected, actual);
        }
    }
}

