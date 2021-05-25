using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPSR
{
    class Rank : IData
    {
        public Rank(string policeRank, double salary)
        {
            PoliceRank = policeRank;
            Salary = salary;
        }

        public string PoliceRank { get; set; }
        public double Salary { get; set; }

        public void GetInfo()
        {

            Console.WriteLine($"Ranga {this.PoliceRank}, p?aca: {this.Salary}.");
        }

    }
}
