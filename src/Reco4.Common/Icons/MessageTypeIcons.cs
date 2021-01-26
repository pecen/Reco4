using Reco4.Common.Icons.Attributes;

namespace Reco4.Common.Icons {
  /// <summary>
  /// Message type icon.
  /// </summary>
  public enum MessageTypeIcons {
    /// <summary>
    /// The information.
    /// </summary>
    [IconDescriptor('\uE6a2')]
    Information,

    /// <summary>
    /// The warning.
    /// </summary>
    [IconDescriptor('\uE6a1')]
    Warning,

    /// <summary>
    /// The error.
    /// </summary>
    [IconDescriptor('\ue6a0')]
    Error,
  }
}
