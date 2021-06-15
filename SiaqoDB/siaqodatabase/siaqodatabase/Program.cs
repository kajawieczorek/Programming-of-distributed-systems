using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqo;

namespace siaqodatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs().First()), "Data");

            if (Directory.Exists(dbPath))
                Directory.Delete(dbPath, true);
            Directory.CreateDirectory(dbPath);

            var db = new Siaqodb(dbPath);

            var trip = new Trip() { Country = "Włochy", City = "Rzym", Days = 14, Price = 1500 };
            var trip2 = new Trip() { Country = "Włochy", City = "Viareggio", Days = 10, Price = 1300 };
            var trip3 = new Trip() { Country = "Hiszpania", City = "Madryt", Days = 20, Price = 3000 };
            var trip4 = new Trip() { Country = "Grecja", City = "Ios", Days = 7, Price = 600 };

            var client = new Client() { Name = "Piotr", Surname = "Kowal", BirthYear = 1990, Trips = new List<Trip>() { trip, trip3 } };
            var client2 = new Client() { Name = "Anna", Surname = "Pakosz", BirthYear = 1980, Trips = new List<Trip>() { trip2 } };
            var client3 = new Client() { Name = "Marian", Surname = "Ludwik", BirthYear = 2000, Trips = new List<Trip>() { trip4 } };

            db.StoreObject(client);
            db.StoreObject(client2);
            db.StoreObject(client3);

            var menu = new Menu(db);
            menu.Run();

            Console.ReadLine();
        }
    }
}
