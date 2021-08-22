using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace StructFile
{
    public struct Aeroflot
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

        static void GetValues(Aeroflot[] Flights)
        {
            for (int i = 0; i < NUMBER_OF_FLIGHTS; i++)
            {
                Aeroflot flight = new Aeroflot();

                Console.Write("\n Input destination: ");
                flight.Destination = Console.ReadLine();
                Console.Write(" Input flight number: ");
                flight.Number = Console.ReadLine();
                Console.Write(" Input plane model: ");
                flight.PlaneModel = Console.ReadLine();
                Flights[i] = flight;
            }
        }
        public static void Line()
        {
            Console.WriteLine("----------------------------");
        }

        public static void Main(string[] args)
        {
            try
            {
                Aeroflot[] Flights = new Aeroflot[NUMBER_OF_FLIGHTS];

                Line();
                Console.WriteLine(" INPUT THE DATA:");

                GetValues(Flights);

                string pathTxt = "file1.txt";
                // Запис файлу:
                using (StreamWriter sw = new StreamWriter(pathTxt, false, Encoding.UTF8))
                {
                    for (int i = 0; i < NUMBER_OF_FLIGHTS; i++)
                        sw.WriteLine(Flights[i]);
                }

                Line();
                // Читання файлу:
                Console.WriteLine(" READ FILE .txt:\n");
                using (StreamReader sr = new StreamReader(pathTxt, Encoding.UTF8))
                    Console.WriteLine(sr.ReadToEnd());

                List<Aeroflot> list = new List<Aeroflot>();

                list.AddRange(Flights);
                XmlSerializer xmlSerialaizer = new XmlSerializer(typeof(List<Aeroflot>));

                string pathXml = "file2.xml";
                using (FileStream fw = new FileStream(pathXml, FileMode.Create))
                {
                    xmlSerialaizer.Serialize(fw, list);
                }
                Line();

                Console.WriteLine(" READ FILE .xml:\n");
                List<Aeroflot> bludaToRead = new List<Aeroflot>();

                using (FileStream fr = new FileStream(pathXml, FileMode.Open))
                {
                    bludaToRead = (List<Aeroflot>)xmlSerialaizer.Deserialize(fr);
                    foreach (Aeroflot i in bludaToRead)
                    {
                        Console.WriteLine($" Destination: {i.Destination}. Number: {i.Number}. Plane model: {i.PlaneModel}");
                    }
                }
                var sort = Flights.OrderBy(aeroflot => aeroflot.Number); // Впорядковування

                Line();
                Console.WriteLine(" SORTED IN ASCENDING ORDER:\n");
                foreach (var s in sort)
                    Console.WriteLine(s);
                Line();
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
                Line();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}