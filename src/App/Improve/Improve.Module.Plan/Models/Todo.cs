using System;
using System.Collections.Generic;
using System.Text;

namespace Improve.Module.Plan.Models
{
    public class Todo: ITodo
    {
        public string Title { get; set; }
        public string Description { get; set; }

        private State _state;
        public State State => _state;

        private DateTime _startTime;
        public DateTime StartTime => _startTime;

        private DateTime _endTime;
        public DateTime EndTime => _endTime;
    }
}
