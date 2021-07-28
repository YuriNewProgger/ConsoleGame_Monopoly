using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class SellHouseInstruction : IInstruction
    {
        public string Name { get; } = "Sell house";

        public void Execute(Player player) => player.SellHouse();
    }
}
