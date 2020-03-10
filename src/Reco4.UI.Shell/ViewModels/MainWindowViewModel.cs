using Prism.Commands;
using Prism.Regions;
using Reco4.UI.Module.Enums;
using Reco4.UI.Module.ViewModels;
using Unity;

namespace Reco4.UI.Shell.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
    private readonly IRegionManager _regionManager;
    private readonly IUnityContainer _container;

    public DelegateCommand<string> NavigateCommand { get; set; }
    public string ContentRegion { get; } = WindowRegions.ContentRegion.ToString();
    public string ComponentRegion { get; } = WindowRegions.ComponentRegion.ToString();
    public string SettingsRegion { get; } = WindowRegions.SettingsRegion.ToString();

    public MainWindowViewModel(IRegionManager regionManager, IUnityContainer container) {
      Title = "Reco 4";

      _regionManager = regionManager;
      _container = container;

      NavigateCommand = new DelegateCommand<string>(Navigate);
    }

    private void Navigate(string uri) {
      // using Navigation mechanism (not view discovery or view injection)
      //_regionManager.RequestNavigate(ContentRegion, uri);
      //_regionManager.RequestNavigate(ComponentRegion, uri);
    }
  }
}
