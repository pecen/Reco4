using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Reco4.Common.Controls.AttachedProperties {
  /// <summary>
  /// RadGridView copy options.
  /// </summary>
  public static class RadGridViewCopyOptions {
    /// <summary>
    /// Gets the command.
    /// </summary>
    /// <param name="dataGrid">The data grid.</param>
    /// <returns>The command.</returns>
    public static ICommand GetCommand(DependencyObject dataGrid) {
      return (ICommand)dataGrid.GetValue(CommandProperty);
    }

    /// <summary>
    /// Sets the command.
    /// </summary>
    /// <param name="dataGrid">The data grid.</param>
    /// <param name="value">The value.</param>
    public static void SetCommand(DependencyObject dataGrid, ICommand value) {
      dataGrid.SetValue(CommandProperty, value);
    }

    /// <summary>
    /// The command property.
    /// </summary>
    public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command",
        typeof(ICommand),
        typeof(RadGridViewCopyOptions),
        new PropertyMetadata(CommandPropertyChanged));

    /// <summary>
    /// Handles the command property changed event.
    /// </summary>
    /// <param name="dependencyObject">The dependency object.</param>
    /// <param name="eventArgs">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
    private static void CommandPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs) {
      RadGridView gridView = dependencyObject as RadGridView;
      ICommand command = (ICommand)eventArgs.NewValue;
      KeyBinding keyBinding = new KeyBinding(command, new KeyGesture(Key.C, ModifierKeys.Control)) {
        CommandParameter = gridView
      };
      gridView.InputBindings.Add(keyBinding);
    }
  }
}
