using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "AxleWheels")]
  public class AxleWheels {
    [XmlElement(ElementName = "Data")]
    public Data Data { get; set; }
  }
}
