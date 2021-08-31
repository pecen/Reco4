using Prism.Commands;
using Reco4.Common.Extensions;
using System;
using System.Windows;

namespace Reco4.Common.Commands {
	/// <summary>
	/// Copy text to the clipboard.
	/// </summary>
	public class CopyToClipboardCommand : DelegateCommand<string> {
		/// <summary>
		/// Initializes a new instance of the CopyToClipboardCommand class.
		/// </summary>
		public CopyToClipboardCommand()
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
			lock (new object()) {
				if (parameter.IsNotNullOrEmpty()) {
					Clipboard.Clear();
					Clipboard.SetText(parameter.Trim() + Environment.NewLine);
				}
			}
		}
	}
}
