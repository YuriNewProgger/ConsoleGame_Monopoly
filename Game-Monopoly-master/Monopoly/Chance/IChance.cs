using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Chance
{
    interface IChance
    {
        string Name { get; }

        void execute(Player player);
    }
}
