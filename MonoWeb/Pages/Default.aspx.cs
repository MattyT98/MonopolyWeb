using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;

namespace MonoWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                    
                //Players & Board        
                Board board = new Board();
                Player p1 = new Player();
                Player p2 = new Player();
                Player p3 = new Player();
                Player p4 = new Player();
                SQLDatabase.DatabaseTable GameInfo = new SQLDatabase.DatabaseTable("GameInfo");
                int turn = 0, tempPlayer = 0;
                bool gameContinue = false;

                if (!string.IsNullOrEmpty((string)Session["continue"]))
                {
                    Label14.Text = (string)Session["continue"];
                    if (Label14.Text == "true")
                        gameContinue = true;
                    else
                        gameContinue = false;
                }

            
                Image41.Attributes["src"] = "../Images/die1.png";
                Image42.Attributes["src"] = "../Images/die1.png";

                Session["gameContinue"] = gameContinue;
                Session["board"] = board;
                Session["p1"] = p1;
                Session["p2"] = p2;
                Session["p3"] = p3;
                Session["p4"] = p4;
                Session["turn"] = turn;
                Session["GameInfo"] = GameInfo;

                InitialiseGame();                    

                UpdateDataBase(p1, ref tempPlayer);
                UpdateDataBase(p2, ref tempPlayer);
                UpdateDataBase(p3, ref tempPlayer);
                UpdateDataBase(p4, ref tempPlayer);
                
                DisplayDatabase();

            }
        }

        //SetUp
        private void InitialiseGame()
        {
            Board board = (Board)Session["board"];
            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];
            bool gameContinue = (bool)Session["gameContinue"];
            SQLDatabase.DatabaseTable GameInfo = (SQLDatabase.DatabaseTable)Session["GameInfo"];
            int turn = (int)Session["turn"];

            if (gameContinue == true)
            {
                for (int r = 0; r < GameInfo.RowCount; r++)
                {
                    string Player = GameInfo.GetRow(r)["Player"];
                    string Money = GameInfo.GetRow(r)["CurrentMoney"]; // Extract the module codes...
                    string Position = GameInfo.GetRow(r)["Position"];
                    string PropertiesOwned = GameInfo.GetRow(r)["PropertiesOwned"];
                    string IsInJail = GameInfo.GetRow(r)["IsInJail"];
                    string JailOutOfFree = GameInfo.GetRow(r)["JailOutOfFree"];
                    string Rolled = GameInfo.GetRow(r)["Rolled"];
                    string BankRupt = GameInfo.GetRow(r)["BankRupt"];

                    int p = Convert.ToInt32(Player);
                    int m = Convert.ToInt32(Money);
                    int pos = Convert.ToInt32(Position);
                    int properties = Convert.ToInt32(PropertiesOwned);
                    bool injail = Convert.ToBoolean(IsInJail);
                    bool jailfree = Convert.ToBoolean(JailOutOfFree);
                    bool rolled = Convert.ToBoolean(Rolled);
                    bool end = Convert.ToBoolean(BankRupt);

                    SetUpContinue(p1, m, pos, properties, injail, jailfree, rolled, end);
                    SetUpContinue(p2, m, pos, properties, injail, jailfree, rolled, end);
                    SetUpContinue(p3, m, pos, properties, injail, jailfree, rolled, end);
                    SetUpContinue(p4, m, pos, properties, injail, jailfree, rolled, end);
                }
            }
            else
            {
                board.InitialiseBoard();
                board.InitialiseBoardCosts();
                board.InitialiseTileBought();
                board.InitialiseBoardOwners();
                board.InitialiseBoardRent();
                board.InitialiseBoardLocations();                
            }


            PlayerLocation(p1.GetPosition(), 1);
            PlayerLocation(p2.GetPosition(), 2);
            PlayerLocation(p3.GetPosition(), 3);
            PlayerLocation(p4.GetPosition(), 4);
            turn = 1;

            Image41.Attributes["src"] = "../Images/die1.png";
            Image42.Attributes["src"] = "../Images/die1.png";

            Session["board"] = board;
            Session["p1"] = p1;
            Session["p2"] = p2;
            Session["p3"] = p3;
            Session["p4"] = p4;
            Session["turn"] = turn;
        }


        private void SetUpContinue(Player p, int m, int pos, int properties, bool injail, bool jailfree, bool rolled, bool end)
        {
            p.setMoney(m);
            p.SetPosition(pos);
            p.PropertiesOwned(properties);
            p.IsInJailContinue(injail);
            p.SetJailFreeContinue(jailfree);
            p.setRolledContinue(rolled);
            p.SetBankRuptContinue(end);
        }

        //Buttons
        protected void Button1_Click(object sender, EventArgs e) //Roll Button
        {
            SQLDatabase.DatabaseTable GameInfo = (SQLDatabase.DatabaseTable)Session["GameInfo"];
            Board board = (Board)Session["board"];
            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];
            int turn = (int)Session["turn"];

            int totalRoll = 0, rolldice1 = 0, rolldice2 = 0;

            RollDice(ref totalRoll, ref rolldice1, ref rolldice2);
            MovePlayer(turn, totalRoll, rolldice1, rolldice2);
            switch (turn)
            {
                case 1: CheckPassGo(ref p1); break;
                case 2: CheckPassGo(ref p2); break;
                case 3: CheckPassGo(ref p3); break;
                case 4: CheckPassGo(ref p4); break;
            }
            switch (turn)
            {
                case 1: CheckChanceChest(ref p1); break;
                case 2: CheckChanceChest(ref p2); break;
                case 3: CheckChanceChest(ref p3); break;
                case 4: CheckChanceChest(ref p4); break;
            }
            switch (turn)
            {
                case 1: PayRent(ref p1); break;
                case 2: PayRent(ref p2); break;
                case 3: PayRent(ref p3); break;
                case 4: PayRent(ref p4); break;
            }
            switch (turn)
            {
                case 1: CheckTax(p1); break;
                case 2: CheckTax(p2); break;
                case 3: CheckTax(p3); break;
                case 4: CheckTax(p4); break;
            }
            switch (turn)
            {
                case 1: CheckGoToJail(ref p1); break;
                case 2: CheckGoToJail(ref p2); break;
                case 3: CheckGoToJail(ref p3); break;
                case 4: CheckGoToJail(ref p4); break;
            }
            switch (turn)
            {
                case 1: UpdateDisplay(p1); break;
                case 2: UpdateDisplay(p2); break;
                case 3: UpdateDisplay(p3); break;
                case 4: UpdateDisplay(p4); break;
            }

            DisplayDatabase();

            Session["board"] = board;
            Session["GameInfo"] = GameInfo;
            Session["p1"] = p1;
            Session["p2"] = p2;
            Session["p3"] = p3;
            Session["p4"] = p4;
        }
        protected void Button2_Click(object sender, EventArgs e) //Buy Property
        {
            Board board = (Board)Session["board"];
            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];
            int turn = (int)Session["turn"];

            switch (turn)
            {
                case 1: BuyProperty(ref p1); break;
                case 2: BuyProperty(ref p2); break;
                case 3: BuyProperty(ref p3); break;
                case 4: BuyProperty(ref p4); break;
            }
            switch (turn)
            {
                case 1: UpdateDisplay(p1); break;
                case 2: UpdateDisplay(p2); break;
                case 3: UpdateDisplay(p3); break;
                case 4: UpdateDisplay(p4); break;
            }

            Session["board"] = board;
            Session["p1"] = p1;
            Session["p2"] = p2;
            Session["p3"] = p3;
            Session["p4"] = p4;
        }
        protected void Button3_Click(object sender, EventArgs e) //End Turn
        {
            Board board = (Board)Session["board"];
            SQLDatabase.DatabaseTable GameInfo = (SQLDatabase.DatabaseTable)Session["gameinfo"];

            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];
            int turn = (int)Session["turn"];
            int tempPlayer = 0;

            switch (turn)
            {
                case 1: EndTurnValidation(p1); break;
                case 2: EndTurnValidation(p2); break;
                case 3: EndTurnValidation(p3); break;
                case 4: EndTurnValidation(p4); break;
            }

            UpdateDataBase(p1, ref tempPlayer);
            UpdateDataBase(p2, ref tempPlayer);
            UpdateDataBase(p3, ref tempPlayer);
            UpdateDataBase(p4, ref tempPlayer);

            DisplayDatabase();

            Session["gameinfo"] = GameInfo;
            Session["board"] = board;
            Session["p1"] = p1;
            Session["p2"] = p2;
            Session["p3"] = p3;
            Session["p4"] = p4;
        }

        protected void Button4_Click(object sender, EventArgs e) //Save Game
        {

        }

        //Functions
        private void RollDice(ref int tr, ref int d1, ref int d2) //Button 3
        {
            int turn = (int)Session["turn"];
            d1 = Rand.Next(1, 7);
            d2 = Rand.Next(1, 7);
            tr = d1 + d2;

            switch (d1)
            {
                case 1: Image41.Attributes["src"] = "../Images/die1.png"; break;
                case 2: Image41.Attributes["src"] = "../Images/die2.png"; break;
                case 3: Image41.Attributes["src"] = "../Images/die3.png"; break;
                case 4: Image41.Attributes["src"] = "../Images/die4.png"; break;
                case 5: Image41.Attributes["src"] = "../Images/die5.png"; break;
                case 6: Image41.Attributes["src"] = "../Images/die6.png"; break;
            }
            switch (d2)
            {
                case 1: Image42.Attributes["src"] = "../Images/die1.png"; break;
                case 2: Image42.Attributes["src"] = "../Images/die2.png"; break;
                case 3: Image42.Attributes["src"] = "../Images/die3.png"; break;
                case 4: Image42.Attributes["src"] = "../Images/die4.png"; break;
                case 5: Image42.Attributes["src"] = "../Images/die5.png"; break;
                case 6: Image42.Attributes["src"] = "../Images/die6.png"; break;
            }
        }
        private void MovePlayer(int player, int roll, int d1, int d2)
        {
            Board board = (Board)Session["board"];
            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];
            switch (player)
            {
                case 1: if (p1.IsInJail() == true) { if (d1 == d2) { p1.SetIsInJail(); } else { ErrorMessage(3); } } else { if (p1.GetRolled() == false) { p1.SetOriginalPosition(); MovePlayerValidation(ref p1, roll); PlayerLocation(p1.GetPosition(), player); if (d1 != d2) { p1.setRolled(); } } else { ErrorMessage(1); } } break;
                case 2: if (p2.IsInJail() == true) { if (d1 == d2) { p2.SetIsInJail(); } else { ErrorMessage(3); } } else { if (p2.GetRolled() == false) { p2.SetOriginalPosition(); MovePlayerValidation(ref p2, roll); PlayerLocation(p2.GetPosition(), player); if (d1 != d2) { p2.setRolled(); } } else { ErrorMessage(1); } } break;
                case 3: if (p3.IsInJail() == true) { if (d1 == d2) { p3.SetIsInJail(); } else { ErrorMessage(3); } } else { if (p3.GetRolled() == false) { p3.SetOriginalPosition(); MovePlayerValidation(ref p3, roll); PlayerLocation(p3.GetPosition(), player); if (d1 != d2) { p3.setRolled(); } } else { ErrorMessage(1); } } break;
                case 4: if (p4.IsInJail() == true) { if (d1 == d2) { p4.SetIsInJail(); } else { ErrorMessage(3); } } else { if (p4.GetRolled() == false) { p4.SetOriginalPosition(); MovePlayerValidation(ref p4, roll); PlayerLocation(p4.GetPosition(), player); if (d1 != d2) { p4.setRolled(); } } else { ErrorMessage(1); } } break;
            }
            Session["board"] = board;
            Session["p1"] = p1;
            Session["p2"] = p2;
            Session["p3"] = p3;
            Session["p4"] = p4;
        }
        private void MovePlayerValidation(ref Player player, int tr)
        {
            Board board = (Board)Session["board"];
            int newpos = player.GetPosition() + tr;

            player.SetPosition(newpos);
            if (player.GetPosition() > (board.SquareNamesCount() - 1))
            {
                player.SetPosition(player.GetPosition() % board.SquareNamesCount());
            }
            Session["board"] = board;
        }

        private void ErrorMessage(int m)
        {
            string message = "";
            switch (m)
            {
                case 1: message = "You Have Already Rolled!"; break;
                case 2: message = "You Need To Roll!"; break;
                case 3: message = "You Are In Jail - Better Luck Next Time"; break;
                case 4: message = "You Need To Roll Before Trying To Buy"; break;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' " + message + " ');", true);
        }
        private void ErrorMessageString(string s)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' " + s + " ');", true);
        }
        private void UpdateDisplay(Player player)
        {
            Board board = (Board)Session["board"];
            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];
            int turn = (int)Session["turn"];

            Label3.Text = "£" + player.GetMoney();
            Label5.Text = player.GetPropertiesOwned().ToString();
            Label7.Text = player.GetJailFree().ToString();
            Label12.Text = turn.ToString();
            Label13.Text = board.GetTileName(player.GetPosition());

            switch (turn)
            {
                case 1: PlayerLocation(p1.GetPosition(), turn); break;
                case 2: PlayerLocation(p2.GetPosition(), turn); break;
                case 3: PlayerLocation(p3.GetPosition(), turn); break;
                case 4: PlayerLocation(p4.GetPosition(), turn); break;
            }
            switch (turn)
            {
                case 1: UpdateDisplay2(p1); break;
                case 2: UpdateDisplay2(p2); break;
                case 3: UpdateDisplay2(p3); break;
                case 4: UpdateDisplay2(p4); break;
            }
        }
        private void UpdateDisplay2(Player player)
        {
            Board board = (Board)Session["board"];
            int turn = (int)Session["turn"];

            List<string> squareNames = new List<string>();
            List<int> TileOwner = new List<int>();

            squareNames = board.SquaresNames();
            TileOwner = board.TilesOwners();

            ListBox1.Items.Clear();
            ListBox1.Items.Add("Properties Owned:");
            for (int i = 0; i < squareNames.Count; i++)
            {
                if (TileOwner[i] == turn)
                {
                    string property = squareNames[i];
                    ListBox1.Items.Add("-" + property);
                }
            }
        }

        private void EndTurnValidation(Player player)
        {
            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];
            int turn = (int)Session["turn"];
            if (player.GetRolled() == true)
            {
                turn++;
                if (turn == 5)
                {
                    turn = 1;
                }
                player.setRolled();
                Session["turn"] = turn;
                switch (turn)
                {
                    case 1: UpdateDisplay(p1); break;
                    case 2: UpdateDisplay(p2); break;
                    case 3: UpdateDisplay(p3); break;
                    case 4: UpdateDisplay(p4); break;
                }
            }
            else
            {
                ErrorMessage(2);
            }
        }
        private void CheckChanceChest(ref Player player)
        {
            Board board = (Board)Session["board"];
            int turn = (int)Session["turn"];

            int card = Rand.Next(1, 13);
            //Chance
            if ((player.GetPosition() == 7) || (player.GetPosition() == 22) || (player.GetPosition() == 36))
            {
                switch (card)
                {
                    case 1: ErrorMessageString("Advance To Go, Collect £200"); player.SetPosition(0); CheckPassGo(ref player); break;
                    case 2: ErrorMessageString("Advance To Trafalgar Square, If pass go, collect £200"); player.SetPosition(24); CheckPassGo(ref player); break;
                    case 3: ErrorMessageString("Advance To Mayfair, If pass go, collect £200"); player.SetPosition(39); CheckPassGo(ref player); break;
                    case 4: ErrorMessageString("Go To Jail! Do Not Pass Go! Do Not Collect £200"); player.SetPosition(30); break;
                    case 5: ErrorMessageString("Bank Pays You Dividend of £50"); player.IncreaseMoney(50); break;
                    case 6: ErrorMessageString("Pay School Fees of £150"); player.decreaseMoney(150); break;
                    case 7: ErrorMessageString("Speeding Fine £15"); player.decreaseMoney(15); break;
                    case 8: ErrorMessageString("You Have Won A Crossword Competition. Collect £100"); player.IncreaseMoney(100); break;
                    case 9: ErrorMessageString("Your Building And Loan Matures. Collect £150"); player.IncreaseMoney(150); break;
                    case 10: ErrorMessageString("Drunk In Charge. Fine £20"); player.decreaseMoney(20); break;
                    case 11: ErrorMessageString("Take a Trip to Marylebone Station. If pass go, Collect £200"); player.SetPosition(15); CheckPassGo(ref player); break;
                    case 12: ErrorMessageString("Advance To Pall Mall. If pass go, collect £200"); player.SetPosition(11); CheckPassGo(ref player); break;
                    case 13: ErrorMessageString("Jail Out Of Free"); player.SetJailFree(); break;
                }
            }
            //Community Chest
            if ((player.GetPosition() == 2) || (player.GetPosition() == 17) || (player.GetPosition() == 33))
            {
                switch (card)
                {
                    case 1: ErrorMessageString("Income Tax refund. Collect £20"); player.IncreaseMoney(20); break;
                    case 2: ErrorMessageString("From Sale Of Stock. Collect £50"); player.IncreaseMoney(50); break;
                    case 3: ErrorMessageString("Its Your Birthday, Collect £150"); player.IncreaseMoney(150); break;
                    case 4: ErrorMessageString("Receive Interest on 7% Preference Shares. Collect £25"); player.IncreaseMoney(25); break;
                    case 5: ErrorMessageString("Pay Hospital £100"); player.IncreaseMoney(100); break;
                    case 6: ErrorMessageString("Advance To Go, Collect £200"); player.SetPosition(0); CheckPassGo(ref player); break;
                    case 7: ErrorMessageString("You Inherit £100"); player.IncreaseMoney(100); break;
                    case 8: ErrorMessageString("Go To Jail!Do Not Pass Go!Do Not Collect £200"); player.SetPosition(30); break;
                    case 9: ErrorMessageString("Doctors Fee, Pay £50"); player.decreaseMoney(50); break;
                    case 10: ErrorMessageString("Annuity Mature. Collect £100"); player.IncreaseMoney(100); break;
                    case 11: ErrorMessageString("Pay your Insurance Premium £50"); player.decreaseMoney(50); break;
                    case 12: ErrorMessageString("Bank Error in your Favour.Collect £200"); player.IncreaseMoney(200); break;
                    case 13: ErrorMessageString("Jail Out Of Free"); if (player.GetJailFree() == false) { player.SetJailFree(); } break;
                }
            }
            Session["board"] = board;
        }
        private void CheckTax(Player player)
        {
            if (player.GetPosition() == 4)
            {
                ErrorMessageString("Pay Income Tax £200");
                player.decreaseMoney(200);
            }
            if (player.GetPosition() == 38)
            {
                ErrorMessageString("Pay Super Tax £100");
                player.decreaseMoney(100);
            }
        }
        private void CheckGoToJail(ref Player player)
        {
            if (player.GetPosition() == 30)
            {
                player.SetPosition(10);
                if (player.IsInJail() != true)
                    player.SetIsInJail();
            }
        }
        private void CheckPassGo(ref Player player)
        {
            if ((player.GetOriginalPosition() == 2) && (player.GetPosition() < 2))
            {
                player.IncreaseMoney(200);
            }
            else if ((player.GetOriginalPosition() == 7) && (player.GetPosition() < 7))
            {
                player.IncreaseMoney(200);
            }
            else if ((player.GetOriginalPosition() == 17) && (player.GetPosition() < 17))
            {
                player.IncreaseMoney(200);
            }
            else if ((player.GetOriginalPosition() == 22) && (player.GetPosition() < 22))
            {
                player.IncreaseMoney(200);
            }
            else if ((player.GetOriginalPosition() == 33) && (player.GetPosition() < 33))
            {
                player.IncreaseMoney(200);
            }
            else if ((player.GetOriginalPosition() == 36) && (player.GetPosition() < 36))
            {
                player.IncreaseMoney(200);
            }
            else if ((player.GetOriginalPosition() < 39) && (player.GetOriginalPosition() > 28) && (player.GetPosition() >= 0) && (player.GetPosition() <= 27))
            {
                player.IncreaseMoney(200);
            }
        }

        private void BuyProperty(ref Player player)
        {
            Board board = (Board)Session["board"];
            int turn = (int)Session["turn"];

            List<bool> TileBought = new List<bool>();
            List<int> TileCost = new List<int>();
            List<int> TileOwner = new List<int>();

            TileBought = board.TilesBoughts();
            TileCost = board.TilesCosts();
            TileOwner = board.TilesOwners();
            if (Image42.Attributes["src"] == Image41.Attributes["src"])
            {
                if (player.GetRolled() == false)
                {
                    if (TileBought[player.GetPosition()] == false)
                    {
                        if (player.GetMoney() > TileCost[player.GetPosition()])
                        {
                            player.decreaseMoney(TileCost[player.GetPosition()]);
                            player.SetPropertiesOwned();
                            board.updateTileBought(player.GetPosition());
                            switch (turn)
                            {
                                case 1: board.UpdateOwner(player.GetPosition(), 1); break;
                                case 2: board.UpdateOwner(player.GetPosition(), 2); break;
                                case 3: board.UpdateOwner(player.GetPosition(), 3); break;
                                case 4: board.UpdateOwner(player.GetPosition(), 4); break;
                            }
                        }
                        else
                        {
                            ErrorMessageString("You Don't Have Enough Money For This!");
                        }
                    }
                    else
                    {
                        ErrorMessageString("The Tile Is Already Owned!");
                    }
                }
                else
                {
                    ErrorMessage(4);
                }
            }
            else
            {
                if (player.GetRolled() == true)
                {
                    if (TileBought[player.GetPosition()] == false)
                    {
                        if (player.GetMoney() > TileCost[player.GetPosition()])
                        {
                            player.decreaseMoney(TileCost[player.GetPosition()]);
                            player.SetPropertiesOwned();
                            board.updateTileBought(player.GetPosition());
                            switch (turn)
                            {
                                case 1: board.UpdateOwner(player.GetPosition(), 1); break;
                                case 2: board.UpdateOwner(player.GetPosition(), 2); break;
                                case 3: board.UpdateOwner(player.GetPosition(), 3); break;
                                case 4: board.UpdateOwner(player.GetPosition(), 4); break;
                            }
                        }
                        else
                        {
                            ErrorMessageString("You Don't Have Enough Money For This!");
                        }
                    }
                    else
                    {
                        ErrorMessageString("The Tile Is Already Owned!");
                    }
                }
                else
                {
                    ErrorMessage(4);
                }
            }
            Session["board"] = board;
        }
        private void PayRent(ref Player player)
        {
            Board board = (Board)Session["board"];
            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];

            List<int> TileOwner = new List<int>();
            List<int> propertyRentCost = new List<int>();
            TileOwner = board.TilesOwners();
            propertyRentCost = board.PropertysRentsCosts();

            int rent, playerToPay, pos;
            pos = player.GetPosition();
            if (TileOwner[pos] != 0)
            {
                rent = propertyRentCost[pos];
                playerToPay = TileOwner[pos];

                switch (playerToPay)
                {
                    case 1: player.decreaseMoney(rent); p1.IncreaseMoney(rent); break;
                    case 2: player.decreaseMoney(rent); p2.IncreaseMoney(rent); break;
                    case 3: player.decreaseMoney(rent); p3.IncreaseMoney(rent); break;
                    case 4: player.decreaseMoney(rent); p4.IncreaseMoney(rent); break;
                }
            }

            Session["board"] = board;
            Session["p1"] = p1;
            Session["p2"] = p2;
            Session["p3"] = p3;
            Session["p4"] = p4;
        }

        private void EndGameValidation()
        {
            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];

            if ((p1.GetBankRupt() == false) && (p2.GetBankRupt() == true) &&(p3.GetBankRupt() == true) && (p4.GetBankRupt() == true))
            {
                ErrorMessageString("P1 Is The Winner!");                
                Response.Redirect("EndGame.aspx");
                
            }
            else
            {
                if ((p1.GetBankRupt() == true) && (p2.GetBankRupt() == false) && (p3.GetBankRupt() == true) && (p4.GetBankRupt() == true))
                {
                    ErrorMessageString("P2 Is The Winner!");
                    Response.Redirect("EndGame.aspx");
                }
                else
                {
                    if ((p1.GetBankRupt() == true) && (p2.GetBankRupt() == true) && (p3.GetBankRupt() == false) && (p4.GetBankRupt() == true))
                    {
                        ErrorMessageString("P3 Is The Winner!");
                        Response.Redirect("EndGame.aspx");
                    }
                    else
                    {
                        if ((p1.GetBankRupt() == true) && (p2.GetBankRupt() == true) && (p3.GetBankRupt() == true) && (p4.GetBankRupt() == false))
                        {
                            ErrorMessageString("P4 Is The Winner!");
                            Response.Redirect("EndGame.aspx");
                        }
                    }
                }
            }
        }

        private void CreateDatabase(Player p, int person)
        {
            SQLDatabase.DatabaseTable GameInfo = (SQLDatabase.DatabaseTable)Session["gameinfo"];
            SQLDatabase.DatabaseRow new_row = GameInfo.NewRow();

            new_row["Player"] = person.ToString();
            new_row["CurrentMoney"] = p.GetMoney().ToString();
            new_row["Position"] = p.GetPosition().ToString();
            new_row["PropertiesOwned"] = p.GetPropertiesOwned().ToString();
            new_row["IsInJail"] = p.IsInJail().ToString();
            new_row["JailOutOfFree"] = p.GetJailFree().ToString();
            new_row["Rolled"] = p.GetRolled().ToString();
            new_row["BankRupt"] = p.GetBankRupt().ToString();

            GameInfo.Insert(new_row);

            Session["gameinfo"] = GameInfo;
        }
        private void UpdateDataBase(Player p, ref int tempPlayer)
        {
            Board board = (Board)Session["board"];
            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];
            int turn = (int)Session["turn"];
            
            SQLDatabase.DatabaseTable GameInfo = (SQLDatabase.DatabaseTable)Session["GameInfo"];
            SQLDatabase.DatabaseRow row = GameInfo.GetRow(tempPlayer);
            
            row["CurrentMoney"] = p.GetMoney().ToString();
            row["Position"] = p.GetPosition().ToString();
            row["PropertiesOwned"] = p.GetPropertiesOwned().ToString();
            row["IsInJail"] = p.IsInJail().ToString();
            row["JailOutOfFree"] = p.GetJailFree().ToString();
            row["Rolled"] = p.GetRolled().ToString();
            row["BankRupt"] = p.GetBankRupt().ToString();

            GameInfo.Update(row);

            if (tempPlayer != 5)
                tempPlayer++;
            else
                tempPlayer = 0;
            Session["GameInfo"] = GameInfo; 
        }

        private void DisplayDatabase()
        {
            SQLDatabase.DatabaseTable GameInfo = (SQLDatabase.DatabaseTable)Session["GameInfo"];

            // Generate rows and cells.           
            int numrows = 5;
            int numcells = 8;

            for (int j = 0; j < numrows; j++)
            { 
                TableRow r = new TableRow();

                if (j == 0)
                {
                    SetTableHeadings(ref r);   
                }
                else
                {
                    for (int i = 0; i < numcells; i++)
                    {   //-1 to cover table headings which doesn't apply in database. +1 to cover db starting from 1
                        TableCell c = new TableCell();
                        c.Controls.Add(new LiteralControl(GameInfo.GetRow(j -1)[GetGameInfo(i + 1)].ToString()));
                        r.Cells.Add(c);

                    }
                }
                Table1.Rows.Add(r);
            }
            Session["GameInfo"] = GameInfo;
        }

        private string GetGameInfo(int choice)
        {
            string cellInfo = "";
            switch (choice)
            {
                case 1: cellInfo = "Player"; break;
                case 2: cellInfo = "CurrentMoney"; break;
                case 3: cellInfo = "Position"; break;
                case 4: cellInfo = "PropertiesOwned"; break;
                case 5: cellInfo = "IsInJail"; break;
                case 6: cellInfo = "JailOutOfFree"; break;
                case 7: cellInfo = "Rolled"; break;
                case 8: cellInfo = "BankRupt"; break;
            }
            return cellInfo;
        }

        private void SetTableHeadings(ref TableRow r)
        {
            TableCell h1 = new TableCell();
            TableCell h2 = new TableCell();
            TableCell h3 = new TableCell();
            TableCell h4 = new TableCell();
            TableCell h5 = new TableCell();
            TableCell h6 = new TableCell();
            TableCell h7 = new TableCell();
            TableCell h8 = new TableCell();


            h1.Controls.Add(new LiteralControl("Player"));
            h2.Controls.Add(new LiteralControl("Current Money"));
            h3.Controls.Add(new LiteralControl("Position"));
            h4.Controls.Add(new LiteralControl("Properties Owned"));
            h5.Controls.Add(new LiteralControl("Is In Jail?"));
            h6.Controls.Add(new LiteralControl("Has Jail Out Of Free?"));
            h7.Controls.Add(new LiteralControl("Has Rolled This Turn?"));
            h8.Controls.Add(new LiteralControl("Is Bankrupt?"));

            r.Cells.Add(h1);
            r.Cells.Add(h2);
            r.Cells.Add(h3);
            r.Cells.Add(h4);
            r.Cells.Add(h5);
            r.Cells.Add(h6);
            r.Cells.Add(h7);
            r.Cells.Add(h8);
        }

        //Movement - Positions
        private void PlayerLocation(int playerPosition, int players)
        {
            Board board = (Board)Session["board"];
            Player p1 = (Player)Session["p1"];
            Player p2 = (Player)Session["p2"];
            Player p3 = (Player)Session["p3"];
            Player p4 = (Player)Session["p4"];

            int tempspacetop = board.GetLocationTop(playerPosition);
            int tempspaceleft = board.GetLocationLeft(playerPosition);

            switch (players)
            {
                case 1:
                    if ((playerPosition == 10) && (p1.IsInJail() == true))
                    {
                        tempspaceleft += 30;
                    }
                    P1Character.Style["top"] = tempspacetop + "px";
                    P1Character.Style["left"] = tempspaceleft + "px";
                    break;
                case 2:
                    if ((playerPosition == 0) || (playerPosition == 20))
                    {
                        tempspacetop += 40;
                    }
                    else
                    {
                        if ((playerPosition == 10) && (p2.IsInJail() == true))
                        {
                            tempspacetop += 24;
                            tempspaceleft += 30;
                        }
                        else
                        {
                            tempspacetop += 24;
                        }
                    }
                    P2Character.Style["top"] = tempspacetop + "px";
                    P2Character.Style["left"] = tempspaceleft + "px";
                    break;
                case 3:
                    if ((playerPosition == 0) || (playerPosition == 20))
                    {
                        tempspaceleft += 40;
                    }
                    else
                    {
                        if ((playerPosition == 10) && (p3.IsInJail() == true))
                        {
                            tempspaceleft += 54;
                        }
                        else
                        {
                            if (playerPosition == 10)
                            {
                                tempspacetop += 48;
                            }
                            else
                            {
                                tempspaceleft += 24;
                            }
                        }
                    }
                    P3Character.Style["top"] = tempspacetop + "px";
                    P3Character.Style["left"] = tempspaceleft + "px";
                    break;
                case 4:
                    if ((playerPosition == 0) || (playerPosition == 20))
                    {
                        tempspacetop += 40;
                        tempspaceleft += 40;
                    }
                    else
                    {
                        if ((playerPosition == 10) && (p4.IsInJail() == true))
                        {
                            tempspaceleft += 54;
                            tempspacetop += 24;
                        }
                        else
                        {
                            if (playerPosition == 10)
                            {
                                tempspacetop += 55;
                                tempspaceleft += 24;
                            }
                            else
                            {
                                tempspacetop += 24;
                                tempspaceleft += 24;
                            }
                        }
                    }
                    P4Character.Style["top"] = tempspacetop + "px";
                    P4Character.Style["left"] = tempspaceleft + "px";
                    break;
            }
            Session["board"] = board;
            Session["p1"] = p1;
            Session["p2"] = p2;
            Session["p3"] = p3;
            Session["p4"] = p4;
        }
    }
}
