using Prism.Commands;
using Prism.Ioc;
using Reco4.Common.Extensions;
using Reco4.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Reco4.Common.Commands {
  /// <summary>
  /// Open email command.
  /// </summary>
  public class OpenEmailCommand : DelegateCommand<string> {
    /// <summary>
    /// The mail service
    /// </summary>
    private static IEmailService _mailService;

    /// <summary>
    /// Initializes a new instance of the OpenMailCommand class.
    /// </summary>
    public OpenEmailCommand()
        : base(Execute, CanExecute) {
    }

    /// <summary>
    /// Gets the mail service.
    /// </summary>
    private static IEmailService MailService {
      get {
        if (_mailService.IsNull()) {
          //IUnityContainer unityContainer = UnityService.Get().Container;
          //_mailService = unityContainer.Resolve<IEmailService>();
          _mailService = UnityService.Get().Resolve<IEmailService>();
        }

        return _mailService;
      }
    }

    /// <summary>
    /// Determines whether the command can be executed.
    /// </summary>
    /// <param name="body">The body.</param>
    /// <returns>True if parameter is not empty; Otherwise false.</returns>
    public new static bool CanExecute(string body) {
      return !string.IsNullOrWhiteSpace(body);
    }

    /// <summary>
    /// Executes the specified command.
    /// </summary>
    /// <param name="body">The mail body.</param>
    public new static void Execute(string body) {
      MailService.OpenMail(body);
    }
  }
}
