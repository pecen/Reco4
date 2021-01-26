using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reco4.UI.Module.ViewModels {
  public class ViewBViewModel : ViewModelBase {
    private string _message;
    public string Message {
      get { return _message; }
      set { SetProperty(ref _message, value); }
    }

    public ViewBViewModel()
    {
      Message = "This is ViewB";
    }
  }
}
