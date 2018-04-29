using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //własne typy danych
            Student s1 = new Student();
            s1.imie = "antek";
            s1.nazwisko = "kowal";
            s1.nrIndexu = "12349";

            //druga metoda z uzyciem klamer
            Student s2 = new Student { imie = "Andrzej", nazwisko = "Nowak", nrIndexu = "12345" };
            //ORM -- object relations matter, polaczenia z bazami danych z OOP -- korzystamy z encji do łączenia sie z bazami danych

            Console.WriteLine(s1);
            Console.WriteLine(s2);

            //SORTOWANIE
            List<int> liczby = new List<int> ( new[]{5, 4, 3} );
            foreach (int liczba in liczby)
                Console.WriteLine(liczba);

            List<Student>studenci = new List<Student>(
                new[] { s1, s2 });
            studenci.Sort();
            foreach (Student s in studenci)
                Console.WriteLine(s);
            //HashSet -- zbior nie moze miec duplikatow, bardzo szybki w sprawdzaniu etc...nie jest zagwarantowana kolejnosc dodawani, powinna byc ale nie musi !!!
            //zbior 
            HashSet<int> zbior = new HashSet<int>();
            zbior.Add(3);
            zbior.Add(4);
            zbior.Add(3);
            foreach (int liczba in zbior) //foreach szersze spektrum niz for()
                Console.WriteLine(liczba);
            //zlicz studentow - liczy instancje obiektow -- jezeli dane beda takie same to ich nie prowna tylko i tak policzy
            HashSet <Student> zb = new HashSet<Student>(studenci);
            Console.WriteLine(studenci.Count());
            HashSet<int> lotek = new HashSet<int>();
            Lotto(lotek);
            foreach (int liczba in lotek)
                Console.WriteLine(liczba);
            SortedSet<int> drugi = new SortedSet<int>();
            Lotto(drugi);
            Console.ReadKey();
        }
        static void Lotto(HashSet<int> zbior)
        {
            Random rand = new Random();
            while (zbior.Count < 6)
                zbior.Add(rand.Next(49) + 1);
        }
    }

    class Student : IComparable<Student>         //komparator generyczny
                                                 //klasa dziedziczy po klasie object
    {
        public String imie { get; set; }
        public String nazwisko { get; set; }
        public String nrIndexu { get; set; }

        public int CompareTo(Student other)
        {
            //throw new NotImplementedException(); defaultowy rzut wyjatku... 
            int wynik = this.nazwisko.CompareTo(other.nazwisko);
            if (wynik == 0)
                return this.imie.CompareTo(other.imie);
            else
                return -wynik;  //mozemy odwrocic sortowanie dodajac minus -wuynik lub wynik
        }

        public override string ToString() //ominiecie dziedziczenia po klasie object
        {
            //sterujemy jak chcemy miec wyswietlane dane
            return String.Format("{0} {1}: {2}", imie, nazwisko, nrIndexu);
            //return base.ToString();
        }
        public override bool Equals(object obj)
        {
            //return base.Equals(obj); -- overload!
            if (obj is Student) // upenij sie ze obj to Student
            {
                Student inny = (Student)obj; //rzutowanie -- typecast
                return this.nrIndexu == inny.nrIndexu;
            }
            else
                return false;
        }

    }
    public override int GetHashCode()
    {
        return base.GetHashCode()
            {
            return nr = int.Parse(this.nrIndexu);
        };
    }

}
