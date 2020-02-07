using System.Collections.Generic;
using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "Axles")]
  public class Axles {
    [XmlElement(ElementName = "Axle")]
    public List<Axle> Axle { get; set; }
  }
}
