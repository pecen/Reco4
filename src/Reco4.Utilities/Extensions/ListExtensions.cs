using System;
using System.Collections.ObjectModel;

namespace Reco4.Utilities.Extensions {
  public static class ListExtensions {
    public static void GetEnumValues<T>(this ObservableCollection<string> list) where T : Enum {
      foreach (T item in Enum.GetValues(typeof(T))) {
        var description = item.GetDescription();
        list.Add(string.IsNullOrEmpty(description)
          ? item.ToString()
          : description);
      }
    }
  }
}
