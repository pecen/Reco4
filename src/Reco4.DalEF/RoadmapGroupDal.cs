using Csla.Data.EF6;
using Reco4.Dal;
using Reco4.Dal.Dto;
using Reco4.Dal.Enum;
using Reco4.Dal.Exceptions;
using Reco4.Dal.Extensions;
using Reco4.DalEF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.DalEF {
  public class RoadmapGroupDal : IRoadmapGroupDal {
    private readonly string _dbName = "Reco4Db";

    public void Delete(int id) {
      throw new NotImplementedException();
    }

    public List<RoadmapGroupDto> Fetch() {
      using (var ctx = DbContextManager<Reco4Context>.GetManager(_dbName)) {
        var result = from r in ctx.DbContext.RoadmapGroups
                     select new RoadmapGroupDto {
                       RoadmapGroupId = r.RoadmapGroupId,
                       OwnerSss = r.OwnerSss,
                       RoadmapName = r.RoadmapName,
                       Protected = r.Protected,
                       CreationTime = r.CreationTime,
                       StartYear = r.StartYear,
                       EndYear = r.EndYear,
                       Xml = r.Xml,
                       ValidationStatusValue = r.Validation_Status,
                       ConvertToVehicleInputStatusValue = r.ConvertToVehicleInput_Status
                     };
        return result.ToList();
      }
    }

    public RoadmapGroupDto Fetch(int id) {
      using (var ctx = DbContextManager<Reco4Context>.GetManager(_dbName)) {
        var result = (from r in ctx.DbContext.RoadmapGroups
                      where r.RoadmapGroupId == id
                      select new RoadmapGroupDto {
                        RoadmapGroupId = r.RoadmapGroupId,
                        OwnerSss = r.OwnerSss,
                        RoadmapName = r.RoadmapName,
                        Protected = r.Protected,
                        CreationTime = r.CreationTime,
                        StartYear = r.StartYear,
                        EndYear = r.EndYear,
                        Xml = r.Xml,
                        ValidationStatusValue = r.Validation_Status,
                        ConvertToVehicleInputStatusValue = r.ConvertToVehicleInput_Status
                      }).FirstOrDefault();
        return result;
      }
    }

    public List<RoadmapGroupDto> Fetch(string filter) {
      using (var ctx = DbContextManager<Reco4Context>.GetManager(_dbName)) {
        var result = from r in ctx.DbContext.RoadmapGroups
                     where r.RoadmapName.Contains(filter)
                     select new RoadmapGroupDto {
                       RoadmapGroupId = r.RoadmapGroupId,
                       OwnerSss = r.OwnerSss,
                       RoadmapName = r.RoadmapName,
                       Protected = r.Protected,
                       CreationTime = r.CreationTime,
                       StartYear = r.StartYear,
                       EndYear = r.EndYear,
                       Xml = r.Xml,
                       ValidationStatusValue = r.Validation_Status,
                       ConvertToVehicleInputStatusValue = r.ConvertToVehicleInput_Status
                     };
        return result.ToList();
      }
    }

    public void Insert(RoadmapGroupDto data) {
      using (var ctx = DbContextManager<Reco4Context>.GetManager(_dbName)) {
        var item = new RoadmapGroup {
          OwnerSss = data.OwnerSss,
          RoadmapName = data.RoadmapName,
          Protected = data.Protected,
          CreationTime = data.CreationTime,
          StartYear = data.StartYear,
          EndYear = data.EndYear,
          Xml = data.Xml,
          Validation_Status = data.ValidationStatusValue,
          ConvertToVehicleInput_Status = data.ConvertToVehicleInputStatusValue
        };

        ctx.DbContext.RoadmapGroups.Add(item);
        ctx.DbContext.SaveChanges();
        data.RoadmapGroupId = item.RoadmapGroupId;
      }
    }

    public void Update(RoadmapGroupDto data) {
      using (var ctx = DbContextManager<Reco4Context>.GetManager(_dbName)) {
        var item = (from r in ctx.DbContext.RoadmapGroups
                    where r.RoadmapGroupId == data.RoadmapGroupId
                    select r).FirstOrDefault();

        if (data == null) {
          throw new DataNotFoundException("RoadmapGroup not found error.");
        }
        if (!data.CreationTime.Matches(item.CreationTime)) {
          throw new ConcurrencyException("ConcurrencyException: CreationTime mismatch.");
        }

        item.RoadmapGroupId = data.RoadmapGroupId;
        item.OwnerSss = data.OwnerSss;
        item.RoadmapName = data.RoadmapName;
        item.Protected = data.Protected;
        item.CreationTime = data.CreationTime;
        item.StartYear = data.StartYear;
        item.EndYear = data.EndYear;
        item.Xml = data.Xml;
        item.Validation_Status = data.ValidationStatusValue;
        item.ConvertToVehicleInput_Status = data.ConvertToVehicleInputStatusValue;

        var count = ctx.DbContext.SaveChanges();

        if(count == 0) {
          throw new UpdateFailureException("Failed to save RoadmapGroup.");
        }
      }
    }
  }
}
