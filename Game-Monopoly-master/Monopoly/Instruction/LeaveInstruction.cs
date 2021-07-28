using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class LeaveInstruction : IInstruction
    {
        public string Name { get; } = "Leave";

        public void Execute(Player player) => player.Leave();
    }
}
