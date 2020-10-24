using Improve.Module.NavigationBar.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Improve.Module.NavigationBar
{
    public class NavigationBarModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Improve.Infrastructure.Contants.RegionNames.NavigationBarRegion, typeof(NavigationBarDefault));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}