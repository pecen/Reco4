using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Common.Commands {
	/// <summary>
	/// Base class for the commands.
	/// </summary>
	/// <typeparam name="TParameter">The type of the parameter.</typeparam>
	public abstract class CommandBase<TParameter> : ICommand<TParameter> {
		/// <summary>
		/// Determines whether command can be executed according to the passed parameter.
		/// </summary>
		/// <param name="parameter">The parameter for the command.</param>
		/// <returns>True if command can be executed; otherwise false.</returns>
		public virtual bool CanExecute(TParameter parameter) {
			return false;
		}

		/// <summary>
		/// Executes the command.
		/// </summary>
		/// <param name="parameter">The parameter for the command.</param>
		public abstract void Execute(TParameter parameter);
	}

	/// <summary>
	/// Base class for the commands working in the given context.
	/// </summary>
	/// <typeparam name="TContext">The type of the context.</typeparam>
	/// <typeparam name="TParameter">The type of the parameter.</typeparam>
	public abstract class CommandBase<TContext, TParameter> : CommandBase<TParameter>, IContextCommand<TContext, TParameter> {
		/// <summary>
		/// Gets the context.
		/// </summary>
		public TContext Context { get; protected set; }

		/// <summary>
		/// Sets the context.
		/// </summary>
		/// <param name="context">The context for the command.</param>
		/// <returns>The called instance.</returns>
		public IContextCommand<TContext, TParameter> SetContext(TContext context) {
			Context = context;
			return this;
		}
	}
}
