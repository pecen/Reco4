using Prism.Ioc;
using Prism.Unity;
using Reco4.Common.Extensions;
using Reco4.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Unity;

namespace Reco4.Common.Commands {
  public class CopySelectedGridRowsCommand : CommandBase<RadGridView> {
    /// <summary>
    /// Reference to the GridViewService
    /// </summary>
    private static IGridViewService _gridViewService;

    private static IGridViewService GridViewService {
      get {
        if (_gridViewService.IsNull()) {
          //IUnityContainer unityContainer = UnityService.Get().Container;
          //_gridViewService = unityContainer.Resolve<IGridViewService>();
          _gridViewService = UnityService.Get().Resolve<IGridViewService>();
        }

        return _gridViewService;
      }
    }

    /// <summary>
    /// Determines whether command can be executed according to the passed parameter.
    /// </summary>
    /// <param name="parameter">The parameter for the command.</param>
    /// <returns>
    /// True if command can be executed; otherwise false.
    /// </returns>
    public override bool CanExecute(RadGridView parameter) {
      return parameter.IsNotNull();
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="parameter">The parameter for the command.</param>
    public override void Execute(RadGridView parameter) {
      GridCommands.CopySelectedRowsCommand.Execute(GridViewService.GetSelectedRowsData(parameter));
    }
  }
}
