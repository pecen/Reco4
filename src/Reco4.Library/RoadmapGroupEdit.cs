using Csla;
using Reco4.Dal;
using Reco4.Dal.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Library {
  [Serializable]
  public class RoadmapGroupEdit : BusinessBase<RoadmapGroupEdit> {
        
    #region Properties

    public static readonly PropertyInfo<int> RoadmapGroupIdProperty = RegisterProperty<int>(c => c.RoadmapGroupId);
    public int RoadmapGroupId {
      get { return GetProperty(RoadmapGroupIdProperty); }
      set { SetProperty(RoadmapGroupIdProperty, value); }
    }

    public static readonly PropertyInfo<string> OwnerSssProperty = RegisterProperty<string>(c => c.OwnerSss);
    public string OwnerSss {
      get { return GetProperty(OwnerSssProperty); }
      set { SetProperty
(OwnerSssProperty, value); }
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

    public static readonly PropertyInfo<DateTime> CreationTimeProperty = RegisterProperty<DateTime>(c => c.CreationTime);
    public DateTime CreationTime {
      get { return GetProperty(CreationTimeProperty); }
      set { SetProperty(CreationTimeProperty, value); }
    }

    public static readonly PropertyInfo<int> StartYearProperty = RegisterProperty<int>(c => c.StartYear);
    public int StartYear {
      get { return GetProperty(StartYearProperty); }
      set { SetProperty(StartYearProperty, value); }
    }

    public static readonly PropertyInfo<int> EndYearProperty = RegisterProperty<int>(c => c.EndYear);
    public int EndYear {
      get { return GetProperty(EndYearProperty); }
      set { SetProperty(EndYearProperty, value); }
    }

    public static readonly PropertyInfo<string> XmlProperty = RegisterProperty<string>(c => c.Xml);
    public string Xml {
      get { return GetProperty(XmlProperty); }
      set { SetProperty(XmlProperty, value); }
    }

    public static readonly PropertyInfo<ValidationStatus> ValidationStatusValueProperty
      = RegisterProperty<ValidationStatus>(c => c.ValidationStatusValue);
    public ValidationStatus ValidationStatusValue {
      get { return GetProperty(ValidationStatusValueProperty); }
      set { SetProperty(ValidationStatusValueProperty, value); }
    }

    public static readonly PropertyInfo<ConvertToVehicleInputStatus> ConvertToVehicleInputStatusValueProperty
      = RegisterProperty<ConvertToVehicleInputStatus>(c => c.ConvertToVehicleInputStatusValue);
    public ConvertToVehicleInputStatus ConvertToVehicleInputStatusValue {
      get { return GetProperty(ConvertToVehicleInputStatusValueProperty); }
      set { SetProperty(ConvertToVehicleInputStatusValueProperty, value); }
    }

    #endregion

    #region Factory Methods

    public static RoadmapGroupEdit CreateRoadmapGroup() {
      return DataPortal.Create<RoadmapGroupEdit>();
    }

    public static RoadmapGroupEdit GetRoadmapGroup(int id) {
      return DataPortal.Fetch<RoadmapGroupEdit>(id);

    }

    #endregion

    #region Data Access

    [RunLocal]
    [Create]
    protected override void DataPortal_Create() {
      base.DataPortal_Create();
    }

    [Fetch]
    private void DataPortal_Fetch(int criteria) {
      using (var dalManager = DalFactory.GetManager()) {
        var dal = dalManager.GetProvider<IRoadmapGroupDal>();
        var data = dal.Fetch(criteria);
        if (data != null) {
          using (BypassPropertyChecks) {
            RoadmapGroupId = data.RoadmapGroupId;
            OwnerSss = data.OwnerSss;
            RoadmapName = data.RoadmapName;
            Protected = data.Protected;
            CreationTime = data.CreationTime;
            StartYear = data.StartYear;
            EndYear = data.EndYear;
            Xml = data.Xml;
            ValidationStatusValue = data.ValidationStatusValue;
            ConvertToVehicleInputStatusValue = data.ConvertToVehicleInputStatusValue;
          }
        }
      }
    }

    #endregion
  }
}
