using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            //PERSONPROPETIES
            //odwolanie do argumentu obiektu:
            //Klasa nazwa = new Konstruktor();
            //Person person = new Person();
            PersonPrperties person = PersonPrperties.Of("Robert");
            Console.WriteLine(person.Name);

            //parsing string with date to a date field
            DateTime dateTime = DateTime.Parse("02.03.2022");
            Console.WriteLine(dateTime);
            //jedyna sposob na modyfikacje DateTime np: value.AddDays
            DateTime newDate = dateTime.AddDays(2);
            Console.WriteLine(dateTime + " two days later... " + newDate);

            string name1 = "adam";
            string name2 = "adam";
            string v = name1.Substring(0, 2);
            Console.WriteLine(name1 == name2);

            //MONEY
            Money money = Money.Of(10, Currency.PLN);
            Console.WriteLine($"money equals {money.Value}");
            //money * 5 -> (money, 5)
            Money result1 = money * 5;
            Money result2 = 2 * money;
            Console.WriteLine($"result1 is {result1.Value} and result2 is {result2.Value}");
            Money sum = money + result1;
            Console.WriteLine($"sum of money and result1 = {sum.Value}");
            Console.WriteLine($"is sum < money? -> {sum < money}");
            Console.WriteLine($"is money == money? -> {money == Money.Of(10, Currency.PLN)}");
            Console.WriteLine($"is money != money? -> {money != Money.Of(10, Currency.PLN)}");

            /*long a = 10L;
            a = 10000000;
            int b = 5;
            a = b;
            b = (int)a;*/

            //projekcja
            decimal amount = money;
            double cost = (double)money;
            float test = (float)money;
            Console.WriteLine(amount);
            Console.WriteLine(cost);
            Console.WriteLine(test);

            //ToString()
            Console.WriteLine("ToString");
            Console.WriteLine(money.ToString());
            Console.WriteLine();

            //sort
            money.Equals(cost);

            Money[] prices =
            {
                Money.Of(14, Currency.PLN),
                Money.Of(37, Currency.USD),
                Money.Of(21, Currency.PLN),
                Money.Of(17, Currency.EUR),
                Money.Of(15, Currency.PLN),
            };

            Array.Sort(prices);
            foreach (var p in prices)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine();

            //TANK
            Tank tank1 = new(150);
            Tank tank2 = new(200);

            tank1.Refuel(100);
            tank2.Refuel(50);
            Console.WriteLine($"Tank1: {tank1}, Tank2: {tank2}");
            tank1.Consume(50);
            Console.WriteLine($"Tank1: {tank1}, Tank2: {tank2}");
            tank1.Refuel(tank2, 50);
            Console.WriteLine($"Tank1: {tank1}, Tank2: {tank2}");
        }
    }

    public class PersonPrperties
    {
        private string _name;

        //konstruktor z argumentem
        private PersonPrperties(string name)
        {
            _name = name;
        }

        public static PersonPrperties Of(string name)
        {
            //if valid -> constructor
            if (name != null && name.Length >= 3)
            {
                return new PersonPrperties(name);
            }
            else
            {
                //exception
                throw new ArgumentOutOfRangeException("Name too short");
            }
        }

        //getter&setter bez validacji:
        //public string Name {get;set;}
        //getter&setter z validacja:
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null && value.Length >= 3)
                {
                    _name = value;
                }
                else
                {
                    //wyjatek (exception)
                    throw new ArgumentOutOfRangeException("Name too short");
                }
            }
        }

        public override string ToString()
        {
            return _name;
        }
    }

    public class Money : IEquatable<Money>, IComparable<Money>
    {
        private readonly decimal _value;
        private readonly Currency _currency;

        //konstruktor
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        //statyczna metoda ktora pilnuje aby nie wpisac wartosci na minusie
        public static Money Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }
        public static Money OfWithException(decimal value, Currency currency)
        {
            //if valid -> constructor
            if (value < 0)
            {
                return Money.Of(value, currency);
            }
            else
            {
                //wyjatek (exception)
                throw new Exception("No money!");
            }
        }

        //metoda ktora parse'uje string na decimal
        public static Money ParseValue(string valueStr, Currency currency)
        {
            decimal valueDec = decimal.Parse(valueStr);
            return Money.Of(valueDec, currency);
        }

        //math operator z walsym typem, bedzie dzialal w obie strony gdy zdefiniujemy druga metode z odwrocnymi wartosciami
        public static Money operator *(Money a, decimal b)
        {
            return Money.Of(a._value * b, a._currency);
        }
        public static Money operator *(decimal b, Money a)
        {
            return Money.Of(b * a._value, a._currency);
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new ArgumentException("Different currencies!");
            return Money.Of(a._value + b._value, a._currency);
        }

        //operator > wymaga zdefiniowania operatora <
        public static bool operator >(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new ArgumentException("Different currencies!");
            return a.Value > b.Value;
        }
        public static bool operator <(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new ArgumentException("Different currencies!");
            return a.Value < b.Value;
        }
        public static bool operator ==(Money a, Money b)
        {
            return a.Value == b.Value && a.Currency == b.Currency;
        }
        public static bool operator !=(Money a, Money b)
        {
            //negacja operatora rownosci
            return !(a == b);
        }

        //niejawna projekcja na decimal
        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }

        //jawna projekcja na double (trzeba dodac nawiasy)
        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }

        //jawna projekcja na float
        public static explicit operator float(Money money)
        {
            return (float)money.Value;
        }

        public override string ToString()
        {
            return $"Value: {_value}, Currency: {_currency}";
        }

        public override bool Equals(object obj)
        {
            return obj is Money money &&
                   _value == money._value &&
                   _currency == money._currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _currency);
        }

        public bool Equals(Money other)
        {
            return _value == other._value &&
                   _currency == other._currency;
        }

        public int CompareTo(Money other)
        {
            int result = _currency.CompareTo(other._currency);
            if (result == 0)
                return _value.CompareTo(other._value);
            return result;
        }

        //wlasciwosci
        public decimal Value
        {
            get { return _value; }
        }
        public Currency Currency
        {
            get { return _currency; }
        }

    }
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }

    public class Tank
    {
        public readonly int Capacity;
        private int _level;
        public Tank(int capacity)
        {
            Capacity = capacity;
        }
        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value < 0 || value > Capacity)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _level = value;
            }
        }
        public bool Refuel(int amount)
        {
            if (amount < 0)
            {
                return false;
            }
            if (_level + amount > Capacity)
            {
                return false;
            }
            _level += amount;
            return true;
        }

        public bool Consume(int amount)
        {
            if (amount > _level)
                return false;
            if (amount < 0)
                return false;
            _level -= amount;
            return true;
        }

        public bool Refuel(Tank sourceTank, int amount)
        {
            if (amount + _level > Capacity)
                return false;
            if (sourceTank._level < amount)
                return false;
            if (amount <= 0)
                return false;
            sourceTank._level -= amount;
            _level += amount;
            return true;
        }

        public override string ToString()
        {
            return $"Tank: Capacity: {Capacity}, Level: {_level}";
        }
    }

    public class Student : IComparable<Student>
    {
        private readonly string _firstName;
        private readonly string _surname;
        private readonly decimal _avgGrade;

        //constructor
        public Student(string firstName, string surname, decimal avgGrade)
        {
            _firstName = firstName;
            _surname = surname;
            _avgGrade = avgGrade;
        }

        public int CompareTo(Student? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return _firstName.CompareTo(other._firstName);
        }
    }

    public static class MoneyExtension
    {
        public static Money Percent(this Money money, decimal percent)
        {
            return Money.Of((money.Value * percent) / 100m, money.Currency) ?? throw new ArgumentException();
        }

        public static Money ToCurrency(this Money money, Currency currency, decimal course)
        {
            if (money.Currency == currency)
                return money;
            return Money.Of(money.Value * course, currency) ?? throw new ArgumentException();
        }
    }
}


/* NOTES:
 * metoda musi zwroic wartosc konstruktor nie
 * statyczna metoda to taka ktora jest przypisana do klas ktore tworza obiekt
 * konstruktor tylko tworzy obiekty
 * 
 * value object to np. string/DateTime
 * value object "jest niezmienny, nie ma tozsamosci"
 * Niezmienniczość lub niemutowalność, oznacza, że wartości przechowywane w obiekcie nie mogą ulec zmianie, czyli należy je deklarować jako readonly
 * Brak tożsamości powoduje, że nie ma potrzeby tworzenia nowych instancji obiektu dla tej samej wartości
*/

/* Exercises: all (do przejrzenia jeszcze raz!)
 * At lab: 1,2,3,4,5,6,7
 * At home: 8,9
*/