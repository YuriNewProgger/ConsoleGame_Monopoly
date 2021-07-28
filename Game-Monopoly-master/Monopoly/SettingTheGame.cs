using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    static class SettingTheGame
    {
        public static List<ConsoleColor> consoleColors = new List<ConsoleColor>()
                { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Cyan};

        private static Player SetPlayer(int numberPlayer)
        {
            Console.WriteLine($"Enter the name of {numberPlayer + 1} player");
            string name = Console.ReadLine();

            Console.WriteLine("Select color");

            for(int i = 0; i < consoleColors.Count; i++)
                Console.WriteLine($"{i + 1}-{consoleColors[i].ToString()}");

            int numberColor = Program.InputNumberInRange(1, consoleColors.Count) - 1;

            ConsoleColor color = SettingTheGame.consoleColors[numberColor];

            consoleColors.RemoveAt(numberColor);

            int money = 1500;

            return new Player(name, money, color);
        }

        public static void AddPlayers(int playerCount, IList<Player> players)
        {
            for (int i = 0; i < playerCount; i++)
            {
                players.Add(SettingTheGame.SetPlayer(i));
                Console.Clear();
            }            
        }

        public static ValueTuple<int, int> RollTheDice() =>
            new ValueTuple<int, int>(GlobalRandom.Random.Next(1, 7), GlobalRandom.Random.Next(1, 7));

        public static IList<Player> SetQueue(IList<Player> players)
        {
            foreach (Player player in players)
            {
                (int, int) valuesRollTheDice = SettingTheGame.RollTheDice();
                player.NumberQueue = valuesRollTheDice.Item1 + valuesRollTheDice.Item2;
            }

            players = players
                .OrderByDescending(i => i.NumberQueue)
                .ToList();

            return players;
        }
        
        public static void ShowQueuePlayers(IList<Player> players)
        {
            foreach (Player player in players)
            {
                Console.ForegroundColor = player.ColorName;
                Console.WriteLine($"{player.Name}-{player.NumberQueue}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

    }
}
