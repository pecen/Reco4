using System.ComponentModel;

namespace Reco4.Library.Enum {
  public enum ConvertToVehicleInputStatus {
    Pending,
    Processing,
    [Description("Converted with success")]
    ConvertedWithSuccess,
    [Description("Converted with failure")]
    ConvertedWithFailures
  }
}
