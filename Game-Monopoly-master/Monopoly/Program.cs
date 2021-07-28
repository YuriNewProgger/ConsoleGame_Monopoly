using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Monopoly
{
    class Program
    {
        public static IList<Player> players;
        public static IList<Street> streets;

        public static IList<IInstruction> instructions;

        static void Main(string[] args)
        {

            Console.SetWindowSize(125, 50);
            Console.SetWindowPosition(0, 0);

            players = new List<Player>();
            streets = new List<Street>();
            //-------------------------------------

            Console.WriteLine("Welcome Monopoly");

            Console.WriteLine("How many players will play (min-2/max-5)?");

            int playerCount = InputNumberInRange(2, 5);

            SettingTheGame.AddPlayers(playerCount, players);

            players = SettingTheGame.SetQueue(players);

            SettingTheGame.ShowQueuePlayers(players);
            Thread.Sleep(3000);
            Console.Clear();

            streets = XElement.Load("streets.xml")
                .Elements("Street")
                .Select(i => Street.FromXml(i))
                .ToList();

            PrintMap();

            PrintListOfPlayers(players);

            instructions = Instructions.listInstructions;
            PrintOfCommand(instructions);

            IInstruction instruction;
            for(int i = 0; players.Count > 1; i++)
            {

                if (i == players.Count)
                    i = 0;

                PrintPlayerTurn(players[i]);

                string command = null;
                do
                {
                    command = Console.ReadLine();
                    instruction = Instructions.Find(command);

                    if (instruction is null)
                    {
                        CarriageShift(50, 11);
                        Console.WriteLine("Unknow command!");
                        Thread.Sleep(2000);
                    }
                    else
                        instruction.Execute(players[i]);

                    if (command == "Leave")
                        break;

                    Console.Clear();
                    PrintMap();
                    PrintOfCommand(instructions);
                    PrintListOfPlayers(players);

                    PrintPlayerTurn(players[i]);

                } while (command != "Move");

                Console.Clear();
                PrintMap();
                PrintOfCommand(instructions);
                PrintListOfPlayers(players);
            }
        }

        public static void PrintPlayerTurn(Player player)
        {
            CarriageShift(50, 10);

            Console.Write($"Player turn: ");
            Console.ForegroundColor = player.ColorName;
            Console.WriteLine($"{player.Name} - {streets[player.Field].StreetName}");
            Console.ForegroundColor = ConsoleColor.White;

            CarriageShift(50, 11);
        }

        public static void PrintMap()
        {
            for (int i = 0; i < streets.Count; i++)
            {
                if (streets[i].Own == "none")
                {
                    if (streets[i].Group == 0)
                        Console.WriteLine(streets[i].StreetName);
                    else
                        PrintInformationOneStreet(i);
                }
                else
                {
                    ConsoleColor colorPlayer = players
                        .Where(p => p.Name == streets[i].Own)
                        .First()
                        .ColorName;

                    if (streets[i].Group == 0)
                    {
                        Console.ForegroundColor = colorPlayer;
                        Console.WriteLine($"{streets[i].StreetName}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = colorPlayer;
                        PrintInformationOneStreet(i);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            } 
        }

        private static void PrintInformationOneStreet(int i)
        {
            Console.Write($"{streets[i].StreetName}");
            CarriageShift(25, i);
            Console.WriteLine($"{streets[i].Group}gr.| Houses-{streets[i].NumberOfHouses}/Hotel-{streets[i].Hotel}");
        }

        public static void PrintOfCommand(IList<IInstruction> commands)
        {
            CarriageShift(50, 0);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Command:");
            CarriageShift(50, 1);
            Console.WriteLine("---------");
            for (int i = 0; i < commands.Count; i++)
            {
                CarriageShift(50, i + 2);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{commands[i].Name}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static int InputNumberInRange(int start, int end)
        {
            int number = -1;
            bool success = false;
            while (number < start || number > end)
            {
                success = int.TryParse(Console.ReadLine(), out number);

                if (!success)
                {
                    Console.WriteLine("This is not a number!");
                    number = -1;
                }
                else if(number < start || number > end)
                    Console.WriteLine("Incorrect value!");
            }

            Console.Clear();

            return number;
        }

        public static void CarriageShift(int x, int y)
        {
            Console.CursorLeft = x;
            Console.CursorTop = y;
        }

        public static void PrintListOfPlayers(IList<Player> players)
        {
            CarriageShift(100, 0);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("List of players:");
            CarriageShift(100, 1);
            Console.WriteLine("----------------");
            for (int i = 0; i < players.Count; i++)
            {
                CarriageShift(100, i + 2);
                Console.ForegroundColor = players[i].ColorName;
                Console.WriteLine($"{players[i].Name} - {players[i].Money}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
