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

    public class PlayUILeftPanel
    {

        public static Rectangle BorderLine { get; set; } = null;
        public static int PanelWidth = 0;
        public static int PanelHeight = 0;
        public static ConsoleColor BorderColor = ConsoleColor.Yellow;

        public static int UIPosCol { get; set; } = 0;
        public static int UIPosRow { get; set; } = 0;

        public static TextBox TitleLabel { get; set; } = null;

        public static TextBox TotalItemLabel { get; set; } = null;
        public static string  TotalItemStr =    "Total         : ";
        public static int TotalItemVal = 0;
        
        public static TextBox GoldItemLabel { get; set; } = null;
        public static string GoldItemStr =      "Gold          : ";
        public static int GoldItemVal = 0;

        public static TextBox SilverItemLabel { get; set; } = null;
        public static string SilverItemStr =    "Silver        : ";
        public static int SilverItemVal = 0;

        public static TextBox KeyItemLabel { get; set; } = null;
        public static string  KeyItemStr =      "Key           : ";
        public static int KeyItemVal = 0;

        public static TextBox ArmourItemLabel { get; set; } = null;
        public static string ArmourItemStr =    "Armour        : ";
        public static int ArmourItemVal = 0;

        public static TextBox SwordItemLabel { get; set; } = null;
        public static string SwordItemStr =     "Sword         : ";
        public static int SwordItemVal = 0;

        public static TextBox HealthPotionLabel { get; set; } = null;
        public static string HealthPotionLabelStr = "Health Potion : ";
        public static int HealthPotionLabelVal = 0;

        public static TextBox WeaponTypeLabel { get; set; } = null;
        public static string WeaponTypeStr =    "Weapon Type";

        public static TextBox WeaponTypeValLabel { get; set; } = null;
        public static string WeaponTypeValStr = "Wooden Sword";

        //public static TextBox WeaponTypePowerValLabel { get; set; } = null;
        //public static int WeaponTypePowerVal = 100;

        public static TextBox ArmourTypeLabel { get; set; } = null;
        public static string  ArmourTypeStr = "Armour Type";

        public static TextBox ArmourTypeValLabel { get; set; } = null;
        public static string ArmourTypeValStr = "Wooden";

        //public static TextBox ArmourTypePowerValLabel { get; set; } = null;
        //public static int ArmourTypePowerVal = 100;

        public static PercentageBar WeaponBar { get; set; } = null;
        public static PercentageBar ArmourBar { get; set; } = null;

        public static TextBox TotalEnemyLabel { get; set; } = null;
        public static string TotalEnemyStr =    "Monst/Globin In Room : ";
        public static int TotalEnemyVal = 0;

        public static TextBox TotalMonsterWLabel { get; set; } = null;
        public static string TotalMonsterWStr = "Monster in World     : ";
        public static int TotalMonsterWVal = 0;

        public static TextBox TotalGlobinWLabel { get; set; } = null;
        public static string TotalGlobinWStr =  "Globin in World      : ";
        public static int TotalGlobinWVal = 0;


        private PlayUILeftPanel() {}

        private static void DrawScreen()
        {           
                BorderLine.draw();
                TitleLabel.draw();
                WeaponTypeLabel.draw();
                ArmourTypeLabel.draw();

                //Updatable
                GoldItemLabel.draw();
                SilverItemLabel.draw();
                KeyItemLabel.draw();
                ArmourItemLabel.draw();
                SwordItemLabel.draw();
                HealthPotionLabel.draw();
                TotalItemLabel.draw();
                TotalEnemyLabel.draw();
                TotalMonsterWLabel.draw();
                TotalGlobinWLabel.draw();
                WeaponTypeValLabel.draw();
                //WeaponTypePowerValLabel.draw();
                ArmourTypeValLabel.draw();
                //ArmourTypePowerValLabel.draw();
                WeaponBar.draw();
                ArmourBar.draw();
                //Updatable


        }


        //TotalItemLabel
        public static void UpdateTotalItem(int val)
        {
            TotalItemVal = val;
            TotalItemLabel.textItem = TotalItemStr + TotalItemVal;
            TotalItemLabel.draw();
        }

        //TotalEnemyLabel
        public static void UpdateTotalEnemyItem(int val)
        {
            TotalEnemyVal = val;
            TotalEnemyLabel.textItem = TotalEnemyStr + TotalEnemyVal;
            TotalEnemyLabel.draw();
        }

        //TotalMonsterWLabel
        public static void UpdateTotalMonsterItem(int val)
        {
            TotalMonsterWVal = val;
            TotalMonsterWLabel.textItem = TotalMonsterWStr + TotalMonsterWVal;
            TotalMonsterWLabel.draw();
        }

        //TotalArmourWLabel
        public static void UpdateTotalGlobinWItem(int val)
        {
            TotalGlobinWVal = val;
            TotalGlobinWLabel.textItem = TotalGlobinWStr + TotalGlobinWVal;
            TotalGlobinWLabel.draw();
        }

        //GoldItemLabel
        public static void UpdateGoldItem(int val)
        {
            GoldItemVal = val;
            GoldItemLabel.textItem = GoldItemStr + GoldItemVal;
            GoldItemLabel.draw();
        }

        //SilverItemLabel
        public static void UpdateSilverItem(int val)
        {
            SilverItemVal = val;
            SilverItemLabel.textItem = SilverItemStr + SilverItemVal;
            SilverItemLabel.draw();
        }


        //KeyItemLabel
        public static void UpdateKeyItem(int val)
        {
            KeyItemVal = val;
            KeyItemLabel.textItem = KeyItemStr + KeyItemVal;
            KeyItemLabel.draw();
        }

        //ArmourItemLabel
        public static void UpdateArmourItem(int val)
        {
            ArmourItemVal = val;
            ArmourItemLabel.textItem = ArmourItemStr + ArmourItemVal;
            ArmourItemLabel.draw();
        }

        //SwordItemLabel
        public static void UpdateSwordItem(int val)
        {
            SwordItemVal = val;
            SwordItemLabel.textItem = SwordItemStr + SwordItemVal;
            SwordItemLabel.draw();
        }

        //HealthPotionLabel
        public static void UpdateHealthPotiontem(int val)
        {
            HealthPotionLabelVal = val;
            HealthPotionLabel.textItem = HealthPotionLabelStr + HealthPotionLabelVal;
            HealthPotionLabel.draw();
        }

        //WeaponTypeValLabel
        public static void UpdateWeaponTypeVal(string val)
        {
            WeaponTypeStr = val;
            WeaponTypeValLabel.textItem = WeaponTypeStr;
            WeaponTypeValLabel.draw();
        }


        //WeaponTypePowerValLabel
        /*
        public static void UpdateWeaponTypePowerVal(int val)
        {
            WeaponTypePowerVal = val;
            WeaponTypePowerValLabel.textItem = WeaponTypePowerVal + " %";
            SwordItemLabel.draw();
        }*/

        //ArmourTypeValLabel
        public static void UpdateArmourTypeVal(string val)
        {
            ArmourTypeStr = val;
            ArmourTypeValLabel.textItem = ArmourTypeStr;
            ArmourTypeValLabel.draw();
        }

        //ArmourTypePowerValLabel
        /*
        public static void UpdateArmourTypePowerVal(int val)
        {
            ArmourTypePowerVal = val;
            ArmourTypePowerValLabel.textItem = ArmourTypePowerVal + " %";
            ArmourTypePowerValLabel.draw();
        }*/

        public static void Init()
        {

            PanelWidth = (ScreenManager.screenWidth - ScreenManager.RoomMapWindowsWidth) / 2-1-1;
            PanelHeight = ScreenManager.MapWidowHeight;

            BorderLine = new Rectangle();
            BorderLine.BorderType = Shapes.BORDER_TYPE.Single;
            BorderLine.Create(
                new Point(0,ScreenManager.HUDHeight +2),
                new Size((ScreenManager.screenWidth - ScreenManager.RoomMapWindowsWidth)/2-1-1,
                ScreenManager.MapWidowHeight),
                         BorderColor, ConsoleColor.Black);

            UIPosCol = 0;
            UIPosRow = ScreenManager.HUDHeight + 2;

            //Top Title
            TitleLabel = new TextBox();
            TitleLabel.Create(
                new Point(UIPosCol+2, UIPosRow+ 1), 
                new Size(PanelWidth-4, 1), 
                ConsoleColor.Yellow, 
                ConsoleColor.Black);
            TitleLabel.alighnment = TextBox.ALIGN_ENUM.Left_Justify;
            TitleLabel.textItem = "Inventory";

            //GoldItemLabel
            GoldItemLabel = new TextBox();
            GoldItemLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 3),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            GoldItemLabel.textItem = GoldItemStr + GoldItemVal;

            //SilverItemLabel
            SilverItemLabel = new TextBox();
            SilverItemLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 4),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            SilverItemLabel.textItem = SilverItemStr + SilverItemVal;

            //KeyItemLabel
            KeyItemLabel = new TextBox();
            KeyItemLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 5),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            KeyItemLabel.textItem = KeyItemStr + KeyItemVal;

            //ArmourItemLabel
            ArmourItemLabel = new TextBox();
            ArmourItemLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 6),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            ArmourItemLabel.textItem = ArmourItemStr + ArmourItemVal;

            //SwordItemLabel
            SwordItemLabel = new TextBox();
            SwordItemLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 7),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            SwordItemLabel.textItem = SwordItemStr + SwordItemVal;

            //HealthPortionLabel
            HealthPotionLabel = new TextBox();
            HealthPotionLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 8),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            HealthPotionLabel.textItem = HealthPotionLabelStr + HealthPotionLabelVal;

            //TotalItemLabel
            TotalItemLabel = new TextBox();
            TotalItemLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 9),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            TotalItemLabel.textItem = TotalItemStr + TotalItemVal;

            //TotalEnemyLabel
            TotalEnemyLabel = new TextBox();
            TotalEnemyLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 11),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            TotalEnemyLabel.textItem = TotalEnemyStr + TotalEnemyVal;

            //TotalMonsterWLabel
            TotalMonsterWLabel = new TextBox();
            TotalMonsterWLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 12),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            TotalMonsterWLabel.textItem = TotalMonsterWStr + TotalMonsterWVal;


            //TotalArmourWLabel
            TotalGlobinWLabel = new TextBox();
            TotalGlobinWLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 13),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            TotalGlobinWLabel.textItem = TotalGlobinWStr + TotalGlobinWVal;

            //WeaponTypeLabel
            /*
            WeaponTypeLabel = new TextBox();
            WeaponTypeLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 11),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.Yellow, ConsoleColor.Black);
            WeaponTypeLabel.textItem = WeaponTypeStr;
            */

            //WeaponTypeLabel
            WeaponTypeLabel = new TextBox();
            WeaponTypeLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 15),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.Yellow, ConsoleColor.Black);
            WeaponTypeLabel.textItem = WeaponTypeStr;

            //WeaponTypeValLabel
            WeaponTypeValLabel = new TextBox();
            WeaponTypeValLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 16),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            WeaponTypeValLabel.alighnment = TextBox.ALIGN_ENUM.Centre_Justify;
            WeaponTypeValLabel.textItem = WeaponTypeValStr;

            //WeaponTypePowerValLabel
            /*
            WeaponTypePowerValLabel = new TextBox();
            WeaponTypePowerValLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 14),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            WeaponTypePowerValLabel.alighnment = TextBox.ALIGN_ENUM.Centre_Justify;
            WeaponTypePowerValLabel.textItem = WeaponTypePowerVal + " %";
            */

            //WeaponTypeLabel
            ArmourTypeLabel = new TextBox();
            ArmourTypeLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 20),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.Yellow, ConsoleColor.Black);
            ArmourTypeLabel.textItem = ArmourTypeStr;

            //ArmourTypeValLabel
            ArmourTypeValLabel = new TextBox();
            ArmourTypeValLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 21),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            ArmourTypeValLabel.alighnment = TextBox.ALIGN_ENUM.Centre_Justify;
            ArmourTypeValLabel.textItem = ArmourTypeValStr;

            //ArmourTypePowerValLabel
            /*
            ArmourTypePowerValLabel = new TextBox();
            ArmourTypePowerValLabel.Create(
                new Point(UIPosCol + 2, UIPosRow + 20),
                new Size(PanelWidth - 4, 1),
                ConsoleColor.White, ConsoleColor.Black);
            ArmourTypePowerValLabel.alighnment = TextBox.ALIGN_ENUM.Centre_Justify;
            ArmourTypePowerValLabel.textItem = ArmourTypePowerVal + " %";
            */

            WeaponBar = new PercentageBar();
            WeaponBar.Create(
                new Point(UIPosCol + 2, UIPosRow + 18),
                new Size(PanelWidth - 4, 1),
              ConsoleColor.White,
              ConsoleColor.Black);
            WeaponBar.BarWidth = 20;
            WeaponBar.Percent = 100;

            ArmourBar = new PercentageBar();
            ArmourBar.Create(
               new Point(UIPosCol + 2, UIPosRow + 23),
               new Size(PanelWidth - 4, 1),
              ConsoleColor.White,
              ConsoleColor.Black);
            ArmourBar.BarWidth = 20;
            ArmourBar.Percent = 100;

        }

        public static void UpdateArmourPowerLevel(int level)
        {
            lock (ScreenManager.LockSection)
            {
                ArmourBar.Percent = level;
                ArmourBar.draw();
            }
        }

        public static void UpdateWeaponrPowerLevel(int level)
        {
            lock (ScreenManager.LockSection)
            {
                WeaponBar.Percent = level;
                WeaponBar.draw();
            }
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
