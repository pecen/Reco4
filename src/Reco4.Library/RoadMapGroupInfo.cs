﻿using Csla;
using Reco4.Dal;
using Reco4.Dal.Dto;
using Reco4.Library.Enum;
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

    public static readonly PropertyInfo<string> OwnerSSSProperty = RegisterProperty<string>(c => c.OwnerSSS);
    public string OwnerSSS {
      get { return GetProperty(OwnerSSSProperty); }
      set { LoadProperty(OwnerSSSProperty, value); }
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

    public static readonly PropertyInfo<int> ValidationStatusValueProperty 
      = RegisterProperty<int>(c => c.ValidationStatus);
    public int ValidationStatus {
      get { return GetProperty(ValidationStatusValueProperty); }
      set { LoadProperty(ValidationStatusValueProperty, value); }
    }

    public static readonly PropertyInfo<int> ConvertToVehicleInputStatusValueProperty 
      = RegisterProperty<int>(c => c.ConvertToVehicleInputStatus);
    public int ConvertToVehicleInputStatus {
      get { return GetProperty(ConvertToVehicleInputStatusValueProperty); }
      set { LoadProperty(ConvertToVehicleInputStatusValueProperty, value); }
    }

    #endregion

    #region Data Access

    [FetchChild]
    private void Child_Fetch(RoadmapGroupDto item) {
      RoadmapGroupId = item.RoadmapGroupId;
      OwnerSSS = item.OwnerSss;
      RoadmapName = item.RoadmapName;
      Protected = item.Protected;
      CreationTime = item.CreationTime;
      StartYear = item.StartYear;
      EndYear = item.EndYear;
      Xml = item.Xml;
      ValidationStatus = item.ValidationStatusValue;
      ConvertToVehicleInputStatus = item.ConvertToVehicleInputStatusValue;
    }

    #endregion
  }
}
