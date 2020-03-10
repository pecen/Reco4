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
using System.IO;

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

    public static readonly PropertyInfo<RoadmapEdit> RoadmapProperty
      = RegisterProperty<RoadmapEdit>(c => c.Roadmap);
    public RoadmapEdit Roadmap {
      get { return GetProperty(RoadmapProperty); }
      set { SetProperty(RoadmapProperty, value); }
    }

    #endregion

    #region Business Rules

    protected override void AddBusinessRules() {
      base.AddBusinessRules();

      BusinessRules.AddRule(new Required(RoadmapNameProperty, "You have to give a name to the Roadmap Group."));
      BusinessRules.AddRule(new ValidStartYear { PrimaryProperty = StartYearProperty, Priority = 0 });
      BusinessRules.AddRule(new ValidEndYear { PrimaryProperty = EndYearProperty, Priority = 0 });
      BusinessRules.AddRule(new StartYearGTEndYear { PrimaryProperty = EndYearProperty, AffectedProperties = { StartYearProperty }, Priority = 1 });
      BusinessRules.AddRule(new ComponentMustExist { PrimaryProperty = XmlProperty });
    }

    private class ComponentMustExist : BusinessRule {
      protected override void Execute(IRuleContext context) {
        int _errors = 0;

        var target = (RoadmapGroupEdit)context.Target;

        if (string.IsNullOrEmpty(target.ReadProperty(XmlProperty))) {
          return;
        }

        var xml = (string)ReadProperty(target, RoadmapGroupEdit.XmlProperty);

        Console.WriteLine($"Start time getting vehicles: {DateTime.Now.ToString("hh:mm:ss fff")}");
        var vehicles = VehiclesInfo.GetVehicles(xml); // target.Xml);  // target.ReadProperty(XmlProperty));
        Console.WriteLine($"End time getting vehicles: {DateTime.Now.ToString("hh:mm:ss fff")}");
        var components = ComponentList.GetComponents();
        Console.WriteLine($"End time getting components: {DateTime.Now.ToString("hh:mm:ss fff")}");
        var pdNumbers = new HashSet<string>(new PDComparer());

        foreach (var item in components) {
          pdNumbers.Add(item.PDNumber);
        }

        Console.WriteLine($"End time setting PDNumbers: {DateTime.Now.ToString("hh:mm:ss fff")}");

        foreach (var vehicle in vehicles?.Vehicles.Vehicle) {
          _errors += GetComponentErrors(vehicle.Components.Engine.EnginePD, pdNumbers);
          _errors += GetComponentErrors(vehicle.Components.GearBoxPD, pdNumbers);
          _errors += GetComponentErrors(vehicle.Components.AxleGearPD, pdNumbers);
          _errors += GetComponentErrors(vehicle.Components.RetarderPD, pdNumbers);
          _errors += GetComponentErrors(vehicle.Components.TorqueConverterPD, pdNumbers);

          foreach (var axle in vehicle.Components.AxleWheels.Data.Axles.Axle) {
            _errors += GetComponentErrors(axle.TyrePD, pdNumbers);
          }
        }

        Console.WriteLine($"End time for component validation: {DateTime.Now.ToString("hh:mm:ss.fff")}");

        if (_errors != 0) {
          context.AddInformationResult($"Missing Components in database. Found {_errors} error{(_errors > 1 ? "s" : string.Empty)} in Xml-file." +
            "\nRoadmap Group created but no Xml uploaded");
          LoadProperty(target, XmlProperty, string.Empty);
          LoadProperty(target, ValidationStatusValueProperty, ValidationStatus.ValidatedWithFailures);
        }
        else {
          target.ValidationStatusValue = ValidationStatus.ValidatedWithSuccess;
          context.AddInformationResult($"Components validated successfully!!");
          context.AddSuccessResult(true);
        }
      }

      private int GetComponentErrors(string pd, HashSet<string> pdNumbers) {
        if (!string.IsNullOrEmpty(pd)) {
          return pdNumbers.Contains(pd) ? 0 : 1;
        }

        return 0;
      }
    }

    private class StartYearGTEndYear : BusinessRule {
      protected override void Execute(IRuleContext context) {
        var target = (RoadmapGroupEdit)context.Target;

        var startYear = target.ReadProperty(StartYearProperty);
        var endYear = target.ReadProperty(EndYearProperty);
        if (startYear.ToString().Length == 4 && endYear.ToString().Length == 4 && startYear > endYear) 
          context.AddErrorResult("Start year can't be after end year");
      }
    }

    private class ValidStartYear : BusinessRule {
      protected override void Execute(IRuleContext context) {
        var target = (RoadmapGroupEdit)context.Target;

        var startYear = target.ReadProperty(StartYearProperty);
        //var endYear = target.ReadProperty(EndYearProperty);

        if (startYear.ToString().Length != 4) {
          context.AddErrorResult("The start year has an incorrect value");
        }
        //if (endYear.ToString().Length != 4) {
        //  context.AddErrorResult("The end year has an incorrect value");
        //}
      }
    }

    private class ValidEndYear : BusinessRule {
      protected override void Execute(IRuleContext context) {
        var target = (RoadmapGroupEdit)context.Target;

        var endYear = target.ReadProperty(EndYearProperty);
        //var endYear = target.ReadProperty(EndYearProperty);

        if (endYear.ToString().Length != 4) {
          context.AddErrorResult("The end year has an incorrect value");
        }
        //if (endYear.ToString().Length != 4) {
        //  context.AddErrorResult("The end year has an incorrect value");
        //}
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
            Roadmap = DataPortal.FetchChild<RoadmapEdit>(RoadmapGroupId);
            //FieldManager.GetChildren();
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
          //FieldManager.UpdateChildren(item.RoadmapGroupId);

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

  #region Comparer

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
}
