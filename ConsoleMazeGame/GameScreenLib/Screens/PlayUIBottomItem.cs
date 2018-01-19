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

    public class PlayUIBottomItem
    {
        public static int screenWidth = 0;
        public static int screenHeight = 0;

        public static Rectangle BorderLine { get; set; } = null;
        public static Rectangle RightBorderLine { get; set; } = null;

        public static ConsoleColor BorderColor = ConsoleColor.Yellow;


        public static int CLEAR                     = 0;
        public static int QUIT_INSTRUNCTION         = 1;
        public static int UP_INSTRUNCTION           = QUIT_INSTRUNCTION << 1;
        public static int DOWN_INSTRUNCTION         = QUIT_INSTRUNCTION << 2;
        public static int RIGHT_INSTRUNCTION        = QUIT_INSTRUNCTION << 3;
        public static int LEFT_INSTRUNCTION         = QUIT_INSTRUNCTION << 4;
        public static int YES_INSTRUNCTION          = QUIT_INSTRUNCTION << 5;
        public static int NO_INSTRUNCTION           = QUIT_INSTRUNCTION << 6;
        public static int CONTINUE_INSTRUNCTION     = QUIT_INSTRUNCTION << 7;
        public static int ATTACK_INSTRUNCTION       = QUIT_INSTRUNCTION << 8;


        private static string[] InstructionList =
        {
            "[ESC] Quit Game",
            "[ARROW UP]   Move North",
            "[ARROW DOWN] Move South",
            "[ARROW LEFT] Move East",
            "[ARROW RGHT] Move West",
            "[Y]  Yes",
            "[N]  No ",
            "[C]  Continue",
            "[A]  Attack"

        };

        public static TextBox InstrutionLabel { get; set; } = null;
        public static string InstrutionLabelStrVal = "";

        public static MenuTextBox menutxtBox { get; set; } = null;
        public static List<string> menuOptions { get; set; } = new List<String>(
        new String[]
        {
             "123456789012345678901234567890",
             "123456789012345678901234567890",
             "123456789012345678901234567890",
             "123456789012345678901234567890",
             "123456789012345678901234567890",
             "123456789012345678901234567890",
             "123456789012345678901234567890",
             "123456789012345678901234567890",
             "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"

        });

        public static MultiLineTextBox messagebox { get; set; } = null;

        private PlayUIBottomItem(){}

        private static void DrawScreen()
        {
            //lock (ScreenManager.LockSection)
            //{
                RightBorderLine.draw();
                BorderLine.draw();
                InstrutionLabel.draw();
                menutxtBox.draw();
                messagebox.draw();
            //}
        }

        public static void Init()
        {
            screenWidth = ScreenManager.screenWidth;
            screenHeight = ScreenManager.screenHeight;

            RightBorderLine = new Rectangle();
            RightBorderLine.BorderType = Shapes.BORDER_TYPE.Single;
            RightBorderLine.Create(
                new Point(ScreenManager.screenWidth - 30 - 2, ScreenManager.MapWidowHeight + ScreenManager.HUDHeight + (ScreenManager.DefaultBorder * 2)),
                new Size(32, ScreenManager.HUDHeight + (ScreenManager.DefaultBorder * 2)),
                BorderColor, ConsoleColor.Black);

            BorderLine = new Rectangle();
            BorderLine.BorderType = Shapes.BORDER_TYPE.Single;
            BorderLine.Create(
                new Point(0, ScreenManager.MapWidowHeight + ScreenManager.HUDHeight + (ScreenManager.DefaultBorder *2)),
                new Size(ScreenManager.MapWindowsWidth + (ScreenManager.DefaultBorder * 2)-32,
                         ScreenManager.HUDHeight + (ScreenManager.DefaultBorder * 2)),
                         BorderColor, ConsoleColor.Black);

            //Top Title
            InstrutionLabel = new TextBox();
            InstrutionLabel.Create(new Point(2, ScreenManager.screenHeight-3),
                new Size(ScreenManager.screenWidth - 32- 4-1, 1), ConsoleColor.White, ConsoleColor.Black);
            //InstrutionLabel.alighnment = TextBox.ALIGN_ENUM.Centre_Justify;
            InstrutionLabel.textItem = InstrutionLabelStrVal;

            /* Text Box for the Options Menu */
            menutxtBox = new MenuTextBox();
            menutxtBox.Create(
            new Point(ScreenManager.screenWidth-30, ScreenManager.MapWidowHeight + ScreenManager.HUDHeight + (ScreenManager.DefaultBorder * 2)+1),
            new Size(29,9),
            ConsoleColor.White, ConsoleColor.Black);
            menutxtBox.lineItem = menuOptions;

            messagebox = new MultiLineTextBox();
            messagebox.Create(
                new Point(2,ScreenManager.MapWidowHeight + ScreenManager.HUDHeight + (ScreenManager.DefaultBorder * 2) + 1), 
                new Size(ScreenManager.screenWidth-32-4-1,9),
                ConsoleColor.White, ConsoleColor.Black);
            messagebox.textItem = "1234567890123456789012345678901234567890123456789012345678901234567890ABCDEFGHIJKLMNOP";

        }

        public static void UpdateInstruction(string val)
        {
            InstrutionLabelStrVal = val;
            InstrutionLabel.textItem = InstrutionLabelStrVal;
            InstrutionLabel.draw();
        }

        public static void UpdateInstruction(int val)
        {
            string printstring = "";
            int bitval = 1;
            int finalval = 0;

            for (int i=0; i< InstructionList.Length; i++)
            {
                finalval = (bitval << i) & val;
                if(finalval != 0)
                {
                    printstring += InstructionList[i] + " ";
                }
            }

            InstrutionLabelStrVal = printstring;
            InstrutionLabel.textItem = InstrutionLabelStrVal;
            InstrutionLabel.draw();
        }

        public static void UpdateMenuOption(int val)
        {
            List<string> menuOptions = new List<String>();
            int bitval = 1;
            int finalval = 0;
            
            menuOptions.Add(" ");
            

            for (int i = 0; i < InstructionList.Length; i++)
            {
                finalval = (bitval << i) & val;
                if (finalval != 0)
                {
                    menuOptions.Add(InstructionList[i]);
                }
            }

            menutxtBox.lineItem = menuOptions;
            menutxtBox.draw();
        }

        // Update individual Element
        public static void updateMessageBox(String msg)
        {
           
            messagebox.textItem = msg;
            messagebox.draw();
        }


        public static List<string> GenerateMenuOption(
           string line1,
           string line2 = null,
           string line3 = null,
           string line4 = null
           )
        {
            List<string> menuOptions = new List<String>();

            menuOptions.Add("      Options:");
            menuOptions.Add(" ");

            if (line1 != null)
                menuOptions.Add(line1);

            if (line2 != null)
                menuOptions.Add(line2);

            if (line3 != null)
                menuOptions.Add(line3);

            if (line4 != null)
                menuOptions.Add(line4);

            return menuOptions;

        }

        public static void Update()
        {
            DrawScreen();
        }

        public static void Exit()
        {

        }

        public static void UIState1()
        {

        }

    }
}
