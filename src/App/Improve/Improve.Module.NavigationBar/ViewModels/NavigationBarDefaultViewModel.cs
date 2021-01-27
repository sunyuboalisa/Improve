using Improve.Infrastructure.Contants;
using Improve.Module.NavigationBar.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Improve.Module.NavigationBar.ViewModels
{
    public class NavigationBarDefaultViewModel : BindableBase
    {
        private DelegateCommand _expendMenucmd;
        private bool _isExpended;
        private bool _isVisibled;
        private readonly IRegionManager _regionManager;

        public NavigationBarDefaultViewModel(IRegionManager manager)
        {
            _regionManager = manager;
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

        private void ExecuteExpendMenuCmd()
        {
            IsExpended = IsExpended ? false : true;
        }

        private DelegateCommand<string> _navigate;
        public DelegateCommand<string> Navigate =>
            _navigate ?? (_navigate = new DelegateCommand<string>(ExecuteNavigate));

        void ExecuteNavigate(string viewName)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, viewName);
        }
    }
}