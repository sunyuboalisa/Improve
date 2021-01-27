using Improve.Infrastructure.Contants;
using Improve.Module.Plan.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Improve.Module.Plan
{
    public class PlanModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //var regionManager = containerProvider.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion(Improve.Infrastructure.Contants.RegionNames.ContentRegion, typeof(HomeView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomeView>(ViewNames.Plan);
        }
    }
}