using Improve.Infrastructure.Contants;
using Improve.Module.StudyRoom.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Improve.Module.StudyRoom
{
    public class StudyRoomModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //var regionManager = containerProvider.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion(Improve.Infrastructure.Contants.RegionNames.ContentRegion, typeof(Room));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Room>(ViewNames.StudyRoom);
        }
    }
}