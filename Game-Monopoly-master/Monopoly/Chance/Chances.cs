using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Chance
{
    static class Chances
    {
        private static IChance superLotteryPrize { get; } = new SuperLotteryPrize();
        private static IChance streetRepairFee { get; } = new StreetRepairFee();
        private static IChance sendToJail { get; } = new SendToJail();
        private static IChance freeExitOutPrison { get; } = new FreeExitOutPrison();


        private static List<IChance> chances = new List<IChance>()
        {
            superLotteryPrize, streetRepairFee, sendToJail, freeExitOutPrison
        };

        public static IChance RandChance() => chances[GlobalRandom.Random.Next(0, 3)];

    }
}
