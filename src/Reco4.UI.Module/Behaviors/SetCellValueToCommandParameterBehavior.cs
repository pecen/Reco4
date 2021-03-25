using Microsoft.Xaml.Behaviors;
using Prism.Ioc;
using Reco4.Common.Extensions;
using Reco4.Common.Services;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Reco4.UI.Module.Behaviors {
  public class SetCellValueToCommandParameterBehavior : Behavior<RadMenuItem> {
    /// <summary>
    /// The grid view service.
    /// </summary>
    private readonly IGridViewService _gridViewService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetCellValueToCommandParameterBehavior"/> class.
    /// </summary>
    public SetCellValueToCommandParameterBehavior() {
      try {
        _gridViewService = UnityService.Get().Resolve<IGridViewService>();
      }
      catch {
      }
    }

    /// <summary>
    /// Called after the behavior is attached to an AssociatedObject.
    /// </summary>
    protected override void OnAttached() {
      base.OnAttached();
      AssociatedObject.Click += AssociatedObject_Click;
      AssociatedObject.Loaded += AssociatedObject_Loaded;
    }

    /// <summary>
    /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    /// <remarks>
    /// Override this to unhook functionality from the AssociatedObject.
    /// </remarks>
    protected override void OnDetaching() {
      base.OnDetaching();
      AssociatedObject.Click -= AssociatedObject_Click;
      AssociatedObject.Loaded -= AssociatedObject_Loaded;
    }
    private void AssociatedObject_Click(object sender, RadRoutedEventArgs e) {
      AssociatedObject.CommandParameter = GetCellValue();
    }
    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e) {
      RadContextMenu menu = AssociatedObject.Menu as RadContextMenu;
      if (menu.IsNotNull()) {
        GridViewCell cell = menu.GetClickedElement<GridViewCell>();
        if (cell.IsNotNull()
            && cell.Value.IsNotNull()
            && cell.IsSelected) {
          TextBlock provider = cell.Content as TextBlock;

          if (provider.IsNull()) {
            AssociatedObject.CommandParameter = cell.Value.ToString();
          }
          else {
            AssociatedObject.CommandParameter = provider.Text == @"\0" ? string.Empty : provider.Text;
          }
        }
      }
    }

    /// <summary>
    /// Gets the cell value
    /// </summary>
    /// <returns></returns>
    private string GetCellValue() {
      RadContextMenu menu = AssociatedObject.Menu as RadContextMenu;
      if (menu.IsNotNull()) {
        GridViewCell cell = menu.GetClickedElement<GridViewCell>();

        if (cell.IsNotNull()) { // && cell.Value.IsNotNull()) {
          TextBlock provider = cell.Content as TextBlock;

          if (provider.IsNull()) {
            return cell.Value.ToString();
          }

          //if (provider.FontFamily.ToString() == NodeListItemTypes.IconFontFamily.GetEnumDescription()) {
          //  return GetIconCellValue(cell.Column.UniqueName, Convert.ToChar(provider.Text));
          //}

          return _gridViewService.GetText(cell);
        }
      }
      return string.Empty;
    }

    /// <summary>
    /// Gets the cell value if the cell holds an icon
    /// </summary>
    /// <param name="columnUniqueName"></param>
    /// <param name="iconChar"></param>
    /// <returns></returns>
    //private static string GetIconCellValue(string columnUniqueName, char? iconChar) {
    //  if (columnUniqueName == NodeListUniqueNames.CANID.GetEnumDescription()) {
    //    return NodeListItemTypes.PerformsCANIdTransformation.GetEnumDescription();
    //  }

    //  if (columnUniqueName == NodeListUniqueNames.NonProgrammable.GetEnumDescription()) {
    //    return NodeListItemTypes.NonProgrammable.GetEnumDescription();
    //  }

    //  if (columnUniqueName == NodeListUniqueNames.NodeConsistency.GetEnumDescription()) {
    //    return new CharToNotificationSeverityConverter().Convert(iconChar);
    //  }

    //  if (columnUniqueName == ActivityUniqueNames.ActivityStatus.GetEnumDescription()) {
    //    if (iconChar.HasValue) {
    //      ProcessStatus? iconValue = new ProcessStatusToIconConverter().ConvertBack(iconChar.Value.GetIcon<EventStateIcon>());
    //      return EnumUtilities.GetEnumDescription(iconValue);
    //    }

    //    return string.Empty;
    //  }

    //  return iconChar.ToString();
    //}
  }
}
