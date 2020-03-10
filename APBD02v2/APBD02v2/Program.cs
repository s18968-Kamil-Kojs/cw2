using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using APBD02v2.Models;

namespace APBD02v2 {
    class Program {
        static void Main(string[] args) {
            string path;
            string result;
            string dataFormat;
            string logPath = "log.txt";
            var list = new List<Student>();

            if(args.Length != 3) {
                path = @"Data/data.csv";
                result = @"result.xml";
                dataFormat = "xml";
            } else {
                path = args[0];
                result = args[1];
                dataFormat = args[2];
            }

            //If log file doesn't exist the follwing line creates it; if it does the line does nothing
            using(StreamWriter w = File.AppendText("log.txt"));

            //Wczytywanie pliku
            var fi = new FileInfo(path);
            using (var stream = new StreamReader(fi.OpenRead())) {

                string line = null;
                while ((line = stream.ReadLine()) != null) {
                    string[] kolumny = line.Split(',');
                    bool whitespace = false;
                    bool duplicate = false;

                    //Check if any column is whitespace
                    for (int i = 0; i < kolumny.Length; i++) {
                        if (String.IsNullOrWhiteSpace(kolumny[i])) {
                            whitespace = true;
                        }
                    }

                    //Check if there already is this student in a list
                    foreach(Student student in list) {
                        string index = "s" + kolumny[4];
                        if(string.Equals(student.Imie, kolumny[0]) && string.Equals(student.Nazwisko, kolumny[1]) && string.Equals(student.Indeks, index)) {
                            duplicate = true;
                        }
                    }

                    //If any column is missing ignore the line and write it to log file
                    if (kolumny.Length != 9) {
                        StreamWriter streamWriter = File.AppendText(logPath);
                        streamWriter.WriteLine("Brak wszystkich danych: " + line);
                        streamWriter.Close();
                    }
                    //If any column is whitespace ignore it and write it to log file
                    else if(kolumny.Length == 9 && whitespace == true) {
                        StreamWriter streamWriter = File.AppendText(logPath);
                        streamWriter.WriteLine("Brak wszystkich danych(kolumna zawiera znaki biale): " + line);
                        streamWriter.Close();
                    }
                    //If this studentInfo is a duplicate then ignore it and write it to log file
                    else if(kolumny.Length == 9 && duplicate == true) {
                        StreamWriter streamWriter = File.AppendText(logPath);
                        streamWriter.WriteLine("Duplikat: " + line);
                        streamWriter.Close();
                    }
                    //All information is valid -> add it to the list
                    else {
                        Student student = new Student {
                            Imie = kolumny[0],
                            Nazwisko = kolumny[1],
                            studies = new Studies(),
                            Indeks = "s"+kolumny[4],
                            Urodz = kolumny[5],
                            Email = kolumny[6],
                            ImieMatki = kolumny[7],
                            ImieOjca = kolumny[8]
                        };
                        student.studies.Kierunek = kolumny[2];
                        student.studies.Tryb = kolumny[3];
                        list.Add(student);
                    }
                    //Console.WriteLine(line);
                }
            }

            Studenci studenci = new Studenci {
                studenci = list
            };

            FileStream writer = new FileStream(result, FileMode.Create);

            //If serializing in XML
            if(string.Equals("xml", dataFormat)) {
                XmlSerializer serializer = new XmlSerializer(typeof(Studenci), new XmlRootAttribute("uczelnia"));
                serializer.Serialize(writer, studenci);
            }
        }
    }
}
