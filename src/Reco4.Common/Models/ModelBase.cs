using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Common.Models {
  public class ModelBase : BindableBase {
    private bool _isChecked;
    public bool IsChecked {
      get => _isChecked;
      set {
        SetProperty(ref _isChecked, value);
      }
    }
  }
}
