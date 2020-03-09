using System;
using System.IO;

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

        }
    }
}
