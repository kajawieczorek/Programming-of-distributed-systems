using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siaqodatabase
{
    public class Client
    {
        public int OID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public List<Trip> Trips { get; set; }

    }

}
