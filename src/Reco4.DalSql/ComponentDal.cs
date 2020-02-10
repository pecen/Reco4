using Csla.Data;
using Reco4.Dal;
using Reco4.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.DalSql {
  public class ComponentDal : IComponentDal {
    private readonly string _dbName = "Reco4Db";

    public bool Exists(string pdNumber) {
      throw new NotImplementedException();
    }

    public IList<ComponentDto> Fetch() {
      using (var ctx = ConnectionManager<SqlConnection>.GetManager(_dbName)) {
        using (var cm = ctx.Connection.CreateCommand()) {
          cm.CommandType = CommandType.Text;
          cm.CommandText = "SELECT * FROM Reco3Component";

          using (var dr = new SafeDataReader(cm.ExecuteReader())) {
            //if (dr.HasRows) {
              var result = new List<ComponentDto>();

              while (dr.Read()) {
                var component = new ComponentDto {
                  ComponentId = dr.GetInt32(0),
                  PDNumber = dr.GetString(1),
                  DownloadedTimestamp = dr.GetDateTime(2),
                  Description = dr.GetString(3),
                  PDStatus = dr.GetInt32(4),
                  ComponentType = dr.GetInt32(5),
                  Xml = dr.GetString(6),
                  PDSource = dr.GetInt32(7),
                  SourceComponentId = dr.GetInt32(8)
                };

                result.Add(component);
              };

              return result;
            //}
            //else {
            //  return null;
            //}
          }
        }
      }
    }
  }
}
