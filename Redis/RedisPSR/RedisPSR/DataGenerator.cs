using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPSR
{
    class DataGenerator
    {
        private IDatabase conn;


        public DataGenerator(IDatabase conn)
        {
            this.conn = conn;
        }
        public void Genetate()
        {
            var helper = new Helper(conn);
            var policemen = new List<Policeman>{ new Policeman("Jan", "Kowal", 1990),
                                      new Policeman("Anna", "Maj", 1890),
                                      new Policeman("Artur", "B?k", 1960),
                                      new Policeman("Bart?omiej", "Stonoga", 1988) };

            foreach (var policeman in policemen)
            {
                var json = JsonConvert.SerializeObject(policeman);
                var key = "policjant:0";
                conn.StringSet(helper.KeyAdjustment(key), json);
            }

            var departments = new List<Department> { new Department("Kielce", "miejska"),
                                                    new Department("Warszawa", "wojewodzka"),
                                                    new Department("Radom", "powiatowa"),
                                                    new Department("Kielce", "powiatowa")};

            foreach (var department in departments)
            {
                var json = JsonConvert.SerializeObject(department);
                var key = "komenda:0";
                conn.StringSet(helper.KeyAdjustment(key), json);
            }

            var ranks = new List<Rank> { new Rank("Funkcjonariusz", 2000),
                                         new Rank("Podkomisarz", 2500),
                                         new Rank("Nadkomisarz", 3500) };

            foreach (var rank in ranks)
            {
                var json = JsonConvert.SerializeObject(rank);
                var key = "ranga:0";
                conn.StringSet(helper.KeyAdjustment(key), json);
            }
        }
    }
}
