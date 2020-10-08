using System;

namespace Reco4.Dal.Extensions {
  public static class TimeStampExtensions {
    public static bool Matches(this DateTime stamp1, DateTime stamp2) {
      if (stamp1 != null && stamp2 != null) {
        if (!stamp1.Equals(stamp2)) {
          return false;
        }
        return true;
      }
      return false;
    }
  }
}
