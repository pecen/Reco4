using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "AirDrag")]
  public class AirDrag {
    [XmlElement(ElementName = "AirDragPD")]
    public string AirDragPD { get; set; }
    [XmlElement(ElementName = "AirDragModel")]
    public string AirDragModel { get; set; }
  }
}
