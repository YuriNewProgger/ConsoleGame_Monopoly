using Monopoly.Coffers;
using Monopoly.EventCoffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly.EventsInTheMonopoliya
{
    class PublicTreasury : IEventInTheMonopoliya
    {
        public string NameField { get; } = "Общественная казна";

        public void execute(Player player)
        {
            EventsCoffers.RandEvent().execute(player);
            Thread.Sleep(3000);
        }
    }
}
