using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Coffers
{
    class GovernmentPayment : IEventCoffers
    {
        public string Name { get; } = "Government payment";

        public void execute(Player player)
        {
            int devident = 200;
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{Name}: +{devident}$");
            player.Money += devident;
        }
    }
}
