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
    

    public class PlayUIWorldMap
    {
        public static int screenWidth = 0;
        public static int screenHeight = 0;

        public static MapBox mapbox = null;

        public static int mapBoxColPos = 0;
        public static int mapBoxRowPos = ScreenManager.HUDHeight + (ScreenManager.DefaultBorder *2);

        private PlayUIWorldMap() {}

        private static void DrawScreen()
        {

             mapbox.draw();
            
        }

        public static void Init()
        {
            screenWidth = ScreenManager.screenWidth;
            screenHeight = ScreenManager.screenHeight;

            mapbox = new MapBox();
            mapbox.Create(
                new Point(mapBoxColPos, mapBoxRowPos), 
                new Size(ScreenManager.MapWindowsWidth, ScreenManager.MapWidowHeight), 
                ConsoleColor.White, ConsoleColor.Black);
        }

        public static void Init(char[][] map)
        {
            screenWidth = ScreenManager.screenWidth;
            screenHeight = ScreenManager.screenHeight;

            mapbox = new MapBox(map);
            mapbox.Create(
                new Point(mapBoxColPos, mapBoxRowPos),
                new Size(ScreenManager.screenWidth, 12),
                ConsoleColor.White, ConsoleColor.Black);
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
