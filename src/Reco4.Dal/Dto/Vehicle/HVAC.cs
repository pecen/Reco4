using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "HVAC")]
  public class HVAC {
    [XmlElement(ElementName = "Technology")]
    public string Technology { get; set; }
  }
}
