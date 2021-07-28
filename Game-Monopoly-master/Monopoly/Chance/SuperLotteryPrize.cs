using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Chance
{
    class SuperLotteryPrize : IChance
    {
        public string Name { get; } = "Super Lottery Prize";

        public void execute(Player player)
        {
            int prize = 500;
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{Name}: +{prize}$");
            player.Money += prize;
        }
    }
}
