using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace APBD02v2.Models {

    public class Uczelnia {

        [XmlAttribute(AttributeName = "createdAt")]
        public string createdAt { get; set; }

        [XmlAttribute(AttributeName = "author")]
        public string author { get; set; }

        public List<Student> studenci { get; set; }

        public ActiveStudies activeStudies { get; set; }

        public Uczelnia() {
        }
    }
}
