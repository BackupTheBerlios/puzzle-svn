﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Samples.Northwind.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Puzzle.NPersist.Tests.Northwind.NET_3._5.Basic
{
    /// <summary>
    /// Summary description for IdentityMapTests
    /// </summary>
    [TestClass]
    public class IdentityMapTests : TestBase
    {
        public IdentityMapTests()
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

        #region TestIdentityMapOnGetObjectById

        /// <summary>
        /// Make sure that if we ask for an employee with the same id twice,
        /// GetObjectById will return two references to the same employee instance
        /// </summary>
        [TestMethod]
        public virtual void TestIdentityMapOnGetObjectById()
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

                //Assert that the employee has the id we asked for
                Assert.AreEqual(employeeId, employee.Id);

                //Ask the context to fetch the employee into a new variable
                //The context should now give us a second reference to the first instance
                //since it keeps all instances in the cache
                Employee employee2 = (Employee)context.GetObjectById(employeeId, typeof(Employee));

                //Assert that it is the same instance
                Assert.AreEqual(employee, employee2);

            }
        }

        #endregion

        #region TestIdentityMapOnGetObjectByNPath

        /// <summary>
        /// Make sure that if we ask for an employee with the same id twice,
        /// GetObjectByQuery will return two references to the same employee instance
        /// </summary>
        [TestMethod]
        public virtual void TestIdentityMapOnGetObjectByNPath()
        {
            int bossid = EnsureBoss();
            int id = EnsureNancy(bossid);

            using (IContext context = GetContext())
            {
                //we want to fetch the employee with id = 1
                string query = string.Format("Select * From Employee Where Id = {0}", id);

                //Ask the context to fetch the employee
                Employee employee = (Employee)context.GetObjectByNPath(query, typeof(Employee));

                //Assert that the context didn't return a null value
                Assert.IsNotNull(employee);

                //Assert that the employee has the id we asked for
                Assert.AreEqual(id, employee.Id);

                //Ask the context to fetch the employee into a new variable
                //The context should now give us a second reference to the first instance
                //since it keeps all instances in the cache
                Employee employee2 = (Employee)context.GetObjectByNPath(query, typeof(Employee));

                //Assert that it is the same instance
                Assert.AreEqual(employee, employee2);

            }
        }

        #endregion

        #region TestIdentityMapOnReferenceProperty

        /// <summary>
        /// Make sure that if we ask for an employee with the same id twice,
        /// we will get two references to the same employee instance even
        /// when we traverse referense properties
        /// </summary>
        [TestMethod]
        public virtual void TestIdentityMapOnReferenceProperty()
        {
            int bossid = EnsureBoss();
            int id = EnsureNancy(bossid);

            using (IContext context = GetContext())
            {
                //Ask the context to fetch the employee with id = 1
                Employee employee = (Employee)context.GetObjectById(id, typeof(Employee));

                //Assert that the context didn't return a null value
                Assert.IsNotNull(employee);

                //Assert that the employee has the id we asked for
                Assert.AreEqual(id, employee.Id);

                //Ask the context to fetch the employee with id = 2 (who happens to be the boss of employee 1)
                Employee boss = (Employee)context.GetObjectById(bossid, typeof(Employee));

                //Read what is hopefully the same instance of the boss via the
                //ReportsTo reference property
                Employee boss2 = employee.ReportsTo;

                //Assert that it is the same instance
                Assert.AreEqual(boss, boss2);

            }
        }

        #endregion

        #region TestIdentityMapOnReferenceListProperty

        /// <summary>
        /// Make sure that if we ask for an employee with the same id twice,
        /// we will get two references to the same employee instance even
        /// when we traverse referense list properties
        /// </summary>
        [TestMethod]
        public virtual void TestIdentityMapOnReferenceListProperty()
        {
            int bossid = EnsureBoss();
            int nancyid = EnsureNancy(bossid);

            using (IContext context = GetContext())
            {
                //Ask the context to fetch the employee with id = @id (who happens to be the boss of employee 1)
                Employee boss = (Employee)context.GetObjectById(bossid, typeof(Employee));

                //Assert that the context didn't return a null value
                Assert.IsNotNull(boss);

                //Assert that the employee has the id we asked for
                Assert.AreEqual(bossid, boss.Id);

                //Ask the context to fetch the employee with id = 1 
                Employee employee = (Employee)context.GetObjectById(nancyid, typeof(Employee));

                //Assert that the context didn't return a null value
                Assert.IsNotNull(employee);

                //Assert that it is this same instance is part of the Employees list
                Assert.IsTrue(boss.Employees.Contains(employee));
            }
        }

        #endregion
    }
}
