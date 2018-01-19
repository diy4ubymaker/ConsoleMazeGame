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

    public class Chest : Item, IStationary, IPrintable, ICarryable, IDropable, ITakable,
        IAvailable
    {

        static int instanceNo = 0;

        public int col { get; set; } = 0;
        public int row { get; set; } = 0;
        public char IconChar { get; set; } = 'C';
        public ConsoleColor ItemColor { get; set; } = ConfigManager.ChestColor;

        public ItemList Goldlist { get; set; } = new ItemList();
        public ItemList Silverlist { get; set; } = new ItemList();
        public ItemList HealthPotionlist { get; set; } = new ItemList();

        private int MAX_GOLD = 4;
        private int MAX_SILVER = 3;
        private int MAX_POTION = 1;



        //Construtor
        public Chest(string itemname_in, string descriptions_in, int col_in, int row_in, string key)
        {
            Gold goldobj = null;
            Silver silverobj = null;
            HealthPotion healthpotionobj = null;

            itemname = itemname_in;
            descriptions = descriptions_in;
            col = col_in;
            row = row_in;
            itemname = itemname + "-" + instanceNo;
            instanceNo++;
            InstanceKey = key;

            itemtype = ITEM_TYPE.Chest;
            IconChar = ConfigManager.ChestIcon;

            for (int i = 0; i < ConfigManager.RandomGen.Next(1, MAX_GOLD); i++)
            {
                goldobj = new Gold("Gold Bar", 0, 0, "0-0");
                Goldlist.Add(goldobj);
            }

            for (int i = 0; i < ConfigManager.RandomGen.Next(1, MAX_SILVER); i++)
            {
                silverobj = new Silver("Silver Coin", 0, 0, "0-0");
                Silverlist.Add(silverobj);
            }

            for (int i = 0; i < ConfigManager.RandomGen.Next(1, MAX_POTION); i++)
            {
                healthpotionobj = new HealthPotion("Health Potion", 0, 0, "0-0");
                HealthPotionlist.Add(healthpotionobj);
            }
        }

        public Chest(string descriptions_in, int col_in, int row_in, string key)
        {
            Gold goldobj = null;
            Silver silverobj = null;
            HealthPotion healthpotionobj = null;

            itemname = "Chest";
            descriptions = descriptions_in;
            col = col_in;
            row = row_in;
            itemname = itemname + "-" + instanceNo;
            instanceNo++;
            InstanceKey = key;

            itemtype = ITEM_TYPE.Chest;
            IconChar = ConfigManager.ChestIcon;

            for (int i = 0; i < ConfigManager.RandomGen.Next(1, MAX_GOLD); i++)
            {
                goldobj = new Gold("Gold Bar", 0, 0, "0-0");
                Goldlist.Add(goldobj);
            }

            for (int i = 0; i < ConfigManager.RandomGen.Next(1, MAX_SILVER); i++)
            {
                silverobj = new Silver("Silver Coin", 0, 0, "0-0");
                Silverlist.Add(silverobj);
            }

            for (int i = 0; i < ConfigManager.RandomGen.Next(1, MAX_POTION); i++)
            {
                healthpotionobj = new HealthPotion("Health Potion", 0, 0, "0-0");
                HealthPotionlist.Add(healthpotionobj);
            }
        }

        public Point GetLocation()
        {
            return new Point(col, row);
        }

        public void SetLocation(int col_in, int row_in)
        {
            throw new NotImplementedException();
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

        public void Carry()
        {
            throw new NotImplementedException();
        }

        public void Drop()
        {
            throw new NotImplementedException();
        }

        public void Take()
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = Console.BackgroundColor;
            Console.Write(" ");
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
