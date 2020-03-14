using System;
using System.Collections;
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
            try {
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
                        foreach (Student student in list) {
                            string index = "s" + kolumny[4];
                            if (string.Equals(student.Imie, kolumny[0]) && string.Equals(student.Nazwisko, kolumny[1]) && string.Equals(student.Indeks, index)) {
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
                        else if (kolumny.Length == 9 && whitespace == true) {
                            StreamWriter streamWriter = File.AppendText(logPath);
                            streamWriter.WriteLine("Brak wszystkich danych(kolumna zawiera znaki biale): " + line);
                            streamWriter.Close();
                        }
                        //If this studentInfo is a duplicate then ignore it and write it to log file
                        else if (kolumny.Length == 9 && duplicate == true) {
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
                                Indeks = "s" + kolumny[4],
                                Urodz = kolumny[5],
                                Email = kolumny[6],
                                ImieMatki = kolumny[7],
                                ImieOjca = kolumny[8]
                            };
                            student.studies.Kierunek = kolumny[2];
                            student.studies.Tryb = kolumny[3];
                            list.Add(student);
                        }
                    }
                }
            } catch (FileNotFoundException fileNotFoundException) {
                StreamWriter streamWriter = File.AppendText(logPath);
                streamWriter.WriteLine("Plik " + path + " nie istnieje");
                streamWriter.Close();
                Console.WriteLine("Plik " + path + " nie istnieje");
            }

            //getting dictionary with unique activeStudies and their student numbers
            Dictionary<string, int> studentNumbers = getActiveStudiesDict(list);
            //putting unique activeStudies into an array
            Studies[] studies = new Studies[studentNumbers.Count];
            int counter = 0;

            foreach (KeyValuePair<string, int> entry in studentNumbers) {
                studies[counter] = new Studies {
                    activeStudiesName = entry.Key,
                    numberOfStudents = ((int)entry.Value).ToString()
                };
                counter++;
            }

            ActiveStudies activeStudies = new ActiveStudies() {
                studies = studies
            };

            //combining final Uczelnia object to serialize
            Uczelnia uczelnia = new Uczelnia {
                createdAt = "13.03.2020",
                author = "Kamil Kojs",
                studenci = list,
                activeStudies = activeStudies
            };

            //preparing fileStream to save XML to file
            FileStream writer = null;
            try {
                writer = new FileStream(result, FileMode.Create);
            } catch (ArgumentException argumentException) {
                StreamWriter streamWriter = File.AppendText(logPath);
                streamWriter.WriteLine("Podana sciezka jest niepoprawna");
                streamWriter.Close();
                Console.WriteLine("Podana sciezka jest niepoprawna");
            }
            //preparing XML serializer
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);

            //If serializing in XML
            if(string.Equals("xml", dataFormat)) {
                XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia));
                serializer.Serialize(writer, uczelnia, xns);        
            }
        }

        //this function returns a dictionary with unique studies along with their student count
        public static Dictionary<string, int> getActiveStudiesDict(List<Student> list) {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            foreach (Student student in list) {
                if (dictionary.ContainsKey(student.studies.Kierunek)) {
                    int ammount = dictionary[student.studies.Kierunek];
                    dictionary[student.studies.Kierunek] = ammount+1;
                } else {
                    dictionary[student.studies.Kierunek] = 1;
                }
            }

            return dictionary;
        }
    }
}
