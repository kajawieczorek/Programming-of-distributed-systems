using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPSR
{
    class Department : IData
    {
        public Department( string province, string type)
        {
            City = province;
            Type = type;
        }

        public string City { get; set; }
        public string Type { get; set; } //komenda miejska, powiatowa, wojewódzka

        public void GetInfo()
        {
            Console.WriteLine($"Komenda {Type}, miasto {City}.");
        }
    }
}
