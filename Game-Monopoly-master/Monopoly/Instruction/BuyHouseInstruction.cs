using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class BuyHouseInstruction : IInstruction
    {
        public string Name { get; } = "Buy house";

        public void Execute(Player player) => player.BuyHome();
    }
}
