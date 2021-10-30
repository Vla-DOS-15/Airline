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

        public override string ToString() => $"Destination: {Destination}. Number: {Number}. Plane model: {PlaneModel}";
    }
    class Program
    {
        const int NUMBER_OF_FLIGHTS = 3;
        static string Line() => "_______________________________________";

        static void GetValues(Airline[] Flights)
        {
            Console.WriteLine(Line());

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
            Console.WriteLine(Line());
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

            Console.WriteLine(Line());
        }
        static void WriteXml(string pathXml, Airline[] Flights, XmlSerializer xmlSerialaizer)
        {
            List<Airline> list = new List<Airline>();
            list.AddRange(Flights);

            using (FileStream fw = new FileStream(pathXml, FileMode.OpenOrCreate))
                xmlSerialaizer.Serialize(fw, list);
        }
        static void ReadXml(string pathXml, XmlSerializer xmlSerialaizer)
        {
            List<Airline> FlightsToRead = new List<Airline>();

            using (FileStream fr = new FileStream(pathXml, FileMode.Open))
                FlightsToRead = (List<Airline>)xmlSerialaizer.Deserialize(fr);

            foreach (Airline i in FlightsToRead)
                Console.WriteLine($" Destination: {i.Destination}. Number: {i.Number}. Plane model: {i.PlaneModel}");

            Console.WriteLine(Line());
        }

        static void Sort(Airline[] Flights)
        {
            var sort = Flights.OrderBy(aeroflot => aeroflot.Number);

            Console.WriteLine(" SORTED IN ASCENDING ORDER:\n");
            foreach (var s in sort)
                Console.WriteLine(s);

            Console.WriteLine(Line());
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
                    Console.WriteLine($" Number: {Flights[i].Number}. Plane Model: {Flights[i].PlaneModel}");
                    matches++;
                }
            }
            if (matches == 0)
            {
                Console.WriteLine("\n THERE IS NO MATCHES!!");
            }
            Console.WriteLine(Line());
        }

        public static void Main(string[] args)
        {
            try
            {
                Airline[] Flights = new Airline[NUMBER_OF_FLIGHTS];

                Console.WriteLine(" INPUT THE DATA:");

                GetValues(Flights);

                string pathTxt = "file1.txt";
                WriteTxt(pathTxt, Flights);

                Console.WriteLine(" READ FILE .txt:\n");
                ReadTxt(pathTxt);

                XmlSerializer xmlSerialaizer = new XmlSerializer(typeof(List<Airline>));

                string pathXml = "file2.xml";
                WriteXml(pathXml, Flights, xmlSerialaizer);

                Console.WriteLine(" READ FILE .xml:\n");
                ReadXml(pathXml, xmlSerialaizer);

                Sort(Flights);

                CheckFlight(Flights);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}