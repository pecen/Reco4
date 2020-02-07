using Csla.Data.EF6;
using Reco4.Dal;
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
        var result = (from r in ctx.DbContext.Component
                      where r.PDNumber == pdNumber
                      select r.ComponentId).Count() > 0;
        return result;
      }
    }
  }
}
