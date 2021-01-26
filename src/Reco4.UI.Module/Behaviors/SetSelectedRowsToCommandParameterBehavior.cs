using Microsoft.Xaml.Behaviors;
using Prism.Ioc;
using Reco4.Common.Extensions;
using Reco4.Common.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Windows;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Reco4.UI.Module.Behaviors {
  public class SetSelectedRowsToCommandParameterBehavior : Behavior<RadMenuItem> {
    private IGridViewService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetSelectedRowsToCommandParameterBehavior"/> class.
    /// </summary>
    public SetSelectedRowsToCommandParameterBehavior() {
      try {
        _service = UnityService.Get().Resolve<IGridViewService>();
      }
      catch (Exception ex) { }
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
      RadContextMenu menu = AssociatedObject.Menu as RadContextMenu;
      if (menu.IsNotNull()) {
        GridViewRow row = menu.GetClickedElement<GridViewRow>();
        if (row.IsNotNull()) {
          AssociatedObject.CommandParameter = _service.GetSelectedRowsData(row.GridViewDataControl);
        }
      }
    }
    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e) {
      RadContextMenu menu = AssociatedObject.Menu as RadContextMenu;
      if (menu.IsNotNull()) {
        GridViewRow row = menu.GetClickedElement<GridViewRow>();
        if (row.IsNotNull() && row.IsSelected) {
          IList<IEnumerable<string>> result = new List<IEnumerable<string>> {
            new List<string>()
          };
          AssociatedObject.CommandParameter = result;
        }
      }
    }
  }
}
