using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "Fan")]
  public class Fan {
    [XmlElement(ElementName = "Technology")]
    public string Technology { get; set; }
  }
}
