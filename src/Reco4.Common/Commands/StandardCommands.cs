using Reco4.Common.Extensions;

namespace Reco4.Common.Commands {
	/// <summary>
	/// Standard commands.
	/// </summary>
	public static class StandardCommands {
		/// <summary>
		/// The copy grid cell command.
		/// </summary>
		private static CopyToClipboardCommand _copyToClipboardCommand;

		/// <summary>
		/// The copy grid cell command.
		/// </summary>
		private static OpenEmailCommand _openEmailCommand;

		/// <summary>
		/// The close application command.
		/// </summary>
		private static CloseApplicationCommand _closeApplicationCommand;

		/// <summary>
		/// Gets the copy to clipboard command.
		/// </summary>
		public static CopyToClipboardCommand CopyToClipboardCommand {
			get {
				if (_copyToClipboardCommand.IsNull()) {
					_copyToClipboardCommand = new CopyToClipboardCommand();
				}

				return _copyToClipboardCommand;
			}
		}

		/// <summary>
		/// Gets the open email command.
		/// </summary>
		public static OpenEmailCommand OpenEmailCommand {
			get {
				if (_openEmailCommand.IsNull()) {
					_openEmailCommand = new OpenEmailCommand();
				}

				return _openEmailCommand;
			}
		}

		/// <summary>
		/// Gets the close application command.
		/// </summary>
		public static CloseApplicationCommand CloseApplicationCommand {
			get {
				if (_closeApplicationCommand.IsNull()) {
					_closeApplicationCommand = new CloseApplicationCommand();
				}

				return _closeApplicationCommand;
			}
		}

	}
}
