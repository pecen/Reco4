using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Reco4.Dal.Exceptions {
  [Serializable]
  public class ConcurrencyException : Exception {
    public ConcurrencyException(string message)
      : base(message) { }

    public ConcurrencyException(string message, Exception innerException)
      : base(message, innerException) { }

    protected ConcurrencyException(
      SerializationInfo info,
      StreamingContext context)
      : base(info, context) { }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public override void GetObjectData(
      SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
    }
  }
}
