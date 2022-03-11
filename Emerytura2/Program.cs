using System;

namespace Emerytura2
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            int retirementAge = int.Parse(Console.ReadLine());

            int toRetirement = retirementAge - age;
            int firstDigit = toRetirement;
            while (firstDigit >= 10)
            {
                firstDigit /= 10;
            }
            int lastDigit = toRetirement % 10;

            Console.WriteLine($"Witaj, {name}!");
            if (age < 0 || retirementAge < 0)
            {
                Console.WriteLine("Wiek nie może być ujemny!");

            }
            else if (toRetirement <= 0)
            {
                Console.WriteLine("Jesteś emerytem!");
            }
            else if (firstDigit == 1 && (lastDigit == 2 || lastDigit == 3 || lastDigit == 4))
            {
                Console.WriteLine($"Do emerytury brakuje Ci {toRetirement} lata!");
            }
            else if (lastDigit == 1)
            {
                Console.WriteLine($"Do emerytury brakuje Ci {toRetirement} rok!");
            }
            else
            {
                Console.WriteLine($"Do emerytury brakuje Ci {toRetirement} lat!");
            }
        }
    }
}
