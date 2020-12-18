using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Text;

namespace Improve.Module.Plan.Models
{
    public class WeekSchedule : Schedule
    {
        /// <summary>
        /// 一天的某个时间段。
        /// </summary>
        public IEnumerable<HourOfDay> TimeQuantum
        {
            get
            {
                for (int i = 0; i < 24; i++)
                {
                    yield return (HourOfDay)i;
                }
            }
        }

        public ObservableCollection<DaySchedule> Schedules { get; set; }
        public ObservableCollection<RowSchedule> RawSchedules { get; set; }
        public WeekSchedule()
        {
            InitSchedules();
            InitRowSchedules();
        }

        void InitSchedules()
        {
            Schedules = new ObservableCollection<DaySchedule>();

            Schedules.Add(new DaySchedule { Date = new DateTime(2020, 12, 13) });
            Schedules.Add(new DaySchedule { Date = new DateTime(2020, 12, 14) });
            Schedules.Add(new DaySchedule { Date = new DateTime(2020, 12, 15) });
            Schedules.Add(new DaySchedule { Date = new DateTime(2020, 12, 16) });
            Schedules.Add(new DaySchedule { Date = new DateTime(2020, 12, 17) });
            Schedules.Add(new DaySchedule { Date = new DateTime(2020, 12, 18) });
            Schedules.Add(new DaySchedule { Date = new DateTime(2020, 12, 19) });
        }

        void InitRowSchedules()
        {
            RawSchedules = new ObservableCollection<RowSchedule>();

            for (int i = 0; i < 24; i++)
            {
                RawSchedules.Add(new RowSchedule((HourOfDay)i) );
            }
        }
    }

    public class DaySchedule : Schedule
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        
    }

    public class RowSchedule : Schedule
    {
        /// <summary>
        /// 一天的某个时间段。
        /// </summary>
        public HourOfDay TimeQuantum { get; }

        public RowSchedule(HourOfDay hourOfDay)
        {
            TimeQuantum = hourOfDay;
        }
    }
}
