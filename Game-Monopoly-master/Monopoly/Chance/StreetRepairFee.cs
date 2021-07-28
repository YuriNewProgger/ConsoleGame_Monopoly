using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Chance
{
    class StreetRepairFee : IChance
    {
        public string Name { get; } = "Street Repair Fee";

        public void execute(Player player)
        {
            int collectingMoney = 50;
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{Name}: -{collectingMoney}$");
            player.Money -= collectingMoney;
        }
    }
}
