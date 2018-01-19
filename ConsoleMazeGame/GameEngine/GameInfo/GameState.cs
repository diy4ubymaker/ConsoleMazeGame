using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine
{

    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.GameEngine.GameInfo;
    using DIY4UMazeGame.GameEngine.GameInfo.Roles;
    using DIY4UMazeGame.GameScreenLib.Screens;

    /* A Class to keep the current user Game State information. */
    public class GameState
    {
        public static GAME_MODE currentGameMode { get; set; } =  GAME_MODE.WORLD;
        public static GAME_SUBMODE subGameMode { get; set; } = GAME_SUBMODE.WORLD_DROPIN;

        public static int currentRoom { get; set; } = 1;
        public static int gameLevel { get; set; } = 1;
        public static int score { get; set; } = 0;
        public static int experience { get; set; } = 0;

        public static GameBattleIndicator gmind { get; set; } = null;

        public static ItemList inventory { get; set; } = new ItemList();

        private static bool BattleOn = false;
        public  static bool LastWin = false;
        private static Object obj = new Object();
        public static Enemy enemyobj { get; set; } = null;

        public enum GAME_SUBMODE
        {
            WORLD_DROPIN = 1,
            WORLD_PROCESSING,
            WORLD_QUIT,
            ROOM_PROCESSING,
            ROOM_QUIT

        };

        public enum GAME_MODE
        {
            WORLD = 1,
            ROOM
        };

        public static void setBattleOn(Object obj)
        {
            lock(obj)
            {
                BattleOn = true;
                LastWin = false;
            }

            //*******************************************
            PlayUIBottomItem.UpdateInstruction(
            PlayUIBottomItem.ATTACK_INSTRUNCTION
            );

            PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
            if (obj is Enemy)
            {
                PlayUIBottomItem.updateMessageBox(
                 ConfigManager.GetStringResource(38) + ((Enemy)obj).InstanceName + "\r\n" + 
                 ConfigManager.GetStringResource(39) + ((Enemy)obj).InstanceName);
            }
            //*******************************************
        }

        public static void setBatteOff()
        {
            lock (obj)
            {
                BattleOn = false;
                enemyobj = null;
                if(gmind!=null)
                {
                    gmind.Kill();
                }
            }
        }

        public static bool isBattleOn()
        {
            lock (obj)
            {
                return BattleOn;
            }
        }

        public static int TotalItemInInventory()
        {
            return inventory.itemList.Count;
           
        }

        public static void UpdateScore()
        {
            score = 0;
            foreach (Item item in inventory.itemList)
            {
                if (item is Gold)
                {
                    score += ((Gold)item).Price;
                }
                else if (item is Silver)
                {
                    score += ((Silver)item).Price;
                }
            }
        }

       

            public static bool IsKeyInInventory()
        {
            foreach (Item item in inventory.itemList)
            {
                if (item is Key)
                {
                    return true;
                }
            }
            return false;
        }

        public static void RemoveKeyFromInventory()
        {
            foreach (Item item in inventory.itemList)
            {
                if (item is Key)
                {
                    inventory.Remove(item);
                    break;
                }
            }
        }

        public static Key GetFirstKeyInInventory()
        {
            foreach (Item item in inventory.itemList)
            {
                if (item is Key)
                {
                    return (Key)item;
                }
            }
            return null;
        }

        public static bool IsArmourInInventory()
        {
            foreach (Item item in inventory.itemList)
            {
                if (item is IronArmour || item is WoodenArmour)
                {
                    return true;
                }
            }
            return false;
        }

        public static void RemoveArmourFromInventory()
        {
            foreach (Item item in inventory.itemList)
            {
                if (item is IronArmour || item is WoodenArmour)
                {
                    inventory.Remove(item);
                    break;
                }
            }
        }

        public static void AddArmourToInventory(Item item)
        {
            if (item is IronArmour || item is WoodenArmour)
            {
                inventory.Add(item);
            }
        }

        public static Item  GetFirstArmourInInventory()
        {
            foreach (Item item in inventory.itemList)
            {
                if (item is IronArmour || item is WoodenArmour)
                {
                      return item;
                }
            }
            return null;
        }

        public static bool IsSwordInInventory()
        {
            foreach (Item item in inventory.itemList)
            {
                if (item is IronSword || item is WoodenSword)
                {
                    return true;
                }
            }
            return false;
        }


        //Rmove the first sword
        public static void RemoveSwordFromInventory()
        {
            foreach (Item item in inventory.itemList)
            {
                if (item is IronSword || item is WoodenSword)
                {
                    inventory.Remove(item);
                    break;
                }
            }
        }

        public static void AddSwordToInventory(Item item)
        {
             if (item is IronSword || item is WoodenSword)
             {
                    inventory.Add(item);
              }
        }

        public static Item GetFirstSwordInInventory()
        {
            foreach (Item item in inventory.itemList)
            {
                if (item is IronSword || item is WoodenSword)
                {
                    return item;
                }
            }
            return null;
        }

        public static int CountGoldInInventory()
        {
            int count = 0;
            foreach (Item item in inventory.itemList)
            {
                if (item is Gold)
                {
                    count++;
                }
            }

            return count;
        }


        public static int CountSilverInInventory()
        {
            int count = 0;
            foreach (Item item in inventory.itemList)
            {
                if (item is Silver)
                {
                    count++;
                }
            }

            return count;
        }

        public static int CountHealthPotionInInventory()
        {
            int count = 0;
            foreach (Item item in inventory.itemList)
            {
                if (item is HealthPotion)
                {
                    count++;
                }
            }

            return count;
        }

        public static int CounKeyInInventory()
        {
            int count = 0;
            foreach (Item item in inventory.itemList)
            {
                if (item is Key)
                {
                    count++;
                }
            }

            return count;
        }

        public static int CountArmourInInventory()
        {
            int count = 0;
            foreach (Item item in inventory.itemList)
            {
                if (item is IronArmour || item is WoodenArmour)
                {
                    count++;
                }
            }

            return count;
        }

        public static int CountSwordInInventory()
        {
            int count = 0;
            foreach (Item item in inventory.itemList)
            {
                if (item is IronSword || item is WoodenSword)
                {
                    count++;
                }
            }

            return count;
        }


        private GameState ()
        {}

        public static void ReleaseEnemies(int currentRoomNo, Object enmobj)
        {
            GameRoom gm;
            GameWorldInfo gwinfo;
            
            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);
            gwinfo = GameDataFactory.curr_gwinfo;
          
            lock (ScreenManager.LockSection)
            {
                // Release Monster
                //noOfMonster = ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).MonsterList.Count;
                if (((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).MonsterList.Count > 0 &&
                    !((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).EnemiesStarted)
                {
                    foreach (Object obj in ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).MonsterList)
                    {
                        if (obj is ILivingOrganism && !Object.Equals(obj, enmobj))
                        {
                            ((ILivingOrganism)obj).WakeUp();
                        }

                    }

                    ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).EnemiesStarted = true;
                }
                // Release Globin
                //noOfGlobin = ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList.Count;
                if (((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList.Count > 0)
                {
                    foreach (Object obj in ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList)
                    {
                        if (obj is ILivingOrganism && !Object.Equals(obj, enmobj))
                        {
                            ((ILivingOrganism)obj).WakeUp();
                        }

                    }

                }
            }
        }


        public static void SuspendEnemies(int currentRoomNo, Object enmobj)
        {
            GameRoom gm;
            GameWorldInfo gwinfo;

            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);
            gwinfo = GameDataFactory.curr_gwinfo;

            lock (ScreenManager.LockSection)
            {
                if (((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).MonsterList.Count > 0 &&
                    ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).EnemiesStarted)
                {
                    foreach (Object obj in ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).MonsterList)
                    {
                        if (obj is ILivingOrganism && !Object.Equals(obj, enmobj))
                        {
                            ((ILivingOrganism)obj).Sleep();
                        }
                    }
                      ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).EnemiesStarted = false;
                }
                // Release Monster
                //noOfGlobin = ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList.Count;
                if (((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList.Count > 0)
                {
                    foreach (Object obj in ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList)
                    {
                        if (obj is ILivingOrganism && !Object.Equals(obj, enmobj))
                        {
                            ((ILivingOrganism)obj).Sleep();
                        }
                    }
                }
            }

        }

        public static void ReleaseEnemies(int currentRoomNo)
        {
            GameRoom gm;
            GameWorldInfo gwinfo;

            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);
            gwinfo = GameDataFactory.curr_gwinfo;

            lock (ScreenManager.LockSection)
            {
                // Release Monster
                //noOfMonster = ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).MonsterList.Count;
                if (((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).MonsterList.Count > 0 &&
                    !((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).EnemiesStarted)
                {
                    foreach (Object obj in ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).MonsterList)
                    {
                        if (obj is ILivingOrganism)
                        {
                            ((ILivingOrganism)obj).WakeUp();
                        }
                    }

                    ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).EnemiesStarted = true;
                }
                // Release Globin
                //noOfGlobin = ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList.Count;
                if (((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList.Count > 0)
                {
                    foreach (Object obj in ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList)
                    {
                        if (obj is ILivingOrganism)
                        {
                            ((ILivingOrganism)obj).WakeUp();
                        }

                    }

                }
            }
        }


        public static void SuspendEnemies(int currentRoomNo)
        {
            GameRoom gm;
            GameWorldInfo gwinfo;

            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);
            gwinfo = GameDataFactory.curr_gwinfo;

            lock (ScreenManager.LockSection)
            {
                if (((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).MonsterList.Count > 0 &&
                    ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).EnemiesStarted)
                {
                    foreach (Object obj in ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).MonsterList)
                    {
                        if (obj is ILivingOrganism)
                        {
                            ((ILivingOrganism)obj).Sleep();
                        }
                    }
                      ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).EnemiesStarted = false;
                }
                // Release Monster
                //noOfGlobin = ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList.Count;
                if (((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList.Count > 0)
                {
                    foreach (Object obj in ((GameRoom)gwinfo.GetGameRoombyNumber(currentRoomNo)).GlobinList)
                    {
                        if (obj is ILivingOrganism)
                        {
                            ((ILivingOrganism)obj).Sleep();
                        }

                    }
                }
            }

        }


    }
}
