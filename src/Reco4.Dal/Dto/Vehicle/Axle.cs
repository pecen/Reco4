using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "Axle")]
  public class Axle {
    [XmlElement(ElementName = "AxleType")]
    public string AxleType { get; set; }
    [XmlElement(ElementName = "TwinTyres")]
    public string TwinTyres { get; set; }
    [XmlElement(ElementName = "Steered")]
    public string Steered { get; set; }
    [XmlElement(ElementName = "TyrePD")]
    public string TyrePD { get; set; }
    [XmlAttribute(AttributeName = "axleNumber")]
    public string AxleNumber { get; set; }
  }
}
