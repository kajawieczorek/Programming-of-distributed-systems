using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPSR
{
    class Helper
    {
        private IDatabase conn;
        public Helper(IDatabase conn)
        {
            this.conn = conn;
        }

        public void AddPoliceman()
        {
            Console.WriteLine("Podaj imi?: ");
            var name = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko: ");
            var surname = Console.ReadLine();
            Console.WriteLine("Podaj rok urodzenia: ");
            var year = Convert.ToInt32(Console.ReadLine());

            var policeman = new Policeman(name, surname, year);
            var json = JsonConvert.SerializeObject(policeman);
            var key = "policjant:0";

            if (conn.StringSet(KeyAdjustment(key), json))
            {
                Console.WriteLine("Dodano pomy?lnie!");
                policeman.GetInfo();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Nie uda?o si? doda?, spróbuj ponownie.");
                AddPoliceman();
            }
        }
        public void UpdatePoliceman(string key)
        {
            Console.WriteLine("Podaj imi?: ");
            var name = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko: ");
            var surname = Console.ReadLine();
            Console.WriteLine("Podaj rok urodzenia: ");
            var year = Convert.ToInt32(Console.ReadLine());

            var policeman = new Policeman(name, surname, year);
            var json = JsonConvert.SerializeObject(policeman);

            if (conn.StringSet(key, json))
            {
                Console.WriteLine("Zmodyfikowano pomy?lnie!");
                policeman.GetInfo();
                Console.ReadLine();
            }
        }
        public void AddDepartment()
        {
            Console.WriteLine("Podaj typ komendy:");
            var type = Console.ReadLine();
            Console.WriteLine("Podaj miasto: ");
            var province = Console.ReadLine();

            var department = new Department(province, type);
            var json = JsonConvert.SerializeObject(department);
            var key = "komenda:0";
            if (conn.StringSet(KeyAdjustment(key), json))
            {
                Console.WriteLine("Dodano pomy?lnie!");
                department.GetInfo();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Nie uda?o si? doda?, spróbuj ponownie.");
                AddDepartment();
            }
        }
        public void UpdateDepartment(string key)
        {
            Console.WriteLine("Podaj typ komendy:");
            var type = Console.ReadLine();
            Console.WriteLine("Podaj miasto: ");
            var province = Console.ReadLine();

            var department = new Department(province, type);
            var json = JsonConvert.SerializeObject(department);
            if (conn.StringSet(key, json))
            {
                Console.WriteLine("Dodano pomy?lnie!");
                department.GetInfo();
                Console.ReadLine();
            }
        }
        public void AddRank()
        {
            Console.WriteLine("Podaj nazw? rangi:");
            var policeRank = Console.ReadLine();
            Console.WriteLine("Podaj pensj?: ");
            var salary = Convert.ToDouble(Console.ReadLine());

            var rank = new Rank(policeRank, salary);
            var json = JsonConvert.SerializeObject(rank);
            var key = "ranga:0";
            if (conn.StringSet(KeyAdjustment(key), json))
            {
                Console.WriteLine("Dodano pomy?lnie!");
                rank.GetInfo();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Nie uda?o si? doda?, spróbuj ponownie.");
                AddRank();
            }
        }

        public void UpdateRank(string key)
        {
            Console.WriteLine("Podaj nazw? rangi:");
            var policeRank = Console.ReadLine();
            Console.WriteLine("Podaj pensj?: ");
            var salary = Convert.ToDouble(Console.ReadLine());

            var rank = new Rank(policeRank, salary);
            var json = JsonConvert.SerializeObject(rank);
            if (conn.StringSet(key, json))
            {
                Console.WriteLine("Dodano pomy?lnie!");
                rank.GetInfo();
                Console.ReadLine();
            }
        }

        public string KeyAdjustment(string key)
        {
            string newKey = key;

            do
            {
                var splitted = newKey.Split(':');
                var number = Convert.ToInt32(splitted[1]);
                newKey = splitted[0] + ":" + ++number;
            } while (conn.KeyExists(newKey));

            return newKey;
        }

        public void GetValues<T>(string keyFrangment, IEnumerable<RedisKey> keys) where T : IData
        {
            foreach (var item in keys)
            {
                var key = item.ToString();
                if (key.Contains(keyFrangment))
                {
                    var deserialized = JsonConvert.DeserializeObject<T>(conn.StringGet(key));
                    Console.Write(key + ":\t");
                    deserialized.GetInfo();
                }
            }
        }

        public void GetInfo<T>(IEnumerable<RedisKey> keys) where T : IData
        {
            foreach (var item in keys)
            {
                var key = item.ToString();

                var deserialized = JsonConvert.DeserializeObject<T>(conn.StringGet(key));
                Console.Write(key + ":\t");
                deserialized.GetInfo();

            }
        }

        public Dictionary<String, Rank> GetAllRanks(IEnumerable<RedisKey> keys)
        {
            var ranks = new Dictionary<String, Rank>();
            foreach (var item in keys)
            {
                var key = item.ToString();
                if (key.Contains("ranga:"))
                {
                    var deserialized = JsonConvert.DeserializeObject<Rank>(conn.StringGet(key));
                    ranks.Add(key, deserialized);
                }
            }
            return ranks;
        }

        public IEnumerable<RedisKey> GetAllDistrictDepartments(IEnumerable<RedisKey> keys)
        {
            var departments = new List<RedisKey>();
            foreach (var item in keys)
            {
                var key = item.ToString();
                if (key.Contains("komenda:"))
                {
                    var deserialized = JsonConvert.DeserializeObject<Department>(conn.StringGet(key));
                    if (deserialized.Type == "powiatowa")
                    {
                        departments.Add(item);
                    }
                }
            }
            return departments;
        }
    }
}
