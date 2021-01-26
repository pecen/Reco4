using Prism.Commands;
using Reco4.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Common.Commands.Providers {
  public class DelegateCommandProvider<TParameter> : ICommandProvider<DelegateCommand<TParameter>, TParameter>, IDelegateCommandProvider<TParameter> {
    public DelegateCommandProvider()
        : base() {
    }

    public DelegateCommand<TParameter> CreateCommand<TCommandType>()
        where TCommandType : class, ICommand<TParameter> {
      TCommandType cmd = Activator.CreateInstance(typeof(TCommandType)) as TCommandType;
      return new DelegateCommand<TParameter>(p => cmd.Execute(p), p => cmd.CanExecute(p));
    }

    public DelegateCommand<TParameter> CreateCommand<TCommandType>(params object[] args) where TCommandType : class, ICommand<TParameter> {
      TCommandType cmd = Activator.CreateInstance(typeof(TCommandType), args) as TCommandType;

      DelegateCommand<TParameter> delegateCommand = new DelegateCommand<TParameter>(p => cmd.Execute(p), p => cmd.CanExecute(p));
      INotifyParametersChangedCommand notifyParametersChangedCommand = cmd as INotifyParametersChangedCommand;

      if (notifyParametersChangedCommand.IsNotNull()) {
        notifyParametersChangedCommand.ParametersChanged += (sender, eventArgs) => delegateCommand.RaiseCanExecuteChanged();
      }

      return delegateCommand;
    }
  }

  public class DelegateCommandProvider<TContext, TParameter> : ICommandProvider<TContext, DelegateCommand<TParameter>, TParameter> {
    public DelegateCommandProvider()
        : base() {
    }

    public DelegateCommand<TParameter> CreateCommand<TCommandType>(TContext context, params dynamic[] args)
        where TCommandType : class, IContextCommand<TContext, TParameter> {
      TCommandType cmd = Activator.CreateInstance(typeof(TCommandType), args) as TCommandType;
      cmd.SetContext(context);
      return new DelegateCommand<TParameter>(p => cmd.Execute(p), p => cmd.CanExecute(p));
    }
  }
}
