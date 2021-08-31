using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Common.Commands {
  public interface ICommand<TParameter> {
    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">The parameter for the command.</param>
    bool CanExecute(TParameter parameter);
    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">The parameter for the command.</param>
    void Execute(TParameter parameter);
  }
}
