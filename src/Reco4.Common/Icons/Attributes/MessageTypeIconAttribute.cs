namespace Reco4.Common.Icons.Attributes {
  /// <summary>
  /// Message type attribute.
  /// </summary>
  public sealed class MessageTypeIconAttribute : IconAttribute {
    /// <summary>
    /// Initializes a new instance of the MessageTypeIconAttribute class.
    /// </summary>
    /// <param name="icon">The icon.</param>
    public MessageTypeIconAttribute(MessageTypeIcons icon)
        : base(icon) {
    }
  }
}
