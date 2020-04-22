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
  public class RoadmapEdit : BusinessBase<RoadmapEdit> {
    #region Properties

    public static readonly PropertyInfo<int> RoadmapIdProperty = RegisterProperty<int>(c => c.RoadmapId);
    public int RoadmapId {
      get { return GetProperty(RoadmapIdProperty); }
      set { SetProperty(RoadmapIdProperty, value); }
    }

    public static readonly PropertyInfo<string> RoadmapNameProperty = RegisterProperty<string>(c => c.RoadmapName);
    public string RoadmapName {
      get { return GetProperty(RoadmapNameProperty); }
      set { SetProperty(RoadmapNameProperty, value); }
    }

    public static readonly PropertyInfo<bool> ProtectedProperty = RegisterProperty<bool>(c => c.Protected);
    public bool Protected {
      get { return GetProperty(ProtectedProperty); }
      set { SetProperty(ProtectedProperty, value); }
    }

    public static readonly PropertyInfo<int> ValidationStatusProperty = RegisterProperty<int>(c => c.ValidationStatus);
    public int ValidationStatus {
      get { return GetProperty(ValidationStatusProperty); }
      set { SetProperty(ValidationStatusProperty, value); }
    }

    public static readonly PropertyInfo<int> ConvertToVehicleStatusProperty = RegisterProperty<int>(c => c.ConvertToVehicleStatus);
    public int ConvertToVehicleStatus {
      get { return GetProperty(ConvertToVehicleStatusProperty); }
      set { SetProperty(ConvertToVehicleStatusProperty, value); }
    }

    public static readonly PropertyInfo<int> RoadmapGroupIdProperty = RegisterProperty<int>(c => c.RoadmapGroupId);
    public int RoadmapGroupId {
      get { return GetProperty(RoadmapGroupIdProperty); }
      set { SetProperty(RoadmapGroupIdProperty, value); }
    }

    public static readonly PropertyInfo<int> CurrentYearProperty = RegisterProperty<int>(c => c.CurrentYear);
    public int CurrentYear {
      get { return GetProperty(CurrentYearProperty); }
      set { SetProperty(CurrentYearProperty, value); }
    }

    public static readonly PropertyInfo<int> ImprovedVehicleCountProperty = RegisterProperty<int>(c => c.ImprovedVehicleCount);
    public int ImprovedVehicleCount {
      get { return GetProperty(ImprovedVehicleCountProperty); }
      set { SetProperty(ImprovedVehicleCountProperty, value); }
    }

    #endregion

    #region Data Access

    [FetchChild]
    private void Child_Fetch(int id) {
      //RoadmapId = item.RoadmapId;
      //RoadmapName = item.RoadmapName;
      //Protected = item.Protected;
      //ValidationStatus = item.ValidationStatus;
      //ConvertToVehicleStatus = item.ConvertToVehicleInputStatus;
      //RoadmapGroupId = item.RoadmapGroupId;
      //CurrentYear = item.CurrentYear;
      //ImprovedVehicleCount = item.ImprovedVehicleCount;

      using (var dalManager = DalFactory.GetManager()) {
        var dal = dalManager.GetProvider<IRoadmapDal>();
        var data = dal.Fetch(id);
        if (data != null) {
          using (BypassPropertyChecks) {
            RoadmapId = data.RoadmapId;
            RoadmapName = data.RoadmapName;
            Protected = data.Protected;
            ValidationStatus = data.ValidationStatus;
            ConvertToVehicleStatus = data.ConvertToVehicleInputStatus;
            RoadmapGroupId = data.RoadmapGroupId;
            CurrentYear = data.CurrentYear;
            ImprovedVehicleCount = data.ImprovedVehicleCount;
          }
        }
      }
    }

    #endregion
  }
}
