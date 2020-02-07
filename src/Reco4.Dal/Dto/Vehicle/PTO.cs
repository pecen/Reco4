using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "PTO")]
  public class PTO {
    [XmlElement(ElementName = "PTOShaftsGearWheels")]
    public string PTOShaftsGearWheels { get; set; }
    [XmlElement(ElementName = "PTOOtherElements")]
    public string PTOOtherElements { get; set; }
  }
}
