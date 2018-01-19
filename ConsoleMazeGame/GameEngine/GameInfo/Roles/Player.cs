using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DIY4UMazeGame.GameEngine.GameInfo.Roles
{
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.GameEngine.GameInfo.Items;

    public class Player : ILivingOrganism, IMovable, IPrintable
    {
        public int col { get; set; } = 10;   //Physical Postion on the screen.
        public int row { get; set; } =  15;  //Physical Postion on the screen.

        public char IconChar { get; set; } = '■';

        public int HealthLevel { get; set; } = 50;
        public int NoOfLife { get; set; } = 1;

        public ConsoleColor ItemColor { get; set; } = ConsoleColor.Green;

        public void getHit(int val)
        {
            //Check Armour
            if(((IShield)GameState.GetFirstArmourInInventory()).shieldPower>0)
            {
                

                ((IShield)GameState.GetFirstArmourInInventory()).shieldPower =
                    ((IShield)GameState.GetFirstArmourInInventory()).shieldPower - val + 
                    ((IShield)GameState.GetFirstArmourInInventory()).shieldLevel;
                
                if (((IShield)GameState.GetFirstArmourInInventory()).shieldPower < 0)
                    ((IShield)GameState.GetFirstArmourInInventory()).shieldPower = 0;
            }
            else
            {
               

                if (HealthLevel >= 0)
                    HealthLevel--;

                if (HealthLevel < 0)
                    HealthLevel = 0;
            }
        }

        private void PaintScreen(
            int col_prev, int row_pre, 
            int col_new, int row_new)
        {
            lock (ScreenManager.LockSection)
            {
                Console.ForegroundColor = ItemColor;
                Console.SetCursorPosition(col_prev, row_pre);
                Console.Write(' ');
                Console.SetCursorPosition(col_new, row_new);
                Console.Write(IconChar);
            }
        }

        public Player(int col_in, int row_in)
        {
            col = col_in;
            row = row_in;
            IconChar = ConfigManager.PlayerIcon;
        }

        public void SetLocation(int col_in, int row_in)
        {
            col = col_in;
            row = row_in;
        }

        public void Update()
        {
            lock (ScreenManager.LockSection)
            {
                Console.SetCursorPosition(col, row);
                Console.ForegroundColor = ItemColor;
                Console.Write(IconChar);
            }
        }

        public void MoveNorth()
        {
            PaintScreen(col, row, col, --row);
        }

        public void MoveSouth()
        {
             PaintScreen(col, row, col, ++row);
        }

        public void MoveEast()
        {
             PaintScreen(col, row, ++col, row);
        }

        public void MoveWest()
        {
               PaintScreen(col, row, --col,row);
        }

        public int GetHealthLevel()
        {
            return HealthLevel;
        }

        public void SetHealthLevel(int healthValue)
        {
            if(HealthLevel >= 0 && HealthLevel <= 100)
                HealthLevel = healthValue;
        }

        public void IncreaseHealthLevel()
        {
            if (HealthLevel < 100)
                HealthLevel++;
        }

        public void  DecreaseHealthLevel()
        {
            if (HealthLevel != 0)
                HealthLevel--;
        }

        public int GetNoOfLife()
        {
            return NoOfLife;
        }

        public void IncreaseNoOfLife()
        {
            NoOfLife++;
        }

        public void DecreaseNoOfLife()
        {
            NoOfLife--;
        }

        public void SetNoOfLife(int noOfLife)
        {
            NoOfLife = noOfLife;
        }

        public Point GetLocation()
        {
            return new Point(col,row); 
        }

        private void Engine()
        {
           
        }

        public void WakeUp()
        {
           
        }

        public void Sleep()
        {
            
        }

        public void Kill()
        {
        }

        public bool YouThere(Dictionary<string, string> ItemsHashTable)
        {
            bool iamhere = false;
            if (ItemsHashTable.ContainsKey(col + "-" + row))
            {
                iamhere = true;
              
            }
            return iamhere;
        }


    }
}
