using Improve.Infrastructure.Events;
using Improve.Module.Plan.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Improve.Module.Plan.ViewModels
{
    public class CalendarViewModel : BindableBase
    {
        private IEventAggregator _ea;
        private DelegateCommand _homeCmd;
        public CalendarViewModel(IEventAggregator ea)
        {
            Week = new Week();
            WeekSchedule = new WeekSchedule();
            AddSampleData();
            _ea = ea;
            _ea.GetEvent<UpdateMenuEvent>().Subscribe(UpdateMenu_Handler);
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

        private void UpdateMenu_Handler()
        {
            ;
        }
    }
}