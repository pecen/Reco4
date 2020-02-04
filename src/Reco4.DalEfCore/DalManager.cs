using Csla.Data.EF6;
using Reco4.Dal;
using Reco4.DalEF;
using System;

namespace Reco4.DalEfCore {
  public class DalManager : IDalManager {
    private static string _typeMask = typeof(DalManager).FullName.Replace("DalManager", @"{0}");

    public T GetProvider<T>() where T : class {
      var typeName = string.Format(_typeMask, typeof(T).Name.Substring(1));
      var type = Type.GetType(typeName);
      if (type != null) {
        return Activator.CreateInstance(type) as T;
      }
      else {
        throw new NotImplementedException(typeName);
      }
    }

    //public DbContextManager<Reco4Context> ConnectionManager { get; private set; }

    public DalManager() {
      //ConnectionManager = DbContextManager<Reco4Context>.GetManager(); // "Reco4Db");
    }

    public void Dispose() {
      //ConnectionManager.Dispose();
      //ConnectionManager = null;
    }
  }
}
