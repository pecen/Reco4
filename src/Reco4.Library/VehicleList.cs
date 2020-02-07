using Csla;
using Reco4.Dal;
using Reco4.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Library {
  [Serializable]
  public class VehicleList : ReadOnlyListBase<VehicleList, VehiclesInfo> {
    #region Factory Methods

    public static VehicleList GetVehicles() {
      return DataPortal.Fetch<VehicleList>();
    }

    #endregion

    #region Data Access

    [Fetch]
    private void DataPortal_Fetch(string path) {
      //var rlce = RaiseListChangedEvents;
      //RaiseListChangedEvents = false;
      //IsReadOnly = false;

      //using (var dalManager = DalFactory.GetManager()) {
      //  IVehicleDal dal = dalManager.GetProvider<IVehicleDal>();

      //   var data = dal.Fetch(path);

      //  if (data != null) {

      //    //foreach (var item in data) {
      //    //  Add(DataPortal.FetchChild<RoadmapGroupInfo>(item));
      //    //}
      //  }
      //}
    }

    #endregion
  }
}
