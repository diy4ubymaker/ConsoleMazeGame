using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace DIY4UMazeGame.GameEngine.GameInfo
{
  
    using DIY4UMazeGame.GameEngine.GameInfo.Roles;
    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;
    using DIY4UMazeGame.Utilities;
    using DIY4UMazeGame.Managers;

    public class GameBattleIndicator
    {
        private Thread This_thread = null;
        private bool _pause = false;
        private object _threadLock = new object();
        private bool _started = false;
        
        public Player thisPlayer { get; set; } = null;
        public Enemy  thisEnemy { get; set; } = null;
        private int colorindex = 0;
        private int waittime = 500; //ms



        public ConsoleColor[] FlashColor =
        {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta
        };

        public GameBattleIndicator()
        {
            This_thread = new Thread(new ThreadStart(Engine));
        }

        private void CheckPause()
        {
            if (_pause)
            {
                lock (_threadLock)
                {
                    Monitor.Wait(_threadLock);

                }
            }
        }

        public void Sleep()
        {
            _pause = true;
        }

        public void Kill()
        {
            _pause = true;
            This_thread.Abort();

            if(thisPlayer != null)
            {
                thisPlayer.Update();
            }
           
            if(thisEnemy  != null)
            {
                ((IPrintable)thisEnemy).Update();
            }
        }

        public void WakeUp()
        {
            if (!_started)
            {
                This_thread.Start();
                _started = true;
            }
            else
            {
                //Suspend
                _pause = false;
                lock (_threadLock)
                {
                    Monitor.Pulse(_threadLock);
                }
            }
        }

        private void Engine()
        {
            colorindex = 0;

            while (true)
            {
                CheckPause();

                lock (ScreenManager.LockSection)
                {

                    if (thisPlayer != null)
                    {
                        Console.SetCursorPosition(thisPlayer.GetLocation().X, thisPlayer.GetLocation().Y);
                        Console.ForegroundColor = FlashColor[colorindex];
                        Console.Write(thisPlayer.IconChar);
                    }

                    if (thisEnemy != null)
                    {
                        Console.SetCursorPosition(((IStationary)thisEnemy).GetLocation().X,
                            ((IStationary)thisEnemy).GetLocation().Y);
                        Console.ForegroundColor = FlashColor[colorindex];
                        Console.Write(((IPrintable)thisEnemy).IconChar);
                    }
                }

                Utility.Wait(waittime);

                if (colorindex < FlashColor.Length-1)
                {
                    colorindex++;
                }
                else if(colorindex == FlashColor.Length-1)
                {
                    colorindex = 0;
                } 

            }
        }
    }
}
