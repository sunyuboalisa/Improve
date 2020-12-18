using Improve.Module.Plan.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Improve.Module.Plan.ViewModels
{
    public class CalendarViewModel : BindableBase
    {
        public Week Week { get; }

        public WeekSchedule WeekSchedule { get; set; }


        public CalendarViewModel()
        {
            Week = new Week();
            WeekSchedule = new WeekSchedule();
            AddSampleData();
        }

        void AddSampleData()
        {
            
        }

    }
}
