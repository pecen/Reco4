using System.ComponentModel;

namespace Reco4.UI.Module.Enums {
  public enum FilterableRoadmapGroupColumns {
    RoadmapGroupId,
    OwnerSss,
    [Description("Roadmap Name")]
    RoadmapName,
    [Description("Validation Status")]
    ValidationStatus,
    [Description("Convertion Status")]
    ConvertToVehicleStatus
  }
}
