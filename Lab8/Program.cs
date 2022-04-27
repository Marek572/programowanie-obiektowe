using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8
{
    record Student(string Name, int Ects);
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 13, 16 };

            Predicate<int> predicate = i =>
            {
                Console.WriteLine("Predykat dla " + i);
                return i % 2 == 0 && i > 4;
            };

            IEnumerable<int> enumerable =
                from n in ints
                    //where predykat (zawse true/false i 1 argument)
                where predicate.Invoke(n)
                select n;
            int sum = enumerable.Sum();
            Console.WriteLine("Suma " + sum);
            Console.WriteLine("Ewaluacja");
            Console.WriteLine(string.Join(", ", enumerable));
            Console.WriteLine();

            string[] strings = { "a", "bb", "ccc", "dddd", "abc", "owo" };

            IEnumerable<string> str =
                from s in strings
                where s.Length == 3
                select s.ToUpper();
            Console.WriteLine(string.Join(", ", str));


            Console.WriteLine(string.Join(", ",
                from i in ints
                select i.ToString("X")
                ));
            Console.WriteLine();


            Student[] students =
            {
            new Student("Ewa", 12),
            new Student("Karol", 42),
            new Student("Adam", 62),
            new Student("Ola", 22),
            new Student("Ewa", 12),
            };

            Console.WriteLine(string.Join("\n",
                from s in students
                where s.Ects > 30
                orderby s.Name
                select s.Name
                ));
            Console.WriteLine();

            IEnumerable<IGrouping<string, Student>> group = 
                from s in students
                group s by s.Name;

            foreach(var item in group)
            {
                Console.WriteLine(item.Key + " " + item.Count());
            };
            Console.WriteLine();

            IEnumerable<(string Key, int)> namesGroup = from s in students
            //into sluzy do zapisania grupy
            group s by s.Name into gr
            select(gr.Key,gr.Count());
            Console.WriteLine(string.Join("\n", namesGroup));
            Console.WriteLine();


            IEnumerable<(int Key, int)> ectsGroup = from s in students
            group s by s.Ects into ects
            select (ects.Key, ects.Count());
            Console.WriteLine(string.Join("\n", ectsGroup));
            Console.WriteLine();


            object p = from i in ints
                       select sum;

            IEnumerable<int> events = ints.Where(n => n % 2 == 0).OrderBy(n => n);
            Console.WriteLine(string.Join(", ", events));
            Console.WriteLine(string.Join("\n", students.OrderBy(s => s.Name).ThenBy(s => s.Ects)));
            Console.WriteLine();

            IEnumerable<(int Key, int)> fluentGroup =
                students
                .GroupBy(s => s.Ects)
                .Select(gr => (gr.Key, gr.Count()));
            Student tenStudent = students.ElementAt(1);
            Console.WriteLine(tenStudent);
            Console.WriteLine(students.Last());
            bool allPassed = students.All(s => s.Ects > 10);
            Console.WriteLine();

            Console.WriteLine(ints.All(i => i % 2 == 0));
            Console.WriteLine(ints.Any(i => i % 2 == 0));
            Console.WriteLine();


            Enumerable.Range(0, 100).Where(n => n % 2 == 0).Sum();

            Random rnd = new Random();
            rnd.Next(5);

            //tablica 1000 liczb z zakresu 0-9
            int[] vs = Enumerable.Range(0, 1000).Select(n => rnd.Next(10)).ToArray();

            //tablica liczb pierwsych mniejszych niz 100
            IEnumerable<int> primes = Enumerable.Range(0, 100).Where(n =>
            {
                return Enumerable.Range(2, n).All(i => n % i != 0);
            });
            Console.WriteLine(string.Join(", ", primes));
        }
    }
}
