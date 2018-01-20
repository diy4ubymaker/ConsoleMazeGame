using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace DIY4UMazeGame.Managers
{
    class SoundManager
    {
        /* Public members */
        public static bool isSoundOn = true;


        /* Private members */
        private static bool isInit = false;

        /* Default Sound - One Sound */
        private static SoundPlayer defaultSndPlayer = null;
        private static SoundPlayer activeSndPlayer = null;


        /* Class is privte and hence it cannot be instantiated */
        private SoundManager()
        { }


        public static void Init()
        {
            if (!isInit)
            {

                defaultSndPlayer = new SoundPlayer(ConfigManager.DefaultSound);

                isInit = true;
            }
        }

        public static void Shutdown()
        {
            if (activeSndPlayer != null && isInit)
                activeSndPlayer.Stop();

        }

        public static void TurnSoundOn()
        {
            isSoundOn = true;
        }

        public static void TurnSoundOff()
        {
            isSoundOn = false;
        }

        public static void PlayLoopDefaultSnd()
        {
            if (isSoundOn && isInit)
            {
                //defaultSndPlayer.Play();
                defaultSndPlayer.PlayLooping();
                activeSndPlayer = defaultSndPlayer;
            }

        }

        public static void PlayDefaultSnd()
        {
            if (isSoundOn && isInit)
            {
                defaultSndPlayer.Play();
                activeSndPlayer = defaultSndPlayer;
            }

        }

       
    }
}
