using Csla;
using Reco4.Dal;
using Reco4.Dal.Dto.Vehicle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Library {
  [Serializable]
  public class VehiclesInfo : ReadOnlyBase<VehiclesInfo> {
    #region Properties

    public static readonly PropertyInfo<Vehicles> VehiclesProperty = RegisterProperty<Vehicles>(c => c.Vehicles);
    public Vehicles Vehicles {
      get { return GetProperty(VehiclesProperty); }
      set { LoadProperty(VehiclesProperty, value); }
    }

    #endregion

    #region Factory Methods

    public static VehiclesInfo GetVehicles(Stream xmlStream) {
      return DataPortal.Fetch<VehiclesInfo>(xmlStream);
    }

    public static bool ComponentExists(string pdNumber) {
      var cmd = DataPortal.Create<ComponentExistsCmd>(pdNumber);
      cmd = DataPortal.Execute(cmd);
      return cmd.ComponentExists;
    }

    #endregion

    #region Data Access

    [Fetch]
    private void DataPortal_Fetch(Stream xmlStream) {
      using (var dalManager = DalFactory.GetManager()) {
        var dal = dalManager.GetProvider<IVehicleDal>();
        var data = dal.Fetch(xmlStream);

        if (data != null) {
          Vehicles = data.Vehicles;
          //foreach (var item in data) {
          //  Add(DataPortal.FetchChild<RoadmapGroupInfo>(item));
          //}
        }
      }
    }

    #endregion
  }
}
