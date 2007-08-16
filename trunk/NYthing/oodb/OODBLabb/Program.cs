using System;
using System.Collections.Generic;
using System.Text;
using NObjectStore;
using MyDM;

namespace OODBLabb
{
    class Program
    {
        static void Main(string[] args)
        {
            Context ctx = new Context(@"\DbFiles");

            Employee roger = ctx.Get<Employee>("c2c57225-a1e9-4373-a817-16053dfe616c");
            roger = null;
            GC.Collect();
            roger = ctx.Get<Employee>("c2c57225-a1e9-4373-a817-16053dfe616c");

            Console.WriteLine(roger.Name);
            roger.Name = "Roger2";
            Console.WriteLine(roger.Name);
            Console.WriteLine(roger.Company.Name);
            Company company = roger.Company;
            roger.Company = company;
            Console.WriteLine(roger.Company.Name);
            ctx.Delete(roger.Company);
            Console.WriteLine(roger.Company);

            ctx.Commit();
            Console.ReadLine();
        }
    }
}
