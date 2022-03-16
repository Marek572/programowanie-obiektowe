using System;
using System.Collections.Generic;

namespace Lab3
{
    class Program
    {
        public abstract class Vehicle
        {
            public double Weight { get; init; } //init tylko do odczytu tak jak readonly
            public int MaxSpeed { get; init; }
            protected int _mileage;
            public int Mealeage
            {
                get { return _mileage; }
            }
            //metoda abstrakcyjna
            public abstract decimal Drive(int distance);
            public override string ToString()
            {
                return $"Vehicle{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage} }}";
            }
        }

        public class Car : Vehicle
        {
            public bool isFuel { get; set; }
            public bool isEngineWorking { get; set; }
            public override decimal Drive(int distance)
            {
                if (isFuel && isEngineWorking)
                {
                    _mileage += distance;
                    return (decimal)(distance / (double)MaxSpeed);
                }
                return -1;
            }
            public override string ToString()
            {
                return $"Car{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}";
            }
        }

        public class Bicycle : Vehicle
        {
            public bool isDriver { get; set; }
            public override decimal Drive(int distance)
            {
                if (isDriver)
                {
                    _mileage += distance;
                    return (decimal)(distance / (double)MaxSpeed);
                }
                return -1;
            }
            public override string ToString()
            {
                return $"Bicycle{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}"; ;
            }
        }

        public abstract class Scooter : Vehicle
        {
            public bool isLight { get; set; }
            public bool isBrake { get; set; }

            public override decimal Drive(int distance)
            {
                if (isLight && isBrake)
                {
                    _mileage += distance;
                    return (decimal)(distance / (double)MaxSpeed);
                }
                return -1;
            }
            public override string ToString()
            {
                return $"Vehicle{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage} }}";
            }
        }

        public abstract class ElectricScooter : Scooter
        {
            public int BatteryLevel { get; set; }
            public int MaxRange { get; init; }

            public override decimal Drive(int distance)
            {
                if (isLight && isBrake)
                {
                    _mileage += distance;
                    return (decimal)(distance / (double)MaxSpeed);
                }
                return -1;
            }
            
            public void RechargeBattery()
            {
                BatteryLevel = 100;
            }

            public override string ToString()
            {
                return $"Vehicle{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage} }}";
            }
        }

        /*interface IElectric
        {
            int Supply()
            {
                throw new NotImplementedException();
            }
        }

        public class Scooter : Vehicle, IElectric
        {
            public override decimal Drive(int distance)
            {
                throw new NotImplementedException();
            }

            int Supply()
            {
                throw new NotImplementedException();
            }
        }

        public class Cooker : IElectric
        {
            int Supply()
            {
                throw new NotImplementedException();
            }
        }*/

        interface IAggregate
        {
            IIterator createIterator();
        }

        interface IIterator
        {
            int GetFirst();
            bool HasNext();
            int GetNext();
        }

        class IntAggregate : IAggregate
        {
            internal int _a = 4;
            internal int _b = 6;
            internal int _c = 2;

            public IIterator createIterator()
            {
                return new IntIterator(this);
            }
        }

        class IntIterator : IIterator
        {
            private IntAggregate _aggregate;
            private int count = 0;

            public IntIterator(IntAggregate aggregate)
            {
                _aggregate = aggregate;
            }

            public int GetFirst()
            {
                return _aggregate._a;
            }

            public int GetNext()
            {
                if(count == 3)
                {
                    return _aggregate._c;
                }
                switch(++count)
                {
                    case 1: return _aggregate._a;
                    case 2: return _aggregate._b;
                    case 3: return _aggregate._c;
                    default: throw new Exception();
                }
            }

            public bool HasNext()
            {
                return count < 3;
            }
        }

        static void Main(string[] args)
        {
            Car car = new Car() { isEngineWorking = true, isFuel = true, MaxSpeed = 100 };
            //do klasy abstrakcyjnej mozna przypisac obiekt
            Vehicle vehicle = car;
            Vehicle anotherVehicle = new Bicycle();
            Vehicle[] vehicles = new Vehicle[3];
            vehicles[0] = car;
            vehicles[1] = anotherVehicle;
            vehicles[2] = new Car();

            foreach (Vehicle v in vehicles)
            {
                Console.WriteLine(v);
                Console.WriteLine(v.Drive(14));
                if (v is Car)
                {
                    //takie rzutowanie musi byc poprzedzone powyzszym if'em; if(x is ...)
                    Car currentCar = (Car)v; //lub "v as Car"
                    Console.WriteLine(currentCar.isEngineWorking);
                }
            }

            IElectric[] electrics = new IElectric[3];
            electrics[0] = new Scooter();
            electrics[1] = new Cooker();


            Console.WriteLine();
            Console.WriteLine("Int iteration");
            IAggregate aggregate = new IntAggregate();
            IIterator iterator = aggregate.createIterator();
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext());
            }
            List<string> names = new List<string>()
            {
                "Adam",
                "Ewa",
                "Karol"
            };


            Console.WriteLine();
            Console.WriteLine("List iteration");
            List<string>.Enumerator enumerator = names.GetEnumerator();
            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            /*TO JEST TO SAM CO WYZEJ!
            foreach(var name in names) 
            {
                Console.WriteLine(name);
            }*/
        }
    }
}

/* NOTES:
 * klasa abstrakcyja - wydzielenie wspolnych cech (w tym wypadku) wszystkich pojazdow, przez klase abstrakcyjna nie robi sie obiektow
 * interface - szczegolny przypadek klasy abstrakcyjnej
 */
