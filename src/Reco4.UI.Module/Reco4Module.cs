using Reco4.UI.Module.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Reco4.UI.Module.Enums;
using Reco4.Common.Services;
using Unity;
using Reco4.UI.Module.Services;

namespace Reco4.UI.Module {
  public class Reco4Module : IModule {
    public static IContainerProvider Container { get; private set; }

    public void OnInitialized(IContainerProvider containerProvider) {
      var regionManager = containerProvider.Resolve<IRegionManager>();
      //regionManager.RegisterViewWithRegion(WindowRegions.ComponentRegion.ToString(), typeof(ViewA));
      //regionManager.RegisterViewWithRegion(WindowRegions.ContentRegion.ToString(), typeof(ViewB));

      regionManager.RegisterViewWithRegion(WindowRegions.TabRegion.ToString(), typeof(RoadmapGroups));

      Container = containerProvider;
    }

    public void RegisterTypes(IContainerRegistry containerRegistry) {
      //containerRegistry.RegisterForNavigation(typeof(ViewA), nameof(ViewA));
      //containerRegistry.RegisterForNavigation(typeof(ViewB), nameof(ViewB));

      containerRegistry.RegisterSingleton<IGridViewService, GridViewService>();
      containerRegistry.RegisterSingleton<IUnityService, UnityService>();

      containerRegistry.RegisterSingleton<IFilteredListService, FilteredListService>();
    }
  }
}