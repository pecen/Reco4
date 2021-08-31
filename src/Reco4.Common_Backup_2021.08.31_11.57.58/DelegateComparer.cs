using System;
using System.Collections.Generic;

namespace Reco4.Common {
  internal class DelegateComparer<T, TIdentity> : IEqualityComparer<T> {
    private readonly Func<T, TIdentity> identitySelector;

    public DelegateComparer(Func<T, TIdentity> identitySelector) {
      this.identitySelector = identitySelector;
    }

    public bool Equals(T x, T y) {
      return Equals(identitySelector(x), identitySelector(y));
    }

    public int GetHashCode(T obj) {
      return identitySelector(obj).GetHashCode();
    }
  }
}
