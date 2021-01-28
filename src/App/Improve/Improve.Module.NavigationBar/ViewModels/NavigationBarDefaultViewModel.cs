using Improve.CompositeCommands.Core.Interfaces;
using Improve.Infrastructure.Contants;
using Improve.Infrastructure.Events;
using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace Improve.Module.NavigationBar.ViewModels
{
    public class NavigationBarDefaultViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private IEventAggregator _ea;
        private DelegateCommand _expendMenucmd;
        private bool _isExpended;
        private bool _isVisibled;
        private DelegateCommand<string> _navigate;

        public NavigationBarDefaultViewModel(IRegionManager manager, IEventAggregator ea,IApplicationCommands applicationCommands)
        {
            _regionManager = manager;
            _ea = ea;
            _applicationCommands = applicationCommands;
        }

        private IApplicationCommands _applicationCommands;

       

        public IApplicationCommands ApplicationCommands
        {
            get { return _applicationCommands; }
            set { SetProperty(ref _applicationCommands, value); }
        }

        public DelegateCommand ExpendMenuCmd =>
            _expendMenucmd ?? (_expendMenucmd = new DelegateCommand(ExecuteExpendMenuCmd));

        public bool IsExpended
        {
            get { return _isExpended; }
            set { SetProperty(ref _isExpended, value); }
        }

        public bool IsVisibled
        {
            get { return _isVisibled; }
            set { SetProperty(ref _isVisibled, value); }
        }

        public DelegateCommand<string> Navigate =>
            _navigate ?? (_navigate = new DelegateCommand<string>(ExecuteNavigate));

       
        private void ExecuteExpendMenuCmd()
        {
            IsExpended = IsExpended ? false : true;
        }

        private void ExecuteNavigate(string viewName)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, viewName, NavigationCallback);
        }

        private void NavigationCallback(NavigationResult result)
        {
            if (result.Result == true)
            {
                RaiseUpdateMenuEvent(result);
            }
        }

        private void RaiseUpdateMenuEvent(NavigationResult result)
        {
            _ea.GetEvent<UpdateMenuEvent>().Publish(result);
        }
    }
}