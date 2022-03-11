using System;

namespace Emerytura1
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            int retirementAge = int.Parse(Console.ReadLine());

            int toRetirement = retirementAge - age;

            Console.WriteLine($"Witaj, {name}!");
            if (age < 0 || retirementAge < 0)
            {
                Console.WriteLine("Wiek nie może być ujemny!");

            }
            else if (toRetirement <= 0)
            {
                Console.WriteLine("Jesteś emerytem!");
            }
            else
            {
                Console.WriteLine($"Do emerytury brakuje Ci {toRetirement} lat!");
            }
        }
    }
}
