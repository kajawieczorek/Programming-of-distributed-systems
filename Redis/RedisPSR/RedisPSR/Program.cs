using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPSR
{
    class Program
    {
        public int UserNumber => Convert.ToInt32(Console.ReadLine());
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase conn = redis.GetDatabase();

            //Generowanie danych
            var gen = new DataGenerator(conn);
            gen.Genetate();


            var menu = new Menu(conn, redis);
            menu.PrintMenu();
        }
    }
}
