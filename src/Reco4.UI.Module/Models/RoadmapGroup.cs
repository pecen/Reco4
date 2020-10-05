using Prism.Mvvm;
using Reco4.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.UI.Module.Models {
  public class RoadmapGroup : BindableBase {
    private bool _isChecked;
    public bool IsChecked {
      get => _isChecked;
      set {
        //if (value == _isChecked) return;
        //_isChecked = value;
        //OnPropertyChanged(nameof(IsChecked));
        SetProperty(ref _isChecked, value);
      }
    }

    public RoadmapGroupInfo RoadmapGroupInfo { get; set; }
  }
}
