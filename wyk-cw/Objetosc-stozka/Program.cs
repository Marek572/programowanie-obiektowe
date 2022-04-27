using System;

namespace Objetosc_stozka
{
    class Program
    {
        public static void ConeVolume(double r, double l)
        {
            double pi = Math.PI;
            double h = Math.Sqrt(Math.Pow(l, 2) - Math.Pow(r, 2));
            double v = (pi * Math.Pow(r, 2) * h) / 3;

            if (r < 0 || l < 0)
                throw new Exception("ujemny argument");
            else if (l<r)
                throw new Exception("obiekt nie istnieje");
            else if (v == 0)
                throw new Exception($"0 0");
            else
            {
                double a = Math.Floor(v);
                double b = Math.Ceiling(v);
                Console.WriteLine($"{a} {b}");
                /*Console.WriteLine($"r= {r}, l= {l}, h= {h}, v= {v}");*/
            }

        }


        static void Main(string[] args)
        {
            string readValues = Console.ReadLine();
            double[] values = Array.ConvertAll<string, double>(readValues.Split(" "), double.Parse);

            double r = values[0];
            double l = values[1];

            if (values.Length < 1)
                Console.WriteLine("za malo danych");
            else if (values.Length > 2)
                Console.WriteLine("za duzo danych");
            else
                ConeVolume(r, l);
        }
    }
}
