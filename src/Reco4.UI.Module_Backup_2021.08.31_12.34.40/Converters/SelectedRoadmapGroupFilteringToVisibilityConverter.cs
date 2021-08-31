﻿using Reco4.UI.Module.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Reco4.UI.Module.Converters {
  public class SelectedRoadmapGroupFilteringToVisibilityConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      var v = (FilterableRoadmapGroupColumns)value;
      var columnName = (FilterableRoadmapGroupColumns)parameter;

      if (columnName == FilterableRoadmapGroupColumns.ValidationStatus
        && v == FilterableRoadmapGroupColumns.ValidationStatus) {
        return Visibility.Visible;
      }
      else if (columnName == FilterableRoadmapGroupColumns.ConvertToVehicleStatus
        && v == FilterableRoadmapGroupColumns.ConvertToVehicleStatus) {
        return Visibility.Visible;
      }

      return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}
