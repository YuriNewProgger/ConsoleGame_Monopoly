using Monopoly.Coffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.EventCoffers
{
    class WeddingGift : IEventCoffers
    {
        public string Name { get; } = "Wedding Gift";

        public void execute(Player player)
        {
            int rate = 200;
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{Name}: +{rate}$");
            player.Money += rate;
        }
    }
}
