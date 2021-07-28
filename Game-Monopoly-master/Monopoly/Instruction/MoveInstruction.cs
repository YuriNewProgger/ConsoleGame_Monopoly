using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class MoveInstruction : IInstruction
    {
        public string Name { get; } = "Move";

        public void Execute(Player player) => player.Move();
        

    }
}
