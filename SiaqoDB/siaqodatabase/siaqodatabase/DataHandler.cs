using Sqo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siaqodatabase
{
    public class DataHandler
    {

        public void PrintAllClientData(IObjectList<Client> clients)
        {
            foreach (Client c in clients)
            {
                Console.WriteLine($"Imię i nazwisko: {c.Name} {c.Surname} \n" +
                                  $"ID: {c.OID}, rok urodzenia: {c.BirthYear}");

                if (c.Trips != null)
                {
                    Console.WriteLine("Wycieczki:");
                    foreach (Trip trip in c.Trips)
                    {
                        Console.WriteLine($"    ID: {trip.OID}");
                        Console.WriteLine($"    Kraj: {trip.Country} \tMiasto: {trip.City}\n" +
                                          $"    Ilość dni: {trip.Days} \tCena:{trip.Price}");
                    }
                }
            }
        }
        public void PrintAllClientData(ISqoQuery<Client> clients)
        {
            foreach (Client c in clients)
            {
                Console.WriteLine($"Imię i nazwisko: {c.Name} {c.Surname} \n" +
                                  $"ID: {c.OID}, rok urodzenia: {c.BirthYear}");

                if (c.Trips != null)
                {
                    Console.WriteLine("Wycieczki:");
                    foreach (Trip trip in c.Trips)
                    {
                        Console.WriteLine($"    ID {trip.OID}:");
                        Console.WriteLine($"    {trip.Country} {trip.City}\n" +
                                          $"    {trip.Days} {trip.Price}");
                    }
                }
            }
        }
        public void PrintAllClientData(Client c)
        {

            Console.WriteLine($"Imię i nazwisko: {c.Name} {c.Surname} \n" +
                              $"ID: {c.OID}, rok urodzenia: {c.BirthYear}");

            if (c.Trips != null)
            {
                Console.WriteLine("Wycieczki:");
                foreach (Trip trip in c.Trips)
                {
                    Console.WriteLine($"    ID: {trip.OID}");
                    Console.WriteLine($"    Kraj: {trip.Country} \tMiasto: {trip.City}\n" +
                                      $"    Ilość dni: {trip.Days} \tCena:{trip.Price}");
                }
            }

        }

        public void PrintAllClientData(IEnumerable<Client> clients)
        {

            foreach (Client c in clients)
            {
                Console.WriteLine($"Imię i nazwisko: {c.Name} {c.Surname} \n" +
                              $"ID: {c.OID}, rok urodzenia: {c.BirthYear}");
                if (c.Trips != null)
                {
                    Console.WriteLine("Wycieczki:");
                    foreach (Trip trip in c.Trips)
                    {
                        Console.WriteLine($"    ID: {trip.OID}");
                        Console.WriteLine($"    Kraj: {trip.Country} \tMiasto: {trip.City}\n" +
                                          $"    Ilość dni: {trip.Days} \tCena:{trip.Price}");
                    }
                }
            }
        }
    }
}
