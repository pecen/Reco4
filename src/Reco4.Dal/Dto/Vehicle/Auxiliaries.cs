using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "Auxiliaries")]
  public class Auxiliaries {
    [XmlElement(ElementName = "Data")]
    public Data Data { get; set; }
  }
}
