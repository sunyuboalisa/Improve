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

        private string _proverbs;
        public string Proverbs
        {
            get { return _proverbs; }
            set { SetProperty(ref _proverbs, value); }
        }

        private string _proverbs_En;
        public string Proverbs_En
        {
            get { return _proverbs_En; }
            set { SetProperty(ref _proverbs_En, value); }
        }

        public HomeViewModel()
        {
            Message = "View A from your Prism Module";
            Plans = new ObservableCollection<Models.Plan>();
            Proverbs = "  View A from your Prism ModuleView A from your Prism ModuleView A from your Prism ModuleView A from your Prism ModuleView A from your Prism Module";
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
