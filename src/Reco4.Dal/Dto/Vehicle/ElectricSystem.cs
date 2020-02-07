using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "ElectricSystem")]
  public class ElectricSystem {
    [XmlElement(ElementName = "Technology")]
    public string Technology { get; set; }
  }
}
