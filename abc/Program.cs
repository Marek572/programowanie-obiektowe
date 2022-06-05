using System;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

class App
{
    public static void Main(string[] args)
    {
        Car[] cars = new Car[]
        {
             new Car(),
             new Car(Model: "Fiat", true),
             new Car(),
             new Car(Power: 100),
             new Car(Model: "Fiat", true),
             new Car(Power: 125),
             new Car()
            };
        Console.WriteLine(Exercise3.CarCounter(cars));
    }
}

enum Direction8
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    UP_LEFT,
    DOWN_LEFT,
    UP_RIGHT,
    DOWN_RIGHT
}

enum Direction4
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

//Cwiczenie 1
//Zdefiniuj metodę NextPoint, która powinna zwracać krotkę ze współrzędnymi piksela przesuniętego jednostkowo w danym kierunku względem piksela point.
//Krotka screenSize zawiera rozmiary ekranu (width, height)
//Przyjmij, że początek układu współrzednych (0,0) jest w lewym górnym rogu ekranu, a prawy dolny ma współrzęne (witdh, height) 
//Pzzykład
//dla danych wejściowych
//(int, int) point1 = (2, 4);
//Direction4 dir = Direction4.UP;
//var point2 = NextPoint(dir, point1);
//w point2 powinny być wartości (2, 3);
//Jeśli nowe położenie jest poza ekranem to metoda powinna zwrócić krotkę z point
class Exercise1
{
    public static (int, int) NextPoint(Direction4 direction, (int, int) point, (int, int) screenSize)
    {
        return direction switch
        {
            Direction4.UP => (point.Item2 + 1, point.Item1),
            Direction4.DOWN => (point.Item2 - 1, point.Item1),
            Direction4.LEFT => (point.Item2, point.Item1 - 1),
            Direction4.RIGHT => (point.Item2, point.Item1 + 1),
            _ => point
        };
    }
}
//Cwiczenie 2
//Zdefiniuj metodę DirectionTo, która zwraca kierunek do piksela o wartości value z punktu point. W tablicy screen są wartości
//pikseli. Początek układu współrzędnych (0,0) to lewy górny róg, więc punkt o współrzęnych (1,1) jest powyżej punktu (1,2) 
//Przykład
// Dla danych weejsciowych
// static int[,] screen =
// {
//    {1, 0, 0},
//    {0, 0, 0},
//    {0, 0, 0}
// };
// (int, int) point = (1, 1);
// po wywołaniu - Direction8 direction = DirectionTo(screen, point, 1);
// w direction powinna znaleźć się stała UP_LEFT
class Exercise2
{
    static int[,] screen =
    {
        {1, 0, 0},
        {0, 0, 0},
        {0, 0, 0}
    };

    private static (int, int) point = (1, 1);

    private Direction8 direction = DirectionTo(screen, point, 1);

    public static Direction8 DirectionTo(int[,] screen, (int, int) point, int value)
    {
        var length = screen.GetLength(0);
        var newpoint = (1, 1);
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < length; i++)
            {
                if (screen[i, j] == 1) { newpoint.Item1 = i; newpoint.Item2 = j; }
            }
        }
        Direction8 wynik = Direction8.DOWN;
        if (newpoint.Item1 - point.Item1 < 0)
        {
            if (newpoint.Item2 - point.Item2 < 0)
            {
                wynik = Direction8.UP_LEFT;
            }
            else if (newpoint.Item2 - point.Item2 > 0)
            {
                wynik = Direction8.DOWN_LEFT;
            }
            else
            {
                wynik = Direction8.LEFT;
            }
        }
        else if (newpoint.Item1 - point.Item1 > 0)
        {
            if (newpoint.Item2 - point.Item2 < 0)
            {
                wynik = Direction8.UP_RIGHT;
            }
            else if (newpoint.Item2 - point.Item2 > 0)
            {
                wynik = Direction8.DOWN_RIGHT;
            }
            else
            {
                wynik = Direction8.RIGHT;
            }
        }
        else
        {
            if (newpoint.Item2 - point.Item2 > 0)
            {
                wynik = Direction8.UP;
            }
            else if (newpoint.Item2 - point.Item2 < 0)
            {
                wynik = Direction8.DOWN;
            }

        }

        return wynik;
    }
}

//Cwiczenie 3
//Zdefiniuj metodę obliczającą liczbę najczęściej występujących aut w tablicy cars
//Przykład
//dla danych wejściowych
// Car[] _cars = new Car[]
// {
//     new Car(),
//     new Car(Model: "Fiat", true),
//     new Car(),
//     new Car(Power: 100),
//     new Car(Model: "Fiat", true),
//     new Car(Power: 125),
//     new Car()
// };
//wywołanie CarCounter(Car[] cars) powinno zwrócić 3
record Car(string Model = "Audi", bool HasPlateNumber = false, int Power = 100);

class Exercise3
{
    public static int CarCounter(Car[] cars)
    {
        List<Car> carsHelper = new();

        foreach (Car car in cars)
        {
            if (carsHelper.IndexOf(car) == -1)
            {
                carsHelper.Add(car);

            }
        }
        return carsHelper.Count;
    }
}


record Student(string LastName, string FirstName, char Group, string StudentId = "");
//Cwiczenie 4
//Zdefiniuj metodę AssignStudentId, która każdemu studentowi nadaje pole StudentId wg wzoru znak_grupy-trzycyfrowy-numer.
//Przykład
//dla danych wejściowych
//Student[] students = {
//  new Student("Kowal","Adam", 'A'),
//  new Student("Nowak","Ewa", 'A')
//};
//po wywołaniu metody AssignStudentId(students);
//w tablicy students otrzymamy:
// Kowal Adam 'A' - 'A001'
// Nowal Ewa 'A'  - 'A002'
//Należy przyjąc, że są tylko trzy możliwe grupy: A, B i C
class Exercise4
{
    public static void AssignStudentId(Student[] students)
    {
        int ACounter = 1;
        int BCounter = 1;
        int CCounter = 1;

        for (int i = 0; i < students.Length; i++)
        {
            if (students[i].Group == 'A')
            {
                /*student.StudentId = "A00" + ACounter.ToString();*/
                students[i] = students[i] with { StudentId = "A00" + ACounter.ToString() };
                ACounter++;
            }
            else if (students[i].Group == 'B')
            {
                students[i] = students[i] with { StudentId = "B00" + BCounter.ToString() };
                BCounter++;
            }
            else
            {
                students[i] = students[i] with { StudentId = "C00" + CCounter.ToString() };
                CCounter++;
            }
        }


    }
}