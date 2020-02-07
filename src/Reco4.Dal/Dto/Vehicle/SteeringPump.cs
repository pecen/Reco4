using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "SteeringPump")]
  public class SteeringPump {
    [XmlElement(ElementName = "Technology")]
    public string Technology { get; set; }
  }
}
