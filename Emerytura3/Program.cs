using System;

namespace Emerytura3
{
    class Program
    {
        static void Main(string[] args)
        {
            string allData = Console.ReadLine();
            string[] splitData = allData.Split(' ');
            string name = splitData[0];
            int age = int.Parse(splitData[1]);
            int retirementAge = int.Parse(splitData[2]);

            int toRetirement = retirementAge - age;
            int firstDigit = toRetirement;
            while (firstDigit >= 10)
            {
                firstDigit /= 10;
            }
            int lastDigit = toRetirement % 10;

            if (age < 0 || retirementAge < 0)
            {
                Console.WriteLine("Wiek nie może być ujemny!");

            }
            else if (toRetirement <= 0)
            {
                Console.Write($"Witaj emerycie {name}!");
            }
            else if (firstDigit != 1 && (lastDigit == 2 || lastDigit == 3 || lastDigit == 4))
            {
                Console.Write($"Witaj {name}! Do emerytury brakuje Ci {toRetirement} lata!");
            }
            else if (toRetirement == 1)
            {
                Console.Write($"Witaj {name}! Do emerytury brakuje Ci {toRetirement} rok!");
            }
            else
            {
                Console.Write($"Witaj {name}! Do emerytury brakuje Ci {toRetirement} lat!");
            }
        }
    }
}
