using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly.EventsInTheMonopoliya
{
    class FieldChanceInTheMonopoliya : IEventInTheMonopoliya
    {
        public string NameField { get; } = "Шанс";

        public void execute(Player player)
        {
            Chance.Chances.RandChance().execute(player);
            Thread.Sleep(3000);
        }
    }
}
