using System;
using System.Collections.Generic;
using System.Text;
using NObjectStore;
using MyDM;
using System.Linq;

namespace OODBLabb
{
    class Program
    {
        static void Main(string[] args)
        {
            Context ctx = new Context(@"\DbFiles");


            Company compona = ctx.CreateNamed<Company>("Company.Compona");
            compona.Name = "Compona";

            Employee roger = ctx.CreateNamed<Employee>("Employee.Roger");
            compona.Employees.Add(roger);

            var query = from c in ctx.AllLoaded<Company>()
                        where c.Name.StartsWith ("C")
                        select c;

            foreach (Company c in query)
            {
                Console.WriteLine(c.Name);
            }

            ctx.Commit();

            /*Company company = ctx.Get<Company>("Company.Compona");
                
                

            company.Name = "gnupe";

            Employee employee = ctx.Create<Employee>();
            company.Employees.Add(employee);
            employee.Name = "Sven";

            Employee employee2 = ctx.Create<Employee>();
            company.Employees.Add(employee2);
            employee2.Name = "Guran";

                */

            //for (int i=0;i<company.Employees.Count;i++)
            //{
            //    Employee emp = company.Employees[i];            
            //    Console.WriteLine("Name {0}", emp.Name);
            //    emp.Name += i.ToString();
            //}

            //ctx.Commit();



            //Employee roger = ctx.Get<Employee>("c2c57225-a1e9-4373-a817-16053dfe616c");
            //roger = null;
            //GC.Collect();
            //roger = ctx.Get<Employee>("c2c57225-a1e9-4373-a817-16053dfe616c");

            //Console.WriteLine(roger.Name);
            //roger.Name = "Roger2";
            //Console.WriteLine(roger.Name);
            //Console.WriteLine(roger.Company.Name);
            //Company company = roger.Company;
            //roger.Company = company;
            //Console.WriteLine(roger.Company.Name);
            //ctx.Delete(roger.Company);
            //Console.WriteLine(roger.Company);

            //ctx.Commit();
            Console.ReadLine();
        }
    }
}
