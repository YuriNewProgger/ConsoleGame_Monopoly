using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    static class Instructions
    {
        private static IInstruction MoveInstruction { get; } = new MoveInstruction();
        private static IInstruction BuyHouseInstruction { get; } = new BuyHouseInstruction();
        private static IInstruction BuyHotelInstruction { get; } = new BuyHotelInstruction();
        private static IInstruction LeaveInstruction { get; } = new LeaveInstruction();
        private static IInstruction SellHouseInstruction { get; } = new SellHouseInstruction();
        private static IInstruction SellHotelInstruction { get; } = new SellHotelInstruction();
        private static IInstruction TradeInstruction { get; } = new TradeInstruction();

        public static List<IInstruction> listInstructions { get; } = new List<IInstruction>()
        {
            MoveInstruction, BuyHouseInstruction, BuyHotelInstruction, LeaveInstruction, SellHouseInstruction,
            SellHotelInstruction, TradeInstruction
        };

        public static IInstruction Find(string name) => listInstructions
                .Where(i => i.Name == name)
                .FirstOrDefault();
        
    }
}
