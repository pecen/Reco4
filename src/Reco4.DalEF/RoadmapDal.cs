using Csla.Data.EF6;
using Reco4.Dal;
using Reco4.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.DalEF {
  public class RoadmapDal : IRoadmapDal {
    private readonly string _dbName = "Reco4Db";

    public List<RoadmapDto> Fetch() {
      using (var ctx = DbContextManager<Reco4Context>.GetManager(_dbName)) {
        var result = from r in ctx.DbContext.Roadmaps
                     select new RoadmapDto {
                       RoadmapId = r.RoadmapId,
                       RoadmapName = r.RoadmapName,
                       Protected = r.Protected,
                       ValidationStatus = r.Validation_Status,
                       ConvertToVehicleInputStatus = r.ConvertToVehicleInput_Status,
                       RoadmapGroupId = r.RoadmapGroupId,
                       CurrentYear = r.CurrentYear,
                       ImprovedVehicleCount = r.ImprovedVehicleCount
                     };
        return result.ToList();
      }
    }

    public RoadmapDto Fetch(int id) {
      using (var ctx = DbContextManager<Reco4Context>.GetManager(_dbName)) {
        var result = (from r in ctx.DbContext.Roadmaps
                      where r.RoadmapGroupId == id
                      select new RoadmapDto {
                        RoadmapId = r.RoadmapId,
                        RoadmapName = r.RoadmapName,
                        Protected = r.Protected,
                        ValidationStatus = r.Validation_Status,
                        ConvertToVehicleInputStatus = r.ConvertToVehicleInput_Status,
                        RoadmapGroupId = r.RoadmapGroupId,
                        CurrentYear = r.CurrentYear,
                        ImprovedVehicleCount = r.ImprovedVehicleCount
                      }).FirstOrDefault();
        return result;
      }
    }
  }
}
