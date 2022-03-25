using System;
using System.Globalization;

namespace Parametry_trojkata
{
    class Program
    {
        //WARUNEK BUDOWY TRÓJKĄTA
        public static void Triangle(double[] sides, out bool isTriangle)
        {
            //warunek długości boków dodatnie
            foreach (var side in sides)
            {
                if (side <= 0)
                {
                    Console.WriteLine("Błędne dane. Długości boków muszą być dodatnie!");
                    isTriangle = false;
                    return;
                }
            }
            //warunek ilość boków
            if (sides.Length <= 2)
            {
                Console.WriteLine("Błędne dane. Trójkąt wymaga 3 boków!");
                isTriangle = false;
            }
            else if (sides.Length >= 4)
            {
                Console.WriteLine("Błędne dane. Za dużo boków!");
                isTriangle = false;
            }
            //warunek suma długści boków
            else if (sides[0] + sides[1] < sides[2] || sides[1] + sides[2] < sides[0] || sides[2] + sides[0] < sides[1])
            {
                Console.WriteLine("Błędne dane. Trójkąta nie można zbudować!");
                isTriangle = false;
            }
            else
                isTriangle = true;
        }

        static void Main(string[] args)
        {
            string readSides = Console.ReadLine();
            //TODO: zaokraglenie do 2 miejsc po przecinku
            double[] sides = Array.ConvertAll<string, double>(readSides.Split("; "), double.Parse);
            bool isTriangle;

            Triangle(sides, out isTriangle);

            //jeśli dane poprawne
            if (isTriangle)
            {
                //obwód
                double perimeter = 0;
                foreach (var s in sides)
                {
                    perimeter += s;
                }
                perimeter = Math.Round(perimeter, 2);
                Console.WriteLine($"obwód = {perimeter:F2}");

                //pole
                double area = 0;
                double p = 0;
                foreach (var side in sides)
                {
                    p += side;
                }
                p /= 2;
                area = Math.Sqrt(p * (p - sides[0]) * (p - sides[1]) * (p - sides[2]));
                area = Math.Round(area, 2);
                Console.WriteLine($"pole = {area:F2}");

                //typ
                //ostrokątny
                if (Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) < Math.Pow(sides[2], 2)
                    || Math.Pow(sides[1], 2) + Math.Pow(sides[2], 2) < Math.Pow(sides[0], 2)
                    || Math.Pow(sides[2], 2) + Math.Pow(sides[0], 2) < Math.Pow(sides[1], 2))
                {
                    Console.WriteLine("trójkąt jest rozwartokątny");
                    if(sides[0] == sides[1] || sides[1] == sides[2] || sides[0] == sides[2])
                        Console.WriteLine("trójkąt równoramienny");
                }
                //prostokątny
                else if (Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) == Math.Pow(sides[2], 2)
                        || Math.Pow(sides[1], 2) + Math.Pow(sides[2], 2) == Math.Pow(sides[0], 2)
                        || Math.Pow(sides[2], 2) + Math.Pow(sides[0], 2) == Math.Pow(sides[1], 2))
                    Console.WriteLine("trójkąt jest prostokątny");
                //rozwartokątny
                else if (Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) > Math.Pow(sides[2], 2)
                        || Math.Pow(sides[1], 2) + Math.Pow(sides[2], 2) > Math.Pow(sides[0], 2)
                        || Math.Pow(sides[2], 2) + Math.Pow(sides[0], 2) > Math.Pow(sides[1], 2))
                {
                    Console.WriteLine("trójkąt jest ostrokątny");
                    if (sides[0] == sides[1] && sides[1]== sides[2] && sides[0] == sides[2])
                        Console.WriteLine("trójkąt równoboczny");
                    else if (sides[0] == sides[1] || sides[1] == sides[2] || sides[0] == sides[2])
                        Console.WriteLine("trójkąt równoramienny");
                }
            }
        }
    }
}
