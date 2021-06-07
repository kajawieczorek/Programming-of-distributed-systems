using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace RealmDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var realm = Realm.GetInstance();
            var operations = new CRUD(realm);
            var menu = new Menu(realm);

            realm.Write( () => realm.RemoveAll());
            
            var p1 = new Player() { Name = "Jan", Surname = "Nowak", BirthYear = 2000, Position = "Bramkarz" };
            var p2 = new Player() { Name = "Piotr", Surname = "Kowal", BirthYear = 1999, Position = "Obrońca" };

            IList<Player> players = new List<Player> { p1, p2 };
            IList<Player> onePlayer = new List<Player> { p2 };
            
            var club1 = new Club() { Id = 1, Name = "Pierwszy klub", Budget = 30000 };
            var club2 = new Club() { Id = 2, Name = "Drugi klub", Budget = 10000};
            club1.Players.Add(p1);
            club1.Players.Add(p2);
            club2.Players.Add(p2);

            // Update and persist objects with a thread-safe transaction
            realm.Write(() =>
            {
                realm.Add(club1);
                realm.Add(club2);
            });

            menu.Run();
            //operations.PrintAllData(realm);

        }

    }
}
