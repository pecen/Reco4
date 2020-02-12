using Csla;
using Csla.Core;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Reco4.Dal;
using Reco4.Dal.Dto;
using Reco4.Library.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
      set { SetProperty(OwnerSssProperty, value); }
    }

    public static readonly PropertyInfo<string> RoadmapNameProperty = RegisterProperty<string>(c => c.RoadmapName);
    //[Required]
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
    [Required]
    public int StartYear {
      get { return GetProperty(StartYearProperty); }
      set { SetProperty(StartYearProperty, value); }
    }

    public static readonly PropertyInfo<int> EndYearProperty = RegisterProperty<int>(c => c.EndYear);
    [Required]
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

    public static readonly PropertyInfo<VehiclesInfo> VehiclesProperty = RegisterProperty<VehiclesInfo>(c => c.Vehicles);
    public VehiclesInfo Vehicles {
      get { return GetProperty(VehiclesProperty); }
      set { SetProperty(VehiclesProperty, value); }
    }

    public static readonly PropertyInfo<ComponentList> ComponentsProperty = RegisterProperty<ComponentList>(c => c.Components);
    public ComponentList Components {
      get { return GetProperty(ComponentsProperty); }
      set { SetProperty(ComponentsProperty, value); }
    }

    public static readonly PropertyInfo<HashSet<string>> PDNumbersProperty = RegisterProperty<HashSet<string>>(c => c.PDNumbers);
    public HashSet<string> PDNumbers {
      get { return GetProperty(PDNumbersProperty); }
      set { SetProperty(PDNumbersProperty, value); }
    }

    #endregion

    #region Business Rules

    protected override void AddBusinessRules() {
      base.AddBusinessRules();

      BusinessRules.AddRule(new Required(RoadmapNameProperty, "You have to give a name to the Roadmap Group."));
      BusinessRules.AddRule(new ComponentNotExists { PrimaryProperty = XmlProperty });
    }

    protected override void OnPropertyChanged(IPropertyInfo propertyInfo) {
      base.OnPropertyChanged(propertyInfo);

      if (propertyInfo == RoadmapNameProperty) {
        CheckPropertyRules(propertyInfo);
      }
    }

    private class ComponentNotExists : BusinessRule {
      //private HashSet<string> _pdNumbers;

      protected override void Execute(Csla.Rules.IRuleContext context) {
        int _errors = 0;

        var target = (RoadmapGroupEdit)context.Target;

        if (string.IsNullOrEmpty(target.ReadProperty(XmlProperty))) {
          return;
        }

        //target.Components = ComponentList.GetComponents();
        target.Vehicles = VehiclesInfo.GetVehicles(target.ReadProperty(XmlProperty));

        //target.PDNumbers = new HashSet<string>(new PDComparer());
        foreach (var item in target.Components) {
          target.PDNumbers.Add(item.PDNumber);
        }

        //_pdNumbers = target.PDNumbers;

        foreach (var vehicle in target.Vehicles?.Vehicles.Vehicle) {
          //_errors += string.IsNullOrEmpty(vehicle.Components.Engine.EnginePD)
          //  ? 0
          //  : target.PDNumbers.Contains(vehicle.Components.Engine.EnginePD)
          //    ? 0
          //    : 1;
          _errors += GetComponentErrors(vehicle.Components.Engine.EnginePD, target.PDNumbers);
          _errors += GetComponentErrors(vehicle.Components.GearBoxPD, target.PDNumbers);
          _errors += GetComponentErrors(vehicle.Components.AxleGearPD, target.PDNumbers);
          _errors += GetComponentErrors(vehicle.Components.RetarderPD, target.PDNumbers);
          _errors += GetComponentErrors(vehicle.Components.TorqueConverterPD, target.PDNumbers);

          foreach (var axle in vehicle.Components.AxleWheels.Data.Axles.Axle) {
            _errors += GetComponentErrors(axle.TyrePD, target.PDNumbers);
          }
        }

        if(_errors != 0) {
          context.AddErrorResult($"Missing Components in database. Found {_errors} errors in Xml-file." +
            "\nRoadmap Group created but no Xml uploaded");
          target.ValidationStatusValue = ValidationStatus.ValidatedWithFailures;
        }
        else {
          target.ValidationStatusValue = ValidationStatus.ValidatedWithSuccess;
          context.AddInformationResult("Roadmap created/updated successfully!!");
        }
      }

      private int GetComponentErrors(string pd, HashSet<string> pdNumbers) {
        if (!string.IsNullOrEmpty(pd)) {
          return pdNumbers.Contains(pd) ? 0 : 1;
        }

        return 0;
      }
    }

    public class PDComparer : IEqualityComparer<string> {
      const int _multiplier = 89;

      public bool Equals(string x, string y) {
        return x == y;
      }

      public int GetHashCode(string obj) {
        // Stores the result.
        int result = 0;

        // Don't compute hash code on null object.
        if (obj == null) {
          return 0;
        }

        // Get length.
        int length = obj.Length;

        // Return default code for zero-length strings [valid, nothing to hash with].
        if (length > 0) {
          // Compute hash for strings with length greater than 1
          char let1 = obj[0];          // First char of string we use
          char let2 = obj[length - 1]; // Final char

          // Compute hash code from two characters
          int part1 = let1 + length;
          result = (_multiplier * part1) + let2 + length;
        }

        return result;
      }
    }

    #endregion

    #region Factory Methods

    public static RoadmapGroupEdit CreateRoadmapGroup() {
      return DataPortal.Create<RoadmapGroupEdit>();
    }

    public static RoadmapGroupEdit GetRoadmapGroup(int id) {
      return DataPortal.Fetch<RoadmapGroupEdit>(id);

    }

    public static void DeleteRoadmapGroup(int id) {
      DataPortal.Delete<RoadmapGroupEdit>(id);
    }

    public static bool Exists(int id) {
      var cmd = DataPortal.Create<RoadmapGroupExistsCmd>(id);
      cmd = DataPortal.Execute(cmd);
      return cmd.RoadmapGroupExists;
    }

    #endregion

    #region Data Access

    [RunLocal]
    [Create]
    private void Create() {
      Components = ComponentList.GetComponents();
      //Vehicles = VehiclesInfo.GetVehicles(ReadProperty(XmlProperty));
      //LoadProperty(ComponentsProperty, DataPortal.CreateChild<ComponentList>());
      //LoadProperty(VehiclesProperty, DataPortal.CreateChild<VehiclesInfo>());
      LoadProperty(PDNumbersProperty, new HashSet<string>(new PDComparer()));
      base.DataPortal_Create();
    }

    [Fetch]
    private void Fetch(int criteria) {
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
            ValidationStatusValue = (ValidationStatus)data.ValidationStatusValue;
            ConvertToVehicleInputStatusValue = (ConvertToVehicleInputStatus)data.ConvertToVehicleInputStatusValue;
          }
        }
      }
    }

    [Insert]
    private void Insert() {
      using (var ctx = DalFactory.GetManager()) {
        var dal = ctx.GetProvider<Dal.IRoadmapGroupDal>();
        using (BypassPropertyChecks) {
          var item = new RoadmapGroupDto {
            OwnerSss = OwnerSss,
            RoadmapName = RoadmapName,
            Protected = Protected,
            CreationTime = CreationTime,
            StartYear = StartYear,
            EndYear = EndYear,
            Xml = Xml,
            ValidationStatusValue = (int)ValidationStatusValue,
            ConvertToVehicleInputStatusValue = (int)ConvertToVehicleInputStatusValue
          };
          dal.Insert(item);

          RoadmapGroupId = item.RoadmapGroupId;
        }
      }
    }

    [Update]
    private void Update() {
      using (var ctx = DalFactory.GetManager()) {
        var dal = ctx.GetProvider<Dal.IRoadmapGroupDal>();
        using (BypassPropertyChecks) {
          var item = new RoadmapGroupDto {
            RoadmapGroupId = RoadmapGroupId,
            OwnerSss = OwnerSss,
            RoadmapName = RoadmapName,
            Protected = Protected,
            CreationTime = CreationTime,
            StartYear = StartYear,
            EndYear = EndYear,
            Xml = Xml,
            ValidationStatusValue = (int)ValidationStatusValue,
            ConvertToVehicleInputStatusValue = (int)ConvertToVehicleInputStatusValue
          };
          dal.Update(item);
        }
      }
    }

    [DeleteSelf]
    private void DeleteSelf() {
      using (BypassPropertyChecks) {
        Delete(RoadmapGroupId);
      }
    }

    [Delete]
    private void Delete(int id) {
      using (var dalManager = DalFactory.GetManager()) {
        var dal = dalManager.GetProvider<IRoadmapGroupDal>();
        dal.Delete(id);
      }
    }

    #endregion
  }
}
