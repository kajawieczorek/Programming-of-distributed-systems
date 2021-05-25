using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPSR
{
    class Policeman : IData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }

        public Policeman(string name, string surname, int birthYear)
        {
            Name = name;
            Surname = surname;
            BirthYear = birthYear;
        }


        public void GetInfo()
        {
            Console.WriteLine($"{Name} {Surname}, urodzony w roku {BirthYear}.");
        }
    }
}
