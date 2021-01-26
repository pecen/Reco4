using System.ComponentModel;

namespace Reco4.UI.Module.Enums {
  public enum ValidationStatus {
    Pending,
    Processing,
    [Description("Validated with success")]
    ValidatedWithSuccess,
    [Description("Validated with failure")]
    ValidatedWithFailures
  }
}
