using Reco4.Library;
using Reco4.UI.Module.Enums;
using System;

namespace Reco4.UI.Module.Services {
  public interface IFilteredListService {
    Func<RoadmapGroupInfo, bool> GetFilteredRoadmapGroupList(FilterableRoadmapGroupColumns column, int selection, string searchText = "");
  }
}
