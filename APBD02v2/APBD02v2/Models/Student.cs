using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace APBD02v2.Models {

    public class Student {

        public Student() {
        }

        //prop + tabx2

        [XmlAttribute(AttributeName = "Imie")]
        public string Imie { get; set; }

        [XmlAttribute(AttributeName = "Nazwisko")]
        //[JsonPropertyName("LastName")]
        public string Nazwisko { get; set; }

        [XmlAttribute(AttributeName = "kierunek")]
        public string Kierunek { get; set; }

        [XmlAttribute(AttributeName = "tryb")]
        public string Tryb { get; set; }

        [XmlAttribute(AttributeName = "jakas liczba")]
        public string Liczba { get; set; }

        [XmlAttribute(AttributeName = "data urodzenia")]
        public string Urodz { get; set; }

        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }

        [XmlAttribute(AttributeName = "imie matki")]
        public string ImieMatki { get; set; }

        [XmlAttribute(AttributeName = "imie ojca")]
        public string ImieOjca { get; set; }
    }
}
