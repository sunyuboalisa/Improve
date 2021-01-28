using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Improve.CompositeCommands.Core.Interfaces
{
    public interface IApplicationCommands
    {
        CompositeCommand AddPlan { get; }
    }
}
