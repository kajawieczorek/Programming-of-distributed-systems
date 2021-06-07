using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmDB
{
    class CRUD
    {
        private Realm realm;

        public CRUD(Realm realm)
        {
            this.realm = realm;
        }

        public void PrintAllData()
        {
            var clubs = realm.All<Club>();

            foreach (var item in clubs)
            {
                item.PrintClub();
            }
            Console.ReadLine();
        }

        public void DeleteClub(int id)
        {
            var toDel = realm.All<Club>().FirstOrDefault(c => c.Id == id);
            using (var transaction = realm.BeginWrite())
            {
                realm.Remove(toDel);
                transaction.Commit();
            }
        }

        public void UpdateClubName(int id, string newName)
        {
            var toUpdate = realm.All<Club>().FirstOrDefault(c => c.Id == id);
            using (var transaction = realm.BeginWrite())
            {
                toUpdate.Name = newName;
                transaction.Commit();
            }
        }
        public void UpdateBudget(int id, double newBudget)
        {
            var toUpdate = realm.All<Club>().FirstOrDefault(c => c.Id == id);
            using (var transaction = realm.BeginWrite())
            {
                toUpdate.Budget = newBudget;
                transaction.Commit();
            }
        }

        public void GetById(int id)
        {
            var toGet = realm.All<Club>().FirstOrDefault(c => c.Id == id);
            toGet.PrintClub();
        }

        public void GetAvarageClubBudget()
        {
            var budgets = realm.All<Club>();
            double sum = 0;
            int counter = 0;
            foreach (var item in budgets)
            {
                sum += item.Budget;
                counter++;
            }
            var avg = sum / counter;
            Console.WriteLine($"Średni budżet wynosi: {avg}");
        }

        public void Add()
        {
            Console.WriteLine("Id: ");
            var id = Console.ReadLine();
            Console.WriteLine("Nazwa: ");
            var name = Console.ReadLine();
            Console.WriteLine("Budżet: ");
            var budget = Console.ReadLine();

            var club = new Club();
            club.Id = Convert.ToInt32(id);
            club.Name = name;
            club.Budget = Convert.ToDouble(budget);
            club.Players.Add(NewPlayer());
            club.Players.Add(NewPlayer());

            realm.Write(() => realm.Add(club));
        }

        private Player NewPlayer()
        {
            var playerToAdd = new Player();
            Console.WriteLine("Wprowadź dane gracza: ");

            Console.WriteLine("Imię: ");
            var name = Console.ReadLine();
            Console.WriteLine("Nazwisko: ");
            var surname = Console.ReadLine();
            Console.WriteLine("Rok urodzenia: ");
            var birthYear = Console.ReadLine();
            Console.WriteLine("Pozycja: ");
            var position = Console.ReadLine();
            playerToAdd = new Player { Name = name, Surname = surname, BirthYear = Convert.ToInt32(birthYear), Position = position };

            return playerToAdd;
        }
    }
}
