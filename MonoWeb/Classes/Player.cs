using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoWeb
{
    public class Player
    {
        private int currentMoney;                     //Used to keep track of a players money
        private int propertiesOwned;                  //Used to count how many properties a player owns
        private int playerPosition;                   //Players location Used in different functions to change different values

        private int originalPosition;                         //used to check pass go        

        private bool rolled;                                  //used to prevent rolling and allow doubles to roll again
        private bool bankRupt;                                //used to end game
        private bool jailOutOfFree;                           //used to define if can get in or out of jail for free
        private bool InJail;                                  //used to set if someone is in jail or not (Set as false in board set up)

        public Player()
        {
            currentMoney = 1500;
            propertiesOwned = 0;
            playerPosition = 0;
            rolled = false;
            bankRupt = false;
            jailOutOfFree = false;
            InJail = false;
        }

        public void UpdateplayerPosition(int position)
        {
            originalPosition = playerPosition;
            playerPosition = position;
        }

        public int GetPosition() { return playerPosition; }
        public void SetPosition(int newpos)
        {
            playerPosition = newpos;
        }
        public int GetOriginalPosition() { return originalPosition; }
        public void SetOriginalPosition() { originalPosition = playerPosition; }

        public bool GetJailFree() { return jailOutOfFree; }
        public void SetJailFree() { jailOutOfFree ^= true; }
        public void SetJailFreeContinue(bool jailfree) { jailOutOfFree = jailfree; }

        public void UpdatePropertiesOwned() { propertiesOwned++; }

        public bool GetRolled() { return rolled; }
        public void setRolled() { rolled ^= true; }
        public void setRolledContinue(bool rolled) { this.rolled = rolled; }


        public int GetMoney() { return currentMoney; }
        public void IncreaseMoney(int amount) { currentMoney = currentMoney + amount; }
        public void decreaseMoney(int amount) { currentMoney = currentMoney - amount; }
        public void setMoney (int amount) { currentMoney = amount; }

        public int GetPropertiesOwned() { return propertiesOwned; }
        public void SetPropertiesOwned() { propertiesOwned++; }
        public void PropertiesOwned(int properties) { propertiesOwned = properties; }

        public bool IsInJail() { return InJail; }
        public void SetIsInJail() { InJail ^= true; }
        public void IsInJailContinue(bool jail) { InJail = jail; }

        public bool GetBankRupt() { return bankRupt; }
        public void SetBankRupt() { bankRupt ^= true; }
        public void SetBankRuptContinue(bool rupt) { bankRupt = rupt; }
    }
}