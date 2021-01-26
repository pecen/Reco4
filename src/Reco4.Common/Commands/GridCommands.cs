using Prism.Commands;
using Prism.Ioc;
using Reco4.Common.Extensions;
using Reco4.Common.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Unity;

namespace Reco4.Common.Commands {
  public static class GridCommands {
    private static ICommandBuilder _commandBuilder;

    /// <summary>
    /// The copy grid rows command.
    /// </summary>
    private static ICommand _copySelectedRowsCommand;

    /// <summary>
    /// The copy selected grid rows command.
    /// </summary>
    private static ICommand _copySelectedGridRowsCommand;

    /// <summary>
    /// The open link command
    /// </summary>
    private static ICommand _openLinkCommand;

    /// <summary>
    /// Gets the copy selected grid rows command.
    /// </summary>
    public static ICommand CopySelectedGridRowsCommand {
      get {
        if (_copySelectedGridRowsCommand.IsNull()) {
          _copySelectedGridRowsCommand = CommandBuilder.CreateCommand<CopySelectedGridRowsCommand, RadGridView>();
        }

        return _copySelectedGridRowsCommand;
      }
    }

    /// <summary>
    /// Gets the copy grid rows command.
    /// </summary>
    public static ICommand CopySelectedRowsCommand {
      get {
        if (_copySelectedRowsCommand.IsNull()) {
          _copySelectedRowsCommand = CommandBuilder.CreateCommand<CopySelectedRowsCommand, IList<IEnumerable<string>>>();
        }

        return _copySelectedRowsCommand;
      }
    }

    /// <summary>
    /// Gets the open link command.
    /// </summary>
    public static ICommand OpenLinkCommand {
      get {
        if (_openLinkCommand.IsNull()) {
          _openLinkCommand = CommandBuilder.CreateCommand<OpenLinkCommand, string>();
        }

        return _openLinkCommand;
      }
    }

    /// <summary>
    /// Gets the command builder.
    /// </summary>
    private static ICommandBuilder CommandBuilder {
      get {
        if (_commandBuilder.IsNull()) {
          //IUnityContainer unityContainer = UnityService.Get().Container;
          try {
            _commandBuilder = UnityService.Get().Resolve<ICommandBuilder>();
          }
          catch (Exception) {
            _commandBuilder = new DummyCommandBuilder();
          }
        }

        return _commandBuilder;
      }
    }

    private class DummyCommandBuilder : ICommandBuilder {

      public DelegateCommand<object> CreateCommand<TCommandType>() where TCommandType : class, ICommand<object> {
        return null;
      }

      public DelegateCommand<object> CreateCommand<TCommandType>(params dynamic[] args) where TCommandType : class, ICommand<object> {
        return null;
      }

      public DelegateCommand<object> CreateCommand<TContext, TCommandType>(TContext context) where TCommandType : class, IContextCommand<TContext, object> {
        return null;
      }

      public CompositeCommand CreateCommand() {
        return null;
      }

      public DelegateCommand<TParameter> CreateCommand<TCommandType, TParameter>() where TCommandType : class, ICommand<TParameter> {
        return null;
      }

      public DelegateCommand<TParameter> CreateCommand<TCommandType, TParameter>(params dynamic[] args) where TCommandType : class, ICommand<TParameter> {
        return null;
      }

      public DelegateCommand<object> CreateCommand<TContext, TCommandType>(TContext context, params dynamic[] args) where TCommandType : class, IContextCommand<TContext, object> {
        return null;
      }

      public DelegateCommand<TParameter> CreateCommand<TContext, TCommandType, TParameter>(TContext context, params dynamic[] args) where TCommandType : class, IContextCommand<TContext, TParameter> {
        return null;
      }
    }
  }
}
