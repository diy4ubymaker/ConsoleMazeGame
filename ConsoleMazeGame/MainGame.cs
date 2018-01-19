using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame
{
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.GameEngine;
    using DIY4UMazeGame.GameEngine.GameMaps;
    using DIY4UMazeGame.GameScreenLib.Screens;

    class MainGame
    {
        private static int DefaultScreenWidth = 
            ScreenManager.MapWindowsWidth + (ScreenManager.DefaultBorder * 2);

        private static int DefaultScreenHeight = 
            ScreenManager.MenuHeight + (ScreenManager.DefaultBorder * 2) +
            ScreenManager.HUDHeight + (ScreenManager.DefaultBorder * 2)  +
            ScreenManager.MapWidowHeight + ScreenManager.BufferAreaHeight ;
         
        static void Main(string[] args)
        {
            GameStateManager.Init();

            ConfigManager.Init();
            ScreenManager.Init(
                DefaultScreenWidth, 
                DefaultScreenHeight,
                true,
                true);           
            SoundManager.Init();

                       
            while (GameStateManager.IsRunning())
            {
                GameStateManager.Update(0.0f);
            }
            
            SoundManager.Shutdown();
            ScreenManager.Shutdown();
            ConfigManager.Shutdown();


        }
    }
}
