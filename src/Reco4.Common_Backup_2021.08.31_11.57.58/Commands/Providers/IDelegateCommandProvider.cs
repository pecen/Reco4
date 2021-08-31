using Prism.Commands;

namespace Reco4.Common.Commands.Providers {
  public interface IDelegateCommandProvider<TParameter> {
    /// <summary>
    /// Creates the command.
    /// </summary>
    /// <typeparam name="TCommandType">The type of the command type.</typeparam>
    /// <returns>A DelegateCommand instance.</returns>
    DelegateCommand<TParameter> CreateCommand<TCommandType>() where TCommandType : class, ICommand<TParameter>;
  }
}
