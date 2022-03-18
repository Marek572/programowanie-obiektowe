using System;

namespace Parametry_trojkata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj dlugosc boku a; b; c");
            string readSides = Console.ReadLine();
            //TODO: zaokraglenie do 2 miejsc po przecinku
            double[] sides = Array.ConvertAll<string, double>(readSides.Split("; "), double.Parse);
            //WARUNEK BUDOWY TRÓJKĄTA
            //warunek długości boków dodatnie
            foreach (var s in sides)
            {
                if(s < 0)
                    Console.WriteLine("Błędne dane. Długości boków muszą być dodatne!");
            }
            //warunek ilość boków
            if (sides.Length <= 2)
                Console.WriteLine("Błędne dane. Trójkąt wymaga 3 boków!");
            if(sides.Length >= 4)
                Console.WriteLine("Błędne dane. Za dużo boków!");
            //warunek suma długści boków
            if(sides[0] + sides[1]<= sides[2] && sides[1] + sides[2] <= sides[0] && sides[2] + sides[0] <= sides[1])
                Console.WriteLine("Błędne dane. Trójkąta nie można zbudować!");

            //jeśli dane poprawne
            //obwód
            double perimeter = 0;
            foreach(var s in sides)
            {
                perimeter += s;
            }
            perimeter = Math.Round(perimeter, 2);
            Console.WriteLine($"Perimeter: {perimeter}");
            //pole
            double area = 0;
        }
    }
}
