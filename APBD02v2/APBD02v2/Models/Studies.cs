using System;
using System.Xml.Serialization;

namespace APBD02v2.Models {

    public class Studies {

        [XmlElement(ElementName = "name")]
        public string Kierunek { get; set; }

        [XmlElement(ElementName = "mode")]
        public string Tryb { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string activeStudiesName { get; set; }

        [XmlAttribute(AttributeName = "numberOfStudents")]
        public string numberOfStudents { get; set; }

        public Studies() {
        }

        public bool ShouldSerializeKierunek() {
            return !string.IsNullOrWhiteSpace(Kierunek);
        }

        public bool ShouldSerializeTryb() {
            return !string.IsNullOrWhiteSpace(Tryb);
        }

        public bool ShouldSerializeActiveStudiesName() {
            return !string.IsNullOrWhiteSpace(activeStudiesName);
        }

        public bool ShouldSerializeNumberOfStudents() {
            return !string.IsNullOrWhiteSpace(numberOfStudents);
        }
    }
}
