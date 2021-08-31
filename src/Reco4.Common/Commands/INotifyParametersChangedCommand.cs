using System;

namespace Reco4.Common.Commands {
	/// <summary>
	/// Interface for command parameter changed
	/// </summary>
	public interface INotifyParametersChangedCommand {
		/// <summary>
		/// Occurs when [parameters changed].
		/// </summary>
		event EventHandler ParametersChanged;
	}
}
