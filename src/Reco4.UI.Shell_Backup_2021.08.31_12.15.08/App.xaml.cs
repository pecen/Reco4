using Reco4.UI.Shell.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Reco4.UI.Module;

namespace Reco4.UI.Shell {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App {
    protected override Window CreateShell() {
      return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry) {

    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) {
      base.ConfigureModuleCatalog(moduleCatalog);

      moduleCatalog.AddModule(typeof(Reco4Module));
    }
  }
}
