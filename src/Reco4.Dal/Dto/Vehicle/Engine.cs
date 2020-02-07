using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "Engine")]
  public class Engine {
    [XmlElement(ElementName = "EnginePD")]
    public string EnginePD { get; set; }
    [XmlElement(ElementName = "EngineStrokeVolume")]
    public string EngineStrokeVolume { get; set; }
  }
}
