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
                        AddDoctor();
                        Console.ReadLine();
                        break;
                    case 4:
                        UpdateDoctor();
                        Console.ReadLine();
                        break;
                    case 5:
                        Delete();
                        Console.ReadLine();
                        break;
                    case 6:
                        DoctorwithMoreThanTwoPatirnts();
                        Console.ReadLine();
                        break;
                    case 7:
                        AvarageSalary();
                        Console.ReadLine();
                        break;
                    case 8:
                        return;
                    default:
                        break;
                }
            }
        }

        private void UpdateDoctor()
        {
            Console.WriteLine("Podaj id doktora: ");
            var id = Int32.Parse(Console.ReadLine());
            var doc = db.LoadAll<Doctor>().Where(x => x.OID == id).First();
            Console.WriteLine("Podaj nowe miasto pracy:");
            var city = Console.ReadLine();
            doc.City = city;
            db.StoreObject(doc);
        }

        private void AddDoctor()
        {
            Console.WriteLine("Imię: ");
            var name = Console.ReadLine();
            Console.WriteLine("Nazwisko: ");
            var surname = Console.ReadLine();
            Console.WriteLine("Rok urodzenia: ");
            var year = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Miasto: ");
            var city = Console.ReadLine();
            Console.WriteLine("Pensja: ");
            var salary = Int32.Parse(Console.ReadLine());

            var client = new Doctor() { Name = name, Surname = surname, BirthYear = year, Salary = salary, City = city };
            db.StoreObject(client);
            Console.WriteLine($"ID dodanego lekarza: {client.OID}");
            Console.WriteLine("Dodano!");
        }

        private void AvarageSalary()
        {
            var q = db.LoadAll<Doctor>().Select(x => x.Salary);
            var counter = 0;
            double sum = 0;
            foreach(var salary in q)
            {
                sum += salary;
                counter++;
            }
            Console.WriteLine($"Średnia pensja wynosi: {sum / counter}.");
        }

        private void PrintAllData()
        {
            var q = db.LoadAll<Doctor>();
            handler.PrintAllClientData(q);
        }
        public void PrintMenu()
        {
            Console.WriteLine("[1] - Pobierz przez id\n" +
                              "[2] - Pobierz wszystko\n" +
                              "[3] - Dodaj lekarza\n" +
                              "[4] - Aktualizuj\n" +
                              "[5] - Kasuj\n" +
                              "[6] - Wyświetl doktorów z Kielc\n" +
                              "[7] - Policz średnią pensję\n" +
                              "[8] - Zakończ");
        }
        public void GetById()
        {
            Console.WriteLine("Podaj id: ");
            var id = Convert.ToInt32(Console.ReadLine());
            var q = db.LoadAll<Doctor>().Where(x => x.OID == id).FirstOrDefault();
            handler.PrintAllClientData(q);
        }


        public void Delete()
        {
            Console.WriteLine("Podaj id: ");
            var id = Convert.ToInt32(Console.ReadLine());
            var q = db.LoadAll<Doctor>().Where(x => x.OID == id).FirstOrDefault();
            db.Delete(q);
            Console.WriteLine("Usunięto!");
        }

        private void DoctorwithMoreThanTwoPatirnts()
        {
            var q = db.LoadAll<Doctor>().Where(x => x.City == "Kielce");
            handler.PrintAllClientData(q);
        }
    }
}
