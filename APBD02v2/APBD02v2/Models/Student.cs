using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace APBD02v2.Models {

    public class Student {

        //prop + tabx2
        [XmlAttribute(AttributeName = "indexNumber")]
        public string Indeks { get; set; }

        [XmlElement(ElementName = "fname")]
        public string Imie { get; set; }

        [XmlElement(ElementName = "lname")]
        //[JsonPropertyName("LastName")]
        public string Nazwisko { get; set; }

        [XmlElement(ElementName = "birthdate")]
        public string Urodz { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "mothersName")]
        public string ImieMatki { get; set; }

        [XmlElement(ElementName = "fathersName")]
        public string ImieOjca { get; set; }

        public Studies studies { get; set; }

        public Student() {
        }
    }
}
