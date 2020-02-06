using Csla;
using Reco4.Dal;
using Reco4.Dal.Dto;
using Reco4.Dal.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Library {
  [Serializable]
  public class RoadmapGroupInfo : ReadOnlyBase<RoadmapGroupInfo> {
    #region Properties

    public static readonly PropertyInfo<int> RoadmapGroupIdProperty = RegisterProperty<int>(c => c.RoadmapGroupId);
    public int RoadmapGroupId {
      get { return GetProperty(RoadmapGroupIdProperty); }
      set { LoadProperty(RoadmapGroupIdProperty, value); }
    }

    public static readonly PropertyInfo<string> OwnerSssProperty = RegisterProperty<string>(c => c.OwnerSss);
    public string OwnerSss {
      get { return GetProperty(OwnerSssProperty); }
      set { LoadProperty(OwnerSssProperty, value); }
    }

    public static readonly PropertyInfo<string> RoadmapNameProperty = RegisterProperty<string>(c => c.RoadmapName);
    public string RoadmapName {
      get { return GetProperty(RoadmapNameProperty); }
      set { LoadProperty(RoadmapNameProperty, value); }
    }

    public static readonly PropertyInfo<bool> ProtectedProperty = RegisterProperty<bool>(c => c.Protected);
    public bool Protected {
      get { return GetProperty(ProtectedProperty); }
      set { LoadProperty(ProtectedProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> CreationTimeProperty = RegisterProperty<DateTime>(c => c.CreationTime);
    public DateTime CreationTime {
      get { return GetProperty(CreationTimeProperty); }
      set { LoadProperty(CreationTimeProperty, value); }
    }

    public static readonly PropertyInfo<int> StartYearProperty = RegisterProperty<int>(c => c.StartYear);
    public int StartYear {
      get { return GetProperty(StartYearProperty); }
      set { LoadProperty(StartYearProperty, value); }
    }

    public static readonly PropertyInfo<int> EndYearProperty = RegisterProperty<int>(c => c.EndYear);
    public int EndYear {
      get { return GetProperty(EndYearProperty); }
      set { LoadProperty(EndYearProperty, value); }
    }

    public static readonly PropertyInfo<string> XmlProperty = RegisterProperty<string>(c => c.Xml);
    public string Xml {
      get { return GetProperty(XmlProperty); }
      set { LoadProperty(XmlProperty, value); }
    }

    public static readonly PropertyInfo<ValidationStatus> ValidationStatusValueProperty 
      = RegisterProperty<ValidationStatus>(c => c.ValidationStatusValue);
    public ValidationStatus ValidationStatusValue {
      get { return GetProperty(ValidationStatusValueProperty); }
      set { LoadProperty(ValidationStatusValueProperty, value); }
    }

    public static readonly PropertyInfo<ConvertToVehicleInputStatus> ConvertToVehicleInputStatusValueProperty 
      = RegisterProperty<ConvertToVehicleInputStatus>(c => c.ConvertToVehicleInputStatusValue);
    public ConvertToVehicleInputStatus ConvertToVehicleInputStatusValue {
      get { return GetProperty(ConvertToVehicleInputStatusValueProperty); }
      set { LoadProperty(ConvertToVehicleInputStatusValueProperty, value); }
    }

    #endregion

    #region Data Access

    [FetchChild]
    private void Child_Fetch(RoadmapGroupDto item) {
      RoadmapGroupId = item.RoadmapGroupId;
      OwnerSss = item.OwnerSss;
      RoadmapName = item.RoadmapName;
      Protected = item.Protected;
      CreationTime = item.CreationTime;
      StartYear = item.StartYear;
      EndYear = item.EndYear;
      Xml = item.Xml;
      ValidationStatusValue = item.ValidationStatusValue;
      ConvertToVehicleInputStatusValue = item.ConvertToVehicleInputStatusValue;
    }

    #endregion
  }
}
