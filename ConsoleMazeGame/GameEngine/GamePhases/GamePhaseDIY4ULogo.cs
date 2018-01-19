using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GamePhases
{
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.Utilities;

    class GamePhaseDIY4ULogo
    {
       private static string[] DIY4ULogo =
       {
        
            "██████╗ ██╗██╗   ██╗██╗  ██╗██╗   ██╗",
            "██╔══██╗██║╚██╗ ██╔╝██║  ██║██║   ██║",
            "██║  ██║██║ ╚████╔╝ ███████║██║   ██║",
            "██║  ██║██║  ╚██╔╝  ╚════██║██║   ██║",
            "██████╔╝██║   ██║        ██║╚██████╔╝",
            "╚═════╝ ╚═╝   ╚═╝        ╚═╝ ╚═════╝ ",
            " " ,   
            "    _   _     _   _   _   _   _",
            "   / \\ / \\   / \\ / \\ / \\ / \\ / \\ ",
            "  ( B | y ) ( M | a | k | e | r )",
            "   \\_/ \\_/   \\_/ \\_/ \\_/ \\_/ \\_/"
        };

        public static ConsoleColor ForegroundColor = ConsoleColor.Green;
        public static ConsoleColor BackgroundColor = ConsoleColor.Black;
        public static int ScreenWidth = 0;
        public static int ScreenHeight = 0;
        public static int WaitTime = 5*1000;  //millisecond

        private static void clearline(int row)
        {
                Console.SetCursorPosition(0,row);
                Console.Write
                    (string.Concat(Enumerable.Repeat(" ", ScreenWidth-1))
                    );
            
        }

        private static void printCentre(string outputstr, int row)
        {
            int col;

            col = (ScreenWidth - outputstr.Length) / 2;
            Console.SetCursorPosition(col, row);
            Console.Write(outputstr);
        }



        public static void Init()
        {
            string[] tmpstrlist = null;
            int row = 1;
            int col = 1;
            int logowidth = 0;
         
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            ScreenWidth = ScreenManager.screenWidth;
            ScreenHeight = ScreenManager.screenHeight;

            
            if (ConfigManager.LogoFile != null)
            {
                tmpstrlist = Utility.ReadFromTextFile(ConfigManager.LogoFile);
                if (tmpstrlist != null)
                    DIY4ULogo = tmpstrlist;
            }
            

            logowidth = DIY4ULogo[0].Length;

           

            for (col = 1; col <= (ScreenWidth - logowidth) / 2; col ++)
            {
                row = 1;
                foreach (string Logo in DIY4ULogo)
                {
                  
                    clearline(row);
                    Console.SetCursorPosition(col, row);
                    Console.Write(Logo);
                    row++;
                  
                }
                Utility.Wait(20);
            }

            Utility.Wait(100);

            Console.ForegroundColor = ConsoleColor.Magenta;

        }

        public static void Update(float dt)
        {
            Utility.Wait(WaitTime);
            GameStateManager.SetNextState(GameStateTable.GAMESTATE_ENUM.GsGamePlay);
        }

        public static void Exit()
        {
            ScreenManager.ClearScreen();
            ScreenManager.ResetColor();

        }


    }
}
