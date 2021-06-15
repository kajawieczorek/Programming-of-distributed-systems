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

        public void PrintAllClientData(IObjectList<Doctor> doctors)
        {
            foreach (Doctor doc in doctors)
            {
                Console.WriteLine($"Imię i nazwisko: {doc.Name} {doc.Surname} \n" +
                                  $"ID: {doc.OID}, rok urodzenia: {doc.BirthYear} " +
                                  $"Pensja: {doc.Salary}, Miasto: {doc.City}");

            }
        }
        public void PrintAllClientData(ISqoQuery<Doctor> doctors)
        {
            foreach (Doctor doc in doctors)
            {
                Console.WriteLine($"Imię i nazwisko: {doc.Name} {doc.Surname} \n" +
                                  $"ID: {doc.OID}, rok urodzenia: {doc.BirthYear} " +
                                  $"Pensja: {doc.Salary}, Miasto: {doc.City}");
            }
        }
        public void PrintAllClientData(Doctor doc)
        {

            Console.WriteLine($"Imię i nazwisko: {doc.Name} {doc.Surname} \n" +
                                  $"ID: {doc.OID}, rok urodzenia: {doc.BirthYear} " +
                                  $"Pensja: {doc.Salary}, Miasto: {doc.City}");

        }

        public void PrintAllClientData(IEnumerable<Doctor> doctors)
        {
            foreach (Doctor doc in doctors)
            {
                Console.WriteLine($"Imię i nazwisko: {doc.Name} {doc.Surname} \n" +
                                  $"ID: {doc.OID}, rok urodzenia: {doc.BirthYear} " +
                                  $"Pensja: {doc.Salary}, Miasto: {doc.City}");
            }
        }
    }
}
