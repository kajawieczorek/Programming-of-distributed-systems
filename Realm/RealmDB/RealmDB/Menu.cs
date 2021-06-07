using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmDB
{
    public class Menu
    {
        private Realm realm;
        private static CRUD crud;

        public Menu(Realm realm)
        {
            this.realm = realm;
            crud = new CRUD(realm);
        }

        public void Run()
        {
            while (true)
            {
                PrintMenu();
                var userInput = Console.ReadLine();
                switch (Convert.ToInt32(userInput))
                {
                    case 1:
                        GetById();
                        Console.ReadLine();
                        break;
                    case 2:
                        crud.Add();
                        Console.ReadLine();
                        break;
                    case 3:
                        UpdateSubmenu();
                        Console.ReadLine();
                        break;
                    case 4:
                        Delete();
                        Console.ReadLine();
                        break;
                    case 5:
                        GetClubWithHighBudget();
                        Console.ReadLine();
                        break;
                    case 6:
                        crud.PrintAllData();
                        Console.ReadLine();
                        break;
                    case 7:
                        crud.GetAvarageClubBudget();
                        Console.ReadLine();
                        break;
                    case 8:
                        return;
                    default:
                        break;
                }
            }
        }

        public void PrintMenu()
        {
            Console.WriteLine("[1] - Pobierz przez id\n" +
                              "[2] - Dodaj\n" +
                              "[3] - Aktualizuj\n" +
                              "[4] - Kasuj\n" +
                              "[5] - Wyświetl kluby z budżetem powyżej 15000\n" +
                              "[6] - Pobierz wszystko\n" +
                              "[7] - Policz średni budżet\n" +
                              "[8] - Zakończ");
        }

        public void GetById()
        {
            Console.WriteLine("Podaj id klubu: ");
            var clubId = Convert.ToInt32(Console.ReadLine());
            crud.GetById(clubId);
        }


        public void UpdateSubmenu()
        {
            Console.WriteLine("Podaj id klubu do zmiany: ");
            var id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("[1] - Zmień nazwę klubu\n" +
                              "[2] - Zmień budżet\n" +
                              "[3] - Powrót.");

            var userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput)
            {
                case 1:
                    UpdateClubName(id);
                    break;
                case 2:
                    UpdateBudget(id);
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            Run();
        }

        private void UpdateBudget(int id)
        {
            Console.WriteLine("Podaj nowy budżet: ");
            var input = Convert.ToDouble(Console.ReadLine());

            crud.UpdateBudget(id, input);

            Console.WriteLine("Zaktualizowano!");
        }

        public void UpdateClubName(int id)
        {
            Console.WriteLine("Nowa nazwa: ");
            var name = Console.ReadLine();

            var o = new CRUD(realm);
            o.UpdateClubName(id, name);

            Console.WriteLine("Zaktualizowano!");
        }

        public void Delete()
        {
            Console.WriteLine("Podaj id klubu do usunięcia: ");
            var input = Convert.ToInt32(Console.ReadLine());

            var o = new CRUD(realm);
            o.DeleteClub(input);

            Console.WriteLine("Usunięto!");
        }

        private void GetClubWithHighBudget()
        {
            var richest = realm.All<Club>().Where(x => x.Budget > 15000);
            foreach (var item in richest)
            {
                Console.WriteLine($"Id: {item.Id}, Nazwa: {item.Name}, Budżet: {item.Budget}");
            }
        }
    }
}
