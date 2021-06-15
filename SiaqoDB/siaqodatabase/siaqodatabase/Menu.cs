using Sqo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siaqodatabase
{
    public class Menu
    {
        private Siaqodb db;
        DataHandler handler = new DataHandler();
        public Menu(Siaqodb db)
        {
            this.db = db;
        }

        public void Run()
        {
            while (true)
            {
                PrintMenu();
                var userInput = Console.ReadLine();
                switch (Convert.ToInt32(userInput))
                {
                    case 1:
                        GetById();
                        Console.ReadLine();
                        break;
                    case 2:
                        PrintAllData();
                        Console.ReadLine();
                        break;
                    case 3:
                        AddClient();
                        Console.ReadLine();
                        break;
                    case 4:
                        UpdateClient();
                        Console.ReadLine();
                        break;
                    case 5:
                        Delete();
                        Console.ReadLine();
                        break;
                    case 6:
                        ClientWithMoreThanOneTrip();
                        Console.ReadLine();
                        break;
                    case 7:
                        AvarageTripCost();
                        Console.ReadLine();
                        break;
                    case 8:
                        AddTripToClient();
                        Console.ReadLine();
                        break;
                    case 9:
                        return;
                    default:
                        break;
                }
            }
        }

        private void UpdateClient()
        {
            Console.WriteLine("Podaj id klienta: ");
            var id = Int32.Parse(Console.ReadLine());
            var person = db.LoadAll<Client>().Where(x => x.OID == id).First();
            Console.WriteLine("Podaj nowy rok urodzenia:");
            var year = Int32.Parse(Console.ReadLine());
            person.BirthYear = year;
            db.StoreObject(person);
        }

        private void AddTripToClient()
        {
            Console.WriteLine("Podaj id klienta: ");
            var id = Int32.Parse(Console.ReadLine());

            var person = db.LoadAll<Client>().Where(x => x.OID == id).FirstOrDefault();

            Console.WriteLine("Kraj: ");
            var country = Console.ReadLine();
            Console.WriteLine("Miasto: ");
            var city = Console.ReadLine();
            Console.WriteLine("Ilość dni: ");
            var days = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Cena: ");
            var price = Convert.ToDouble(Console.ReadLine());

            var trip = new Trip() { Country = country, City = city, Days = days, Price = price };
            if (person.Trips == null)
                person.Trips = new List<Trip>() { trip };
            else
                person.Trips.Add(trip);
            db.StoreObject(person);
            Console.WriteLine("Dodano!");
        }

        private void AddClient()
        {
            Console.WriteLine("Imię: ");
            var name = Console.ReadLine();
            Console.WriteLine("Nazwisko: ");
            var surname = Console.ReadLine();
            Console.WriteLine("Rok urodzenia: ");
            var year = Int32.Parse(Console.ReadLine());

            var client = new Client() { Name = name, Surname = surname, BirthYear = year };
            db.StoreObject(client);
            Console.WriteLine($"ID dodanego klienta: {client.OID}");
            Console.WriteLine("Dodano!");
        }

        private void AvarageTripCost()
        {
            var q = db.LoadAll<Client>().Select(x => x.Trips);
            var counter = 0;
            double sum = 0;
            foreach(var trips in q)
            {
                foreach (var trip in trips)
                {
                    sum += trip.Price;
                    counter++;
                }
            }
            Console.WriteLine($"Średnia cena sprzedanych wycieczek wynosi: {sum / counter}.");
        }

        private void PrintAllData()
        {
            var q = db.LoadAll<Client>();
            handler.PrintAllClientData(q);
        }
        public void PrintMenu()
        {
            Console.WriteLine("[1] - Pobierz przez id\n" +
                              "[2] - Pobierz wszystko\n" +
                              "[3] - Dodaj klienta\n" +
                              "[4] - Aktualizuj\n" +
                              "[5] - Kasuj\n" +
                              "[6] - Wyświetl klientów z więcej niż jedną wycieczką\n" +
                              "[7] - Policz średnią cenę sprzedanych wycieczek\n" +
                              "[8] - Dodaj wycieczkę do klienta\n" +
                              "[9] - Zakończ");
        }

        public void GetById()
        {
            Console.WriteLine("Podaj id: ");
            var id = Convert.ToInt32(Console.ReadLine());
            var q = db.LoadAll<Client>().Where(x => x.OID == id).FirstOrDefault();
            handler.PrintAllClientData(q);
        }


        public void Delete()
        {
            Console.WriteLine("Podaj id: ");
            var id = Convert.ToInt32(Console.ReadLine());
            var q = db.LoadAll<Client>().Where(x => x.OID == id).FirstOrDefault();
            db.Delete(q);
            Console.WriteLine("Usunięto!");
        }

        private void ClientWithMoreThanOneTrip()
        {
            var moreThanOne = db.LoadAll<Client>().Where(x => x.Trips.Count() > 1);
            handler.PrintAllClientData(moreThanOne);
        }
    }
}
