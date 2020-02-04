using Reco4.Dal.Dto;
using Reco4.DalEF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.DalEF {
  public class Reco4Context : DbContext {
    public Reco4Context() : base("Reco4Context") { }

    public Reco4Context(string connectionString) : base(connectionString) {
      // The below row is an ugly hack to make sure all the dll's for Ef is copied
      var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
    }

    public DbSet<RoadmapGroup> RoadmapGroups { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }
  }
}
