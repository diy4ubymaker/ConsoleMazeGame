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

    public class PlayUITopItem
    {
        public static int screenWidth = 0;
        public static int screenHeight = 0;

        public static Rectangle BorderLine { get; set; } = null;

        public static TextBox TitleLabel { get; set; } = null;

        public static TextBox RoomLabel { get; set; } = null;
        public static string  RoomLabelStr =        "Location           : ";
        public static string  RoomLableStrVal = "Passage Way";

        public static TextBox ScoreLabel { get; set; } = null;
        public static string  ScoreLabelStr =       "Score              : ";
        public static int  ScoreLableVal = 0;

        public static TextBox ExperienceLabel { get; set; } = null;
        public static string ExperienceLabelStr = "Experience Level   : ";
        public static int ExperienceLabelVal = 0;

        public static TextBox GameLevelLabel { get; set; } = null;
        public static string GameLevelLabelStr = "Game Level         : ";
        public static int GameLevelLabelVal = 0;

        public static TextBox HealthLevelLabel { get; set; } = null;
        public static string HealthLevelLabelStr = "Health Level       : ";
        public static int HealthLevelLabelVal = 0;

        
        //Second Column
        public static TextBox HealthPotionLabel { get; set; } = null;
        public static string HealthPotionLabelStr = "Health Potion      : ";
        public static int HealthPotionLabelVal = 0;


        public static TextBox NoRoomLabel { get; set; } = null;
        public static string NoRoomLabelStr = "No of Rooms        : ";
        public static int NoRoomLabelVal = 0;

        public static TextBox TotalGoldLabel { get; set; } = null;
        public static string TotalGoldLabelStr = "Total Gold         : ";
        public static int TotalGoldLabelVal = 0;

        public static TextBox TotalSilverLabel { get; set; } = null;
        public static string TotalSilverLabelStr = "Total Silver       : ";
        public static int TotalSilverLabelVal = 0;

        public static TextBox WeaponLabel { get; set; } = null;
        public static string WeaponLabelStr = "Weapon On Hand     : ";
        public static string WeaponLabelStrVal = "Wooden Sword";

        public static TextBox ArmourLabel { get; set; } = null;
        public static string ArmourLabelStr = "Armour Type        : ";
        public static string ArmourLabelStrVal = "Wodden Armour";


        public static ConsoleColor BorderColor = ConsoleColor.Yellow;

        private PlayUITopItem() {}

        private static void DrawScreen()
        {
            //lock (ScreenManager.LockSection)
            //{
                TitleLabel.draw();
                BorderLine.draw();               
                ScoreLabel.draw();
                NoRoomLabel.draw();
                ExperienceLabel.draw();
                GameLevelLabel.draw();
                HealthLevelLabel.draw();

                //Second Column
                HealthPotionLabel.draw();
                RoomLabel.draw();
                TotalGoldLabel.draw();
                TotalSilverLabel.draw();
                WeaponLabel.draw();
                ArmourLabel.draw();

                DrawLogo();

            //}
        }

        public static void Init()
        {
            screenWidth = ScreenManager.screenWidth;
            screenHeight = ScreenManager.screenHeight;

            BorderLine = new Rectangle();
            BorderLine.BorderType = Shapes.BORDER_TYPE.Single;
            BorderLine.Create(
                new Point(0, 0), 
                new Size(ScreenManager.MapWindowsWidth + (ScreenManager.DefaultBorder * 2), 
                         ScreenManager.HUDHeight + (ScreenManager.DefaultBorder *2)), 
                         BorderColor, ConsoleColor.Black);

            //Top Title
            TitleLabel = new TextBox();
            TitleLabel.Create(new Point(2, 1), new Size(ScreenManager.screenWidth-4, 1), ConsoleColor.Magenta, ConsoleColor.Black);
            TitleLabel.alighnment = TextBox.ALIGN_ENUM.Centre_Justify;
            TitleLabel.textItem = ScreenManager.ScreenTitle;

            //*************************************************************************************
            //First Column
            RoomLabel = new TextBox();
            RoomLabel.Create(new Point(2, 1), new Size(35, 1), ConsoleColor.White, ConsoleColor.Black);
            RoomLabel.textItem = RoomLabelStr + RoomLableStrVal;

            ScoreLabel = new TextBox();
            ScoreLabel.Create(new Point(2, 5), new Size(26, 1), ConsoleColor.White, ConsoleColor.Black);
            ScoreLabel.textItem = ScoreLabelStr + ScoreLableVal;

            ExperienceLabel = new TextBox();
            ExperienceLabel.Create(new Point(2, 7), new Size(26, 1), ConsoleColor.White, ConsoleColor.Black);
            ExperienceLabel.textItem = ExperienceLabelStr + ExperienceLabelVal;

            GameLevelLabel = new TextBox();
            GameLevelLabel.Create(new Point(2, 3), new Size(26, 1), ConsoleColor.White, ConsoleColor.Black);
            GameLevelLabel.textItem = GameLevelLabelStr + GameLevelLabelVal;

            //HealthLabel
            HealthLevelLabel = new TextBox();
            HealthLevelLabel.Create(new Point(2, 9), new Size(26, 1), ConsoleColor.White, ConsoleColor.Black);
            HealthLevelLabel.textItem = HealthLevelLabelStr + HealthLevelLabelVal;
            //************************************************************************************

            //***************************************************************************************
            //Second Column

            //HealthPotionLabel
            HealthPotionLabel = new TextBox();
            HealthPotionLabel.Create(new Point(26 + 2 + 2, 3), new Size(26, 1), ConsoleColor.White, ConsoleColor.Black);
            HealthPotionLabel.textItem = HealthPotionLabelStr + HealthPotionLabelVal;

            //NoRoomLabel
            NoRoomLabel = new TextBox();
            NoRoomLabel.Create(new Point(26+2+2, 5), new Size(29, 1), ConsoleColor.White, ConsoleColor.Black);
            NoRoomLabel.textItem = NoRoomLabelStr + NoRoomLabelVal;

            //TotalGoldLabel
            TotalGoldLabel = new TextBox();
            TotalGoldLabel.Create(new Point(26 + 2 + 2, 7), new Size(29, 1), ConsoleColor.White, ConsoleColor.Black);
            TotalGoldLabel.textItem = TotalGoldLabelStr + TotalGoldLabelVal;


            //TotalSilverLabel
            TotalSilverLabel = new TextBox();
            TotalSilverLabel.Create(new Point(26 + 2 + 2, 9), new Size(29, 1), ConsoleColor.White, ConsoleColor.Black);
            TotalSilverLabel.textItem = TotalSilverLabelStr + TotalSilverLabelVal;

            //WeaponLabel
            WeaponLabel = new TextBox();
            WeaponLabel.Create(new Point(26 + 2 +26 + 2 + 2+3, 7), new Size(35, 1), ConsoleColor.White, ConsoleColor.Black);
            WeaponLabel.textItem = WeaponLabelStr + WeaponLabelStrVal;

            //ArmourLabel
            ArmourLabel = new TextBox();
            ArmourLabel.Create(new Point(26 + 2 + 26 + 2 + 2+3, 9), new Size(35, 1), ConsoleColor.White, ConsoleColor.Black);
            ArmourLabel.textItem = ArmourLabelStr + ArmourLabelStrVal;
            //************************************************************************************

        }

        private static void DrawLogo()
        {
            lock (ScreenManager.LockSection)
            {
                int row = 1;
                int col = 75;

                Console.ForegroundColor = ConsoleColor.Red ;

                foreach (string Logo in Gamelogo1)
                {
                    Console.SetCursorPosition(col, row);
                    Console.Write(Logo);
                    row++;
                }
                //row++;
                col = 100;
                foreach (string Logo in Gamelogo2)
                {
                    Console.SetCursorPosition(col, row);
                    Console.Write(Logo);
                    row++;
                }
            }
        }

        public static void Update()
        {
            DrawScreen();
        }


        public static void UpdateRoom(int val)
        {
            RoomLableStrVal = Convert.ToString(val);
            RoomLabel.textItem = RoomLabelStr + "Room " + RoomLableStrVal;
            RoomLabel.draw();
        }

        public static void UpdateRoomasPassageWay()
        {
            RoomLableStrVal = "Passage Way";
            RoomLabel.textItem = RoomLabelStr + RoomLableStrVal;
            RoomLabel.draw();
        }

        public static void UpdateScore(int val)
        {
            ScoreLableVal = val;
            ScoreLabel.textItem = ScoreLabelStr + ScoreLableVal;
            ScoreLabel.draw();
        }

        public static void UpdateExperienceLevel(int val)
        {
            ExperienceLabelVal = val;
            ExperienceLabel.textItem = ExperienceLabelStr + ExperienceLabelVal;
            ExperienceLabel.draw();
        }

        public static void UpdateGameLevel(int val)
        {
            GameLevelLabelVal = val;
            GameLevelLabel.textItem = GameLevelLabelStr + GameLevelLabelVal;
            GameLevelLabel.draw();
        }

        public static void UpdateGoldLevel(int val)
        {
            TotalGoldLabelVal = val;
            TotalGoldLabel.textItem = TotalGoldLabelStr + TotalGoldLabelVal;
            TotalGoldLabel.draw();
        }

        public static void UpdateSilverLevel(int val)
        {
            TotalSilverLabelVal = val;
            TotalSilverLabel.textItem = TotalSilverLabelStr + TotalSilverLabelVal;
            TotalSilverLabel.draw();
        }

        //NoRoomLabel
        public static void UpdateNoOfRoom(int val)
        {
            NoRoomLabelVal = val;
            NoRoomLabel.textItem = NoRoomLabelStr + NoRoomLabelVal;
            NoRoomLabel.draw();
        }

        //HealthPotionLabel
        public static void UpdateHealthPotion(int val)
        {
            HealthPotionLabelVal = val;
            HealthPotionLabel.textItem = HealthPotionLabelStr + HealthPotionLabelVal;
            HealthPotionLabel.draw();
        }

        //HealthLabel
        public static void UpdateHealthLevel(int val)
        {
            HealthLevelLabelVal = val;
            HealthLevelLabel.textItem = HealthLevelLabelStr + HealthLevelLabelVal + "%";
            HealthLevelLabel.draw();
        }

        //Weapon
        public static void UpdateWeaponType(string val)
        {
            WeaponLabelStrVal = val;
            WeaponLabel.textItem = WeaponLabelStr + WeaponLabelStrVal;
            WeaponLabel.draw();
        }

        //Armour
        public static void UpdateArmourType(string val)
        {
            ArmourLabelStrVal = val;
            ArmourLabel.textItem = ArmourLabelStr + ArmourLabelStrVal;
            ArmourLabel.draw();
        }


        public static void Exit()
        {

        }

        private static string[] Gamelogo1 =
        {
             @" __  __                     _               ",
             @"|  \/  |  ___   _ __   ___ | |_  ___  _ __  ",
             @"| |\/| | / _ \ | '_ \ / __|| __|/ _ \| '__| ",
             @"| |  | || (_) || | | |\__ \| |_|  __/| |    ",
             @"|_|  |_| \___/ |_| |_||___/ \__|\___||_|    "
        };

        private static string[] Gamelogo2 =
        {
           @" __      ___ _____ ",
           @"/  \|  ||__ /__`|  ",
           @"\__X\__/|___.__/|  "

        };
    }
}
