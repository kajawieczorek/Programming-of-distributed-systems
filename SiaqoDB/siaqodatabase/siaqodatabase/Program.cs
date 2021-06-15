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

            var client = new Doctor() { Name = "Piotr", Surname = "Kowal", BirthYear = 1990, Salary = 3000, City = "Kielce" };
            var client2 = new Doctor() { Name = "Anna", Surname = "Pakosz", BirthYear = 1980, Salary = 6000, City = "Kielce" };
            var client3 = new Doctor() { Name = "Marian", Surname = "Ludwik", BirthYear = 2000,  Salary = 4000, City = "Warszawa" };

            db.StoreObject(client);
            db.StoreObject(client2);
            db.StoreObject(client3);

            var menu = new Menu(db);
            menu.Run();

            Console.ReadLine();
        }
    }
}
