using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Improve.Infrastructure.Events
{
    public class UpdateMenuEvent :PubSubEvent<NavigationResult>
    {
    }
}
