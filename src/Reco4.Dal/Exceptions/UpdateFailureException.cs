using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Reco4.Dal.Exceptions {
  [Serializable]
  public class UpdateFailureException : Exception {
    public UpdateFailureException(string message)
      : base(message) { }

    public UpdateFailureException(string message, Exception innerException)
      : base(message, innerException) { }

    protected UpdateFailureException(
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
