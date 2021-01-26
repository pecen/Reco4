using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Common.Commands {
  public interface ICommandBuilder {
    /// <summary>
    /// Creates a DelegateCommand.
    /// </summary>
    /// <typeparam name="TCommandType">The type of the command type.</typeparam>
    /// <returns>A DelegateCommand&lt;object&gt; instance.</returns>
    DelegateCommand<object> CreateCommand<TCommandType>() where TCommandType : class, ICommand<object>;

    /// <summary>
    /// Creates a DelegateCommand.
    /// </summary>
    /// <typeparam name="TCommandType">The type of the command type.</typeparam>
    /// <param name="args">The arguments passed to the commands constructor on create.</param>
    /// <returns>A DelegateCommand&lt;object&gt; instance.</returns>
    DelegateCommand<object> CreateCommand<TCommandType>(params dynamic[] args) where TCommandType : class, ICommand<object>;

    /// <summary>
    /// Creates a DelegateCommand.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <typeparam name="TCommandType">The type of the command type.</typeparam>
    /// <param name="context">The context.</param>
    /// <returns>A DelegateCommand&lt;object&gt; instance.</returns>
    DelegateCommand<object> CreateCommand<TContext, TCommandType>(TContext context) where TCommandType : class, IContextCommand<TContext, object>;

    /// <summary>
    /// Creates a CompositeCommand.
    /// </summary>
    /// <returns>A compositeCommand.</returns>
    CompositeCommand CreateCommand();

    /// <summary>
    /// Creates a DelegateCommand.
    /// </summary>
    /// <typeparam name="TCommandType">The type of the command type.</typeparam>
    /// <typeparam name="TParameter">The type of the parameter.</typeparam>
    /// <returns>A DelegateCommand&lt;TParameter&gt; instance.</returns>
    DelegateCommand<TParameter> CreateCommand<TCommandType, TParameter>() where TCommandType : class, ICommand<TParameter>;

    DelegateCommand<TParameter> CreateCommand<TCommandType, TParameter>(params dynamic[] args) where TCommandType : class, ICommand<TParameter>;

    /// <summary>
    /// Creates a DelegateCommand.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <typeparam name="TCommandType">The type of the command.</typeparam>
    /// <param name="context">The context.</param>
    /// <param name="args">The arguments passed to the commands constructor on create.</param>
    /// <returns>A DelegateCommand&lt;object&gt; instance.</returns>
    DelegateCommand<object> CreateCommand<TContext, TCommandType>(TContext context, params dynamic[] args) where TCommandType : class, IContextCommand<TContext, object>;

    /// <summary>
    /// Creates a DelegateCommand.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <typeparam name="TCommandType">The type of the command.</typeparam>
    /// <typeparam name="TParameter">The type of the parameter.</typeparam>
    /// <param name="context">The context.</param>
    /// <param name="args">The arguments passed to the commands constructor on create.</param>
    /// <returns>A DelegateCommand&lt;TParameter&gt; instance.</returns>
    DelegateCommand<TParameter> CreateCommand<TContext, TCommandType, TParameter>(TContext context, params dynamic[] args) where TCommandType : class, IContextCommand<TContext, TParameter>;
  }
}
