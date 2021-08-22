/*Описати структуру з ім'ям Airline, що містить такі поля:
— Назва пункту призначення рейсу;
— Номер рейсу(ціле число);
— Тип літака(рядок).
Написати програму, яка виконуватиме такі дії:
- Введення з клавіатури даних у масив структур типу Airline; 
- збереження цього масиву в
обидва файли(текстовий та формату xml); 
- структури повинні бути впорядковані за
- зростанням номера рейсу;
- Читання даних з цих файлів(окремо лише з одного, окремо лише з іншого) та виведення
на екран номерів рейсів і типів літаків, що вилітають в пункт призначення, назва якого
збіглася з назвою, введеною з клавіатури; 
- якщо таких рейсів немає, видати на дисплей відповідне повідомлення.
*/

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace StructFile
{
    public struct Airline
    {
        public string Destination { get; set; }
        public string Number { get; set; }
        public string PlaneModel { get; set; }

        public override string ToString()
        {
            return string.Format($"Destination: {Destination}. Number: {Number}. Plane model: {PlaneModel}");
        }
    }
    class Program
    {
        const int NUMBER_OF_FLIGHTS = 3;

        static void GetValues(Airline[] Flights)
        {
            for (int i = 0; i < NUMBER_OF_FLIGHTS; i++)
            {
                Airline flight = new Airline();

                Console.Write("\n Input destination: ");
                flight.Destination = Console.ReadLine();
                Console.Write(" Input flight number: ");
                flight.Number = Console.ReadLine();
                Console.Write(" Input plane model: ");
                flight.PlaneModel = Console.ReadLine();
                Flights[i] = flight;
            }
        }

        static void WriteTxt(string pathTxt, Airline[] Flights)
        {
            using (StreamWriter sw = new StreamWriter(pathTxt, false, Encoding.UTF8))
            {
                for (int i = 0; i < NUMBER_OF_FLIGHTS; i++)
                    sw.WriteLine(Flights[i]);
            }
        }

        static void ReadTxt(string pathTxt)
        {
            using (StreamReader sr = new StreamReader(pathTxt, Encoding.UTF8))
                Console.WriteLine(sr.ReadToEnd());
        }
        static void WriteXml(string pathXml, Airline[] Flights, XmlSerializer xmlSerialaizer)
        {
            List<Airline> list = new List<Airline>();
            list.AddRange(Flights);

            using (FileStream fw = new FileStream(pathXml, FileMode.Create))
                xmlSerialaizer.Serialize(fw, list);
        }
        static void ReadXml(string pathXml, XmlSerializer xmlSerialaizer)
        {
            List<Airline> FlightsToRead = new List<Airline>();

            using (FileStream fr = new FileStream(pathXml, FileMode.Open))
            {
                FlightsToRead = (List<Airline>)xmlSerialaizer.Deserialize(fr);
                foreach (Airline i in FlightsToRead)
                {
                    Console.WriteLine($" Destination: {i.Destination}. Number: {i.Number}. Plane model: {i.PlaneModel}");
                }
            }
        }

        static void Sort(Airline[] Flights)
        {
            var sort = Flights.OrderBy(aeroflot => aeroflot.Number); // Впорядковування

            Console.WriteLine(" SORTED IN ASCENDING ORDER:\n");
            foreach (var s in sort)
                Console.WriteLine(s);
        }

        static void CheckFlight(Airline[] Flights)
        {
            Console.Write("\n Input destination: ");

            string DestinationToCompare = Console.ReadLine();
            int matches = 0;

            for (int i = 0; i < Flights.Length; i++)
            {
                if (Flights[i].Destination == DestinationToCompare)
                {
                    Console.WriteLine($" Number: {Flights[i].Number}\n Plane Model: {Flights[i].PlaneModel}");
                    matches++;
                }
            }
            if (matches == 0)
            {
                Console.WriteLine("\n THERE IS NO MATCHES!!");
            }
        }        

        static void Line()
        {
            Console.WriteLine("----------------------------");
        }

        public static void Main(string[] args)
        {
            try
            {
                Airline[] Flights = new Airline[NUMBER_OF_FLIGHTS];
                Line();

                Console.WriteLine(" INPUT THE DATA:");

                GetValues(Flights);
                Line();

                string pathTxt = "file1.txt";
                WriteTxt(pathTxt, Flights);

                Console.WriteLine(" READ FILE .txt:\n");
                ReadTxt(pathTxt);
                Line();

                XmlSerializer xmlSerialaizer = new XmlSerializer(typeof(List<Airline>));

                string pathXml = "file2.txt";
                WriteXml(pathXml, Flights, xmlSerialaizer);

                Console.WriteLine(" READ FILE .xml:\n");
                ReadXml(pathXml,  xmlSerialaizer);
                Line();

                Sort(Flights);
                Line();

                CheckFlight(Flights);
                Line();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}