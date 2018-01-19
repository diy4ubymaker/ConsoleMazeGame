using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DIY4UMazeGame.GameEngine.GameInfo
{
    using DIY4UMazeGame.GameEngine.GameMaps;
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.GameEngine.GameInfo.Roles;
    using DIY4UMazeGame.GameScreenLib.Screens;
    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;
    using DIY4UMazeGame.GameEngine.GameInfo.Items;
    
    public class GameRoom
    {
        public RoomMap Room_Map { get; set; } = null;
        public Dictionary<string, object> ItemsHashTable { get; set; } = new Dictionary<string, object>();
        
        // Gold and Silver does has list of its own
        public List<Monster> MonsterList = new List<Monster>();
        public List<Globin> GlobinList = new List<Globin>();

        public List<Key> KeyList = new List<Key>();
        public List<IShield> ArmourList = new List<IShield>();
        public List<IWeapon> WeaponList = new List<IWeapon>();

        public List<Chest> ChestList = new List<Chest>();

        public int TotalGoldItem  { get; set; } = 0;
        public int TotalSilverItem { get; set; } = 0;
        public int TotalHealthPotionItem { get; set; } = 0;
        public int TotalItem { get; set; } = 0;

        public bool EnemiesStarted { get; set; } = false;


        public GameRoom(RoomMap Room_MapIn)
        {
            Room_Map = Room_MapIn;
        }

        public void GenerateEnvrionemnt()
        {
            int col = 0;
            int row = 0;
            int maxnoitem = ConfigManager.MaxEnemyPerRoom;
            Random random = ConfigManager.RandomGen;

            int numofIem;
            string key = null;
            Monster monsterobj = null;
            Globin globinobj = null;
            Gold goldobj = null;
            Silver silverobj = null;
            HealthPotion healthpobj = null;

            //Generate Gold In Room
            //maxnoitem = random.Next(1, ConfigManager.MaxGoldItemPerRoom);
            maxnoitem = ConfigManager.MaxGoldItemPerRoom;
            for (numofIem = 0; numofIem < maxnoitem; numofIem++)
            {
                while (true)
                {
                    col = random.Next(((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                    ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                    row = random.Next(0, RoomMap.RoomHeight - 1);

                    if (Room_Map.IsEmpty(col, row))
                    {
                         key = col + "-" + row;
                        if (!ItemsHashTable.ContainsKey(key))
                        {
                            goldobj = new Gold("Gold Bar", col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos,key);
                            ItemsHashTable.Add(key, goldobj);
                            TotalGoldItem++;
                            TotalItem++;
                            break;
                        }
                    }
                }
            }

            //Generate Silver In Room
            //maxnoitem = random.Next(1, ConfigManager.MaxSilverItemPerRoom);
            maxnoitem = ConfigManager.MaxSilverItemPerRoom;
            for (numofIem = 0; numofIem < maxnoitem; numofIem++)
            {
                while (true)
                {
                    col = random.Next(((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                    ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                    row = random.Next(0, RoomMap.RoomHeight - 1);

                    if (Room_Map.IsEmpty(col, row))
                    {
                        key = col + "-" + row;
                        if (!ItemsHashTable.ContainsKey(key))
                        {
                            silverobj = new Silver("Silver Coin", col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos,key);
                            ItemsHashTable.Add(key, silverobj);
                            TotalSilverItem++;
                            TotalItem++;
                            break;
                        }
                    }
                }
            }


            //Generate Health Potion In Room
            //maxnoitem = random.Next(1, ConfigManager.MaxHealthPotionPerRoom);
            maxnoitem = ConfigManager.MaxHealthPotionPerRoom;
            for (numofIem = 0; numofIem < maxnoitem; numofIem++)
            {
                while (true)
                {
                    col = random.Next(((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                    ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                    row = random.Next(0, RoomMap.RoomHeight - 1);

                    if (Room_Map.IsEmpty(col, row))
                    {
                        key = col + "-" + row;
                        if (!ItemsHashTable.ContainsKey(key))
                        {
                            healthpobj = new HealthPotion("Health Potion", 
                                col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos, key);
                            ItemsHashTable.Add(key, healthpobj);
                            TotalHealthPotionItem++;
                            TotalItem++;
                            break;
                        }
                    }
                }
            }

            //Enemies are not item.
            //Generate different Enimies type
            //maxnoitem = random.Next(1, ConfigManager.MaxMonsterPerRoom);
            maxnoitem = ConfigManager.MaxMonsterPerRoom;
            //Monster first
            for (numofIem = 0; numofIem < maxnoitem; numofIem++)
            {
                /*
                col = random.Next(
                    ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                    ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                row = random.Next(0, RoomMap.RoomHeight - 1);
                */
                while (true)
                {
                    col = random.Next(((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                    ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                    row = random.Next(0, RoomMap.RoomHeight - 1);

                    if (Room_Map.IsEmpty(col, row))
                    {
                        //key = (col + PlayUIWorldMap.mapBoxColPos)+ "-" + (row + PlayUIWorldMap.mapBoxRowPos);
                        key = col + "-" + row;

                        if (!ItemsHashTable.ContainsKey(key))
                        {
                            monsterobj = new Monster(col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos,key, Room_Map);
                            //ItemsHashTable.Add(key, monsterobj);
                            MonsterList.Add(monsterobj);

                            break;
                        }
                    }
                }
            }

            //maxnoitem = random.Next(1, ConfigManager.MaxGlobinPerRoom);
            maxnoitem = ConfigManager.MaxGlobinPerRoom;
            // Globin
            for (numofIem = 0; numofIem < maxnoitem; numofIem++)
            {
                /*
                col = random.Next(
                    ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                    ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                row = random.Next(0, RoomMap.RoomHeight - 1);
                */
                while (true)
                {
                    col = random.Next(((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                    ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                    row = random.Next(0, RoomMap.RoomHeight - 1);

                    if (Room_Map.IsEmpty(col, row))
                    {
                        //key = (col + PlayUIWorldMap.mapBoxColPos)+ "-" + (row + PlayUIWorldMap.mapBoxRowPos);
                        key = col + "-" + row;

                        if (!ItemsHashTable.ContainsKey(key))
                        {
                            globinobj = new Globin(col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos,key, Room_Map);
                            //ItemsHashTable.Add(key, globinobj);
                            GlobinList.Add(globinobj);
                            break;
                        }
                    }
                }
            }
        }

        public void UpdateRoomItem()
        {
            Object item = null;

            lock (ScreenManager.LockSection)
            {
                foreach (KeyValuePair<string, object> Pair in ItemsHashTable)
                {
                    item = Pair.Value;
                    if (item is IPrintable)
                    {
                        ((IPrintable)item).Update();
                    }
                }
            }

        }

        public bool IsLocationInRoomTaken(int col, int row)
        {
            string key = null;

            key = col + "-" + row;

            return ItemsHashTable.ContainsKey(key);
            
        }

    }
}
