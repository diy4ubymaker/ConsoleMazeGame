#define DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;


namespace DIY4UMazeGame.Managers
{
    using DIY4UMazeGame.Utilities;
    using DIY4UMazeGame.GameScreenLib.Screens;
    using DIY4UMazeGame.GameEngine.GameInfo;
    using DIY4UMazeGame.GameEngine;
    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;
    using DIY4UMazeGame.GameEngine.GameInfo.Roles;
    using DIY4UMazeGame.GameEngine.GameInfo.Items;

    class ScreenManager
    {
        /* Public members */
        public static int screenWidth = Console.WindowWidth;
        public static int screenHeight = Console.WindowHeight;
        public static bool windowFixed = false;
        public static bool cursorOff = false;
        public static int topLeftXPos = 0;
        public static int topLeftYPos = 0;


        public static int HUDHeight = 10;
        public static int MenuHeight = 10;
        public static int MapWidowHeight = 25;
        public static int MapWindowsWidth = 120;
        public static int RoomMapWindowsWidth = 60;
        public static int RoomMapWidowHeight = 25;
        public static int DefaultBorder = 1;
        public static int BufferAreaHeight = 1;
        public static String ScreenTitle = "DIY4U Console Game";

        public static Object LockSection = new Object();

        /* Private members */
        private static bool isInit = false;

        /* Class is privte and hence it cannot be instantiated */
        private ScreenManager()
        { }

        public static void Init()
        {
            if (!isInit)
            {
                screenWidth = Console.WindowWidth;
                screenHeight = Console.WindowHeight;
                Console.SetBufferSize(screenWidth, screenHeight); // Remove SCroll bar

                WindowsAPI.SetWindowsPosition(topLeftXPos, topLeftYPos);
                Console.Title = ScreenTitle;

                isInit = true;

                if (windowFixed)
                    WindowsAPI.DisableResize();

                if (cursorOff)
                {
                    Console.CursorVisible = !cursorOff;
                }
            }
        }

        public static void Shutdown()
        {

        }

        public static void Init(bool windowfixed)
        {
            if (!isInit)
            {
                screenWidth = Console.WindowWidth;
                screenHeight = Console.WindowHeight;
                Console.SetBufferSize(screenWidth, screenHeight); // Remove SCroll bar

                WindowsAPI.SetWindowsPosition(topLeftXPos, topLeftYPos);
                Console.Title = ScreenTitle;

                isInit = true;
            }

            windowFixed = windowfixed;

            if (windowFixed)
                WindowsAPI.DisableResize();

            if (cursorOff)
            {
                Console.CursorVisible = !cursorOff;
            }

        }

        public static void Init(int screenwidth, int screenheight,
            bool windowfixed = false, bool cursoroff = false,
            int topLeftXpos = 0, int topLeftYpos = 0)
        {
            if (!isInit)
            {
                Console.SetWindowSize(screenwidth, screenheight);
                screenWidth = Console.WindowWidth;
                screenHeight = Console.WindowHeight;
                Console.SetBufferSize(screenWidth, screenHeight); // Remove SCroll bar

                topLeftXPos = topLeftXpos;
                topLeftYPos = topLeftYpos;

                WindowsAPI.SetWindowsPosition(topLeftXPos, topLeftYPos);
                Console.Title = ScreenTitle;

                isInit = true;
            }

            windowFixed = windowfixed;
            cursorOff = cursoroff;

            if (windowFixed)
                WindowsAPI.DisableResize();

            if (cursorOff)
            {
                Console.CursorVisible = !cursorOff;
            }

        }

        public static bool IsWindowSizeCorrect()
        {
            if (screenWidth == Console.WindowWidth &&
                screenHeight == Console.WindowHeight &&
                isInit)
                return true;
            else
                return false;
        }

        public static void WaitForBufferClear()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
            }
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static void ResetColor()
        {
            Console.ResetColor();
        }


        public static ConsoleColor GetConsoleColor(string colorname)
        {
            switch (colorname)
            {
                case "DarkBlue":
                    return ConsoleColor.DarkBlue;
                case "DarkGreen":
                    return ConsoleColor.DarkGreen;
                case "DarkCyan":
                    return ConsoleColor.DarkCyan;
                case "DarkRed":
                    return ConsoleColor.DarkRed;
                case "DarkMagenta":
                    return ConsoleColor.DarkMagenta;
                case "DarkYellow":
                    return ConsoleColor.DarkYellow;
                case "Gray":
                    return ConsoleColor.Gray;
                case "DarkGray":
                    return ConsoleColor.DarkGray;
                case "Blue":
                    return ConsoleColor.Blue;
                case "Green":
                    return ConsoleColor.Green;
                case "Cyan":
                    return ConsoleColor.Cyan;
                case "Red":
                    return ConsoleColor.Red;
                case "Magenta":
                    return ConsoleColor.Magenta;
                case "Yellow":
                    return ConsoleColor.Yellow;
                case "White":
                    return ConsoleColor.White;
            }
            return ConsoleColor.White;
        }


        public static void printScreenQuit01()
        {
            //*******************************************************************************
            PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.YES_INSTRUNCTION ^ PlayUIBottomItem.NO_INSTRUNCTION);
            PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
            PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(4) + "\n" + ConfigManager.GetStringResource(5));
            //*******************************************************************************
        }

        public static void printInitScreen()
        {
            GameRoom gm;
            Player player;
            GameWorldInfo gwinfo;
            Item item;

            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);
            gwinfo = GameDataFactory.curr_gwinfo;
            player = GameDataFactory.curr_gwinfo.The_Player;

            //****************************************************************************
            PlayUIBottomItem.UpdateInstruction(
                PlayUIBottomItem.QUIT_INSTRUNCTION
                //PlayUIBottomItem.UP_INSTRUNCTION   ^
                //PlayUIBottomItem.DOWN_INSTRUNCTION   ^
                //PlayUIBottomItem.RIGHT_INSTRUNCTION   ^
                //PlayUIBottomItem.LEFT_INSTRUNCTION 
                );

            PlayUIBottomItem.UpdateMenuOption(
              // PlayUIBottomItem.QUIT_INSTRUNCTION ^
              PlayUIBottomItem.UP_INSTRUNCTION ^
              PlayUIBottomItem.DOWN_INSTRUNCTION ^
              PlayUIBottomItem.RIGHT_INSTRUNCTION ^
              PlayUIBottomItem.LEFT_INSTRUNCTION
              );

            PlayUIBottomItem.updateMessageBox(
                ConfigManager.GetStringResource(2) + "\n" + ConfigManager.GetStringResource(3));


            PlayUITopItem.UpdateGameLevel(GameState.gameLevel);
            PlayUITopItem.UpdateExperienceLevel(GameState.experience);
            PlayUITopItem.UpdateNoOfRoom(gwinfo.NumberOfRooms);

            PlayUITopItem.UpdateHealthLevel(player.GetHealthLevel());

            PlayUITopItem.UpdateSilverLevel(GameState.CountSilverInInventory());
            PlayUITopItem.UpdateGoldLevel(GameState.CountGoldInInventory());
            PlayUITopItem.UpdateHealthPotion(GameState.CountHealthPotionInInventory());

            item = GameState.GetFirstArmourInInventory();
            if (item != null)
            {
                PlayUITopItem.UpdateArmourType(item.descriptions);
            }
            else
            {
                PlayUITopItem.UpdateArmourType("No");

            }

            item = GameState.GetFirstSwordInInventory();
            if (item != null)
            {
                PlayUITopItem.UpdateWeaponType(item.descriptions);
            }
            else
            {
                PlayUITopItem.UpdateWeaponType("No");
            }

            PlayUITopItem.UpdateScore(GameState.score);
            //************************************************************************************
        }

        public static void printInitScreen4Room()
        {
            GameRoom gm;
            Player player;
            GameWorldInfo gwinfo;
            Item item;

            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);
            gwinfo = GameDataFactory.curr_gwinfo;
            player = GameDataFactory.curr_gwinfo.The_Player;

            PlayUILeftPanel.UpdateArmourItem(GameState.CountArmourInInventory());
            PlayUILeftPanel.UpdateSwordItem(GameState.CountSwordInInventory());
            PlayUILeftPanel.UpdateTotalItem(GameState.TotalItemInInventory());
            PlayUILeftPanel.UpdateSilverItem(GameState.CountSilverInInventory());
            PlayUILeftPanel.UpdateGoldItem(GameState.CountGoldInInventory());
            PlayUILeftPanel.UpdateKeyItem(GameState.CounKeyInInventory());
            PlayUILeftPanel.UpdateHealthPotiontem(GameState.CountHealthPotionInInventory());
            PlayUILeftPanel.UpdateTotalEnemyItem(
                gm.GlobinList.Count + gm.MonsterList.Count);
            PlayUILeftPanel.UpdateTotalGlobinWItem(gwinfo.NumberOfGlobin);
            PlayUILeftPanel.UpdateTotalMonsterItem(gwinfo.NumberOfMonster);

            item = GameState.GetFirstArmourInInventory();
            if (item != null && item is IShield)
            {
                PlayUILeftPanel.UpdateArmourTypeVal(item.descriptions);
                PlayUILeftPanel.UpdateArmourPowerLevel(((IShield)item).shieldPower);
            }
            else
            {
                PlayUILeftPanel.UpdateArmourTypeVal("Not Found");
                PlayUILeftPanel.UpdateArmourPowerLevel(0);
            }

            item = GameState.GetFirstSwordInInventory();
            if (item != null && item is IWeapon)
            {
                PlayUILeftPanel.UpdateWeaponTypeVal(item.descriptions);
                PlayUILeftPanel.UpdateWeaponrPowerLevel(
                    Utility.getPercentageVal(((IWeapon)item).capableHit,400));
            }
            else
            {
                PlayUILeftPanel.UpdateWeaponTypeVal("Not Found");
                PlayUILeftPanel.UpdateWeaponrPowerLevel(0);
            }

            PlayUIRightPanel.UpdateHealthLevel(player.HealthLevel);
        }

        public static void printScreenGoldAndSilver()
        {
            GameRoom gm;
            Player player;
            GameWorldInfo gwinfo;
            //Item item;

            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);
            gwinfo = GameDataFactory.curr_gwinfo;
            player = GameDataFactory.curr_gwinfo.The_Player;

            PlayUILeftPanel.UpdateSilverItem(GameState.CountSilverInInventory());
            PlayUILeftPanel.UpdateGoldItem(GameState.CountGoldInInventory());
            PlayUITopItem.UpdateSilverLevel(GameState.CountSilverInInventory());
            PlayUITopItem.UpdateGoldLevel(GameState.CountGoldInInventory());
            PlayUILeftPanel.UpdateHealthPotiontem(GameState.CountHealthPotionInInventory());
            PlayUITopItem.UpdateScore(GameState.score);
            PlayUILeftPanel.UpdateTotalItem(GameState.TotalItemInInventory());

            PlayUITopItem.UpdateHealthPotion(GameState.CountHealthPotionInInventory());
            PlayUIRightPanel.UpdateHealthLevel(player.GetHealthLevel());
            PlayUITopItem.UpdateHealthLevel(player.GetHealthLevel());

        }

        public static void moveOnInstruction()
        {
            PlayUIBottomItem.updateMessageBox(
              ConfigManager.GetStringResource(47) + "\r\n" + ConfigManager.GetStringResource(48));
        }

        public static void moveBlockInstruction()
        {
            PlayUIBottomItem.updateMessageBox(
              ConfigManager.GetStringResource(49) + "\r\n" + ConfigManager.GetStringResource(48));
        }



        public static void upDateRoomLocation()
        { 
            PlayUITopItem.UpdateRoom(GameState.currentRoom);
            PlayUIBottomItem.updateMessageBox(
            ConfigManager.GetStringResource(13) + GameState.currentRoom);

        }

        public static void upDatePassageLocation()
        {

            PlayUITopItem.UpdateRoomasPassageWay();

        }


        public static void WriteDebugLine(string msg)
        {
#if (DEBUG)
            if (msg != null)
                Debug.WriteLine(msg);
#endif
        }

    }
}
