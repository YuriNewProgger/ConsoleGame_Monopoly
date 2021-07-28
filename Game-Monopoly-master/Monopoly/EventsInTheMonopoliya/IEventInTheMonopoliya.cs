using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.EventsInTheMonopoliya
{
    interface IEventInTheMonopoliya
    {
        string NameField { get; }
        void execute(Player player);
    }
}
