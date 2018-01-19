using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.Managers
{
    class SystemManager
    {
        /* Public members */
      

        /* Private members */
        private static bool isInit = false;


        /* Class is privte and hence it cannot be instantiated */
        private SystemManager()
        { }

        /* Default Manager */
        public static void Init()
        {
            if (!isInit)
            {
                ConfigManager.Init();
                ScreenManager.Init();
                SoundManager.Init();

                isInit = true;
            }
        }

        public static void Shutdown()
        {

        }
    }
}
