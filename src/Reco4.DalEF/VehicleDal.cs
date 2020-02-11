using Reco4.Dal;
using Reco4.Dal.Dto;
using Reco4.Dal.Dto.Vehicle;
using System.IO;
using System.Xml.Serialization;

namespace Reco4.DalEF {
  public class VehicleDal : IVehicleDal {
    public VehicleDto Fetch(Stream xmlStream) {
      // Use the following if file path is used
      //XmlDocument xml = new XmlDocument();
      //xml.Load(path);
      //return new VehicleDto {
      //  Vehicles = new XmlSerializer(typeof(Vehicles))
      //    .Deserialize(new FileStream(path, FileMode.Open, FileAccess.Read)) as Vehicles
      //};

      // Use the following if stream is used
      return new VehicleDto {
        Vehicles = new XmlSerializer(typeof(Vehicles)).Deserialize(xmlStream) as Vehicles
      };
    }
  }
}
