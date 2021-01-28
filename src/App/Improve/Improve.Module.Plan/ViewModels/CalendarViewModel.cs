using Improve.CompositeCommands.Core.Interfaces;
using Improve.Infrastructure.Events;
using Improve.Module.Plan.Models;
using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace Improve.Module.Plan.ViewModels
{
    public class CalendarViewModel : BindableBase, IActiveAware
    {
        private IApplicationCommands _applicationCommands;

        private IEventAggregator _ea;
        private DelegateCommand _homeCmd;

        public CalendarViewModel(IEventAggregator ea, IApplicationCommands applicationCommands)
        {
            Week = new Week();
            WeekSchedule = new WeekSchedule();
            _ea = ea;
            _ea.GetEvent<UpdateMenuEvent>().Subscribe(UpdateMenu_Handler);
            _applicationCommands = applicationCommands;
        }

        public event EventHandler IsActiveChanged;

        private bool _isActive;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnIsActiveChanged();
            }
        }

        private void OnIsActiveChanged()
        {
            HomeCmd.IsActive = IsActive; //set the command as active
            IsActiveChanged?.Invoke(this, new EventArgs()); //invoke the event for all listeners
        }

        public DelegateCommand HomeCmd =>
            _homeCmd ?? (_homeCmd = new DelegateCommand(ExecuteHomeCmd));

        public Week Week { get; }

        public WeekSchedule WeekSchedule { get; set; }

        private void AddSampleData()
        {
        }

        private void ExecuteHomeCmd()
        {
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
                _applicationCommands.AddPlan.RegisterCommand(HomeCmd);
            }
            else
            {
                Destroy();
            };
        }

        private void Destroy()
        {
            _applicationCommands.AddPlan.UnregisterCommand(HomeCmd);
        }
    }
}