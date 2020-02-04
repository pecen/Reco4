using Reco4.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Dal {
  public interface IRoadmapGroupDal {
    bool Exists(int id);
    List<RoadmapGroupDto> Fetch();
    RoadmapGroupDto Fetch(int id);
    List<RoadmapGroupDto> Fetch(string filter);
    void Insert(RoadmapGroupDto data);
    void Update(RoadmapGroupDto data);
    void Delete(int id);
  }
}
