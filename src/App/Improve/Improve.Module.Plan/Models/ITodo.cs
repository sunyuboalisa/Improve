using System;
using System.Collections.Generic;
using System.Text;

namespace Improve.Module.Plan.Models
{
    public enum State
    {
        NotStarted,
        InProgress,
        Completed,
        Overdue
    }

    interface ITodo
    {
        string Title { get; }
        string Description { get; }

        State State { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
    }
}
