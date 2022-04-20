using System;
using System.Collections.Generic;

namespace Lab6
{
    //mozna tez uzyc record Nazwa zamiast dodawac Equals
    //record Student
    class Student
    {
        public string Name { get; set; }
        public int Ects { get; set; }

        //musi byc zeby mozna bylo korzystac z kolekcji
        public override bool Equals(object obj)
        {
            //Console.WriteLine("Student equals");
            return obj is Student student &&
                   Name == student.Name &&
                   Ects == student.Ects;
        }

        public override int GetHashCode()
        {
            //Console.WriteLine("Student HashCode");
            return HashCode.Combine(Name, Ects);
        }

        public int CompareTo(Student other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return $"Name: {Name}, Ects: {Ects}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //List kiedy wazna jest kolejnosc obiektow
            ICollection<string> names = new List<string>();
            names.Add("Ewa");
            names.Add("Karol");
            names.Add("Robert");

            //sprawdza czy dana rzecz istnieje w kolekcji
            Console.WriteLine(names.Contains("Karol"));

            //usuwa dana rzecz z kolekcji
            names.Remove("Ewa");

            Console.WriteLine();
            foreach(string name in names)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("--------------------------------------");
            ICollection<Student> students = new List<Student>();
            students.Add(new Student() { Name = "Karol", Ects = 20 });
            students.Add(new Student() { Name = "Ewa", Ects = 25 });
            students.Add(new Student() { Name = "Robert", Ects = 23 });

            Console.WriteLine(students.Contains(new Student() { Name = "Robert", Ects = 23 }));

            students.Remove(new Student() { Name = "Ewa", Ects = 25 });

            Console.WriteLine();
            foreach(Student student in students)
            {
                Console.WriteLine(student.Name + " " + student.Ects);
            }

            Console.WriteLine();
            List<Student> list = (List<Student>) students;
            Console.WriteLine(list[0]);

            //aby dodac miedzy obiektami
            list.Insert(0, new Student() { Name = "Ania", Ects = 45 });

            int index = list.IndexOf(new Student() { Name = "Robert", Ects = 23 });
            Console.WriteLine();
            Console.WriteLine(index);

            //HashSet kiedy lista nieuporzadkowan
            ISet<string> setNames = new HashSet<string>();
            setNames.Add("Ewa");
            setNames.Add("Karol");
            setNames.Add("Robert");
            setNames.Add("Robert");

            Console.WriteLine();
            Console.WriteLine(string.Join(", ", setNames));

            Console.WriteLine("--------------------------------------");
            ISet<Student> studentGroup = new HashSet<Student>();
            studentGroup.Add(list[0]);
            studentGroup.Add(list[1]);
            studentGroup.Add(list[2]);
            studentGroup.Add(new Student() { Name = "Ania", Ects = 45 });

            foreach(Student student in students)
            {
                Console.WriteLine(student);
            }

            Console.WriteLine("-----CONTAINS-----");
            Console.WriteLine(studentGroup.Contains(list[2]));

            list.Add(new Student { Name = "Ela", Ects = 56 });
            list.Add(new Student { Name = "Marek", Ects = 16 });

            //List<Student> result = new List<Student>();
            //foreach(Student student in list)
            //{
            //    if(studentGroup.Contains(student))
            //        result.Add(student);
            //}
            //zamiast tego wyzej uzywac tego nizej

            ISet<Student> commonSet = new HashSet<Student>(studentGroup);
            commonSet.IntersectWith(list);

            Console.WriteLine();
            Console.WriteLine(string.Join(", ", commonSet));

            //SortedSet posortowany zbior
            //nie dziala xd
            //ISet<Student> sortedSet = new SortedSet<Student>(studentGroup);
            //sortedSet.Add(new Student() { Name = "Ewa", Ects = 34 });

            //foreach(Student s in sortedSet)
            //{
            //    Console.WriteLine(s);
            //}

            Dictionary<Student, List<string>> phones = new Dictionary<Student, List<string>>();
            phones[list[0]] = new List<string>();
            phones[list[0]].Add("781349526");

            Console.WriteLine();
            foreach(var item in phones)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }

            Console.WriteLine(string.Join(", ", phones.Keys));
            Console.WriteLine(string.Join(", ", phones.Values));
        }
    }
}
