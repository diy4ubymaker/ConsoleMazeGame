using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject
{
    using DIY4UMazeGame.GameEngine.GameInfo.Roles;
    using DIY4UMazeGame.GameEngine.GameInfo.Items;
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.Utilities;

    public class HealthPotion : Item, IStationary, IPrintable, ICarryable, IDropable, ITakable, IGiveLife,
         IAvailable
    {
        static int instanceNo = 0;

        public int lifeLine { get; set; } = 0;
        private int max_lifeline = 10;
        private int min_lifeline = 8;

        public int col { get; set; } = 0;
        public int row { get; set; } = 0;
        public char IconChar { get; set; } = 'G';
        public ConsoleColor ItemColor { get; set; } = ConfigManager.HealthPotionColor;
        
        //Construtor
        public HealthPotion(string itemname_in, string descriptions_in, int col_in, int row_in, string key)
        {
            itemname = itemname_in;
            descriptions = descriptions_in;
            col = col_in;
            row = row_in;
            itemname = itemname + "-" + instanceNo;
            instanceNo++;
            InstanceKey = key;

            //Price = 0;
            lifeLine = ConfigManager.RandomGen.Next(min_lifeline, max_lifeline);
            itemtype = ITEM_TYPE.Health_Potion;
            IconChar = ConfigManager.HealthPotionIcon;

        }

        public HealthPotion(string descriptions_in, int col_in, int row_in, string key)
        {
            itemname = "Health Potion";
            descriptions = descriptions_in;
            col = col_in;
            row = row_in;
            itemname = itemname + "-" + instanceNo;
            instanceNo++;
            InstanceKey = key;

            //Price = 0;
            itemtype = ITEM_TYPE.Health_Potion;
            IconChar = ConfigManager.HealthPotionIcon;
            lifeLine = ConfigManager.RandomGen.Next(min_lifeline, max_lifeline);
        }

        public Point GetLocation()
        {
            return new Point(col, row);
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

        public void Take()
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = Console.BackgroundColor;
            Console.Write(" ");
        }

        public void Carry()
        {
            throw new NotImplementedException();
        }

        public void Drop()
        {
            throw new NotImplementedException();
        }

        //*********************************************************************
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public bool isAvailable { get; set; } = true; //Default is true
        private Thread This_thread = null;
        private int waittime = 100;  // ms
        private bool hasStaretd = false;

        public void SetNotAvailable(int ms)
        {
            if (!hasStaretd)
            {
                waittime = ms;
                This_thread = new Thread(new ThreadStart(Engine));
                hasStaretd = true;
                This_thread.Start();
            }
        }

        private void Engine()
        {
            while (true)
            {
                isAvailable = false;
              
                Utility.Wait(waittime);
                isAvailable = true;
                hasStaretd = false;
              
                break;
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //********************************************************************


    }
}
