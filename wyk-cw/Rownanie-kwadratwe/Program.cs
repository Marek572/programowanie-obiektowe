using System;

namespace Rownanie_kwadratwe
{
    class Program
    {
        public static void QuadraticEquation(long a, long b, long c)
        {
            long delta = (int)Math.Pow(b, 2) - (4 * a * c);
            Console.WriteLine("Delta: " + delta);

            if (a == 0 && b == 0 && c == 0)
                Console.WriteLine("infinity");
            else if(a == 0 && b == 0)
                Console.WriteLine("empty");
            else if (a == 0)
            {
                double x = -((double)c / (double)b);

                Console.WriteLine($"x={x:F2}");
            }
            else if (delta > 0)
            {
                double x1 = (double)((-b) - Math.Sqrt(delta)) / (2 * a);
                double x2 = (double)((-b) + Math.Sqrt(delta)) / (2 * a);

                Console.WriteLine($"x1={x1:F2}");
                Console.WriteLine($"x2={x2:F2}");
            }
            else if (delta == 0)
            {
                try
                {
                    double x = (-b) / (2 * a);
                    Console.WriteLine($"x={x:F2}");
                }
                catch
                {
                    Console.WriteLine("empty");
                }
            }
            else if (delta < 0)
                Console.WriteLine("empty");
        }

        static void Main(string[] args)
        {
            string readIntegers = Console.ReadLine();
            int[] integers = Array.ConvertAll<string, int>(readIntegers.Split("; "), int.Parse);

            int a = integers[0];
            int b = integers[1];
            int c = integers[2];

            if (integers.Length > 3)
                Console.WriteLine("za duzo danych");

            
            QuadraticEquation(a, b, c);

        }
    }
}
