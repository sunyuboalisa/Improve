using Improve.Infrastructure.Contants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Improve.Controls
{
    class NavigationBarDefaultViewModel: BindableBase
    {
        private readonly IRegionManager _regionManager;

        public NavigationBarDefaultViewModel(IRegionManager manager)
        {
            _regionManager = manager;
        }

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string viewName)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, viewName);
        }
    }
}
