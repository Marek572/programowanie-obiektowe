using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab5
{
    class Program
    {
        class Team : IEnumerable<string>
        {

            public string Leader { get; init; }
            public string ScrumMaster { get; init; }
            public string Developer { get; init; }

            public IEnumerator<string> GetEnumerator()
            {
                //return new TeamEnumerator(this);

                //kazda linijka odpowiada za kolejne wywlywania MoveNext()
                yield return Leader;
                yield return ScrumMaster;
                yield return Developer;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        class TeamEnumerator : IEnumerator<string>
        {
            private Team _team;
            private int _count = -1;

            public TeamEnumerator(Team team)
            {
                _team = team;
            }

            //zwraca łancuch
            public string Current
            {
                get
                {
                    return _count switch
                    {
                        0 => _team.Leader,
                        1 => _team.ScrumMaster,
                        2 => _team.Developer,
                        _ => null
                    };
                }
            }

            //zwraca object
            object IEnumerator.Current => Current;

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                return ++_count < 3;
            }

            //zerowanie iteratora
            public void Reset()
            {
                _count = -1;
            }
        }

        class Parking : IEnumerable<string>
        {
            private string[] _cars = { "Fiar", "Audi", "BMW", null, "Ford" };
            public string this[char index]
            {
                //indeks jako litera alfabetu ('a', 'b', ...)
                get
                {
                    return _cars[index - 'a'];
                }
                set
                {
                    _cars[index - 'a'] = value;
                }
            }

            public IEnumerator<string> GetEnumerator()
            {
                foreach(string car in _cars)
                {
                    if(car != null)
                        yield return car;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }


        static void Main(string[] args)
        {
            Team team = new Team() { Leader = "Nowak", Developer = "Kos", ScrumMaster = "Marzec" };
            IEnumerator<string> members = team.GetEnumerator();

            Console.WriteLine("TEAM");
            Console.WriteLine("while");
            while(members.MoveNext())
            {
                Console.WriteLine(members.Current);
            }

            //members.Reset(); //nie dziala przy yeld return

            Console.WriteLine();
            Console.WriteLine("foreach");
            foreach(string member in team)
            {
                Console.WriteLine(member);
            }

            Console.WriteLine();
            Console.WriteLine("join");
            Console.WriteLine(string.Join(", ", team));

            //Parking
            Parking parking = new Parking();
            Console.WriteLine();
            Console.WriteLine("PARKING");
            parking['d'] = "Dacia";
            Console.WriteLine(string.Join(", ", parking));
        }
    }
}

/* NOTES:
 * index - 'a' - gdzie index to litera ('c' - 'a') dziala tak jak na cyfrach czyli wynik bedzie 2 (3 - 1)
 */
