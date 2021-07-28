using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Chance
{
    class FreeExitOutPrison : IChance
    {
        public string Name { get; } = "Free Exit Out Prison";

        public void execute(Player player)
        {
            int exitOutPrison = 1;
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{Name}: +{exitOutPrison}$");
            player.FreeExitOutPrison += exitOutPrison;
        }
    }
}
