using Reco4.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reco4.Common.Commands {
	public class CopySelectedRowsCommand : CommandBase<IList<IEnumerable<string>>> {
		/// <summary>
		/// Initializes a new instance of the CopySelectedRowsCommand class.
		/// </summary>
		public CopySelectedRowsCommand()
				: base() {
		}

		/// <summary>
		/// Determines whether this instance can execute the specified parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <returns>True if parameter is not null and contains data; Otherwise false.</returns>
		public override bool CanExecute(IList<IEnumerable<string>> parameter) {
			return (parameter.IsNotNull() && !parameter.IsEmpty());
		}

		/// <summary>
		/// Executes the specified parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public override void Execute(IList<IEnumerable<string>> parameter) {
			lock (new object()) {
				if (parameter.IsNotNull() && !parameter.IsEmpty()) {
					string text = string.Concat(
																	string.Join(Environment.NewLine, parameter.Select(fields =>
																			string.Join(@" | ", fields).Replace(Environment.NewLine, @" / "))),
																	Environment.NewLine);

					Clipboard.Clear();
					Clipboard.SetText(text);
				}
			}
		}
	}
}
