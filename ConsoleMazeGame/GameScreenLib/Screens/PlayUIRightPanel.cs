using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DIY4UMazeGame.GameScreenLib.Screens
{
    using DIY4UMazeGame.GameScreenLib.Screens.Elements;
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.GameEngine.GameInfo;
    using DIY4UMazeGame.GameEngine;
    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;
    using DIY4UMazeGame.GameEngine.GameInfo.Roles;


    class PlayUIRightPanel
    {

        public static Rectangle BorderLine { get; set; } = null;

        public static int UIPosCol { get; set; } = 0;
        public static int UIPosRow { get; set; } = 0;
        public static int PanelWidth = 0;
        public static int PanelHeight = 0;
        public static TextBox TitleLabel { get; set; } = null;

        public static TextBox HealthLevelTitleLabel { get; set; } = null;
        public static TextBox EnemyTitleLabel { get; set; } = null;

        public static PercentageBar HealthBar { get; set; } = null;
        public static PercentageBarEx1 EnemyHealthBar { get; set; } = null;


        public static ConsoleColor BorderColor = ConsoleColor.Yellow;

        private static string[] LegendList=
        {
            " - Gold",
            " - Silver",
            " - Wooden Sword",
            " - Iron Sword",
            " - Wooden Armour",
            " - Iron Armour",
            " - Treasure Chest",
            " - Key ",
            " - Health Potion",
            " - Monster",
            " - Globin",
            " - YOU"
        };


        public static int CLEAR = 0;
        public static int GOLD_ITEM = 1;
        public static int SILVER_ITEM = GOLD_ITEM << 1;
        public static int WOODEN_SWORD = GOLD_ITEM << 2;
        public static int IRON_SWORD = GOLD_ITEM << 3;
        public static int WOODEN_ARMOUR = GOLD_ITEM << 4;
        public static int IRON_ARMOUR = GOLD_ITEM << 5;
        public static int CHEST = GOLD_ITEM << 6;
        public static int CHEST_KEY = GOLD_ITEM << 7;
        public static int HEALTH_POTION = GOLD_ITEM << 8;
        public static int MONSTER = GOLD_ITEM << 9;
        public static int GLOBIN = GOLD_ITEM << 10;
        public static int PLAYER = GOLD_ITEM << 11;

        public static MenuTextBox menutxtBox { get; set; } = null;
        public static List<string> menuOptions { get; set; } = new List<String>(
        new String[]
        {
             "12345678901234567890123456",
             "12345678901234567890123456",
             "12345678901234567890123456",
             "12345678901234567890123456",
             "12345678901234567890123456",
             "12345678901234567890123456",
             "12345678901234567890123456",
             "12345678901234567890123456",
             "12345678901234567890123456",
             "12345678901234567890123456",
             "AAAAAAAAAAAAAAAAAAAAAAAAAA"

        });

        private PlayUIRightPanel() {}

        //public static void PaintLegendOption(int roomnumber) { }

        
        public static void PaintLegendOption(int roomnumber)
        {
            int finalval = 0;
            Object item;
            GameRoom gm;
            Dictionary<string, object> Includedlist = new Dictionary<string, object>();
            
            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);

            foreach (KeyValuePair<string, object> Pair in gm.ItemsHashTable)
            {
                item = Pair.Value;
                if (item is Item && !Includedlist.ContainsKey(Convert.ToString((int)((Item)item).itemtype)))
                {
                    finalval = finalval ^ (int)((Item)item).itemtype;
                    Includedlist.Add(Convert.ToString((int)((Item)item).itemtype), item);

                }
            }

            //MonsterList
            foreach (Monster obj in gm.MonsterList)
            {
                if (obj is Enemy)
                {
                    finalval = finalval ^ (int)((Enemy)obj).EnemyType;
                    break;
                }
            }

            foreach (Globin obj in gm.GlobinList)
            {
                if (obj is Enemy)
                {
                    finalval = finalval ^ (int)((Enemy)obj).EnemyType;
                    break;
                }

            }

            finalval = finalval ^ PLAYER;

            UpdateLegendOption(finalval);

        }
        

        public static void UpdateLegendOption(int val)
        {
            List<string> menuOptions = new List<String>();
            List<ConsoleColor> colorOptions = new List<ConsoleColor>();
            List<char> iconOptions = new List<char>();

            int bitval = 1;
            int finalval = 0;

            for (int i = 0; i < LegendList.Length; i++)
            {
                finalval = (bitval << i) & val;
                if (finalval != 0)
                {
                    menuOptions.Add(LegendList[i]);
                    iconOptions.Add(ConfigManager.IconList[finalval]);
                    colorOptions.Add(ConfigManager.IconColorList[finalval]);
                }
            }

            menutxtBox.lineItem = menuOptions;
            menutxtBox.drawwithColorOption(iconOptions,colorOptions);
        }

        private static void DrawScreen()
        {
            //lock (ScreenManager.LockSection)
            //{
                BorderLine.draw();
                TitleLabel.draw();
                menutxtBox.draw();

                HealthLevelTitleLabel.draw();
                EnemyTitleLabel.draw();

                HealthBar.draw();
                EnemyHealthBar.draw();
            //}
        }

       
        public static void UpdateEnemyTitle(string val)
        {
            EnemyTitleLabel.textItem = val;
            EnemyTitleLabel.draw();
        }

        public static void UpdateHealthLevel(int level)
        {
            HealthBar.Percent = level;
            HealthBar.draw();
        }

        public static void UpdateEnemyHealthLevel(int level)
        {
            EnemyHealthBar.Percent = level;
            EnemyHealthBar.draw();
        }

        /*
        public static void UpdateArmourLevel(int level)
        {
            ArmourBar.Percent = level;
            ArmourBar.draw();
        }
        */

        public static void Init()
        {
          
            PanelWidth = (ScreenManager.screenWidth - ScreenManager.RoomMapWindowsWidth) / 2 - 1;
            PanelHeight = ScreenManager.MapWidowHeight;

            BorderLine = new Rectangle();
            BorderLine.BorderType = Shapes.BORDER_TYPE.Single;
            BorderLine.Create(
                new Point(
                    ((ScreenManager.screenWidth - ScreenManager.RoomMapWindowsWidth) / 2) + ScreenManager.RoomMapWindowsWidth + 1,
                ScreenManager.HUDHeight + 2),
                new Size(
                (ScreenManager.screenWidth - ScreenManager.RoomMapWindowsWidth) / 2 - 1,
                ScreenManager.MapWidowHeight),
                         BorderColor, ConsoleColor.Black);

            UIPosCol = ((ScreenManager.screenWidth - ScreenManager.RoomMapWindowsWidth) / 2) + ScreenManager.RoomMapWindowsWidth + 1;
            UIPosRow = ScreenManager.HUDHeight + 2;

            //Top Title
            TitleLabel = new TextBox();
            TitleLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 1),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.Yellow,
                ConsoleColor.Black);
            TitleLabel.alighnment = TextBox.ALIGN_ENUM.Left_Justify;
            TitleLabel.textItem = "Legend";

            //HealthLevelTitleLabel
            HealthLevelTitleLabel = new TextBox();
            HealthLevelTitleLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 15),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.Yellow,
                ConsoleColor.Black);
            HealthLevelTitleLabel.alighnment = TextBox.ALIGN_ENUM.Left_Justify;
            HealthLevelTitleLabel.textItem = "Health Level";

            //HealthBar
            
            HealthBar = new PercentageBar();
            HealthBar.Create(
              new Point(UIPosCol + 2, UIPosRow + 17),
              new Size(PanelWidth-4, 1),
              ConsoleColor.White,
              ConsoleColor.Black);
            HealthBar.BarWidth = 20;
            HealthBar.Percent = 100;


            //EnemyTitleLabel

            EnemyTitleLabel = new TextBox();
            EnemyTitleLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 19),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.Yellow,
                ConsoleColor.Black);
            EnemyTitleLabel.alighnment = TextBox.ALIGN_ENUM.Left_Justify;
            EnemyTitleLabel.textItem = "";


            //EnemyHealthBar

            EnemyHealthBar = new PercentageBarEx1();
            EnemyHealthBar.Create(
              new Point(UIPosCol + 2, UIPosRow + 21),
              new Size(PanelWidth - 4, 1),
              ConsoleColor.White,
              ConsoleColor.Black);
            EnemyHealthBar.BarWidth = 20;
            EnemyHealthBar.Percent = 0;
            
          
            /* Text Box for the Options Menu */
            menutxtBox = new MenuTextBox();
            menutxtBox.Create(
            new Point(UIPosCol + 2, UIPosRow + 3),
            new Size(PanelWidth-4, 11),
            ConsoleColor.White, ConsoleColor.Black);
            menutxtBox.lineItem = menuOptions;

        }

        public static void Update()
        {
            DrawScreen();
        }

        public static void Exit()
        {

        }
    }
}
