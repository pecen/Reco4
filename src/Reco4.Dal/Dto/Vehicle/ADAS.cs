using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "ADAS")]
  public class ADAS {
    [XmlElement(ElementName = "EngineStopStart")]
    public string EngineStopStart { get; set; }
    [XmlElement(ElementName = "EcoRollWithoutEngineStop")]
    public string EcoRollWithoutEngineStop { get; set; }
    [XmlElement(ElementName = "EcoRollWithEngineStop")]
    public string EcoRollWithEngineStop { get; set; }
    [XmlElement(ElementName = "PredictiveCruiseControl")]
    public string PredictiveCruiseControl { get; set; }
  }
}
