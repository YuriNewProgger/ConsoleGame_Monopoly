using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.EventsInTheMonopoliya
{
    static class EventsInTheMonopoliya
    {
        private static IEventInTheMonopoliya Tax { get; } = new Tax();
        private static IEventInTheMonopoliya BigTax { get; } = new BigTax();
        private static IEventInTheMonopoliya PublicTreasury { get; } = new PublicTreasury();
        private static IEventInTheMonopoliya Chance { get; } = new FieldChanceInTheMonopoliya();


        private static List<IEventInTheMonopoliya> events = new List<IEventInTheMonopoliya>()
        {
            Tax, BigTax, PublicTreasury, Chance
        };

        public static IEventInTheMonopoliya FindEvent(string nameField) => events
            .Where(i => i.NameField == nameField)
            .FirstOrDefault();
    }
}
