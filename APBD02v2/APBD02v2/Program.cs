using System;
using System.Collections.Generic;
using System.IO;
using APBD02v2.Models;

namespace APBD02v2 {
    class Program {
        static void Main(string[] args) {
            string path = @"Data/dane.csv";

            //Wczytywanie pliku
            var fi = new FileInfo(path);
            using (var stream = new StreamReader(fi.OpenRead())) {

                string line = null;
                while ((line = stream.ReadLine()) != null) {
                    Console.WriteLine(line);
                }
            }
            //stream.Dispose();

            //XML
            var list = new List<Student>();
            var st = new Student {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Email = "kowalski@wp.pl"
            };
            list.Add(st);
        }
    }
}
