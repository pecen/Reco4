using Reco4.Dal;
using Reco4.Dal.Dto;
using Reco4.Dal.Dto.Vehicle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reco4.DalSql {
  public class VehicleDal : IVehicleDal {
    public VehicleDto Fetch(Stream xmlStream) {
      return new VehicleDto {
        Vehicles = new XmlSerializer(typeof(Vehicles)).Deserialize(xmlStream) as Vehicles
      };
    }
  }
}
