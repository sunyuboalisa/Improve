using Improve.CompositeCommands.Core.Interfaces;
using Improve.Infrastructure.Events;
using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace Improve.Module.Plan.ViewModels
{
    public class HomeViewModel : BindableBase, IActiveAware
    {
        private DelegateCommand _AddPlanCmd;
        private IApplicationCommands _applicationCommands;
        private IEventAggregator _ea;
        private bool _isActive;

        private string _message;

        private string _proverbs;

        private string _proverbs_En;

        public HomeViewModel(IEventAggregator ea, IApplicationCommands applicationCommands)
        {
            Message = "View A from your Prism Module";
            Plans = new ObservableCollection<Models.Plan>();
            Proverbs = "  View A from your Prism ModuleView A from your Prism ModuleView A from your Prism ModuleView A from your Prism ModuleView A from your Prism Module";
            _ea = ea;
            _ea.GetEvent<UpdateMenuEvent>().Subscribe(UpdateMenu_Handler);
            _applicationCommands = applicationCommands;
        }

        public event EventHandler IsActiveChanged;
        public DelegateCommand AddPlanCmd =>
            _AddPlanCmd ?? (_AddPlanCmd = new DelegateCommand(ExecuteAddPlanCmd));

        private DelegateCommand<string> _openSchedule;
        public DelegateCommand<string> OpenSchedule =>
            _openSchedule ?? (_openSchedule = new DelegateCommand<string>(ExecuteOpenSchedule));

        void ExecuteOpenSchedule(string viewNames)
        {
            _applicationCommands.Navigate.Execute(viewNames);
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnIsActiveChanged();
            }
        }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ObservableCollection<Models.Plan> Plans { get; set; }

        public string Proverbs
        {
            get { return _proverbs; }
            set { SetProperty(ref _proverbs, value); }
        }

        public string Proverbs_En
        {
            get { return _proverbs_En; }
            set { SetProperty(ref _proverbs_En, value); }
        }

        private void Destroy()
        {
            _applicationCommands.AddPlan.UnregisterCommand(AddPlanCmd);
            _applicationCommands.OpenSchedule.UnregisterCommand(OpenSchedule);
        }

        private void ExecuteAddPlanCmd()
        {
            
        }

        private void OnIsActiveChanged()
        {
            //HomeCmd.IsActive = IsActive; //set the command as active
            IsActiveChanged?.Invoke(this, new EventArgs()); //invoke the event for all listeners
        }
        private void UpdateMenu_Handler(NavigationResult result)
        {
            //
            // 页面切换的时候要更新导航栏菜单。
            // 导航到当前页面时，加载当前页面的命令和显示当前的菜单选项。
            // 从当前页面导航到其他页面的时候，需要去除当前页面的命令和菜单选项。
            //
            if (IsActive)
            {
                _applicationCommands.AddPlan.RegisterCommand(AddPlanCmd);
                _applicationCommands.OpenSchedule.RegisterCommand(OpenSchedule);
            }
            else
            {
                Destroy();
            };
        }
    }
}