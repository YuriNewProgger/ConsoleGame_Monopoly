using Monopoly.EventsInTheMonopoliya;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly
{
    class Player
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public ConsoleColor ColorName { get; set; }
        public int Field { get; set; } = 0;
        public int Double { get; set; }
        public int NumberQueue { get; set; }
        public int FreeExitOutPrison { get; set; }

        public Player(){}

        public Player(string name, int money, ConsoleColor colorName)
        {
            Name = name;
            Money = money;
            ColorName = colorName;
            FreeExitOutPrison = 0;
        }

        public void Move()
        {
            //ToDo Добавить ф-ю проверки дублей
            //Если 3 раза подряд, отправить игрока в тюрьму и он пропускает 3 хода

            (int, int) valuecRollTheDice = SettingTheGame.RollTheDice();
            
            int sumDices = valuecRollTheDice.Item1 + valuecRollTheDice.Item2;

            if ((sumDices + Field) > 39)
            {
                Field = (Field + sumDices) - 39;
                Money += 200;
            }
            else
                Field += valuecRollTheDice.Item1 + valuecRollTheDice.Item2;
            //------ToDo после реализации всех евентов в игре(шансы, пол. уч., общественная кзна)
            //удалить данную проверку.
            //В XML файле удалить поле IsBisy.
            if (Program.streets[Field].IsBuy == false)
            {
                Program.CarriageShift(50, 11);
                Console.WriteLine($"{Name} dice = {sumDices}");
                PrintInformationAboutStreet("Can't buy");
                Thread.Sleep(3000);
                return;
            }
            //----------------------^------------------------------------
            IEventInTheMonopoliya _event = EventsInTheMonopoliya
                .EventsInTheMonopoliya
                .FindEvent(Program.streets[Field].StreetName);

            if(_event != null)
            {
                _event.execute(this);
                return;
            }

            //ToDo Если попали на поле с полицейским участком, то отправляем игрока на клетку с тюрьмой.
            //В дальнейшем переделать данный функционал, напропуски хода и так далее
            if(Field == 30)
            {
                Field = 10;
                return;
            }    

            if (PayRent())
                return;

            OfferToBuy(sumDices);
        }

        private void OfferToBuy(int sumDices)
        {
            Program.CarriageShift(50, 11);
            Console.WriteLine($"{Name} dice = {sumDices}");

            PrintInformationAboutStreet("Buy / Pass / Auction");

            string command = Console.ReadLine();

            if (command != "Buy" && command != "Pass" && command != "Auction")
                command = InputCommandBuyOrPass(command);

            switch (command)
            {
                case "Buy":
                    BuyStreet();
                    break;
                case "Auction":
                    Auction();
                    break;
                case "Pass": break;
            }
        }

        private bool PayRent()
        {

            if (Program.streets[Field].Own != Name && Program.streets[Field].Own != "none")
            {
                Player otherPlayer = Program.players
                                .Where(i => i.Name == Program.streets[Field].Own)
                                .First();
                
                int sumRent = Program.streets[Field].Rent;
                Money -= sumRent;
                otherPlayer.Money += sumRent;

                PrintInformationPayRent(otherPlayer, sumRent);

                return true;
            }
            return false;
        }

        private void BuyStreet()
        {
            if(Money < Program.streets[Field].PriceStreet)
            {
                PrintInformationAboutStreet("Due to low balance, you cannot buy.");
                Thread.Sleep(2000);
                Auction();
                //return;
                //если денег не достаточно, то улица попадает на аукцион автоматически
            }

            Program.streets[Field].Own = Name;
            Money -= Program.streets[Field].PriceStreet;
        }

        private string InputCommandBuyOrPass(string command)
        {
            while (command != "Buy" && command != "Pass" && command != "Auction")
            {
                Program.CarriageShift(50, 13);

                Console.WriteLine("Unknow command!");

                Thread.Sleep(2000);

                Console.Clear();

                Program.PrintMap();

                Program.PrintOfCommand(Program.instructions);

                Program.PrintListOfPlayers(Program.players);

                Program.PrintPlayerTurn(this);

                PrintInformationAboutStreet("Buy / Pass / Auction");

                command = Console.ReadLine();
            }

            return command;
        }

        private void PrintInformationAboutStreet(string info)
        {
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{info}: {Program.streets[Field].StreetName} = {Program.streets[Field].PriceStreet} $");
            Program.CarriageShift(50, 13);
        }

        private void PrintInformationPayRent(Player otherPlayer, int sum)
        {
            Console.ForegroundColor = ColorName;
            Program.CarriageShift(50, 12);
            Console.WriteLine($"{Name} paid the rent to {otherPlayer.Name} in the amount of {sum} $");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(3000);
        }

        private int PaymentsFromHouses() => 
            (Program.streets[Field].PriceStreet * (5 * Program.streets[Field].NumberOfHouses)) / 100;

        private int PaymentFromHotel() =>
            (Program.streets[Field].PriceStreet * 30) / 100;

        public void BuyHome()
        {
            Program.CarriageShift(50, 13);
            Console.WriteLine("Which street do you want to buy houses on?");

            Program.CarriageShift(50, 14);
            string nameStreetForBuyHouse = Console.ReadLine();

            Street street = Program.streets
                .Where(i => i.StreetName == nameStreetForBuyHouse)
                .FirstOrDefault();

            if (!IsExistStreet(street))
                return;

            if (!IsBuild(street))
            {
                Program.CarriageShift(50, 14);
                Console.WriteLine("Building a house is not possible.");
                Thread.Sleep(2000);
                return;
            }
            

            Console.WriteLine("Enter the number of houses to buy:");
            Program.CarriageShift(50, 15);

            int countHouse = int.Parse(Console.ReadLine());

            if (countHouse <= 4 && countHouse > 0)
            {
                street.NumberOfHouses += countHouse;

                street.Rent += PaymentsFromHouses();
            }
        }

        private bool IsBuild(Street street)
        {
            IList<Street> groupStreet = Program.streets
                            .Where(i => i.Group == street.Group)
                            .ToList();

            foreach (Street streetFromGroup in groupStreet)
                if (streetFromGroup.Own != Name)
                    return false;
            
            return true;
        }

        public bool IsExistStreet(Street street)
        {
            if (street is null)
            {
                Program.CarriageShift(50, 15);
                Console.WriteLine("No such street exists!");
                Thread.Sleep(2000);

                Console.Clear();
                Program.PrintMap();
                Program.PrintOfCommand(Program.instructions);
                Program.PrintListOfPlayers(Program.players);

                Program.PrintPlayerTurn(this);

                return false;
            }
            return true;
        }

        public void BuyHotel()
        {
            Program.CarriageShift(50, 13);
            Console.WriteLine("Which street do you want to buy houses on?");

            Program.CarriageShift(50, 14);
            string nameStreetForBuyHouse = Console.ReadLine();

            Street street = Program.streets
                .Where(i => i.StreetName == nameStreetForBuyHouse)
                .First();

            if (street.Hotel > 0)
            {
                Program.CarriageShift(50, 15);
                Console.WriteLine("The hotel is already standing!");
                return;
            }

            if (street.NumberOfHouses < 4)
            {
                Program.CarriageShift(50, 15);
                Console.WriteLine("Not enough houses to build a hotel!");
                return;
            }

            street.Hotel++;
            street.Rent += PaymentFromHotel();
        }

        public void Leave()
        {
            List<Street> streets = Program.streets
                .Where(i => i.Own == Name)
                .ToList();

            foreach (Street street in streets)
                street.Own = "none";

            Program.players.Remove(this);
        }

        public void SellHouse()
        {
            Program.CarriageShift(50, 11);
            Console.WriteLine("Which street do you want to sell the house on?");

            Program.CarriageShift(50, 12);
            string nameStreetForSellHouse = Console.ReadLine();

            Street street = Program.streets
                .Where(i => i.StreetName == nameStreetForSellHouse)
                .FirstOrDefault();

            if(street is null)
            {
                Program.CarriageShift(50, 13);
                Console.WriteLine("Street not found.");
                return;
            }

            if(street.Own != Name)
            {
                Program.CarriageShift(50, 13);
                Console.WriteLine("You can't sell other people's houses.");
                return;
            }

            if(street.Hotel > 0)
            {
                PrintInformationAboutStreet("Sell ​​the hotel first.");
                return;
            }

            Program.CarriageShift(50, 13);
            Console.WriteLine("Enter the number of homes for sale:");

            Program.CarriageShift(50, 14);

            int count = 0;

            while (!int.TryParse(Console.ReadLine(), out count))
            {
                Program.CarriageShift(50, 15);
                Console.WriteLine("Incirrect value!");

                Console.Clear();
                Program.PrintMap();
                Program.PrintOfCommand(Program.instructions);
                Program.PrintListOfPlayers(Program.players);
            }
            
            if(count < 1 || count > street.NumberOfHouses)
            {
                Program.CarriageShift(50, 15);
                Console.WriteLine("Incirrect value!");
                Thread.Sleep(2000);

                Console.Clear();
                Program.PrintMap();
                Program.PrintOfCommand(Program.instructions);
                Program.PrintListOfPlayers(Program.players);
            }

             
            street.NumberOfHouses -= count;
            int difference = street.Rent - PaymentsFromHouses();
            street.Rent -= difference;
        }

        public void SellHotel()
        {
            Program.CarriageShift(50, 11);
            Console.WriteLine("Which street do you want to sell the house on?");

            Program.CarriageShift(50, 12);
            string nameStreetForSellHouse = Console.ReadLine();

            Street street = Program.streets
                .Where(i => i.StreetName == nameStreetForSellHouse)
                .FirstOrDefault();

            if (street is null)
            {
                Program.CarriageShift(50, 13);
                Console.WriteLine("Street not found.");
                return;
            }

            if (street.Own != Name)
            {
                Program.CarriageShift(50, 13);
                Console.WriteLine("You can't sell other people's houses.");
                return;
            }

            
            int difference = street.Rent - PaymentFromHotel();
            street.Hotel = 0;
            street.Rent -= difference;
        }

        public void Trade()
        {
            Program.CarriageShift(50, 11);
            Console.WriteLine("Enter the name of the player you want to trade with.");

            Program.CarriageShift(50, 12);
            string name = Console.ReadLine();

            Player player = Program.players
                .Where(i => i.Name == name)
                .FirstOrDefault();

            if (player is null)
            {
                Program.CarriageShift(50, 13);
                Console.WriteLine("Player not found.");
                Thread.Sleep(2000);
                return;
            }

            Console.Clear();
            Console.WriteLine("~~~Trade~~~");
            Console.WriteLine("What do you offer (Money/Street)?");

            string offer = Console.ReadLine();

            Console.WriteLine("What do you want (Money/Street)?");

            string want = Console.ReadLine();

            PrintOwn(player);

            Program.CarriageShift(0, 5);

            if (offer == "Money" && want == "Street")
            {
                int sumOffer = InputNumber("Enter sum:");

                if (!CheckBalanceForTrade(Money, sumOffer))
                    return;

                Street streetWant = FindStreet();

                if (streetWant is null)
                {
                    Console.WriteLine("Street not found!");
                    Thread.Sleep(2000);
                    return;
                }

                Console.WriteLine($"{player.Name} Do you agree with this offer?");

                string command = Console.ReadLine();

                if (command != "Yes")
                {
                    Console.WriteLine("The deal fell through");
                    Thread.Sleep(2000);
                    return;
                }

                Money -= sumOffer;
                player.Money += sumOffer;
                streetWant.Own = Name;
            }
            else if (offer == "Street" && want == "Street")
            {
                Street streetOffer = FindStreet();
                Street streetWant = FindStreet();

                Console.WriteLine($"{player.Name} Do you agree with this offer?");

                string command = Console.ReadLine();

                if (command != "Yes")
                {
                    Console.WriteLine("The deal fell through");
                    Thread.Sleep(2000);
                    return;
                }

                streetOffer.Own = player.Name;
                streetWant.Own = Name;
                //Протестировать правилность работы данной функции
            }
            else if (offer == "Street" && want == "Money")
            {
                Street streetOffer = FindStreet();
                int sumWant = InputNumber("Enter sum:");

                if (!CheckBalanceForTrade(player.Money, sumWant))
                    return;

                Console.WriteLine($"{player.Name} Do you agree with this offer?");

                string command = Console.ReadLine();

                if (command != "Yes")
                {
                    Console.WriteLine("The deal fell through.");
                    Thread.Sleep(2000);
                    return;
                }

                streetOffer.Own = player.Name;
                player.Money -= sumWant;
                Money += sumWant;
                //Протестировать правилность работы данной функции
            }
            else
            {
                Console.WriteLine("Terms of the deal are wrong!");
                Thread.Sleep(2000);
            }
        }

        private int InputNumber(string info)
        {
            Console.WriteLine(info);
            int sum = -1;
            while (sum <= 0)
            {

                if (!int.TryParse(Console.ReadLine(), out sum))
                    Console.WriteLine("Incorrect value!");
            }
            return sum;
        }
        
        private Street FindStreet()
        {
            Console.WriteLine("Enter name street:");
            string name = Console.ReadLine();
            Street street = Program.streets
                .Where(i => i.StreetName == name)
                .FirstOrDefault();

            return street;
        }

        private bool CheckBalanceForTrade(int balance, int sumOffer)
        {
            if (balance < sumOffer)
            {
                Console.WriteLine("There is no money on the balance!");
                Thread.Sleep(2000);
                return false;
            }
            return true;
        }

        private void PrintOwn(Player player)
        {
            List<Street> streets = Program.streets
                .Where(i => i.Own == Name || i.Own == player.Name && i.NumberOfHouses == 0 && i.Hotel == 0)
                .ToList();

            for (int i = 0; i < streets.Count; i++)
            {
                Program.CarriageShift(50, i);
                if(streets[i].Own == Name)
                {
                    Console.ForegroundColor = ColorName;
                    Console.WriteLine(streets[i].StreetName);
                }
                else
                {
                    Console.ForegroundColor = player.ColorName;
                    Console.WriteLine(streets[i].StreetName);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void Auction()
        {
            List<Player> auctionParticipants = new List<Player>(Program.players.ToList());

            int prevRate = 0;
            int currentRate = 0;
            string command = null;

            for(int i = 0; auctionParticipants.Count >= 1; i++)
            {
                if (i >= auctionParticipants.Count)
                    i = 0;

                if(auctionParticipants.Count == 1)
                {
                    Program.streets[Field].Own = auctionParticipants[i].Name;
                    auctionParticipants[i].Money -= prevRate;
                    Console.WriteLine($"Auction won {auctionParticipants[i].Name}");
                    return;
                }

                PrintInfoAboutParticipantsAndAuction(Program.streets[Field], auctionParticipants);
                Console.WriteLine($"The last bet was {prevRate} $");
                Console.WriteLine($"The player makes a bet: {auctionParticipants[i].Name}");
                Console.WriteLine("Rate / Pass");
                command = InputCommandForAuc();

                if(command == "Pass")
                {
                    auctionParticipants.RemoveAt(i);
                    i--;
                    continue;
                }

                currentRate = MakesBet(currentRate, prevRate);
                if(!CheckBalance(auctionParticipants[i], currentRate))
                {
                    auctionParticipants.RemoveAt(i);
                    Console.WriteLine("Insufficient funds on the balance sheet!");
                    Thread.Sleep(2000);
                    continue;
                }
                prevRate = currentRate;
            }
        }

        private void PrintInfoAboutParticipantsAndAuction(Street street, List<Player> auctionParticipants)
        {
            Console.Clear();
            Console.WriteLine("~~~ Auction ~~~");
            Console.WriteLine($"Play out - {Program.streets[Field].StreetName}");
            Console.WriteLine("~~~~~~~~~~~~~~~");
            foreach(Player player in auctionParticipants)
                Console.WriteLine(player.Name);
        }

        private string InputCommandForAuc()
        {
            string command = null;
            while (command != "Rate" && command != "Pass")
            {
                command = Console.ReadLine();

                if(command != "Rate" && command != "Pass")
                    Console.WriteLine("Unknow command!");
            }
            return command;
        }

        private int MakesBet(int currentRate, int prevRate)
        {
            while (currentRate <= prevRate)
            {
                currentRate = InputNumber("Enter sum: ");

                if(currentRate < prevRate)
                    Console.WriteLine("The current rate must be higher than the previous one.");
            }

            return currentRate;
        }

        private bool CheckBalance(Player player, int currentRate)
        {
            if (player.Money < currentRate)
                return false;
            return true;
        }
    }
}