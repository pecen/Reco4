using System.ComponentModel;

namespace Reco4.UI.Module.Enums {
  public enum FilterableRoadmapGroupColumns {
    RoadmapGroupId,
    OwnerSss,
    [Description("Validated with success")]
    RoadmapName,
    [Description("Validated with failure")]
    ValidationStatus,
    ConvertToVehicleStatus
  }
}
