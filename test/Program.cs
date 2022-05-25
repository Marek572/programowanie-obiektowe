using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        Complex a = new Complex() { Re = 6d, Im = 2d };
        double factor = 2.0d;
        int points = 0;
        if ((a / factor).Re == 3 && (a / factor).Im == 1 && (new Complex() { Re = 1, Im = -5 } / -1).Im == 5)
        {
            Console.WriteLine("Zadanie 1: Ok");
            points++;
        }
        if (new Complex() { Re = 2, Im = 2 } > new Complex() { Re = 1, Im = 1 } &&
            new Complex() { Re = 1, Im = -1 } < new Complex() { Re = 3, Im = 3 })
        {
            Console.WriteLine(("Zadanie 2: Ok"));
            points++;
        }

        if (new Complex() { Re = 1, Im = 2 }.Equals(new Complex() { Re = 1, Im = 2 }) &&
            !new Complex() { Re = 1, Im = 2 }.Equals(new Complex() { Re = 1, Im = 3 }))
        {
            Console.WriteLine(("Zadanie 3: Ok"));
            points++;
        }

        List<Complex> list = new List<Complex>()
        {
            new Complex() {Re = -1, Im = 1},
            new Complex() {Re = 2, Im = -2},
            new Complex() {Re = 3, Im = 3},
            new Complex() {Re = -4, Im = 4},
            new Complex() {Re = -1, Im = -1},
            new Complex() {Re = 0, Im = 2},
        };
        Task4(list);
        if (list[0] == new Complex() { Re = -4, Im = 4 } && list[1] == new Complex() { Re = -1, Im = -1 } &&
            list[5] == new Complex() { Re = 3, Im = 3 })
        {
            Console.WriteLine(("Zadanie 4: Ok"));
            points++;
        }

        var result1 = Task5(list);
        var result2 = Task5(Enumerable.Repeat<Complex>(new Complex() { Re = 3, Im = 3 }, 2));
        if (result1 == 5.65685424949238 && result2 == 8.48528137423857)
        {
            Console.WriteLine(("Zadanie 5: Ok"));
            points++;
        }
        Console.WriteLine($"Suma punktów: {points}");
    }

    internal class Complex
    {
        public double Re { get; set; }
        public double Im { get; set; }

        public double Module()
        {
            return Math.Sqrt(Math.Pow(Re, 2) + Math.Pow(Im, 2));
        }

        public static Complex? Of(double re, double im)
        {
            return re < 0 ? null : new Complex() { Re=re, Im=im };
        }

        //Zadanie 1
        //zdefiniuj operator dzielenia liczby zespolonej przez wartość rzeczywistą typu double
        //Przykład
        //Complex result = new Complex(){ Re = 4, Im = 2} / 2d;
        //Console.WriteLine(result);        // {Re = 2.0000, Im = 1.0000}

        public static Complex operator /(Complex complex, double d)
        {
            if (complex == null)
            {
                throw new ArgumentNullException(nameof(complex));
            }

            return new Complex() { Re = complex.Re / d, Im = complex.Im / d };
        }

        public static Complex operator /(double d, Complex complex)
        {
            if (complex == null)
            {
                throw new ArgumentNullException(nameof(complex));
            }

            return new Complex() { Re= d / complex.Re, Im= d / complex.Im };
        }

        //Zadanie 2
        //Zdefiniuj relacji < i > dla klasy Complex. Liczba większa to taka, której obie składowe Re i Im są większe do składowych drugiego obiektu
        //Przykłady
        //bool IsGreater = new Complex(){Re = 2, Im = 3} > new Complex(){Re = 1, Im = 2}; //true
        //bool IsLower = new Complex(){Re = 1, Im = 2} < new Complex(){Re = 1, Im = 2}; //false

        public static bool operator >(Complex complex1, Complex complex2)
        {
            return complex1.Im > complex2.Im && complex1.Re > complex2.Re;
        }

        public static bool operator <(Complex complex1, Complex complex2) => !(complex1 > complex2);

        //Zadanie 3
        //Zdefiniuj metodę ToString(), GetHashCode i Equals. Dwie liczby są sobie równe jeśli odpowiednio ich pola Re i Im są sobie równe
        //Przykład
        //Console.Write(new Complex(){Re = 1, Im = 2}); //{ Re= 1.000000, Im = 2.00000}
        //new Complex(){Re = 1, Im = 2}.Equals(new Complex(){Re = 1, Im = 2});  //true
        //new Complex(){Re = 1, Im = 2}.Equals(new Complex(){Re = 2, Im = 2});  //false

        public override string ToString()
        {
            return $"Re = {Re}, Im = {Im}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Re, Im);
        }

        public override bool Equals(object obj)
        {
            return obj is Complex complex &&
                   Re == complex.Re &&
                   Im == complex.Im;
        }

    }
    //Zadanie 4
    //Zaimplementuj metodę, aby sortowała listę liczb zespolonowych w porządku rosnących części rzeczywistych, a dla liczb
    //o tych samych wartościach rzeczywistych decyduje część urojona
    static void Task4(List<Complex> list)
    {

    }

    //Zadanie 5
    //Zaimplementuj metodę za pomoca LINQ, aby zwróciła sumę modułów liczb, o identycznych składowych Re == Im 
    static double Task5(IEnumerable<Complex> list)
    {
        return 0;
    }

}