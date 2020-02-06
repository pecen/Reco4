using Reco4.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Dal {
  public interface IRoadmapDal {
    List<RoadmapDto> Fetch();
    RoadmapDto Fetch(int id);
  }
}
