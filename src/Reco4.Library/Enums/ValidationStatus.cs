using System.ComponentModel;

namespace Reco4.Library.Enum {
  public enum ValidationStatus {
    Pending,
    Processing,
    [Description("Validated with success")]
    ValidatedWithSuccess,
    [Description("Validated with failures")]
    ValidatedWithFailures
  }
}
