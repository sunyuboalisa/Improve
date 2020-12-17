using Improve.Module.Plan.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Improve.Module.Plan.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public HomeViewModel()
        {
            Message = "View A from your Prism Module";
            Plans = new ObservableCollection<Models.Plan>();
        }

        public ObservableCollection<Models.Plan> Plans { get; set; }

        private DelegateCommand _AddPlanCmd;
        public DelegateCommand AddPlanCmd =>
            _AddPlanCmd ?? (_AddPlanCmd = new DelegateCommand(ExecuteAddPlanCmd));

        void ExecuteAddPlanCmd()
        {
            var plan = new Models.Plan() { Name = "Plan1", Description = "sample plan.", Title = "Plan1 Title" };
            Plans.Add(plan);
        }
    }
}
