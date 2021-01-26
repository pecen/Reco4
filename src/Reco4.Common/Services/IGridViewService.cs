using System.Collections.Generic;
using Telerik.Windows.Controls.GridView;

namespace Reco4.Common.Services {
  public interface IGridViewService {
    /// <summary>
    /// Gets the text from current cell.
    /// </summary>
    /// <param name="cell">The cell.</param>
    /// <returns>A string with a value if any value exist; Otherwise an empty string.</returns>
    string GetText(GridViewCell cell);

    /// <summary>
    /// Gets the selected rows data.
    /// </summary>
    /// <param name="grid">The grid.</param>
    /// <param name="includeHeader">If set to true include header.</param>
    /// <returns>Selected rows data.</returns>
    IList<IEnumerable<object>> GetSelectedRowsData(GridViewDataControl grid, bool includeHeader = false);
    //IList<IEnumerable<string>> GetSelectedRowsData(GridViewDataControl grid, bool includeHeader = false);
  }
}
