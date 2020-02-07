using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "Data")]
  public class Data {
    [XmlElement(ElementName = "Fan")]
    public Fan Fan { get; set; }
    [XmlElement(ElementName = "SteeringPump")]
    public SteeringPump SteeringPump { get; set; }
    [XmlElement(ElementName = "ElectricSystem")]
    public ElectricSystem ElectricSystem { get; set; }
    [XmlElement(ElementName = "PneumaticSystem")]
    public PneumaticSystem PneumaticSystem { get; set; }
    [XmlElement(ElementName = "HVAC")]
    public HVAC HVAC { get; set; }
    [XmlElement(ElementName = "Axles")]
    public Axles Axles { get; set; }
  }
}
