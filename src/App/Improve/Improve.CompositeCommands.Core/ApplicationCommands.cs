using Improve.CompositeCommands.Core.Interfaces;
using Prism.Commands;
using System;

namespace Improve.CompositeCommands.Core
{
    public class ApplicationCommands : IApplicationCommands
    {
        private CompositeCommand _addPlan = new CompositeCommand();
        public CompositeCommand AddPlan => _addPlan;

        private CompositeCommand _navigate = new CompositeCommand();
        public CompositeCommand Navigate => _navigate;

        private CompositeCommand _openSchedule = new CompositeCommand();
        public CompositeCommand OpenSchedule => _openSchedule;
    }
}
