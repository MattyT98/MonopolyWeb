using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoWeb
{
    public class Board
    {
        //Attributes
        List<string> squareNames = new List<string>();             //List of names

        List<int> TileCost = new List<int>();                      //List of costs
        List<bool> TileBought = new List<bool>();                  //Tile bought or not (automatically true for non buyable)
        List<int> TileOwner = new List<int>();                     //Tile Owners 0 - 4 (0 being bank)
        List<int> propertyRentCost = new List<int>();              //Property rent cost   

        List<Tuple<int, int>> TileLocation = new List<Tuple<int, int>>();

        //Initialise Board
        public void InitialiseBoard()
        {

            // FBottom Row (10 Spaces - Including Pass Go)
            squareNames.Add("Pass Go");
            squareNames.Add("Old Kent Road");
            squareNames.Add("Community Chest");
            squareNames.Add("Whitechapel Road");
            squareNames.Add("Income Tax");
            squareNames.Add("Kings Cross Station");
            squareNames.Add("The Angel Islington");
            squareNames.Add("Chance");
            squareNames.Add("Euston Road");
            squareNames.Add("Pentonville Road");

            // Left Row (10 Spaces - Including Jail)
            squareNames.Add("Jail");
            squareNames.Add("Pall Mall");
            squareNames.Add("Electric Company");
            squareNames.Add("Whitehall");
            squareNames.Add("Northumrl'd Avenue");
            squareNames.Add("Marylebone Station");
            squareNames.Add("Bow Street");
            squareNames.Add("Community Chest");
            squareNames.Add("Marlborough Street");
            squareNames.Add("Vine Street");

            // Top Row (10 Spaces - Including  Free Parking)
            squareNames.Add("FreeParking");
            squareNames.Add("Strand");
            squareNames.Add("Chance");
            squareNames.Add("Fleet Street");
            squareNames.Add("Trafalgar Square");
            squareNames.Add("Fenchurch St. Station");
            squareNames.Add("Leicester Square");
            squareNames.Add("Coventry Street");
            squareNames.Add("Water Works");
            squareNames.Add("Piccadily");

            // Right Row (9 Spaces - Including Go To Jail)
            squareNames.Add("Go To Jail");
            squareNames.Add("Regent Street");
            squareNames.Add("Community Chest");
            squareNames.Add("Oxford Street");
            squareNames.Add("Bond Street");
            squareNames.Add("Liverpool St. Station");
            squareNames.Add("Chance");
            squareNames.Add("Park Lane");
            squareNames.Add("Super Tax");
            squareNames.Add("Mayfair");
        }
        public void InitialiseBoardCosts()
        {
            //Bottom Row including passgo
            TileCost.Add(200);    //Passgo
            TileCost.Add(60);
            TileCost.Add(0);    //Community Chest
            TileCost.Add(60);
            TileCost.Add(200);  //Income Tax
            TileCost.Add(200);
            TileCost.Add(100);
            TileCost.Add(0);    //Chance
            TileCost.Add(100);
            TileCost.Add(120);

            //Left Row including jail
            TileCost.Add(0);    //Jail
            TileCost.Add(140);
            TileCost.Add(150);
            TileCost.Add(140);
            TileCost.Add(160);
            TileCost.Add(200);
            TileCost.Add(180);
            TileCost.Add(0);    //Community Chest
            TileCost.Add(180);
            TileCost.Add(200);

            //Top row including freeparking
            TileCost.Add(0);    //Freeparking
            TileCost.Add(220);
            TileCost.Add(0);    //Chance
            TileCost.Add(220);
            TileCost.Add(240);
            TileCost.Add(200);
            TileCost.Add(260);
            TileCost.Add(260);
            TileCost.Add(150);
            TileCost.Add(280);

            //right row including go to jail
            TileCost.Add(0);    //Go To Jail
            TileCost.Add(300);
            TileCost.Add(0);    //Community Chest
            TileCost.Add(300);
            TileCost.Add(320);
            TileCost.Add(200);
            TileCost.Add(0);    //Chance
            TileCost.Add(350);
            TileCost.Add(100);  //Super Tax
            TileCost.Add(400);
        }
        public void InitialiseTileBought()
        {
            TileBought.Add(true); //Pass GO
            TileBought.Add(false); //Old Kent Road
            TileBought.Add(true); //Community Chest
            TileBought.Add(false); //WhiteChapel Road
            TileBought.Add(true); //Income Tax
            TileBought.Add(false); //Kings Cross Station
            TileBought.Add(false); //The Angel Islington
            TileBought.Add(true); //Chance
            TileBought.Add(false); //Euston Road
            TileBought.Add(false); //Pentonvilleroad

            TileBought.Add(true); //Jail
            TileBought.Add(false); //Pall Mall
            TileBought.Add(false); //Electric Company
            TileBought.Add(false); // Whitehall
            TileBought.Add(false); //Northumrl'd Avenue
            TileBought.Add(false); //Marylebone Station
            TileBought.Add(false); //Bow Street
            TileBought.Add(true); //Community Chest
            TileBought.Add(false); //Malborough Street
            TileBought.Add(false); //Vine Street

            TileBought.Add(true); //FreeParking
            TileBought.Add(false); //Strand
            TileBought.Add(true); //Chance
            TileBought.Add(false); //Fleet Street
            TileBought.Add(false); //Trafalgar Square
            TileBought.Add(false); //Fenchurch St. Station
            TileBought.Add(false); //Leicester Square
            TileBought.Add(false); //Coventry Street
            TileBought.Add(false); //Water Works
            TileBought.Add(false); //Piccadily

            TileBought.Add(true); //Go To Jail
            TileBought.Add(false); //Regent Street
            TileBought.Add(true); //Community CHest
            TileBought.Add(false); //Oxford Street
            TileBought.Add(false); //Bond Street
            TileBought.Add(false); //LiverPool St. Station
            TileBought.Add(true); //Chance
            TileBought.Add(false); //Park Lane
            TileBought.Add(true); //Super Tax
            TileBought.Add(false); //Mayfair
        }
        public void InitialiseBoardOwners()
        {
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);

            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);

            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);

            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
        }
        public void InitialiseBoardRent()
        {
            //Bottom Row including passgo
            propertyRentCost.Add(0);    //Passgo
            propertyRentCost.Add(2);    //Old Kent Road
            propertyRentCost.Add(0);    //Community Chest
            propertyRentCost.Add(4);    //WhiteChapel Road
            propertyRentCost.Add(0);    //Income Tax
            propertyRentCost.Add(25);   //Kings Cross station
            propertyRentCost.Add(6);    // The Angel Islington
            propertyRentCost.Add(0);    //Chance
            propertyRentCost.Add(6);    //Euston Road
            propertyRentCost.Add(8);    //Pentonville Road

            //Left Row including jail
            propertyRentCost.Add(0);    //Jail
            propertyRentCost.Add(10);   //Pall Mall
            propertyRentCost.Add(0);    //electric comapny
            propertyRentCost.Add(10);   //WhiteHall
            propertyRentCost.Add(12);   //Northumberland Avenue
            propertyRentCost.Add(25);   //Marylebone Station
            propertyRentCost.Add(14);   //Bow Street
            propertyRentCost.Add(0);    //Community Chest
            propertyRentCost.Add(14);   //Marlborough Street
            propertyRentCost.Add(16);   // Vine Street

            //Top row including freeparking
            propertyRentCost.Add(0);    //Freeparking
            propertyRentCost.Add(18);   //The Strand
            propertyRentCost.Add(0);    //Chance
            propertyRentCost.Add(18);   //Fleet Street
            propertyRentCost.Add(20);   //Trafalgar Square
            propertyRentCost.Add(25);   //Fenchurch St Station
            propertyRentCost.Add(22);   //Leicester square
            propertyRentCost.Add(22);   //Coventry Street
            propertyRentCost.Add(0);    //Water Works
            propertyRentCost.Add(22);   //Piccadilly

            //right row including go to jail
            propertyRentCost.Add(0);    //Go To Jail
            propertyRentCost.Add(26);   //Regent Street
            propertyRentCost.Add(0);    //Community Chest
            propertyRentCost.Add(26);   //Oxford Street
            propertyRentCost.Add(28);   //Bond Street
            propertyRentCost.Add(25);   //Liverpool Street Station
            propertyRentCost.Add(0);    //Chance
            propertyRentCost.Add(35);   //Park Lane
            propertyRentCost.Add(0);    //Super Tax
            propertyRentCost.Add(50);   //Mayfair
        }
        public void InitialiseBoardLocations()
        {   //p1                               //Top  Left
            //TileLocation.Add(new Tuple<int, int>(621, 565));        //Pass Go

            TileLocation.Add(new Tuple<int, int>(721, 845));        //Pass Go
            TileLocation.Add(new Tuple<int, int>(645, 505));        //
            TileLocation.Add(new Tuple<int, int>(645, 453));        // 
            TileLocation.Add(new Tuple<int, int>(645, 401));        //
            TileLocation.Add(new Tuple<int, int>(645, 349));        //
            TileLocation.Add(new Tuple<int, int>(645, 297));        //
            TileLocation.Add(new Tuple<int, int>(645, 245));        //
            TileLocation.Add(new Tuple<int, int>(645, 193));        //
            TileLocation.Add(new Tuple<int, int>(645, 141));        //
            TileLocation.Add(new Tuple<int, int>(645, 90));         //

            TileLocation.Add(new Tuple<int, int>(625, 2));        //Visiting Jail
            TileLocation.Add(new Tuple<int, int>(569, 15));
            TileLocation.Add(new Tuple<int, int>(517, 15));
            TileLocation.Add(new Tuple<int, int>(465, 15));
            TileLocation.Add(new Tuple<int, int>(413, 15));
            TileLocation.Add(new Tuple<int, int>(361, 15));
            TileLocation.Add(new Tuple<int, int>(309, 15));
            TileLocation.Add(new Tuple<int, int>(257, 15));
            TileLocation.Add(new Tuple<int, int>(205, 15));
            TileLocation.Add(new Tuple<int, int>(153, 15));

            TileLocation.Add(new Tuple<int, int>(75, 15));     //Free Parking
            TileLocation.Add(new Tuple<int, int>(75, 90));
            TileLocation.Add(new Tuple<int, int>(75, 141));
            TileLocation.Add(new Tuple<int, int>(75, 193));
            TileLocation.Add(new Tuple<int, int>(75, 245));
            TileLocation.Add(new Tuple<int, int>(75, 297));
            TileLocation.Add(new Tuple<int, int>(75, 349));
            TileLocation.Add(new Tuple<int, int>(75, 401));
            TileLocation.Add(new Tuple<int, int>(75, 453));
            TileLocation.Add(new Tuple<int, int>(75, 505));

            TileLocation.Add(new Tuple<int, int>(100, 565));    //GoToJail
            TileLocation.Add(new Tuple<int, int>(153, 580));
            TileLocation.Add(new Tuple<int, int>(205, 580));
            TileLocation.Add(new Tuple<int, int>(257, 580));
            TileLocation.Add(new Tuple<int, int>(309, 580));
            TileLocation.Add(new Tuple<int, int>(361, 580));
            TileLocation.Add(new Tuple<int, int>(413, 580));
            TileLocation.Add(new Tuple<int, int>(465, 580));
            TileLocation.Add(new Tuple<int, int>(517, 580));
            TileLocation.Add(new Tuple<int, int>(569, 580));
        }

        //Functions
        public int SquareNamesCount() { return squareNames.Count; }
        public void updateTileBought(int position) { TileBought[position] = true; }
        public void updateTileOwner(int position, int player) { TileOwner[position] = player; }
        public int GetLocationTop(int PlayerPosition) { return TileLocation[PlayerPosition].Item1; }
        public int GetLocationLeft(int PlayerPosition) { return TileLocation[PlayerPosition].Item2; }
        public string GetTileName(int playerPosition) { return squareNames[playerPosition]; }

        public List<bool> TilesBoughts() { return TileBought; }
        public List<int> TilesCosts() { return TileCost; }
        public List<int> TilesOwners() { return TileOwner; }
        public List<string> SquaresNames() { return squareNames; }
        public List<int> PropertysRentsCosts() { return propertyRentCost; }

        public void UpdateOwner(int pos, int newOwner) { TileOwner[pos] = newOwner; }
        public void UpdateBought(int pos) { TileBought[pos] = true; }
    }
}