using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3_SPROGB
{
    class Program
    {
        static void Main(string[] args)
        {
            //--------------------------------------------------------------------------------------------------------
            /*Linq -- zapytania w j.podobnym do SQLa, natywnie do jezyka C#.
                Zapytanie pisane w sposob natywny - bez two. dodatkowych obiektow i ich wywolywania. 
                Zapisy moga byc stos wobec zrodel danych bez zwiazku z baza danych.
                *REQ. Linq Provider -> wobec dowolnego zrodla danych, kolekcje, MS SQL i dokumenty XML
                *Linq korzysta z konceptu wyrazen lambda, obsluga kolekcji etc ...
            */
            //--------------------------------------------------------------------------------------------------------
            List<User> lista = new List<User>();
            lista.Add(new User
            {
                PESEL = "12345",
                imie = "Jan",
                Nazwisko = "Kowalski"
            });
            lista.Add(new User
            {
                PESEL = "12665",
                imie = "Zenon",
                Nazwisko = "Kiepura"
            });
            lista.Add(new User
            {
                PESEL = "65465",
                imie = "Julisz",
                Nazwisko = "Cezar"
            });
            lista.Add(new User
            {
                PESEL = "54768",
                imie = "Adam",
                Nazwisko = "zBaranamiNieGadam"
            });
            lista.Add(new User
            {
                PESEL = "83838",
                imie = "Slawek",
                Nazwisko = "Janiga"
            });
            lista.Add(new User
            {
                PESEL = "123243",
                imie = "Adam",
                Nazwisko = "Czyzewski"
            });

            //znakdz usera po nazwisko w c# -- "vanilla" way :)
            User wybrany = null;
            foreach (User u in lista)
                if (u.Nazwisko == "Kowalski")
                    wybrany = u;
            Console.WriteLine(wybrany);
            //zadziala ale bez potraktowania kluczowego atrybutu -- moze zwrocic kolekcje a nie odpow. usera.
            wybrany = lista.
                Where(u => u.Nazwisko == "Kowalski").
                    FirstOrDefault(); // Lub Last albo First
            Console.WriteLine(wybrany);
            //LAMBDA -- do przetwarzania kolekcji
            var adamowie = lista. //var pozwala pominac deklaracje typu
                Where(u => u.imie == "Adam");
            foreach (User u in adamowie)
                Console.WriteLine(u);

            lista.ForEach(u => Console.WriteLine(u));
            adamowie.ToList().ForEach(u => Console.WriteLine(u));
            lista.
                Skip(2).
                Take(2).
                ToList().
                ForEach(u => Console.WriteLine(u));

            //zaoytanie Linq
            var wyniki = from u in lista
                         where u.imie == "Adam"
                         select u;
            wyniki.ToList().ForEach(u => Console.WriteLine(u));
            (from u in lista
             where u.imie == "Adam"
             select u).
                ToList().
                ForEach(u => Console.WriteLine(u));

            (from u in lista
             select u.Nazwisko).
             ToList().
             ForEach(s => Console.WriteLine(s));

            var nowe = (from u in lista
             select new { u.imie, u.Nazwisko });
            nowe.
                ToList().
                ForEach(u => Console.WriteLine(u));
            var ulozone = from u in lista
                           orderby u.PESEL ascending //albo descending zamiast MS SQL asc, desc
                           select u;
            //var ulozone2 = lista.OrderBy(u => u.PESEL);  sortowanie peseli w formi lambda


            ulozone.
                ToList().
                ForEach(u => Console.WriteLine(u));

            //Grupowanie -- grupowanie wedlug pewnej informacji, przydarne przy raportowaniu
            var grupy = from u in lista
                        group u by u.imie into g
                        select g;
            foreach (var g in grupy)
                Console.WriteLine(g.Key + " " + g.Count());

            var grupy2 = from u in lista
                         group u by u.imie into g
                         select new { g.Key, Grupa = g.ToList() }; //obiekt anonimowy Grupa z nadana wlasciwoscia ToList
            foreach (var grupa in grupy2)
                Console.WriteLine(grupa.Key + " " + grupa.Grupa.Count());

            var zlozone = from u in lista
                          group u by new { u.imie, u.Nazwisko } into g
                          select g;
            foreach (var g in zlozone)
                Console.WriteLine(g);

            List<Urzadzenie> urzadzenia = new List<Urzadzenie>();
            urzadzenia.Add(new Urzadzenie
            {
                Nazwa = "Apple X",
                uzytkPesel = "12345",
                Rodzaj = RodzUrzadzenie.TELEFON
            });
            urzadzenia.Add(new Urzadzenie
            {
                Nazwa = "Samsung",
                uzytkPesel = "83838",
                Rodzaj = RodzUrzadzenie.KOMPUTER
            });

            var q = from user in lista
                    join dev in urzadzenia
                    on user.PESEL equals dev.uzytkPesel
                    select new { user.imie, user.Nazwisko, dev.Nazwa };
            q.ToList().ForEach(z => Console.WriteLine(z));

            var big = from user in lista
                      join dev in urzadzenia
                      on user.PESEL equals dev.uzytkPesel
                      group user by dev.Rodzaj into g
                      select new { g.Key, Liczba = g.Count() };
            big.
                ToList().
                ForEach(g => Console.WriteLine(g.Key + " " + g.Liczba));

            //zip -- zszywanie


            //** ORM -- mapowanie obiektowo relacyjne 
            Console.ReadKey();
        }
    }

    class User
    {
        public String PESEL { get; set; }
        public String imie { get; set; }
        public String Nazwisko { get; set; }
        public override string ToString()
        {
            return PESEL + " " + imie + " " + Nazwisko;
        }
    }

}

class Urzadzenie
{
    public String Nazwa { get; set; }
    public RodzUrzadzenie Rodzaj {get; set;}
    public String uzytkPesel { get; set; }
}
enum RodzUrzadzenie
{
    TELEFON, TABLET, KOMPUTER
}