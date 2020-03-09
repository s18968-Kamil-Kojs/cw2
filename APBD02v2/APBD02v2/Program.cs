using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using APBD02v2.Models;

namespace APBD02v2 {
    class Program {
        static void Main(string[] args) {
            string path = @"Data/dane.csv";
            var list = new List<Student>();

            //Wczytywanie pliku
            var fi = new FileInfo(path);
            using (var stream = new StreamReader(fi.OpenRead())) {

                string line = null;
                while ((line = stream.ReadLine()) != null) {
                    string[] kolumny = line.Split(',');
                    Console.WriteLine(line);
                }
            }
            //stream.Dispose();

            //XML
            var st = new Student {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Email = "kowalski@wp.pl"
            };
            list.Add(st);

            FileStream writer = new FileStream(@"data.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>), new XmlRootAttribute("uczelnia"));
            serializer.Serialize(writer, list);
        }
    }
}
