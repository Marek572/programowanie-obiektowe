using System;

namespace Lab4
{
    //w enumie nie moze byc liczba nie calkowita
    enum Degree
    {
        A = 50,
        B = 45,
        C = 40,
        D = 35,
        E = 30,
        F = 20
    }
    class Program
    {
        public static double Convert(Degree degree)
        {
            return degree switch
            {
                Degree.A => 5.0,
                Degree.B => 4.5,
                Degree.C => 4.0,
                Degree.D => 3.5,
                Degree.E => 3.0,
                Degree.F => 2.0,
                //wartosc domyslna (default)
                _ => 1.0
            };
        }

        public static string DegreeType(Degree degree)
        {
            return degree switch
            {
                Degree.A or Degree.B or Degree.C or Degree.D or Degree.E => "Positive",
                _ => "Negative"
            };
        }

        public static Degree GetDegree(int points)
        {
            return points switch
            {
                >= 90               => Degree.A,
                >= 80 and <=89      => Degree.B,
                >= 70 and <=79      => Degree.C,
                >= 60 and <=69      => Degree.D,
                >= 50 and <=69      => Degree.E,
                _                   => Degree.F
            };
        }

        public static (string, bool)[] Exams((string name, int points, char exam)[] examInfo)
        {
            (string, bool)[] result = new (string, bool)[examInfo.Length];
            int i = 0;
            foreach(var item in examInfo)
            {
                result[i++] = (item.name,
                    item switch
                    {
                        //points >20 i pierwsza litera nazwy egzaminu
                        { points: > 20, exam: 'C' } => true,
                        { points: > 40, exam: 'B' } => true,
                        { points: > 30, exam: 'A' } => true,
                        _ => false
                    }
                    );
                
            }
            return result;
        }

        //record domyslnie jest klasy class
        //typ Wlasciwosc
        record Student(string Name, int Ects);

        static void Main(string[] args)
        {
            Degree studentDegree = Degree.A;
            Console.WriteLine("Your degree: " + (int)studentDegree * 0.1);

            Console.WriteLine();
            Console.WriteLine("All degrees");
            foreach (string name in Enum.GetNames<Degree>())
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            Console.WriteLine("All degrees with values");
            foreach (Degree degree in Enum.GetValues<Degree>())
            {
                Console.WriteLine($"{degree} {(int)degree}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter degree");
            string str = Console.ReadLine();
            try
            {
                studentDegree = Enum.Parse<Degree>(str);
                Console.WriteLine($"You entered degree {studentDegree} {(int)studentDegree}");
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Degree don't exist");
            }


            Console.WriteLine();
            Student student1 = new Student("Karol", 23);
            Console.WriteLine(student1);
        }
    }
}
