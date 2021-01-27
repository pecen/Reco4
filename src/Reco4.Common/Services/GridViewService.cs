using Reco4.Common.Commands.Providers;
using Reco4.Common.Controls.AttachedProperties;
using Reco4.Common.Extensions;
using Reco4.Common.Models;
using Reco4.Library;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using TWC = Telerik.Windows.Controls;

namespace Reco4.Common.Services {
	/// <summary>
	/// Grid view service.
	/// </summary>
	public class GridViewService : IGridViewService {
		/// <summary>
		/// Gets the text from current cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <returns>A string with a value if any value exist; Otherwise an empty string.</returns>
		public string GetText(GridViewCell cell) {
			if (cell.IsNotNull() && cell.Value.IsNotNull()) {
				string result = string.Empty;

				TextBlock provider = cell.Content as TextBlock;

				if (provider.IsNull()) {
					return cell.Value.ToString();
				}

				// Handle when provider is Hyperlink
				if (provider is HyperlinkButton) {
					HyperlinkButton button = cell.Content as HyperlinkButton;
					provider = button.Content as TextBlock;
					provider.IfNotNull(() => result = provider.Text);
					return result;
				}

				if (provider is TextBlock && provider.Visibility == Visibility.Visible) {
					return provider.Text;
				}
			}

			return string.Empty;
		}

		/// <summary>
		/// Gets the selected rows data with headers.
		/// </summary>
		/// <param name="grid">The grid.</param>
		/// <param name="includeHeader">If set to true include header.</param>
		/// <returns>Selected rows data.</returns>
		public IList<IEnumerable<string>> GetSelectedRowsData(GridViewDataControl grid, bool includeHeader = false) {
			List<IEnumerable<string>> result = new List<IEnumerable<string>>();

			grid.SelectedItem.IfNotNull(() => {
				if (includeHeader) {
					result.Add(GetHeaders(grid));
				}

				result.AddRange(GetSelectedData(grid));
			});

			return result;
		}

		// The following method should be checked and have a re-make/re-factoring

		/// <summary>
		/// Gets the selected data.
		/// </summary>
		/// <param name="grid">The grid.</param>
		/// <returns>Selected data.</returns>
		private static IList<IEnumerable<string>> GetSelectedData(GridViewDataControl grid) {
			List<IEnumerable<string>> result = new List<IEnumerable<string>>();
			//List<IEnumerable<object>> result = new List<IEnumerable<object>>();

			IEnumerable<object> selectedItems = OrderSelectedItems(grid);
			//IEnumerable<string> selectedItems = OrderSelectedItems(grid).Cast<string>();

			//selectedItems.ForEach(item => {
			//  if (item.GetType() == typeof(string)) {
			//    IList<string> provider = new List<string> {
			//      item.ToString()
			//    };
			//    result.Add(provider);
			//  }
			//  else {
			//    IDataProvider provider = item as IDataProvider;
			//    provider.IfNotNull(() => {
			//      if (provider.Data != null) {
			//        result.Add(provider.Data);
			//      }
			//    });
			//  }
			//});

			selectedItems.ForEach(item => {
				if (item.GetType() == typeof(string)) {
					IList<string> provider = new List<string> {
						item.ToString()
					};
					//IList<object> provider = new List<object> {
					//  item
					//};
					result.Add(provider);
				} else if (item.GetType() == typeof(RoadmapGroup)) {
					var row = item as RoadmapGroup;
					row.IfNotNull(() => {
						if (row.RoadmapGroupInfo != null) {
							var rg = row.RoadmapGroupInfo;
							var props = rg.ToPropertyStringValues();
							result.Add(props);
						}
					});
				} else {
					IDataProvider provider = item as IDataProvider;
					provider.IfNotNull(() => {
						if (provider.Data != null) {
							result.Add(provider.Data);
						}
					});
				}
			});

			return result;
		}

		/// <summary>
		/// Gets the headers.
		/// </summary>
		/// <param name="grid">The grid.</param>
		/// <returns></returns>
		private static IEnumerable<string> GetHeaders(GridViewDataControl grid) {
			IList<string> list = new List<string>();

			foreach (TWC.GridViewColumn column in grid.Columns) {
				string header = ColumnHeaderText.GetHeaderText(column) ?? column.Header as string;
				if (header.IsNotNull()) {
					list.Add(header);
				}
			}

			return list;
		}

		/// <summary>
		/// Orders the SelectedItems in the grid to the same
		/// order as is displayed in the view
		/// </summary>
		/// <param name="gridView">The grid</param>
		/// <returns>The ordered collection</returns>
		private static IEnumerable<object> OrderSelectedItems(GridViewDataControl gridView) {
			var v = gridView.Items.Cast<object>().Where(t => gridView.SelectedItems.Contains(t));
			return v.ToList();
		}
	}
}
