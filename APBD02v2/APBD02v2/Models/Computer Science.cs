using System;
using System.Xml.Serialization;

namespace APBD02v2.Models {

    public class Computer_Science {

        [XmlAttribute(AttributeName = "name")]
        public string name { get; set; }

        [XmlAttribute(AttributeName = "numberOfStudents")]
        public int numberOfStudents { get; set; }

        public Computer_Science() {
        }
    }
}
