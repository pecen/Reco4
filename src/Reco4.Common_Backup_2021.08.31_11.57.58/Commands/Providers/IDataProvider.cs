using System.Collections.Generic;

namespace Reco4.Common.Commands.Providers {
  public interface IDataProvider {
    /// <summary>
    /// Gets the data in a collection 
    /// </summary>
    IEnumerable<string> Data { get; }
  }
}
