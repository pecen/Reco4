using Prism.Commands;
using Reco4.Common.Extensions;
using System;

namespace Reco4.Common.Commands {
  /// <summary>
  /// Copy text to the clipboard.
  /// </summary>
  public class CloseApplicationCommand : DelegateCommand<string> {
    /// <summary>
    /// Initializes a new instance of the CopyToClipboardCommand class.
    /// </summary>
    public CloseApplicationCommand()
        : base(Execute, CanExecute) {
    }

    /// <summary>
    /// Determines whether this instance can execute the specified parameter.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <returns>True if parameter is not null and contains data; Otherwise false.</returns>
    public new static bool CanExecute(string parameter) {
      return parameter.IsNotNullOrEmpty();
    }

    /// <summary>
    /// Executes the specified parameter.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    public new static void Execute(string parameter) {
      Environment.Exit(1);
    }
  }
}
