using Monopoly.Coffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.EventCoffers
{
    static class EventsCoffers
    {
        private static IEventCoffers GovernmentPayment { get; } = new GovernmentPayment();
        private static IEventCoffers HomeRepairs { get; } = new HomeRepairs();
        private static IEventCoffers PaymentOfBills { get; } = new PaymentOfBills();
        private static IEventCoffers WeddingGift { get; } = new WeddingGift();

        private static List<IEventCoffers> events = new List<IEventCoffers>()
        {
            GovernmentPayment, HomeRepairs, PaymentOfBills, WeddingGift
        };

        public static IEventCoffers RandEvent() => events[GlobalRandom.Random.Next(0, 3)];
        
    }
}
