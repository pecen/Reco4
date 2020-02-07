using Reco4.Dal;
using Reco4.Dal.Dto;
using Reco4.Dal.Dto.Vehicle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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

        //using (XmlReader reader = XmlReader.Create(path)) {
        //  reader.MoveToContent();
        //  reader.Read();
        //  return new VehicleDto {
        //    Vehicles = new XmlSerializer(typeof(Vehicles)).Deserialize(xml) as Vehicles
        //  };
        //}


      }
    }
}
