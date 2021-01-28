using Improve.Infrastructure.Contants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Improve.Module.NavigationBar.ViewModels
{
    public class NavigationBarDefaultViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private DelegateCommand _expendMenucmd;
        private bool _isExpended;
        private bool _isVisibled;
        private DelegateCommand<string> _navigate;

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

        public DelegateCommand<string> Navigate =>
            _navigate ?? (_navigate = new DelegateCommand<string>(ExecuteNavigate));

        private void ExecuteExpendMenuCmd()
        {
            IsExpended = IsExpended ? false : true;
        }

        private void ExecuteNavigate(string viewName)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, viewName);
        }
    }
}