using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace RealmDB
{
    class Club : RealmObject
    {

        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Player> Players { get; }
        public double Budget { get; set; }

        public void PrintClub()
        {
            Console.WriteLine($"Id: {Id}, Nazwa: {Name}, Budżet: {Budget}, Gracze:");
            foreach (var item in Players)
            {
                item.Print();
            }
        }
    }
}