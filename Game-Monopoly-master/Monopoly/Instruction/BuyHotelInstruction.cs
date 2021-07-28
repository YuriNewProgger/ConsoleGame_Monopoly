using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class BuyHotelInstruction : IInstruction
    {
        public string Name { get; } = "Buy hotel";

        public void Execute(Player player) => player.BuyHotel();
    }
}
