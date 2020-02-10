using Csla.Data.EF6;
using Reco4.Dal;
using Reco4.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.DalEF {
  public class ComponentDal : IComponentDal {
    private readonly string _dbName = "Reco4Db";

    public bool Exists(string pdNumber) {
      using (var ctx = DbContextManager<Reco4Context>.GetManager(_dbName)) {
        var result = (from r in ctx.DbContext.Components
                      where r.PDNumber == pdNumber
                      select r.ComponentId).Count() > 0;
        return result;
      }
    }

    public IList<ComponentDto> Fetch() {
      using (var ctx = DbContextManager<Reco4Context>.GetManager(_dbName)) {
        var result = from r in ctx.DbContext.Components
                     select new ComponentDto {
                       ComponentId = r.ComponentId,
                       PDNumber = r.PDNumber,
                       DownloadedTimestamp = r.DownloadedTimestamp,
                       Description = r.Description,
                       PDStatus = r.PD_Status,
                       ComponentType = r.Component_Type,
                       Xml = r.XML,
                       PDSource = r.PD_Source,
                       SourceComponentId = r.SourceComponentId
                     };
        return result.ToList();
      }
    }
  }
}
