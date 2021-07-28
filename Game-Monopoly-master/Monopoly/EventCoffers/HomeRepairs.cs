using Monopoly.Coffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.EventCoffers
{
    class HomeRepairs : IEventCoffers
    {
        public string Name { get; } = "Home repairs";

        public void execute(Player player)
        {
            int rate = 200;
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{Name}: -{rate}$");
            player.Money -= rate;
        }
    }
}
