using Reco4.Common.Extensions;

namespace Reco4.Common.Commands {
	/// <summary>
	/// Opens selected cell link.
	/// </summary>
	public class OpenLinkCommand : CommandBase<string> {
		public OpenLinkCommand()
				: base() {
		}

		/// <summary>
		/// Determines whether this instance can execute the specified parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <returns>True if parameter is not null and contains data; Otherwise false.</returns>
		public override bool CanExecute(string parameter) {
			return parameter.IsNotNullOrEmpty();
		}

		/// <summary>
		/// Executes the specified parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public override void Execute(string parameter) {
			System.Diagnostics.Process.Start(parameter);
		}
	}
}
