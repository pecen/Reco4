using Csla;
using Reco4.Dal;
using Reco4.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Library {
  [Serializable]
  public class ComponentList : ReadOnlyListBase<ComponentList, ComponentInfo> {
    #region Factory Methods

    public static ComponentList GetComponents() {
      return DataPortal.Fetch<ComponentList>();
    }

    #endregion

    #region Data Access

    [CreateChild]
    private void Child_Create() {
      // Do initialization here when creating the object.
    }

    [Fetch]
    private void DataPortal_Fetch() {
      var rlce = RaiseListChangedEvents;
      RaiseListChangedEvents = false;
      IsReadOnly = false;

      using (var dalManager = DalFactory.GetManager()) {
        IComponentDal dal = dalManager.GetProvider<IComponentDal>();
        IList<ComponentDto> data = dal.Fetch();

        if (data != null) {
          foreach (var item in data) {
            Add(DataPortal.FetchChild<ComponentInfo>(item));
          }
        }
      }
    }

    #endregion
  }
}
