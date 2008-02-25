using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Samples.Northwind.Domain;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.Persistence;

namespace Puzzle.NPersist.Tests.Northwind.NET_3._5.Basic
{
    [TestClass]
    public class DeleteTests : TestBase
    {

        [TestMethod]
        public void TestCreateAndDeleteEmployeeAndBoss()
        {
            using (IContext context = GetContext())
            {
                //Encapsulate our whole test in a transaction,
                //so that any changes to the database are rolled back in case
                //of a test failure
                ITransaction tx = context.BeginTransaction();

                try
                {
                    //Ask the context to create the new employee
                    Employee employee = (Employee)context.CreateObject(typeof(Employee));

                    //Ask the context to create the new employee
                    Employee boss = (Employee)context.CreateObject(typeof(Employee));

                    //Assert that the context didn't return a null value
                    Assert.IsNotNull(employee);
                    Assert.IsNotNull(boss);

                    //Set the properties of the new employee object
                    employee.FirstName = "Mats";
                    employee.LastName = "Helander";
                    employee.HireDate = DateTime.Now;

                    boss.FirstName = "Roger";
                    boss.LastName = "Alsing";
                    boss.HireDate = DateTime.Now;

                    employee.ReportsTo = boss;

                    Assert.AreEqual(ObjectStatus.UpForCreation, context.GetObjectStatus(employee));
                    Assert.AreEqual(ObjectStatus.UpForCreation, context.GetObjectStatus(boss));

                    //Ask the context to insert our new employee into the database
                    context.Commit();

                    //The employee has an Id mapping to an autoincreasing column.
                    //Make sure the employee object has been updated with its new identity
                    Assert.IsTrue(employee.Id > 0, "Employee was not given identity!");

                    //Make sure that the row was inserted into the database...
                    //To do this we resort to some good old sql...
                    string sql1 = "Select Count(*) From Employees Where EmployeeId = " + employee.Id.ToString() +
                        " And FirstName = 'Mats'" +
                        " And LastName = 'Helander'" +
                        " And ReportsTo = " + boss.Id.ToString();

                    //Execute the sql statement via the context (so that it is in the same transaction...that helps ;-))
                    int result = (int)context.SqlExecutor.ExecuteScalar(sql1);

                    //Make sure that the query for our new row returned exactly one hit
                    Assert.AreEqual(1, result, "Row not inserted!");

                    string sql2 = "Select Count(*) From Employees Where EmployeeId = " + boss.Id.ToString() +
                        " And FirstName = 'Roger'" +
                        " And LastName = 'Alsing'";

                    //Execute the sql statement via the context (so that it is in the same transaction...that helps ;-))
                    result = (int)context.SqlExecutor.ExecuteScalar(sql2);

                    //Make sure that the query for our new row returned exactly one hit
                    Assert.AreEqual(1, result, "Boss not inserted!");

                    //Assert that the properties have the correct values
                    Assert.AreEqual("Mats", employee.FirstName, "FirstName");
                    Assert.AreEqual("Helander", employee.LastName, "LastName");

                    context.DeleteObject(boss);

                    //Ask the context to delete our employee
                    context.DeleteObject(employee);

                    //Ask the context to remove the employee from the database again
                    context.Commit();

                    //Execute the sql statement again
                    result = (int)context.SqlExecutor.ExecuteScalar(sql1);

                    //Make sure that the query for our new row returns no hits
                    Assert.AreEqual(0, result, "Row not deleted!");

                    //Execute the sql statement again
                    result = (int)context.SqlExecutor.ExecuteScalar(sql2);

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

        [TestMethod]
        public void TestCreateAndDeleteEmployeeAndBossDeleteEmployeeFirst()
        {
            using (IContext context = GetContext())
            {
                //Encapsulate our whole test in a transaction,
                //so that any changes to the database are rolled back in case
                //of a test failure
                ITransaction tx = context.BeginTransaction();

                try
                {
                    //Ask the context to create the new employee
                    Employee employee = (Employee)context.CreateObject(typeof(Employee));

                    //Ask the context to create the new employee
                    Employee boss = (Employee)context.CreateObject(typeof(Employee));

                    //Assert that the context didn't return a null value
                    Assert.IsNotNull(employee);
                    Assert.IsNotNull(boss);

                    //Set the properties of the new employee object
                    employee.FirstName = "Mats";
                    employee.LastName = "Helander";
                    employee.HireDate = DateTime.Now;

                    boss.FirstName = "Roger";
                    boss.LastName = "Alsing";
                    boss.HireDate = DateTime.Now;

                    employee.ReportsTo = boss;

                    Assert.AreEqual(ObjectStatus.UpForCreation, context.GetObjectStatus(employee));
                    Assert.AreEqual(ObjectStatus.UpForCreation, context.GetObjectStatus(boss));

                    //Ask the context to insert our new employee into the database
                    context.Commit();

                    //The employee has an Id mapping to an autoincreasing column.
                    //Make sure the employee object has been updated with its new identity
                    Assert.IsTrue(employee.Id > 0, "Employee was not given identity!");

                    //Make sure that the row was inserted into the database...
                    //To do this we resort to some good old sql...
                    string sql1 = "Select Count(*) From Employees Where EmployeeId = " + employee.Id.ToString() +
                        " And FirstName = 'Mats'" +
                        " And LastName = 'Helander'" +
                        " And ReportsTo = " + boss.Id.ToString();

                    //Execute the sql statement via the context (so that it is in the same transaction...that helps ;-))
                    int result = (int)context.SqlExecutor.ExecuteScalar(sql1);

                    //Make sure that the query for our new row returned exactly one hit
                    Assert.AreEqual(1, result, "Row not inserted!");

                    string sql2 = "Select Count(*) From Employees Where EmployeeId = " + boss.Id.ToString() +
                        " And FirstName = 'Roger'" +
                        " And LastName = 'Alsing'";

                    //Execute the sql statement via the context (so that it is in the same transaction...that helps ;-))
                    result = (int)context.SqlExecutor.ExecuteScalar(sql2);

                    //Make sure that the query for our new row returned exactly one hit
                    Assert.AreEqual(1, result, "Boss not inserted!");

                    //Assert that the properties have the correct values
                    Assert.AreEqual("Mats", employee.FirstName, "FirstName");
                    Assert.AreEqual("Helander", employee.LastName, "LastName");

                    //Ask the context to delete our employee
                    context.DeleteObject(employee);
                    context.DeleteObject(boss);

                    //Ask the context to remove the employee from the database again
                    context.Commit();

                    //Execute the sql statement again
                    result = (int)context.SqlExecutor.ExecuteScalar(sql1);

                    //Make sure that the query for our new row returns no hits
                    Assert.AreEqual(0, result, "Row not deleted!");

                    //Execute the sql statement again
                    result = (int)context.SqlExecutor.ExecuteScalar(sql2);

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


    }
}
