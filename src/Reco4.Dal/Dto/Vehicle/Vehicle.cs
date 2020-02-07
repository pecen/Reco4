using System.Xml.Serialization;

namespace Reco4.Dal.Dto.Vehicle {
  [XmlRoot(ElementName = "vehicle")]
  public class Vehicle {
    [XmlElement(ElementName = "VIN")]
    public string VIN { get; set; }
    [XmlElement(ElementName = "Date")]
    public string Date { get; set; }
    [XmlElement(ElementName = "Model")]
    public string Model { get; set; }
    [XmlElement(ElementName = "DevelopmentLevel")]
    public string DevelopmentLevel { get; set; }
    [XmlElement(ElementName = "LegislativeClass")]
    public string LegislativeClass { get; set; }
    [XmlElement(ElementName = "VehicleCategory")]
    public string VehicleCategory { get; set; }
    [XmlElement(ElementName = "AxleConfiguration")]
    public string AxleConfiguration { get; set; }
    [XmlElement(ElementName = "CurbMassChassis")]
    public string CurbMassChassis { get; set; }
    [XmlElement(ElementName = "GrossVehicleMass")]
    public string GrossVehicleMass { get; set; }
    [XmlElement(ElementName = "IdlingSpeed")]
    public string IdlingSpeed { get; set; }
    [XmlElement(ElementName = "RetarderType")]
    public string RetarderType { get; set; }
    [XmlElement(ElementName = "RetarderRatio")]
    public string RetarderRatio { get; set; }
    [XmlElement(ElementName = "AngledriveType")]
    public string AngledriveType { get; set; }
    [XmlElement(ElementName = "PTO")]
    public PTO PTO { get; set; }
    [XmlElement(ElementName = "VocationalVehicle")]
    public string VocationalVehicle { get; set; }
    [XmlElement(ElementName = "SleeperCab")]
    public string SleeperCab { get; set; }
    [XmlElement(ElementName = "NgTankSystem")]
    public string NgTankSystem { get; set; }
    [XmlElement(ElementName = "ADAS")]
    public ADAS ADAS { get; set; }
    [XmlElement(ElementName = "ZeroEmissionVehicle")]
    public string ZeroEmissionVehicle { get; set; }
    [XmlElement(ElementName = "HybridElectricHDV")]
    public string HybridElectricHDV { get; set; }
    [XmlElement(ElementName = "DualFuelVehicle")]
    public string DualFuelVehicle { get; set; }
    [XmlElement(ElementName = "MaxNetPower1")]
    public string MaxNetPower1 { get; set; }
    [XmlElement(ElementName = "MaxNetPower2")]
    public string MaxNetPower2 { get; set; }
    [XmlElement(ElementName = "Components")]
    public Components Components { get; set; }
  }
}
