using Csla.Data;
using Reco4.Dal;
using Reco4.Dal.Dto;
using Reco4.Dal.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.DalSql {
  public class RoadmapGroupDal : IRoadmapGroupDal {
    private readonly string _dbName = "Reco4Db";

    public void Delete(int id) {
      throw new NotImplementedException();
    }

    public bool Exists(int id) {
      throw new NotImplementedException();
    }

    public List<RoadmapGroupDto> Fetch() {
      using (var ctx = ConnectionManager<SqlConnection>.GetManager(_dbName)) {
        using (var cm = ctx.Connection.CreateCommand()) {
          cm.CommandType = CommandType.Text;
          cm.CommandText = "SELECT * FROM RoadmapGroups";
          //cm.CommandType = CommandType.StoredProcedure;
          //cm.CommandText = "GetRoadmapGroups";

          using (var dr = cm.ExecuteReader()) {
            if (dr.HasRows) {
              var result = new List<RoadmapGroupDto>();

              while (dr.Read()) {
                var roadmapGroup = new RoadmapGroupDto {
                  RoadmapGroupId = dr.GetInt32(0),
                  OwnerSss = dr.GetString(1),
                  RoadmapName = dr.GetString(2),
                  Protected = dr.GetBoolean(3),
                  CreationTime = dr.GetDateTime(4),
                  StartYear = dr.GetInt32(5),
                  EndYear = dr.GetInt32(6),
                  Xml = dr.GetString(7),
                  ValidationStatusValue = (ValidationStatus)dr.GetInt32(8),
                  ConvertToVehicleInputStatusValue = (ConvertToVehicleInputStatus)dr.GetInt32(9)
                };

                result.Add(roadmapGroup);
              };

              return result;
            }
            else {
              return null;
            }
          }
        }
      }
    }

    public RoadmapGroupDto Fetch(int id) {
      using (var ctx = ConnectionManager<SqlConnection>.GetManager(_dbName)) {
        using (var cm = ctx.Connection.CreateCommand()) {
          cm.CommandType = CommandType.Text;
          cm.CommandText = "SELECT * FROM RoadmapGroups WHERE RoadmapGroupId = @id";
          cm.Parameters.AddWithValue("@id", id);

          using (var dr = cm.ExecuteReader()) {
            if (dr.HasRows) {
              dr.Read();

              var result = new RoadmapGroupDto {
                RoadmapGroupId = dr.GetInt32(0),
                OwnerSss = dr.GetString(1),
                RoadmapName = dr.GetString(2),
                Protected = dr.GetBoolean(3),
                CreationTime = dr.GetDateTime(4),
                StartYear = dr.GetInt32(5),
                EndYear = dr.GetInt32(6),
                Xml = dr.GetString(7),
                ValidationStatusValue = (ValidationStatus)dr.GetInt32(8),
                ConvertToVehicleInputStatusValue = (ConvertToVehicleInputStatus)dr.GetInt32(9)
              };

              return result;
            }
            else {
              return null;
            }
          }
        }
      }
    }

    public List<RoadmapGroupDto> Fetch(string filter) {
      using (var ctx = ConnectionManager<SqlConnection>.GetManager(_dbName)) {
        using (var cm = ctx.Connection.CreateCommand()) {
          cm.CommandType = CommandType.Text;
          cm.CommandText = "SELECT * FROM RoadmapGroups WHERE RoadmapName LIKE '%' + @filter + '%'";
          cm.Parameters.AddWithValue("@filter", filter);

          using (var dr = cm.ExecuteReader()) {
            if (dr.HasRows) {
              var result = new List<RoadmapGroupDto>();

              while (dr.Read()) {
                var roadmapGroup = new RoadmapGroupDto {
                  RoadmapGroupId = dr.GetInt32(0),
                  OwnerSss = dr.GetString(1),
                  RoadmapName = dr.GetString(2),
                  Protected = dr.GetBoolean(3),
                  CreationTime = dr.GetDateTime(4),
                  StartYear = dr.GetInt32(5),
                  EndYear = dr.GetInt32(6),
                  Xml = dr.GetString(7),
                  ValidationStatusValue = (ValidationStatus)dr.GetInt32(8),
                  ConvertToVehicleInputStatusValue = (ConvertToVehicleInputStatus)dr.GetInt32(9)
                };

                result.Add(roadmapGroup);
              };

              return result;
            }
            else {
              return null;
            }
          }
        }
      }
    }

    public void Insert(RoadmapGroupDto data) {
      using (var ctx = ConnectionManager<SqlConnection>.GetManager(_dbName)) {
        using (var cm = ctx.Connection.CreateCommand()) {
          cm.CommandType = CommandType.Text;

          cm.CommandText = "INSERT INTO RoadmapGroups (OwnerSss, RoadmapName, Protected, CreationTime, StartYear, " +
            "EndYear, XML, Validation_Status, ConvertToVehicleInput_Status) " +
            "VALUES (@ownerSss, @roadmapName, @protected, @creationTime, @startYear, @endYear, @xml, @validationStatus, @convertToVehicleInputStatus)";

          cm.Parameters.AddWithValue("@ownerSss", data.OwnerSss);
          cm.Parameters.AddWithValue("@roadmapName", data.RoadmapName);
          cm.Parameters.AddWithValue("@protected", data.Protected);
          cm.Parameters.AddWithValue("@creationTime", data.CreationTime);
          cm.Parameters.AddWithValue("@startYear", data.StartYear);
          cm.Parameters.AddWithValue("@endYear", data.EndYear);
          cm.Parameters.AddWithValue("@xml", data.Xml);
          cm.Parameters.AddWithValue("@validationStatus", data.ValidationStatusValue);
          cm.Parameters.AddWithValue("@convertToVehicleInputStatus", data.ConvertToVehicleInputStatusValue);

          cm.ExecuteNonQuery();
          cm.Parameters.Clear();
          cm.CommandText = "SELECT @@identity";
          var r = cm.ExecuteScalar();
          var newId = int.Parse(r.ToString());
          data.RoadmapGroupId = newId;
        }
      }
    }

    public void Update(RoadmapGroupDto data) {
      throw new NotImplementedException();
    }
  }
}
