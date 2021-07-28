using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Monopoly
{
    class Street
    {
        public string StreetName { get; set; }
        public int StreetID { get; set; }
        public int PriceStreet { get; set; }
        public int Rent { get; set; }
        public int NumberOfHouses { get; set; }
        public int Hotel { get; set; }
        public bool IsBuy { get; set; }
        public string Own { get; set; }
        public int Group { get; set; }

        public Street() { }

        public Street(string streetName, int streetID, int priceStreet, int rent, int numberOfHouses, int hotel, bool isBuy, string own, int group)
        {
            StreetName = streetName;
            StreetID = streetID;
            PriceStreet = priceStreet;
            Rent = rent;
            NumberOfHouses = numberOfHouses;
            Hotel = hotel;
            IsBuy = isBuy;
            Own = own;
            Group = group;
        }

        public static Street FromXml(XElement node) =>
            new Street(
                (string)node.Element("StreetName"),
                (int)node.Element("StreetID"),
                (int)node.Element("PriceStreet"),
                (int)node.Element("Rent"),
                (int)node.Element("NumberOfHouses"),
                (int)node.Element("Hotel"),
                (bool)node.Element("IsBuy"),
                (string)node.Element("Own"),
                (int)node.Element("Group"));
    }
}
