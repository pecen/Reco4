using Reco4.Library;
using Reco4.UI.Module.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.UI.Module.Services {
  public class FilteredListService : IFilteredListService {
    public Func<RoadmapGroupInfo, bool> GetFilteredRoadmapGroupList(FilterableRoadmapGroupColumns column, int selection, string searchText = "") {
      Func<RoadmapGroupInfo, bool> filter;

      switch (column) {
        case FilterableRoadmapGroupColumns.ValidationStatus:
          filter = r => r.ValidationStatus == selection;
          break;
        case FilterableRoadmapGroupColumns.ConvertToVehicleStatus:
          filter = r => r.ConvertToVehicleInputStatus == selection;
          break;
        default:
          filter = r => r.RoadmapName.Contains(searchText);
          break;
      }

      return filter;
    }
  }
}
