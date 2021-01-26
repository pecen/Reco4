using Prism.Commands;
using Reco4.Common.Commands.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Common.Commands {
  public class CommandBuilder : ICommandBuilder {
    public CompositeCommand CreateCommand() {
      CompositeCommand compcmd = new CompositeCommand();
      return compcmd;
    }

    public DelegateCommand<object> CreateCommand<TCommandType>(params dynamic[] args) where TCommandType : class, ICommand<object> {
      return new DelegateCommandProvider<object>().CreateCommand<TCommandType>(args);
    }

    public DelegateCommand<TParameter> CreateCommand<TCommandType, TParameter>() where TCommandType : class, ICommand<TParameter> {
      return new DelegateCommandProvider<TParameter>().CreateCommand<TCommandType>();
    }

    public DelegateCommand<TParameter> CreateCommand<TCommandType, TParameter>(params dynamic[] args) where TCommandType : class, ICommand<TParameter> {
      return new DelegateCommandProvider<TParameter>().CreateCommand<TCommandType>(args);
    }

    public DelegateCommand<object> CreateCommand<TContext, TCommandType>(TContext context, params object[] args) where TCommandType : class, IContextCommand<TContext, object> {
      return new DelegateCommandProvider<TContext, object>().CreateCommand<TCommandType>(context, args);
    }

    public DelegateCommand<TParameter> CreateCommand<TContext, TCommandType, TParameter>(TContext context, params object[] args) where TCommandType : class, IContextCommand<TContext, TParameter> {
      return new DelegateCommandProvider<TContext, TParameter>().CreateCommand<TCommandType>(context, args);
    }

    public DelegateCommand<object> CreateCommand<TCommandType>() where TCommandType : class, ICommand<object> {
      return new DelegateCommandProvider<object>().CreateCommand<TCommandType>();
    }

    public DelegateCommand<object> CreateCommand<TContext, TCommandType>(TContext context) where TCommandType : class, IContextCommand<TContext, object> {
      return new DelegateCommandProvider<TContext, object>().CreateCommand<TCommandType>(context);
    }
  }
}
