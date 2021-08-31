using System.Windows;
using Telerik.Windows.Controls;

namespace Reco4.Common.Controls.AttachedProperties {
  public static class ColumnHeaderText {
    /// <summary>
    /// Gets the header text.
    /// </summary>
    /// <param name="dataGrid">The data grid.</param>
    /// <returns></returns>
    public static string GetHeaderText(DependencyObject dataGrid) {
      return (string)dataGrid.GetValue(HeaderTextProperty);
    }

    /// <summary>
    /// Sets the header text.
    /// </summary>
    /// <param name="dataGrid">The data grid.</param>
    /// <param name="value">The value.</param>
    public static void SetHeaderText(DependencyObject dataGrid, string value) {
      dataGrid.SetValue(HeaderTextProperty, value);
    }

    /// <summary>
    /// The header text property
    /// </summary>
    public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.RegisterAttached("HeaderText",
        typeof(string),
        typeof(GridViewDataColumn),
        new PropertyMetadata(null));
  }
}
