using Csla;
using Reco4.Dal;
using Reco4.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Library {
  [Serializable]
  public class RoadmapGroupList : ReadOnlyListBase<RoadmapGroupList, RoadmapGroupInfo> {
    #region Factory Methods

    public static RoadmapGroupList GetRoadmapGroups() {
      return DataPortal.Fetch<RoadmapGroupList>();
    }

    public static RoadmapGroupList GetRoadmapGroups(string filter) {
      return DataPortal.Fetch<RoadmapGroupList>(filter);
    }

    #endregion

    #region Data Access

    private void Child_Create() {
      // Do initialization here when creating the object.
    }

    private void DataPortal_Fetch() {
      DataPortal_Fetch(null);
    }

    private void DataPortal_Fetch(string pdNumber) {
      var rlce = RaiseListChangedEvents;
      RaiseListChangedEvents = false;
      IsReadOnly = false;

      using (var dalManager = DalFactory.GetManager()) {
        IRoadmapGroupDal dal = dalManager.GetProvider<IRoadmapGroupDal>();
        IList<RoadmapGroupDto> data = dal.Fetch();

        if (data != null) {
          foreach (var item in data) {
            Add(DataPortal.FetchChild<RoadmapGroupInfo>(item));
          }
        }
      }
    }

    #endregion
  }
}
