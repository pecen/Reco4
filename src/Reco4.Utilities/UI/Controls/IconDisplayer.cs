using Reco4.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using E = Reco4.Common.Extensions;

namespace Reco4.Utilities.UI.Controls {
  /// <summary>
  /// Icon displayer control.
  /// </summary>
  public class IconDisplayer : TextBlock {
    /// <summary>
    /// Internal cache of available icons
    /// </summary>
    private static Dictionary<KeyValuePair<Type, Enum>, char> IconsCache = new Dictionary<KeyValuePair<Type, Enum>, char>();

    /// <summary>
    /// Initializes a new instance of the <see cref="IconDisplayer"/> class.
    /// </summary>
    public IconDisplayer() {
      Loaded += IconDisplayerLoaded;
    }

    /// <summary>
    /// Gets or sets the label.
    /// </summary>
    public string Label {
      get { return (string)GetValue(LabelProperty); }
      set { SetValue(LabelProperty, value); }
    }

    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    public Enum Icon {
      get { return (Enum)GetValue(IconProperty); }
      set { SetValue(IconProperty, value); }
    }

    /// <summary>
    /// Gets the copy text.
    /// </summary>
    public string CopyText {
      get { return string.Format("[{0}] {1}", Icon.GetAltText(), Label); }
    }

    /// <summary>
    /// The label property.
    /// </summary>
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(E.Name.Of<IconDisplayer>(x => x.Label),
            typeof(string),
            typeof(IconDisplayer),
            new PropertyMetadata(new PropertyChangedCallback(TextChanged)));

    /// <summary>
    /// The icon property.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(E.Name.Of<IconDisplayer>(x => x.Icon),
            typeof(Enum),
            typeof(IconDisplayer),
            new PropertyMetadata(new PropertyChangedCallback(TextChanged)));

    /// <summary>
    /// Icons the changed handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The DependencyPropertyChangedEventArgs instance containing the event data.</param>
    public static void TextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args) {
      IconDisplayer iconDisplayer = (IconDisplayer)sender;
      iconDisplayer.UpdateText();
    }

    /// <summary>
    /// Updates the text.
    /// </summary>
    public void UpdateText() {
      KeyValuePair<Type, Enum> key = new KeyValuePair<Type, Enum>(null, Icon);
      if (Icon.IsNotNull()) {
        key = new KeyValuePair<Type, Enum>(Icon.GetType(), Icon);
      }
      if (!IconsCache.TryGetValue(key, out char charcode)) {
        charcode = Icon.GetIconCharacter();
        IconsCache.Add(key, charcode);
      }

      Text = $"{charcode}{Label}";
    }

    /// <summary>
    /// Handles the Loaded event of the IconDisplayer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void IconDisplayerLoaded(object sender, RoutedEventArgs e) {
      UpdateText();
    }
  }
}
