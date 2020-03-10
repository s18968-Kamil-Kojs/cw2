using System;
using System.Xml.Serialization;

namespace APBD02v2.Models {

    public class Studies {

        [XmlElement(ElementName = "name")]
        public string Kierunek { get; set; }

        [XmlElement(ElementName = "mode")]
        public string Tryb { get; set; }

        public Studies() {
        }
    }
}
