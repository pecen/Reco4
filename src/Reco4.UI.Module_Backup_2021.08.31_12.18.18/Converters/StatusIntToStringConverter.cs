using Reco4.Common.Extensions;
using Reco4.UI.Module.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Reco4.UI.Module.Converters {
  public class StatusIntToStringConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      var s = parameter.ToString();
      string enumValue = string.Empty;
      string description = string.Empty;

      if (!string.IsNullOrEmpty(s)) {
        s = s.Substring(s.LastIndexOf('.') + 1);
        switch (s) {
          case "PDSource":
            description = ((PDSource)value).GetDescription();
            enumValue = string.IsNullOrEmpty(description) ? ((PDSource)value).ToString() : description;
            break;
          case "PDStatus":
            description = ((PDStatus)value).GetDescription();
            enumValue = string.IsNullOrEmpty(description) ? ((PDStatus)value).ToString() : description;
            break;
          case "ComponentType":
            description = ((ComponentType)value).GetDescription();
            enumValue = string.IsNullOrEmpty(description) ? ((ComponentType)value).ToString() : description;
            break;
          case "VehicleMode":
            description = ((VehicleMode)value).GetDescription();
            enumValue = string.IsNullOrEmpty(description) ? ((VehicleMode)value).ToString() : description;
            break;
          case "ValidationStatus":
            description = ((ValidationStatus)value).GetDescription();
            enumValue = string.IsNullOrEmpty(description) ? ((ValidationStatus)value).ToString() : description;
            break;
          case "ConvertToVehicleStatus":
            description = ((ConvertToVehicleStatus)value).GetDescription();
            enumValue = string.IsNullOrEmpty(description) ? ((ConvertToVehicleStatus)value).ToString() : description;
            break;
        }
      }

      return enumValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}
