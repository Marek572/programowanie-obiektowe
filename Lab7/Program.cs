using System;

namespace Lab7
{
    class Program
    {
        delegate double operation(double a, double b);

        delegate bool stringPredicate(string s);
        
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Mul(double a, double b)
        {
            return a * b;
        }

        public static bool fiveChars(string s)
        {
            return s.Length == 5;
        }

        static void Main(string[] args)
        {
            operation op = Add;

            //invoke do wywolania operacji
            Console.WriteLine(op.Invoke(4,6));

            op = Mul;
            Console.WriteLine(op.Invoke(4, 6));


            stringPredicate predicate = fiveChars;
            Console.WriteLine(predicate.Invoke("abcdef"));

            //typy argumentow a ostatnia typ zwracalny
            Func<double, double, double> funcOperator = delegate (double a, double b)
            {
                return a * b;
            };
            Func<int, string> formatDelegate = delegate (int i)
            {
                return string.Format("{0:x}", i);
            };
            Console.WriteLine(formatDelegate.Invoke(14));


            Predicate<string> OnlyFive = fiveChars;
            Predicate<int> InRange100 = delegate (int i)
            {
                return i >= 0 && i <= 100;
            };
            Console.WriteLine(InRange100.Invoke(60));

            Func<int, int, int, bool> InRangeMinMax = delegate (int value, int min, int max)
            {
                return value >= min && value <= max;
            };

            Action<string> Print = delegate (string s)
            {
                Console.WriteLine(s);
            };

            //jesli mamy typ po lewej stronie to nie trzeba go z prawej definiowac
            Action<string> PrintLambda = s => Console.WriteLine(s);
            Func<int, int, int, bool> InRangeLambda = (value, min, max) => value >= min && value <= max;

            operation DIV = (a, b) =>
            {
                if (b != 0)
                    return a / b;
                else
                    throw new Exception("b is 0");
            };
            Console.WriteLine(DIV.Invoke(2,4));
        }
    }
}
