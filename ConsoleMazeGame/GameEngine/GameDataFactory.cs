using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine
{
    using DIY4UMazeGame.GameEngine.GameInfo;
    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;
    using DIY4UMazeGame.GameEngine.GameMaps;
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.GameEngine.GameInfo.Roles;

    class GameDataFactory
    {
        private GameDataFactory() {}

        public static GameWorldInfo curr_gwinfo = null;

        public static GameWorldInfo CreateInitialWorldData()
        {
            if(curr_gwinfo == null)
                curr_gwinfo = new GameWorldInfo();

            //Init GameState
            GameState.currentGameMode = GameState.GAME_MODE.WORLD;
            GameState.currentRoom = 1;
            GameState.gameLevel = 1;
            GameState.inventory = new ItemList();
            GameState.score = 0;

            //Init Player Data
            
            WoodenArmour wmobj = null;
            if (ConfigManager.StartWithArmour)
            {
                wmobj = new WoodenArmour("Wooden Armour", 0, 0, "Personal-" + GameState.inventory.GetCount() + 1);
                wmobj.shieldPower = 100;
                GameState.inventory.Add(wmobj);
            }

            WoodenSword wswbj = null;
            if (ConfigManager.StartWithSword)
            {
                wswbj = new WoodenSword("Wooden Sword", 0, 0, "Personal-" + GameState.inventory.GetCount() + 1);
                wswbj.capableHit = 380;
                GameState.inventory.Add(wswbj);
            }
          

            return curr_gwinfo;
        }

        private static void KillAllEnemies()
        {
            for (int room = 1; room <= curr_gwinfo.NumberOfRooms; room++)
            { 

                if (((GameRoom)curr_gwinfo.GetGameRoombyNumber(room)).MonsterList.Count > 0)
                {
                    foreach (Object obj in ((GameRoom)curr_gwinfo.GetGameRoombyNumber(room)).MonsterList)
                    {
                        if (obj is ILivingOrganism)
                        {
                            ((ILivingOrganism)obj).Kill();
                        }

                    }
                }
                // Release Monster
                //noOfGlobin = ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList.Count;
                if (((GameRoom)curr_gwinfo.GetGameRoombyNumber(room)).GlobinList.Count > 0)
                {
                    foreach (Object obj in ((GameRoom)curr_gwinfo.GetGameRoombyNumber(room)).GlobinList)
                    {
                        if (obj is ILivingOrganism)
                        {
                            ((ILivingOrganism)obj).Kill();
                        }

                    }
                }
            }
        }

        public static void ClearlWorldData()
        {
           
            GameState.currentGameMode = GameState.GAME_MODE.WORLD;
            GameState.currentRoom = 1;
            GameState.gameLevel = 1;
            GameState.inventory = new ItemList();
            GameState.score = 0;

            KillAllEnemies();
        }
    }
}
