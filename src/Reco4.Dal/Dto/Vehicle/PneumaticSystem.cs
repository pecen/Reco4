using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "PneumaticSystem")]
  public class PneumaticSystem {
    [XmlElement(ElementName = "Technology")]
    public string Technology { get; set; }
  }
}
