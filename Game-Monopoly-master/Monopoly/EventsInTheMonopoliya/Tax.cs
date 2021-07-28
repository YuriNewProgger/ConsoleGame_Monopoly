using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly.EventsInTheMonopoliya
{
    class Tax : IEventInTheMonopoliya
    {
        public string NameField { get; } = "Подоходный налог";

        public void execute(Player player)
        {
            int tax = (player.Money * 10) / 100;
            player.Money -= tax;
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{player.Name} pay tax: -{tax}"); 
            Thread.Sleep(3000);
        }
    }
}
