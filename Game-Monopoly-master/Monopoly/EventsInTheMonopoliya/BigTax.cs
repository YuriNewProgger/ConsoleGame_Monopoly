using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly.EventsInTheMonopoliya
{
    class BigTax : IEventInTheMonopoliya
    {
        public string NameField { get; } = "Сверх налог";

        public void execute(Player player)
        {
            int bigTax = (player.Money * 40) / 100;
            player.Money -= bigTax;
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{player.Name} pay big tax: -{bigTax}");
            Thread.Sleep(3000);
        }
    }
}
