using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "Components")]
  public class Components {
    [XmlElement(ElementName = "Engine")]
    public Engine Engine { get; set; }
    [XmlElement(ElementName = "GearBoxPD")]
    public string GearBoxPD { get; set; }
    [XmlElement(ElementName = "AxleGearPD")]
    public string AxleGearPD { get; set; }
    [XmlElement(ElementName = "RetarderPD")]
    public string RetarderPD { get; set; }
    [XmlElement(ElementName = "TorqueConverterPD")]
    public string TorqueConverterPD { get; set; }
    [XmlElement(ElementName = "AirDrag")]
    public AirDrag AirDrag { get; set; }
    [XmlElement(ElementName = "Auxiliaries")]
    public Auxiliaries Auxiliaries { get; set; }
    [XmlElement(ElementName = "AxleWheels")]
    public AxleWheels AxleWheels { get; set; }
  }
}
