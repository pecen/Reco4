using System.ComponentModel;

namespace Reco4.UI.Module.Enums {
  public enum ConvertToVehicleStatus {
    Pending,
    Processing,
    [Description("Converted with success")]
    ConvertedWithSuccess,
    [Description("Converted with failure")]
    ConvertedWithFailures
  }
}
