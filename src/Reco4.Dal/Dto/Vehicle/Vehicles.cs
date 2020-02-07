using System.Collections.Generic;
using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "vehicles")]
  public class Vehicles {
    [XmlElement(ElementName = "vehicle")]
    public List<Vehicle> Vehicle { get; set; }
  }
}
