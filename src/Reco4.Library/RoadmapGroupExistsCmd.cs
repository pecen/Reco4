using Csla;
using Reco4.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Library {
  [Serializable]
  public class RoadmapGroupExistsCmd : CommandBase<RoadmapGroupExistsCmd> {
    public static readonly PropertyInfo<int> RoadmapGroupIdProperty = RegisterProperty<int>(c => c.RoadmapGroupId);
    private int RoadmapGroupId {
      get { return ReadProperty(RoadmapGroupIdProperty); }
      set { LoadProperty(RoadmapGroupIdProperty, value); }
    }

    public static readonly PropertyInfo<bool> RoadmapGroupExistsProperty = RegisterProperty<bool>(c => c.RoadmapGroupExists);
    public bool RoadmapGroupExists {
      get { return ReadProperty(RoadmapGroupExistsProperty); }
      private set { LoadProperty(RoadmapGroupExistsProperty, value); }
    }

    public RoadmapGroupExistsCmd() { }

    //public RoadmapGroupExistsCmd(int id) {
    //  RoadmapGroupId = id;
    //}

    [Create, RunLocal]
    protected void Create(int id) {
      RoadmapGroupId = id;
    }

    protected override void DataPortal_Execute() {
      using (var ctx = DalFactory.GetManager()) {
        var dal = ctx.GetProvider<IRoadmapGroupDal>();
        RoadmapGroupExists = dal.Exists(RoadmapGroupId);
      }
    }
  }
}
