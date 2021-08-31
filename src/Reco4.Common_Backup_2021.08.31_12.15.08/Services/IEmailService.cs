using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Common.Services {
  /// <summary>
  /// Interface for e-mail client service
  /// </summary>
  public interface IEmailService {
    /// <summary>
    /// Opens the mail.
    /// </summary>
    /// <param name="body">The body.</param>
    /// <param name="subject">The subject.</param>
    void OpenMail(string body, string subject = null);
  }
}
