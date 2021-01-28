using Improve.CompositeCommands.Core.Interfaces;
using Prism.Commands;
using System;

namespace Improve.CompositeCommands.Core
{
    public class ApplicationCommands : IApplicationCommands
    {
        private CompositeCommand _addPlan = new CompositeCommand();
        public CompositeCommand AddPlan => _addPlan;
    }
}
