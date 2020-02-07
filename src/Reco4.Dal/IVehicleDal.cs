using Reco4.Dal.Dto;
using System.IO;

namespace Reco4.Dal {
  public interface IVehicleDal {
    VehicleDto Fetch(Stream xmlStream);
  }
}
