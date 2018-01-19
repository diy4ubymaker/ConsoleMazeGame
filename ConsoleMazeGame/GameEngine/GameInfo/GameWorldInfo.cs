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
    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;
    using DIY4UMazeGame.GameScreenLib.Screens;
    using DIY4UMazeGame.GameEngine.GameSearch;
    using DIY4UMazeGame.GameEngine.GameInfo.Items;


    public class GameWorldInfo
    {
        public WorldMap WorldMapInfo { get; set; } = null;
        public List<RoomMap> RoomMapList { get; set; } = null;
        public List<Point> dropPointlist { get; set; } = null;
        public List<BoundaryPoint>[] boundaryPointlist = null;

        public int NumberOfMonster { get; set; } = 0;
        public int NumberOfGlobin { get; set; } = 0;


        public int NumberOfRooms = 0;
        public Player The_Player = null;

        public List<GameRoom> GameRoomList { get; set; } = new List<GameRoom>();

        //Main Data Structure 
        public GameWorldInfo()
        {
            RoomMapFileInfo rmmapInfo = null;

            WorldMapInfo = new WorldMap();
            rmmapInfo = RoomInfoFileReader.ReadRoomMapInfo(ConfigManager.RoomInfoFile);

            RoomMapList = rmmapInfo.roomaplist;
            dropPointlist = rmmapInfo.dropPointlist;
            boundaryPointlist = rmmapInfo.boundaryPointlist;

            if (RoomMapList != null)
                NumberOfRooms = RoomMapList.Count;

            The_Player = new Player(0, 0);  //Init the Player

            GameRoom rm = null;

            for (int roomindex = 0; roomindex < NumberOfRooms; roomindex++)
            {
                GameRoomList.Add(new GameRoom(RoomMapList.ElementAt(roomindex)));
                rm = GameRoomList.ElementAt(roomindex);
                rm.GenerateEnvrionemnt();
                NumberOfMonster = NumberOfMonster + rm.MonsterList.Count;
                NumberOfGlobin = NumberOfGlobin + rm.GlobinList.Count;

            }

            //Generate item in room

            int maxnoitem = ConfigManager.MaxChestPerWorld;
            int rmindex = 0;
            Random random = ConfigManager.RandomGen;
            Dictionary<string, string> ItemsHashTable = new Dictionary<string, string>();
            int col = 0;
            int row = 0;
            Chest chestobj = null;
            String key = null;

            //Chest First
            for (int item = 0; item < maxnoitem; item++)
            {
                while (true)
                {
                    rmindex = random.Next(0, NumberOfRooms - 1);   //Get a Random Room 

                    if (!ItemsHashTable.ContainsKey(Convert.ToString(rmindex)))
                    {
                        ItemsHashTable.Add(Convert.ToString(rmindex), Convert.ToString(rmindex));
                        rm = GameRoomList.ElementAt(rmindex);

                        while (true)
                        {
                            col = random.Next(
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                            row = random.Next(0, RoomMap.RoomHeight - 1);
                            if (!rm.IsLocationInRoomTaken(col, row) && rm.Room_Map.IsEmpty(col, row))
                            {
                                key = col + "-" + row;
                                chestobj = new Chest("Treasure Chest", col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos, key);
                                rm.ChestList.Add(chestobj);
                                rm.ItemsHashTable.Add(key, chestobj);
                                rm.TotalItem++;
                                break;
                            }
                        }
                    }
                    break;
                }
            }

            //Key Next
            maxnoitem = ConfigManager.MaxKeyPerWorld;
            Key keyobj = null;
            ItemsHashTable = new Dictionary<string, string>();

            for (int item = 0; item < maxnoitem; item++)
            {

                while (true)
                {
                    rmindex = random.Next(0, NumberOfRooms - 1);   //Get a Random Room 

                    if (!ItemsHashTable.ContainsKey(Convert.ToString(rmindex)))
                    {
                        ItemsHashTable.Add(Convert.ToString(rmindex), Convert.ToString(rmindex));
                        rm = GameRoomList.ElementAt(rmindex);

                        while (true)
                        {
                            col = random.Next(
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                            row = random.Next(0, RoomMap.RoomHeight - 1);
                            if (!rm.IsLocationInRoomTaken(col, row) && rm.Room_Map.IsEmpty(col, row))
                            {
                                key = col + "-" + row;
                                keyobj = new Key("Treasure Chest Key", col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos, key);
                                rm.KeyList.Add(keyobj);
                                rm.ItemsHashTable.Add(key, keyobj);
                                rm.TotalItem++;
                                break;
                            }
                        }
                    }
                    break;
                }
            }

            //Iron Armour Next
            maxnoitem = ConfigManager.MaxIronArmourPerWorld;
            IronArmour amobj = null;
            ItemsHashTable = new Dictionary<string, string>();

            for (int item = 0; item < maxnoitem; item++)
            {

                while (true)
                {
                    rmindex = random.Next(0, NumberOfRooms - 1);   //Get a Random Room 

                    if (!ItemsHashTable.ContainsKey(Convert.ToString(rmindex)))
                    {
                        ItemsHashTable.Add(Convert.ToString(rmindex), Convert.ToString(rmindex));
                        rm = GameRoomList.ElementAt(rmindex);

                        while (true)
                        {
                            col = random.Next(
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                            row = random.Next(0, RoomMap.RoomHeight - 1);
                            if (!rm.IsLocationInRoomTaken(col, row) && rm.Room_Map.IsEmpty(col, row))
                            {
                                key = col + "-" + row;
                                amobj = new IronArmour("Iron Armour", col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos, key);
                                rm.ArmourList.Add(amobj);
                                rm.ItemsHashTable.Add(key, amobj);
                                rm.TotalItem++;
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            
            //Wooden Armour Next
            maxnoitem = ConfigManager.MaxWoodenArmourPerWorld;
            WoodenArmour wmobj = null;
            ItemsHashTable = new Dictionary<string, string>();

            for (int item = 0; item < maxnoitem; item++)
            {

                while (true)
                {
                    rmindex = random.Next(0, NumberOfRooms - 1);   //Get a Random Room 

                    if (!ItemsHashTable.ContainsKey(Convert.ToString(rmindex)))
                    {
                        ItemsHashTable.Add(Convert.ToString(rmindex), Convert.ToString(rmindex));
                        rm = GameRoomList.ElementAt(rmindex);

                        while (true)
                        {
                            col = random.Next(
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                            row = random.Next(0, RoomMap.RoomHeight - 1);
                            if (!rm.IsLocationInRoomTaken(col, row) && rm.Room_Map.IsEmpty(col, row))
                            {
                                key = col + "-" + row;
                                wmobj = new WoodenArmour("Wooden Armour", col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos, key);
                                rm.ArmourList.Add(wmobj);
                                rm.ItemsHashTable.Add(key, wmobj);
                                rm.TotalItem++;
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            

            //Iron Sword Next
            maxnoitem = ConfigManager.MaxIronSwordPerWorld;
            IronSword iswbj = null;
            ItemsHashTable = new Dictionary<string, string>();

            for (int item = 0; item < maxnoitem; item++)
            {

                while (true)
                {
                    rmindex = random.Next(0, NumberOfRooms - 1);   //Get a Random Room 

                    if (!ItemsHashTable.ContainsKey(Convert.ToString(rmindex)))
                    {
                        ItemsHashTable.Add(Convert.ToString(rmindex), Convert.ToString(rmindex));
                        rm = GameRoomList.ElementAt(rmindex);

                        while (true)
                        {
                            col = random.Next(
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                            row = random.Next(0, RoomMap.RoomHeight - 1);
                            if (!rm.IsLocationInRoomTaken(col, row) && rm.Room_Map.IsEmpty(col, row))
                            {
                                key = col + "-" + row;
                                iswbj = new IronSword("Iron Sword", col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos, key);
                                rm.WeaponList.Add(iswbj);
                                rm.ItemsHashTable.Add(key, iswbj);
                                rm.TotalItem++;
                                break;
                            }
                        }
                    }
                    break;
                }
            }


            //Wooden Sword Next
            maxnoitem = ConfigManager.MaxWoodenSwordPerWorld;
            WoodenSword wswbj = null;
            ItemsHashTable = new Dictionary<string, string>();

            for (int item = 0; item < maxnoitem; item++)
            {

                while (true)
                {
                    rmindex = random.Next(0, NumberOfRooms - 1);   //Get a Random Room 

                    if (!ItemsHashTable.ContainsKey(Convert.ToString(rmindex)))
                    {
                        ItemsHashTable.Add(Convert.ToString(rmindex), Convert.ToString(rmindex));
                        rm = GameRoomList.ElementAt(rmindex);

                        while (true)
                        {
                            col = random.Next(
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + 1,
                            ((RoomMap.RoomWidth - RoomMap.RoomEffectiveWidth) / 2) + RoomMap.RoomEffectiveWidth - 1);

                            row = random.Next(0, RoomMap.RoomHeight - 1);
                            if (!rm.IsLocationInRoomTaken(col, row) && rm.Room_Map.IsEmpty(col, row))
                            {
                                key = col + "-" + row;
                                wswbj = new WoodenSword("Wooden Sword", col + PlayUIWorldMap.mapBoxColPos, row + PlayUIWorldMap.mapBoxRowPos, key);
                                rm.WeaponList.Add(wswbj);
                                rm.ItemsHashTable.Add(key, wswbj);
                                rm.TotalItem++;
                                break;
                            }
                        }
                    }
                    break;
                }
            }

        }

        public int GetCurrentRoom(int col, int row)
        {
            int roomNumber = 0;
            foreach (RoomMap item in RoomMapList)
            {
                roomNumber++;
                if (
                   (col >= item.TopLeftCol && row >= item.TopLeftRow) &&
                   (col <= item.BottomRightCol && row <= item.BottomRightRow)
                 )
                {
                    return roomNumber;
                }
            }

            return -1;

        }

        public bool IsRoomPointOccupied(int col, int row, int roomnumber)
        {
            GameRoom gm;

            if (roomnumber >= 1 && roomnumber <= NumberOfRooms)
            {
                gm = GameRoomList.ElementAt(roomnumber - 1);
                if (gm.ItemsHashTable.ContainsKey(col + "-" + row))
                {
                    return true;
                }
                return false;
            }
            else
                return true;
        }

        public Object CheckRoomPointOccupied(int col, int row, int roomnumber)
        {
            GameRoom gm;

            if (roomnumber >= 1 && roomnumber <= NumberOfRooms)
            {
                gm = GameRoomList.ElementAt(roomnumber - 1);
                if (gm.ItemsHashTable.ContainsKey(col + "-" + row))
                {
                    return gm.ItemsHashTable[col + "-" + row];
                }
                return null;
            }
            else
                return null;
        }


        public Point GetRoomWorldMapCentrePos(int roomNumner)
        {
            RoomMap item = null;
            Point pos = new Point(0, 0);

            item = RoomMapList.ElementAt(roomNumner - 1);

            if (item != null)
            {
                pos = new Point(
                   item.TopLeftCol + (item.BottomRightCol - item.TopLeftCol) / 2,
                   item.TopLeftRow + (item.BottomRightRow - item.TopLeftRow) / 2
                    );
            }
            return pos;
        }

        public void updateRoomnByNumber(int roomnumber)
        {
            GameRoom gm;

            if (roomnumber >= 1 && roomnumber <= NumberOfRooms)
            {
                gm = GameRoomList.ElementAt(roomnumber - 1);
                gm.UpdateRoomItem();
            }
        }


        public RoomMapSearchResult SearchRoom(int roomnumber, int col, int row, int dir)
        {
            // ONLY ALL is implemented
            RoomMapSearchResult searchresult = null;
            string key = "";
            GameRoom gm = null;
            int search_col = 0;
            int search_row = 0;
            RoomMapSearchResult.SEARCH_TYPE searchtype = RoomMapSearchResult.SEARCH_TYPE.ALL_DIRECTION;

            if (roomnumber >= 1 && roomnumber <= NumberOfRooms)
            {
                search_col = col;
                search_row = row;
                gm = GameRoomList.ElementAt(roomnumber - 1);
                searchtype = (RoomMapSearchResult.SEARCH_TYPE)dir;

                searchresult = new RoomMapSearchResult();

                if (searchtype == RoomMapSearchResult.SEARCH_TYPE.ALL_DIRECTION)
                {
                    //North
                    search_col = col;
                    search_row = row;
                    search_row--;
                    key = search_col + "-" + search_row;
                    if (gm.ItemsHashTable.ContainsKey(key) && 
                                                ((IAvailable)gm.ItemsHashTable[key]).isAvailable)
                    {
                        searchresult.ItemFound++;
                        searchresult.North_Item = gm.ItemsHashTable[key];
                        if(AutoAddToInventory(gm.ItemsHashTable[key], searchresult))
                        {
                            gm.ItemsHashTable.Remove(key);
                        }
                        else
                        {
                            ProcessInventory(gm.ItemsHashTable[key], searchresult);
                        }
                    }

                    //North East
                    search_col = col;
                    search_row = row;
                    search_row--;
                    search_col++;
                    key = search_col + "-" + search_row;
                    if (gm.ItemsHashTable.ContainsKey(key) &&
                                                ((IAvailable)gm.ItemsHashTable[key]).isAvailable)
                    {
                        searchresult.ItemFound++;
                        searchresult.NorthEast_Item = gm.ItemsHashTable[key];

                        if (AutoAddToInventory(gm.ItemsHashTable[key], searchresult))
                        {
                            gm.ItemsHashTable.Remove(key);
                        }
                        else
                        {
                            ProcessInventory(gm.ItemsHashTable[key], searchresult);
                        }
                    }

                    //North West
                    search_col = col;
                    search_row = row;
                    search_row--;
                    search_col--;
                    key = search_col + "-" + search_row;
                    if (gm.ItemsHashTable.ContainsKey(key) &&
                                                ((IAvailable)gm.ItemsHashTable[key]).isAvailable)
                    {
                        searchresult.ItemFound++;
                        searchresult.NorthWest_Item = gm.ItemsHashTable[key];
                        if (AutoAddToInventory(gm.ItemsHashTable[key], searchresult))
                        {
                            gm.ItemsHashTable.Remove(key);
                        }
                        else
                        {
                            ProcessInventory(gm.ItemsHashTable[key], searchresult);
                        }
                    }


                    //South
                    search_col = col;
                    search_row = row;
                    search_row++;
                    key = search_col + "-" + search_row;
                    if (gm.ItemsHashTable.ContainsKey(key) &&
                                                ((IAvailable)gm.ItemsHashTable[key]).isAvailable)
                    {
                        searchresult.ItemFound++;
                        searchresult.South_Item = gm.ItemsHashTable[key];
                        if (AutoAddToInventory(gm.ItemsHashTable[key], searchresult))
                        {
                            gm.ItemsHashTable.Remove(key);
                        }
                        else
                        {
                            ProcessInventory(gm.ItemsHashTable[key], searchresult);
                        }
                    }

                    //South East
                    search_col = col;
                    search_row = row;
                    search_row++;
                    search_col++;
                    key = search_col + "-" + search_row;
                    if (gm.ItemsHashTable.ContainsKey(key) &&
                                                ((IAvailable)gm.ItemsHashTable[key]).isAvailable)
                    {
                        searchresult.ItemFound++;
                        searchresult.SouthEast_Item = gm.ItemsHashTable[key];
                        if (AutoAddToInventory(gm.ItemsHashTable[key], searchresult))
                        {
                            gm.ItemsHashTable.Remove(key);
                        }
                        else
                        {
                            ProcessInventory(gm.ItemsHashTable[key], searchresult);
                        }
                    }

                    //South West
                    search_col = col;
                    search_row = row;
                    search_row++;
                    search_col--;
                    key = search_col + "-" + search_row;
                    if (gm.ItemsHashTable.ContainsKey(key) &&
                                                ((IAvailable)gm.ItemsHashTable[key]).isAvailable)
                    {
                        searchresult.ItemFound++;
                        searchresult.West_Item = gm.ItemsHashTable[key];
                        if (AutoAddToInventory(gm.ItemsHashTable[key], searchresult))
                        {
                            gm.ItemsHashTable.Remove(key);
                        }
                        else
                        {
                            ProcessInventory(gm.ItemsHashTable[key], searchresult);
                        }
                    }


                    //East
                    search_col = col;
                    search_row = row;
                    search_col++;
                    key = search_col + "-" + search_row;
                    if (gm.ItemsHashTable.ContainsKey(key) &&
                                                ((IAvailable)gm.ItemsHashTable[key]).isAvailable)
                    {
                        searchresult.ItemFound++;
                        searchresult.East_Item = gm.ItemsHashTable[key];
                        if (AutoAddToInventory(gm.ItemsHashTable[key], searchresult))
                        {
                            gm.ItemsHashTable.Remove(key);
                        }
                        else
                        {
                            ProcessInventory(gm.ItemsHashTable[key], searchresult);
                        }
                    }

                    //West
                    search_col = col;
                    search_row = row;
                    search_col--;
                    key = search_col + "-" + search_row;
                    if (gm.ItemsHashTable.ContainsKey(key) &&
                                                ((IAvailable)gm.ItemsHashTable[key]).isAvailable)
                    {
                        searchresult.ItemFound++;
                        searchresult.East_Item = gm.ItemsHashTable[key];
                        if (AutoAddToInventory(gm.ItemsHashTable[key], searchresult))
                        {
                            gm.ItemsHashTable.Remove(key);
                        }
                        else
                        {
                            ProcessInventory(gm.ItemsHashTable[key], searchresult);
                        }
                    }

                }
            }

            return searchresult;
        }

        //Those can be auto pickup
        private bool AutoAddToInventory(Object obj, RoomMapSearchResult searchresult)
        {
            if (obj is Gold || obj is Silver || obj is HealthPotion)
            {
                searchresult.AutoCollectedList.Add(obj);
                if (obj is ITakable)
                {
                    ((ITakable)obj).Take();
                }
                return true;
            }
       
            return false;
        }

        private void ProcessInventory(Object obj, RoomMapSearchResult searchresult)
        {
            if (obj is Chest )
            {
                //searchresult.ChestList.Add(obj);
                searchresult.CollectedList.Add(obj);
            }
            else if (obj is IronArmour || obj is WoodenArmour)
            {
                //searchresult.ArmourList.Add(obj);
                searchresult.CollectedList.Add(obj);
            }
            else if (obj is IronSword || obj is WoodenSword)
            {
                //searchresult.SwordList.Add(obj);
                searchresult.CollectedList.Add(obj);
            }
            else if (obj is Key)
            {
                //searchresult.KeyList.Add(obj);
                searchresult.CollectedList.Add(obj);
            }
            else if (obj is Globin || obj is Monster)
            {
                //searchresult.EnemyList.Add(obj);
                searchresult.CollectedList.Add(obj);
            }

        }

        public GameRoom GetGameRoombyNumber(int roomnumber)
        {
            GameRoom gm = null;

            if (roomnumber >= 1 && roomnumber <= NumberOfRooms)
            {
                gm = GameRoomList.ElementAt(roomnumber - 1);
                
            }
            return gm;
        }

        public void UpdatePlyerHealth(int val)
        {
           
            if (The_Player.GetHealthLevel() + val <= 100)
            {
                The_Player.SetHealthLevel(The_Player.GetHealthLevel() + val);
                //*******************************************
                PlayUIBottomItem.updateMessageBox(
                    ConfigManager.GetStringResource(17) + "\r\n" +
                    ConfigManager.GetStringResource(18) + val + "\r\n" +
                    ConfigManager.GetStringResource(20) + val);
                //*******************************************
            }
            else
            {
                //*******************************************
                PlayUIBottomItem.updateMessageBox(
                    ConfigManager.GetStringResource(17) + "\r\n" +
                    ConfigManager.GetStringResource(18) + val + "\r\n" +
                    ConfigManager.GetStringResource(19));
                //*******************************************
            }
        }

        public bool IsPlayerWithinAttackRange(
                                    int col, int row)
        {
            Dictionary<string, string> ItemsHashTable = new Dictionary<string, string>();

            //First Ring
            ItemsHashTable.Add((col+1) + "-" + (row), "");
            ItemsHashTable.Add((col-1) + "-" + (row), "");
            ItemsHashTable.Add((col) + "-" + (row-1), "");
            ItemsHashTable.Add((col) + "-" + (row+1), "");
            ItemsHashTable.Add((col+1) + "-" + (row-1), "");
            ItemsHashTable.Add((col -1) + "-" + (row-1), "");
            ItemsHashTable.Add((col + 1) + "-" + (row+1), "");
            ItemsHashTable.Add((col - 1) + "-" + (row+1), "");

            // Second Ring
            /*
            ItemsHashTable.Add((col + 2) + "-" + (row), "");
            ItemsHashTable.Add((col - 2) + "-" + (row), "");
            ItemsHashTable.Add((col) + "-" + (row - 2), "");
            ItemsHashTable.Add((col) + "-" + (row + 2), "");
            ItemsHashTable.Add((col + 2) + "-" + (row - 2), "");
            ItemsHashTable.Add((col - 2) + "-" + (row - 2), "");
            ItemsHashTable.Add((col + 2) + "-" + (row + 2), "");
            ItemsHashTable.Add((col - 2) + "-" + (row + 2), "");
            */
            return The_Player.YouThere(ItemsHashTable);
        }






    }
}
