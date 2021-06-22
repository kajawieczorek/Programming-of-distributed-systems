using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;

namespace JanusGrph
{
    public class Actions
    {
        GraphTraversalSource g;

        public Actions(GraphTraversalSource g)
        {
            this.g = g;
        }

        public void Add()
        {
            Console.WriteLine("Podaj id klienta: ");
            var id = Console.ReadLine();
            Console.WriteLine("Podaj imię klienta: ");
            var name = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko klienta: ");
            var surname = Console.ReadLine();
            var t = g.AddV("Client").Property("id", id).Property("name", name).Property("surname", surname);
            Console.WriteLine("Dodano!");

            id = t.Values<string>("id").Next();
            PrintClient(id);
        }
        public void AddTrip()
        {
            Console.WriteLine("Podaj id wycieczki: ");
            var id = Console.ReadLine();
            Console.WriteLine("Podaj kraj: ");
            var country = Console.ReadLine();
            Console.WriteLine("Podaj miasto: ");
            var city = Console.ReadLine();
            Console.WriteLine("Podaj cenę: ");
            var price = Console.ReadLine();
            var t = g.AddV("Trip").Property("id", id).Property("country", country).Property("city", city).Property("price", price).Next();
            Console.WriteLine("Dodano!");

            PrintTrip(id);
        }

        private void PrintTrip(string id)
        {
            Console.Write($"Wycieczka {id}:");
            var x = g.V().HasLabel("Trip").Has("id", id).Next();
            var country = g.V(x).Values<string>("country").Next();
            var city = g.V(x).Values<string>("city").Next();
            var price = g.V(x).Values<int>("price").Next();

            Console.WriteLine($"{country}, {city}, {price} zł");
        }

        public void PrintClient(string id)
        {
            Console.Write($"Klient {id}:");
            PrintName(id); PrintSurname(id);
        }
        public void PrintName(string id)
        {
            var x = g.V().Has("id", id).Next();
            var name = g.V(x).Values<string>("name").Next();
            Console.WriteLine(name);
        }
        public void PrintSurname(string id)
        {
            var x = g.V().Has("id", id).Next();
            var surname = g.V(x).Values<string>("surname").Next();
            Console.WriteLine(surname);
        }

        public void Run()
        {
            while (true)
            {
                PrintMenu();
                var input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Add();
                        Console.ReadLine();
                        break;
                    case 2:
                        Update();
                        Console.ReadLine();
                        break;
                    case 3:
                        GetByCountry();
                        Console.ReadLine();
                        break;
                    case 4:
                        GetAll();
                        Console.ReadLine();
                        break;
                    case 5:
                        Delete();
                        Console.ReadLine();
                        break;
                    case 6:
                        AvarageTripPrice();
                        Console.ReadLine();
                        break;
                    case 7:
                        DeleteAll();
                        Console.ReadLine();
                        break;
                    case 8:
                        AddTripToClient();
                        Console.ReadLine();
                        break;
                    case 9:
                        AddTrip();
                        Console.ReadLine();
                        break;
                    default:
                        break;
                }
            }
        }

        private void GetByCountry()
        {
            Console.WriteLine("Podaj kraj: ");
            var serched = Console.ReadLine();
            var trips = g.V().HasLabel("Trip").ToList();
            foreach (var it in trips)
            {
                var j = it.Id;
                var id = g.V(j).Values<string>("id").Next();
                var country = g.V(j).Values<string>("country").Next();
                var city = g.V(j).Values<string>("city").Next();
                var price = g.V(j).Values<int>("price").Next();
                if(country == serched)
                    Console.WriteLine($"Wycieczka {id}: {country} {city}, {price} zł");
            }
        }

        private void AvarageTripPrice()
        {
            var y = g.V().HasLabel("Trip").ToList();
            int counter = 0;
            int sum = 0;
            foreach (var it in y)
            {
                var j = it.Id;
                var price = g.V(j).Values<int>("price").Next();
                sum += price;
                counter++;

            }
            Console.WriteLine($"Średnia cena: {sum / counter}");
        }

        private void Update()
        {
            Console.WriteLine("Podaj id wycieczki: ");
            var id = Console.ReadLine();
            var trip = g.V().HasLabel("Trip").Has("id", id).Next();
            Console.WriteLine("Podaj nową cenę: ");
            var price = Console.ReadLine();
            g.V(trip).Property(Cardinality.Single, "price", price).Next();
        }

        private void AddTripToClient()
        {
            Console.WriteLine("Podaj id klienta: ");
            var id = Console.ReadLine();
            var client = g.V().HasLabel("Client").Has("id", id).Next();
            var name = g.V(client).Values<string>("name").Next();

            Console.WriteLine("Podaj id wycieczki: ");
            var idt = Console.ReadLine();
            var trip = g.V().HasLabel("Trip").Has("id", idt).Next();
            var country = g.V(trip).Values<string>("country").Next();

            g.V(client).AddE("goesTo").To(trip);

            Console.WriteLine($"Utworzono!\n {name} -> goesTo -> {country}");
        }

        private void DeleteAll()
        {
            var y = g.V().HasLabel("Client").ToList();
            foreach (var it in y)
            {
                var j = it.Id;
                g.V(j).Drop().Iterate();
            }
            var trips = g.V().HasLabel("Trip").ToList();
            foreach (var it in trips)
            {
                var j = it.Id;
                g.V(j).Drop().Iterate();
            }
        }

        private void Delete()
        {
            Console.WriteLine("Podaj id klienta do usunięcia: ");
            var id = Console.ReadLine();
            var x = g.V().HasLabel("Client").Has("id", id).Next();
            g.V(x).Drop().Iterate();

            Console.WriteLine("Usunięto!");
        }

        private void GetAll()
        {
            var y = g.V().HasLabel("Client").ToList();
            //var x = g.V().ValueMap<string, string>("id", "name", "surname").ToList();

            Console.WriteLine("Klienci:");
            foreach (var it in y)
            {
                var j = it.Id;
                var name = g.V(j).Values<string>("name").Next();
                var surname = g.V(j).Values<string>("surname").Next();
                var id = g.V(j).Values<string>("id").Next();
               
                Console.WriteLine($"\tKlient {id}: {name}, {surname}");
            }

            Console.WriteLine("Wycieczki:");
            var trips = g.V().HasLabel("Trip").ToList();
            foreach (var it in trips)
            {
                var j = it.Id;
                var id = g.V(j).Values<string>("id").Next();
                var country = g.V(j).Values<string>("country").Next();
                var city = g.V(j).Values<string>("city").Next();
                var price = g.V(j).Values<int>("price").Next();
                Console.WriteLine($"\tWycieczka {id}: {country} {city}, {price} zł");
            }
        }



        public void PrintMenu()
        {
            Console.WriteLine("1 - Dodaj \n" +
                "2 - Edytuj\n" +
                "3 - Pobierz przez kraj\n" +
                "4 - Pobierz wszystko\n" +
                "5 - Usuń klienta\n" +
                "6 - Policz średni koszt wycieczki\n" +
                "7 - Usuń wszystko\n" +
                "8 - Dodaj wycieczkę do klienta\n" +
                "9 - Dodaj wycieczkę");
        }
    }
}
