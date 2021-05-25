using Newtonsoft.Json;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPSR
{
    class Menu
    {
        private IDatabase conn;
        private ConnectionMultiplexer redis;
        private int UserNumber => Convert.ToInt32(Console.ReadLine());

        public Menu(IDatabase conn, ConnectionMultiplexer redis)
        {
            this.conn = conn;
            this.redis = redis;
        }

        public Menu(IDatabase conn)
        {
            this.conn = conn;
        }

        public void PrintMenu()
        {
            var helper = new Helper(conn);

            Console.WriteLine("[1] - Pobierz\n" +
                              "[2] - Dodaj\n" +
                              "[3] - Aktualizuj\n" +
                              "[4] - Kasuj\n" +
                              "[5] - Oblicz ?redni? pensj? wg rang\n" +
                              "[6] - Wy?wietl komendy powiatowe\n" +
                              "[7] - Zako?cz");

            var input = UserNumber;

            while (true)
            {
                if (input == 1)
                {

                    Console.WriteLine("[1] - Wszystkie dane\t" +
                                      "[2] - Policjanci\t" +
                                      "[3] - Komendy\t" +
                                      "[4] - Rangi");
                    input = UserNumber;
                    var keys = redis.GetServer(redis.GetEndPoints().First()).Keys();
                    if (input == 1)
                    {
                        helper.GetValues<Policeman>("policjant:", keys);
                        helper.GetValues<Department>("komenda:", keys);
                        helper.GetValues<Rank>("ranga:", keys);

                        PrintMenu();
                    }
                    else if (input == 2)
                    {
                        helper.GetValues<Policeman>("policjant:", keys);
                        PrintMenu();
                    }
                    else if (input == 3)
                    {
                        helper.GetValues<Department>("komenda:", keys);
                        PrintMenu();
                    }
                    else
                    {
                        helper.GetValues<Rank>("ranga:", keys);
                        PrintMenu();
                    }
                }
                else if (input == 2)
                {
                    Console.WriteLine("[1] - Policjanta\t" +
                                      "[2] - Komend?\t" +
                                      "[3] - Rang?");
                    input = UserNumber;
                    if (input == 1)
                    {
                        helper.AddPoliceman();
                        PrintMenu();
                    }
                    else if (input == 2)
                    {
                        helper.AddDepartment();
                        PrintMenu();
                    }
                    else if (input == 3)
                    {
                        helper.AddRank();
                        PrintMenu();
                    }
                    else
                    {
                        Console.WriteLine("Z?y numer!");
                        PrintMenu();
                    }

                }
                else if (input == 3)
                {
                    Console.WriteLine("Podaj klucz, którego watro?? chcesz edytowa?:");
                    var key = Console.ReadLine();
                    if (!conn.KeyExists(key))
                    {
                        Console.WriteLine("Klucz nie isnieje!");
                        PrintMenu();
                    }
                    if (key.Contains("policjant"))
                    {
                        helper.UpdatePoliceman(key);
                    }
                    else if (key.Contains("komenda"))
                    {
                        helper.UpdateDepartment(key);
                    }
                    else
                        helper.UpdateRank(key);
                    PrintMenu();
                }
                else if (input == 4)
                {
                    Console.WriteLine("Podaj klucz do usuni?cia: ");
                    var key = Console.ReadLine();

                    if (conn.KeyDelete(key))
                    {
                        Console.WriteLine("Usuni?to pomy?lnie!");
                    }

                    PrintMenu();
                }
                else if (input == 5)
                {
                    //?rednia pensja
                    var keys = redis.GetServer(redis.GetEndPoints().First()).Keys();
                    var ranks = helper.GetAllRanks(keys);
                    var avarageSalary = ranks.Select(x => x.Value.Salary).Average();
                    Console.WriteLine($"?rednia pensja wynosi: {avarageSalary}");
                    PrintMenu();
                }
                else if (input == 6)
                {
                    //komendy powiatowe
                    var keys = redis.GetServer(redis.GetEndPoints().First()).Keys();
                    var departments = helper.GetAllDistrictDepartments(keys);

                    helper.GetInfo<Department>(departments);
                    PrintMenu();
                }
                else if (input == 7)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Z?y numer!");
                    PrintMenu();
                }
            }
        }

    }
}