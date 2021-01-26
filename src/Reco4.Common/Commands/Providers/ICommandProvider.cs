namespace Reco4.Common.Commands.Providers {
  public interface ICommandProvider<TCommand, TParameter> {
    /// <summary>
    /// Creates a command of the type ICommand&lt;TParameter&gt;.
    /// </summary>
    /// <typeparam name="TCommandType">The type of the command type.</typeparam>
    /// <returns>A TCommand.</returns>
    TCommand CreateCommand<TCommandType>() where TCommandType : class, ICommand<TParameter>;

    /// <summary>
    /// Creates a command of the type ICommand&lt;TParameter&gt;.
    /// </summary>
    /// <typeparam name="TCommandType">The type of the command type.</typeparam>
    /// <param name="args">The arguments passed to the commands constructor on create.</param>
    /// <returns>A TCommand.</returns>
    TCommand CreateCommand<TCommandType>(params object[] args) where TCommandType : class, ICommand<TParameter>;
  }

  public interface ICommandProvider<TContext, TCommand, TParameter> {
    /// <summary>
    /// Creates a command of the type ICommand&lt;TParameter&gt;> with a context.
    /// </summary>
    /// <param name="context">The context the command operates on.</param>
    /// <param name="args">The arguments passed to the commands constructor on create.</param>
    /// <returns>A TCommand.</returns>
    TCommand CreateCommand<TCommandType>(TContext context, params object[] args) where TCommandType : class, IContextCommand<TContext, TParameter>;
  }
}
