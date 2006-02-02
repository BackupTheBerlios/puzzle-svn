using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPath.Framework;
using System.Collections;

namespace InMemFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleEngine engine = new SimpleEngine();

            IList realList = new List<Person>();

            Person p1 = new Person();
            p1.Age = 11;
            p1.FirstName = "Arne";
            p1.LastName = "Weise";
            realList.Add (p1);

            Person p2 = new Person();
            p2.Age = 33;
            p2.FirstName = "Kalle";
            p2.LastName = "Anka";
            realList.Add (p2);

            Person p3 = new Person();
            p3.Age = 3;
            p3.FirstName = "Stina";
            p3.LastName = "MeKlumpfota";
            realList.Add (p3);


            
            IList resultList = engine.Select ("where FirstName != null or SomeMethod(Age) > 100 order by FirstName desc",realList);

            foreach (Person person in resultList)
            {
                Console.WriteLine ("{0} {1}",person.FirstName,person.LastName);
            }

            Console.ReadLine ();

        }
    }
}
