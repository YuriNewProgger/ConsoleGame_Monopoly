using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Chance
{
    class SendToJail : IChance
    {
        public string Name { get; } = "Send To Jail";

        public void execute(Player player)
        {
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{Name}");
            player.Field = 10;
        }
    }
}
