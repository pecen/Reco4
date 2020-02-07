using Csla;
using Reco4.Dal;
using System;

namespace Reco4.Library {
  [Serializable]
  public class ComponentExistsCmd : CommandBase<ComponentExistsCmd> {
    public static readonly PropertyInfo<string> PDNumberProperty = RegisterProperty<string>(c => c.PDNumber);
    public string PDNumber {
      get { return ReadProperty(PDNumberProperty); }
      set { LoadProperty(PDNumberProperty, value); }
    }

    public static readonly PropertyInfo<bool> ComponentExistsProperty = RegisterProperty<bool>(c => c.ComponentExists);
    public bool ComponentExists {
      get { return ReadProperty(ComponentExistsProperty); }
      private set { LoadProperty(ComponentExistsProperty, value); }
    }

    public ComponentExistsCmd() { }

    [RunLocal]
    [Create]
    protected void Create(string pdNumber) {
      PDNumber = pdNumber;
    }

    [Execute]
    protected override void DataPortal_Execute() {
      using (var ctx = DalFactory.GetManager()) {
        var dal = ctx.GetProvider<IComponentDal>();
        ComponentExists = dal.Exists(PDNumber);
      }
    }
  }
}
