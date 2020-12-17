using System;
using System.Collections.Generic;
using System.Text;

namespace Improve.Module.Plan.Models
{
    public class Plan:ITodo
    {
        public string Name { get; set; }
        public List<Todo> TodoList { get; set; }

        #region ITodo 接口实现

        public string Title { get; set; }
        public string Description { get; set; }

        private DateTime _startTime;
        public DateTime StartTime => _startTime;

        private DateTime _endTime;
        public DateTime EndTime => _endTime;

        private State _state;
        public State State => _state;

        #endregion
    }
}
