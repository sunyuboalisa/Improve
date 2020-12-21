using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Improve.Module.Plan.Models
{
    public class Todo: BindableBase,ITodo
    {
        public string Title { get; set; }
        public string Description { get; set; }

        private State _state;
        public State State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
            set { SetProperty(ref _startTime, value); }
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
            set { SetProperty(ref _endTime, value); }
        }
    }
}
