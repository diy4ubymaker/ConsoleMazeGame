using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace DIY4UMazeGame.GameEngine.GamePhases
{
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.GameEngine.GameInfo;
    using DIY4UMazeGame.GameScreenLib.Screens;
    using DIY4UMazeGame.GameEngine.GameMaps;
    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;
    using DIY4UMazeGame.GameEngine.GameInfo.Roles;
    using DIY4UMazeGame.GameEngine.GameSearch;
    using DIY4UMazeGame.Utilities;
    using DIY4UMazeGame.GameEngine.GameInfo.Items;

    class GamePhasePlay
    {
        public static int WaitTime = 5 * 1000;  //millisecond
        private static GameWorldInfo gwinfo = null;

        private static int col = 10;  //Physical Postion on the screen.
        private static int row = 15;  //Physical Postion on the screen.
                     
        private static DIRECTION LastDirection = DIRECTION.DIR_UNKNOW;

        public enum DIRECTION
        {
            DIR_W_NORTH = 1,
            DIR_W_SOUTH = 0,
            DIR_W_EAST = 3,
            DIR_W_WEST = 2,
            DIR_R_NORTH = 0,
            DIR_R_SOUTH = 1,
            DIR_R_EAST = 2,
            DIR_R_WEST = 3,
            DIR_UNKNOW =-1
        };

        private static bool backFromQuit = false;

        public static void Init()
        {
            gwinfo = GameDataFactory.CreateInitialWorldData();
            GameState.currentGameMode = GameState.GAME_MODE.WORLD;

            PlayUIWorldMap.Init(gwinfo.WorldMapInfo.MapInfo);
              
            PlayUITopItem.Init();
            PlayUIBottomItem.Init();
            PlayUILeftPanel.Init();
            PlayUIRightPanel.Init();

            //****************************************************************************
            ScreenManager.printInitScreen();
            //************************************************************************************

            ScreenManager.ClearScreen();

            PlayUITopItem.Update();
            PlayUIWorldMap.Update();
            PlayUIBottomItem.Update();
        }


        // World Mode
        private static void processRoomMapInput(RoomMap roommap)
        {
            ConsoleKeyInfo keyInfo;
            RoomMap.MAPTYPE maptype;
            //bool itemexist = false;
            Object item = null;
            bool toContinue = true;

            gwinfo.updateRoomnByNumber(GameState.currentRoom);

            gwinfo.The_Player.Update();

            while (toContinue)
            {
                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            maptype = roommap.ItemAtPostion(
                               col - PlayUIWorldMap.mapBoxColPos,
                               row - 1 - PlayUIWorldMap.mapBoxRowPos);

                            item = gwinfo.CheckRoomPointOccupied(col - PlayUIWorldMap.mapBoxColPos,
                               row - 1 - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);

                            //if (maptype  == RoomMap.MAPTYPE.SPACE && !itemexist )
                            if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                            {
                                LastDirection = DIRECTION.DIR_R_NORTH;
                                gwinfo.The_Player.MoveNorth();
                                row--;
                                ScreenManager.moveOnInstruction();
                            }
                            else if (maptype == RoomMap.MAPTYPE.EXIT)
                            {
                                //currentMode = MAP_MODE.WORLD;
                                GameState.currentGameMode = GameState.GAME_MODE.WORLD;
                                GameState.subGameMode = GameState.GAME_SUBMODE.WORLD_PROCESSING;
                                return;
                            }
                            else ScreenManager.moveBlockInstruction();

                            break;

                        case ConsoleKey.RightArrow:
                            maptype = roommap.ItemAtPostion(
                                col + 1 - PlayUIWorldMap.mapBoxColPos,
                                row - PlayUIWorldMap.mapBoxRowPos);


                            item = gwinfo.CheckRoomPointOccupied(
                             col + 1 - PlayUIWorldMap.mapBoxColPos,
                             row - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);

                            if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                            {
                                LastDirection = DIRECTION.DIR_R_EAST;
                                gwinfo.The_Player.MoveEast();
                                col++;
                                ScreenManager.moveOnInstruction();
                            }
                            else if (maptype == RoomMap.MAPTYPE.EXIT)
                            {
                                //currentMode = MAP_MODE.WORLD;
                                GameState.currentGameMode = GameState.GAME_MODE.WORLD;
                                GameState.subGameMode = GameState.GAME_SUBMODE.WORLD_PROCESSING;
                                return;
                            }
                            else ScreenManager.moveBlockInstruction();

                            break;

                        case ConsoleKey.DownArrow:
                            maptype = roommap.ItemAtPostion(
                               col - PlayUIWorldMap.mapBoxColPos,
                               row + 1 - PlayUIWorldMap.mapBoxRowPos);



                            item = gwinfo.CheckRoomPointOccupied(
                            col - PlayUIWorldMap.mapBoxColPos,
                            row + 1 - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);

                            if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                            {
                                LastDirection = DIRECTION.DIR_R_SOUTH;
                                gwinfo.The_Player.MoveSouth();
                                row++;
                                ScreenManager.moveOnInstruction();
                            }
                            else if (maptype == RoomMap.MAPTYPE.EXIT)
                            {
                                //currentMode = MAP_MODE.WORLD;
                                GameState.currentGameMode = GameState.GAME_MODE.WORLD;
                                GameState.subGameMode = GameState.GAME_SUBMODE.WORLD_PROCESSING;
                                return;
                            }
                            else ScreenManager.moveBlockInstruction();

                            break;

                        case ConsoleKey.LeftArrow:
                            maptype = roommap.ItemAtPostion(
                                col - 1 - PlayUIWorldMap.mapBoxColPos,
                                row - PlayUIWorldMap.mapBoxRowPos);


                            item = gwinfo.CheckRoomPointOccupied(
                             col - 1 - PlayUIWorldMap.mapBoxColPos,
                             row - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);

                            if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                            {
                                LastDirection = DIRECTION.DIR_R_WEST;
                                gwinfo.The_Player.MoveWest();
                                col--;
                                ScreenManager.moveOnInstruction();
                            }
                            else if (maptype == RoomMap.MAPTYPE.EXIT)
                            {
                                //currentMode = MAP_MODE.WORLD;
                                GameState.currentGameMode = GameState.GAME_MODE.WORLD;
                                GameState.subGameMode = GameState.GAME_SUBMODE.WORLD_PROCESSING;
                                return;
                            }
                            else ScreenManager.moveBlockInstruction();

                            break;

                        case ConsoleKey.Escape:
                            GameState.subGameMode = GameState.GAME_SUBMODE.ROOM_QUIT;
                            toContinue = false;
                            break;

                    }  //end Switch

                }                             
                gwinfo.The_Player.Update();

                //Look Around
                LookAround(col - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos,
                    GameState.currentRoom);
                //Look Around

                
                if (!BattleisOn())
                {
                    toContinue = false;
                    break;
                }
                

                //End of Main Game Loop
            }

        }

        private static bool BattleisOn()
        {
            if (GameState.isBattleOn())
            {
                
                HandleBattle(col - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);

              
                ScreenManager.WaitForBufferClear();

                if (gwinfo.The_Player.HealthLevel <= 0)
                {
                   

                    HandleGameOver();

                    GameStateManager.SetNextState(GameStateTable.GAMESTATE_ENUM.GsGameOver);
                    return false;
                    
                }
                else if (gwinfo.NumberOfGlobin + gwinfo.NumberOfMonster == 0)
                {
                  

                    ScreenManager.WaitForBufferClear();

                    HandleVictory();

                    GameStateManager.SetNextState(GameStateTable.GAMESTATE_ENUM.GsCredits);
                    return false;
                }
                else if(GameState.LastWin)
                {
                    // You win
                    HandleWin();
                }



            }

            return true;
        }


            //Main Loop for World Mode
            private static void processWorldNMapInput()
            {
                ConsoleKeyInfo keyInfo;
                WorldMap.MAPTYPE maptype;
                bool toContinue = true;

                GameState.currentRoom = gwinfo.GetCurrentRoom(
                    col - PlayUIWorldMap.mapBoxColPos, 
                    row - PlayUIWorldMap.mapBoxRowPos);

                gwinfo.The_Player.Update();

                //GameState.subGameMode = GameState.GAME_SUBMODE.WORLD_QUIT;
                GameState.subGameMode = GameState.GAME_SUBMODE.WORLD_PROCESSING;

                //while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape
                while (toContinue)
                {
                    keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:

                            maptype = gwinfo.WorldMapInfo.ItemAtPostion(
                               col - PlayUIWorldMap.mapBoxColPos, row - 1 - PlayUIWorldMap.mapBoxRowPos);

                            if (maptype == WorldMap.MAPTYPE.SPACE)
                            {
                                LastDirection = DIRECTION.DIR_W_NORTH;
                                gwinfo.The_Player.MoveNorth();
                                row--;
                                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(6));
                            }
                            else if (maptype == WorldMap.MAPTYPE.EXIT)
                            {
                                // User At Exit of the World Map
                                toContinue = HandleExitWorld(col - PlayUIWorldMap.mapBoxColPos, 
                                    row - PlayUIWorldMap.mapBoxRowPos);
                            }
                            break;

                        case ConsoleKey.RightArrow:
                            maptype = gwinfo.WorldMapInfo.ItemAtPostion(
                               col + 1 - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos);

                            if( maptype == WorldMap.MAPTYPE.SPACE)
                            {
                                LastDirection = DIRECTION.DIR_W_EAST;
                                gwinfo.The_Player.MoveEast();
                                col++;
                                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(8));
                            }
                            else if (maptype == WorldMap.MAPTYPE.EXIT)
                            {
                                // User At Exit of the World Map
                                toContinue = HandleExitWorld(col - PlayUIWorldMap.mapBoxColPos,
                                    row - PlayUIWorldMap.mapBoxRowPos);
                            }
                            break;

                        case ConsoleKey.DownArrow:
                            maptype = gwinfo.WorldMapInfo.ItemAtPostion(
                               col - PlayUIWorldMap.mapBoxColPos, row + 1 - PlayUIWorldMap.mapBoxRowPos);

                            if (maptype == WorldMap.MAPTYPE.SPACE)
                            {
                                LastDirection = DIRECTION.DIR_W_SOUTH;
                                gwinfo.The_Player.MoveSouth();
                                row++;
                                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(7));
                            }
                            else if (maptype == WorldMap.MAPTYPE.EXIT)
                            {
                                // User At Exit of the World Map
                                toContinue =  HandleExitWorld(col - PlayUIWorldMap.mapBoxColPos,
                                    row - PlayUIWorldMap.mapBoxRowPos);
                            }
                            break;

                        case ConsoleKey.LeftArrow:
                            maptype = gwinfo.WorldMapInfo.ItemAtPostion(
                                col - 1 - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos);
                            if (maptype == WorldMap.MAPTYPE.SPACE)
                            {
                                LastDirection = DIRECTION.DIR_W_WEST;
                                gwinfo.The_Player.MoveWest();
                                col--;
                                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(9));
                            }
                            else if (maptype == WorldMap.MAPTYPE.EXIT)
                            {
                                // User At Exit of the World Map
                                toContinue =  HandleExitWorld(col - PlayUIWorldMap.mapBoxColPos,
                                    row - PlayUIWorldMap.mapBoxRowPos);
                            }
                            break;

                        case ConsoleKey.Escape:
                            GameState.subGameMode = GameState.GAME_SUBMODE.WORLD_QUIT;
                            toContinue = false;
                            break;
                    }

                    GameState.currentRoom = gwinfo.GetCurrentRoom(
                    col - PlayUIWorldMap.mapBoxColPos,
                    row - PlayUIWorldMap.mapBoxRowPos);

                    if (GameState.currentRoom != -1)
                    {
                        /*          
                        PlayUITopItem.RoomLabel.textItem = PlayUITopItem.RoomLableStr + "Room " + GameState.currentRoom;
                        PlayUITopItem.RoomLabel.draw();
                        */

                    GameState.currentGameMode = GameState.GAME_MODE.ROOM;
                    GameState.subGameMode = GameState.GAME_SUBMODE.ROOM_PROCESSING;
                                        
                    return;
                }
                else
                {
                    //ReleaseEnemies(GameState.currentRoom);
                    /*
                    PlayUITopItem.RoomLabel.textItem = PlayUITopItem.RoomLableStr + "Passage Way";
                    PlayUITopItem.RoomLabel.draw();
                    */
                }    
                
            }
        }

        public static void switchMode(RoomMap roommap)
        { 
            PlayUIWorldMap.mapbox.MapInfo = roommap.RoomInfo;
            PlayUIWorldMap.Update();
        }

        public static void Update(float dt)
        {
            Point pos;
            BoundaryPoint bpos;

                if (GameState.currentGameMode == GameState.GAME_MODE.WORLD)
                {
                    PlayUIWorldMap.mapbox.MapInfo = gwinfo.WorldMapInfo.MapInfo;
                    PlayUIWorldMap.Update();

                //**********************************************
                    ScreenManager.upDatePassageLocation();
                //*********************************************

                    if (GameState.subGameMode == GameState.GAME_SUBMODE.WORLD_DROPIN)   //Init Phase
                    {
                        Random random = ConfigManager.RandomGen;
                        int randomNumber = random.Next(0, gwinfo.dropPointlist.Count - 1);
                        pos = gwinfo.dropPointlist.ElementAt(randomNumber);

                        col = pos.X + PlayUIWorldMap.mapBoxColPos;
                        row = pos.Y + PlayUIWorldMap.mapBoxRowPos;

                        gwinfo.The_Player.SetLocation(col, row);

                        processWorldNMapInput();
                    }
                    else if (GameState.subGameMode == GameState.GAME_SUBMODE.WORLD_PROCESSING)
                    {                                
                        if (!backFromQuit)
                        {
                            //Susepend all Enemy
                            SuspendEnemies(GameState.currentRoom);

                            bpos = gwinfo.boundaryPointlist[GameState.currentRoom - 1].ElementAt((int)LastDirection);
                            col = bpos.World.X + PlayUIWorldMap.mapBoxColPos;
                            row = bpos.World.Y + PlayUIWorldMap.mapBoxRowPos;
                        }
                        else
                        {
                            //Do nothing
                            backFromQuit = false;
                        }

                        gwinfo.The_Player.SetLocation(col, row);
                        processWorldNMapInput();

                    }
                    else if (GameState.subGameMode == GameState.GAME_SUBMODE.WORLD_QUIT)
                    {
                        HandleWorldQuit(col, row);
                    }
                    //processWorldNMapInput(); 
                }
            
                
                /*else if (GameState.currentGameMode == GameState.GAME_MODE.ROOM &&
                     GameState.subGameMode == GameState.GAME_SUBMODE.WORLD_PROCESSING)*/
                if (GameState.currentGameMode == GameState.GAME_MODE.ROOM)
                {
                    RoomMap roommap = null;

                    if (GameState.subGameMode == GameState.GAME_SUBMODE.ROOM_PROCESSING)
                    {
                        //Get the room data
                        roommap = gwinfo.RoomMapList.ElementAt(GameState.currentRoom - 1);

                        PlayUIWorldMap.mapbox.MapInfo = roommap.RoomInfo;
                        PlayUIWorldMap.Update();

                        //*********************************
                        PlayUILeftPanel.Update();
                        PlayUIRightPanel.Update();
                        PlayUIRightPanel.PaintLegendOption(GameState.currentRoom);

                        ScreenManager.upDateRoomLocation();
                        ScreenManager.printInitScreen4Room();
                        //*********************************

                    if (!backFromQuit)
                        {
                            bpos = gwinfo.boundaryPointlist[GameState.currentRoom - 1].ElementAt((int)LastDirection);
                            col = bpos.Room.X + PlayUIWorldMap.mapBoxColPos;
                            row = bpos.Room.Y + PlayUIWorldMap.mapBoxRowPos;
                            //Release the Enemy 
                            ReleaseEnemies(GameState.currentRoom);
                        
                        }
                        else
                        {
                                //Do nothing
                                //Release the Enemy 
                                ReleaseEnemies(GameState.currentRoom);
                                backFromQuit = false;

                        }

                        gwinfo.The_Player.SetLocation(col, row);

                        processRoomMapInput(roommap);
                    }
                    else if (GameState.subGameMode == GameState.GAME_SUBMODE.ROOM_QUIT)
                    {
                        HandleRoomQuit(col, row);
                    }
                }
            

        }//End Fucnction


        private static void LookAround(int col, int row, int roomnumber)
        {
            //Object item = null;
            RoomMapSearchResult searchresult;
            List<Object> objectlist1 = null;
            List<Object> objectlist2 = null;

            //Serach surrounding 1 character away in all directon.
            searchresult = gwinfo.SearchRoom(roomnumber, col, row, (int)RoomMapSearchResult.SEARCH_TYPE.ALL_DIRECTION);

           
            objectlist1 = new List<Object>(searchresult.AutoCollectedList);
            objectlist2 = new List<Object>(searchresult.CollectedList);

            if (searchresult.ItemFound > 0)
            {
                //Pick up auto collectable item first
                //foreach (Object item in searchresult.AutoCollectedList)
                foreach (Object item in objectlist1)
                {
                    if (item is Gold)
                    {
                        gwinfo.GetGameRoombyNumber(roomnumber).TotalGoldItem--;
                        gwinfo.GetGameRoombyNumber(roomnumber).TotalItem--;

                        //*******************************************
                        PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(15));
                        //*******************************************

                    }
                    else if (item is Silver)
                    {
                        gwinfo.GetGameRoombyNumber(roomnumber).TotalSilverItem--;
                        gwinfo.GetGameRoombyNumber(roomnumber).TotalItem--;

                        //*******************************************
                        PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(16));
                        //*******************************************
                       
                    }
                    else if (item is HealthPotion )
                    {
                        gwinfo.GetGameRoombyNumber(roomnumber).TotalHealthPotionItem--;
                        gwinfo.GetGameRoombyNumber(roomnumber).TotalItem--;                   
                        gwinfo.UpdatePlyerHealth(((HealthPotion)item).lifeLine);
                        
                    }

                    GameState.inventory.Add((Item)item);
                }

                //foreach (Object item in searchresult.AutoCollectedList)
                foreach (Object item in objectlist2)
                {
                    if (item is Key)
                    {
                       

                        //Susepend all Enemy
                        SuspendEnemies(GameState.currentRoom);
                        GameItemHandler.HandleKey(item);
                        
                        //************************************
                        PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(25));
                        //************************************
                        
                        ReleaseEnemies(GameState.currentRoom);

                    }
                    else if (item is IronArmour)
                    {
                        
                        SuspendEnemies(GameState.currentRoom);
                        GameItemHandler.HandleArmour(item);
                        
                        //************************************
                        PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(25));
                        //************************************
                        
                        ReleaseEnemies(GameState.currentRoom);

                    }
                    else if (item is WoodenArmour)
                    {
                       

                        SuspendEnemies(GameState.currentRoom);
                        GameItemHandler.HandleArmour(item);
                        
                        //************************************
                        PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(25));
                        //************************************
                        
                        ReleaseEnemies(GameState.currentRoom);
                    }
                    else if (item is IronSword)
                    {
                      

                        SuspendEnemies(GameState.currentRoom);
                        GameItemHandler.HandleSword(item);
                        
                        //************************************
                        PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(25));
                        //************************************
                        
                        ReleaseEnemies(GameState.currentRoom);

                    }
                    else if (item is WoodenSword)
                    {
                     
                        SuspendEnemies(GameState.currentRoom);
                        GameItemHandler.HandleSword(item);
                        
                        //************************************
                        PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(25));
                        //************************************
                        
                        ReleaseEnemies(GameState.currentRoom);
                    }
                    else if (item is Chest)
                    {
                        
                        SuspendEnemies(GameState.currentRoom);
                        GameItemHandler.HandleChest(item);

                        //************************************
                        PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(25));
                        //************************************

                        ReleaseEnemies(GameState.currentRoom);
                    }
                }

                GameState.UpdateScore();
                searchresult = null;

                //*************************************
                ScreenManager.printScreenGoldAndSilver();
                PlayUIRightPanel.PaintLegendOption(GameState.currentRoom);
                //*************************************
            }
        }


        private static bool HandleExitWorld(int col, int row)
        {
            

            ConsoleKeyInfo keyInfo;
            bool toProcessing = true;
            bool returnStat = true;

            ScreenManager.WaitForBufferClear();  //Clear Buffer


            //************************************************************
            PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.YES_INSTRUNCTION ^ PlayUIBottomItem.NO_INSTRUNCTION);
            PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
            PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(10));
            //*****************************************************************

            while (toProcessing)
            {
                keyInfo = Console.ReadKey(true);

               

                switch (Char.ToUpper(keyInfo.KeyChar))
                {
                    case 'N':
                        GameState.subGameMode = GameState.GAME_SUBMODE.WORLD_PROCESSING;
                        toProcessing = false;
                        
                        //************************************
                        PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(11));
                        //************************************

                        

                        break;

                    case 'Y':
                        toProcessing = false;
                        backFromQuit = false;
                        returnStat = false; // Quitting
                        GameStateManager.SetNextState(GameStateTable.GAMESTATE_ENUM.GsGameOver);

                        //*****************************************************
                        PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(12));
                        PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.CLEAR);
                        ///*******************************************************

                      
                        Utility.Wait(2000);

                        break;
                }

            }

            return returnStat;



        }

        private static void HandleWorldQuit(int col, int row)
        {
          
            ConsoleKeyInfo keyInfo;
            bool toProcessing = true;

            ScreenManager.WaitForBufferClear();  //Clear Buffer

            //*****************************************************************************
            ScreenManager.printScreenQuit01();
            //*******************************************************************************

            while (toProcessing)
            {
                keyInfo = Console.ReadKey(true);

               

                switch (Char.ToUpper(keyInfo.KeyChar))
                {
                    case 'N':
                        GameState.subGameMode = GameState.GAME_SUBMODE.WORLD_PROCESSING;
                        toProcessing = false;
                        backFromQuit = true;
                        break;

                    case 'Y':
                        toProcessing = false;
                        GameStateManager.SetNextState(GameStateTable.GAMESTATE_ENUM.GsGameOver);
                        break;
                }

            }

        }

        private static void HandleRoomQuit(int col, int row)
        {
            

            //Susepend all Enemy
            SuspendEnemies(GameState.currentRoom);

            ConsoleKeyInfo keyInfo;
            bool toProcessing = true;

            ScreenManager.WaitForBufferClear();  //Clear Buffer

            //*****************************************************************************
                ScreenManager.printScreenQuit01();
            //*******************************************************************************

            while (toProcessing)
            {
                keyInfo = Console.ReadKey(true);

             
                switch (Char.ToUpper(keyInfo.KeyChar))
                {
                    case 'N':
                        GameState.subGameMode = GameState.GAME_SUBMODE.ROOM_PROCESSING;
                        toProcessing = false;
                        backFromQuit = true;
                        break;

                    case 'Y':
                        toProcessing = false;
                        GameStateManager.SetNextState(GameStateTable.GAMESTATE_ENUM.GsGameOver);
                        break;
                }

            }

        }

        // Main Attack Function.
        private static void HandleBattle(int col, int row, int room) 
        {
            ConsoleKeyInfo keyInfo;
            bool toProcessing = true;
            Item weapon = null;
            
         

            //while (!Console.KeyAvailable && GameState.isBattleOn() && toProcessing)
            while (GameState.isBattleOn() && toProcessing)
            {
              

                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);

                 
                    switch (Char.ToUpper(keyInfo.KeyChar))
                    {
                        case 'A':
                            
                            if(GameState.GetFirstSwordInInventory() == null)
                            {
                                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(51) + "\r\n" +
                                  ConfigManager.GetStringResource(52));
                                toProcessing = false;
                                break;
                            }
                                                      

                            weapon = GameState.GetFirstSwordInInventory();
                            if(weapon is IronSword || weapon is WoodenSword)
                            {
                                ((ILivingOrganism)GameState.enemyobj).getHit(((IWeapon)weapon).hitPower);
                                //Reduce SWord Power.
                                ((IWeapon)weapon).doHit();
                                PlayUIRightPanel.UpdateEnemyHealthLevel(((ILivingOrganism)GameState.enemyobj).HealthLevel);

                                if(((ILivingOrganism)GameState.enemyobj).HealthLevel == 0)
                                {
                                    //Enmey Health gone.
                                    //Stop the Enemy
                                    ((ILivingOrganism)GameState.enemyobj).Sleep();
                                    
                                    ((GameRoom)gwinfo.GetGameRoombyNumber(GameState.currentRoom)).ItemsHashTable.Remove(GameState.enemyobj.InstanceKey);

                                    if (GameState.enemyobj is Monster)
                                    {
                                        ((GameRoom)gwinfo.GetGameRoombyNumber(GameState.currentRoom)).MonsterList.Remove((Monster)GameState.enemyobj);
                                        gwinfo.NumberOfMonster--;

                                    }
                                    else if (GameState.enemyobj is Globin)
                                    {
                                        ((GameRoom)gwinfo.GetGameRoombyNumber(GameState.currentRoom)).GlobinList.Remove((Globin)GameState.enemyobj);
                                        gwinfo.NumberOfGlobin--;
                                    }

                                    if (GameState.enemyobj is ITakable)
                                        ((ITakable)GameState.enemyobj).Take();


                                    GameState.setBatteOff();
                                    GameState.LastWin = true;
                                    GameState.ReleaseEnemies(GameState.currentRoom);

                                    //********************************************
                                    PlayUIRightPanel.UpdateEnemyTitle("");
                                    PlayUIRightPanel.UpdateEnemyHealthLevel(0);
                                    PlayUILeftPanel.UpdateTotalEnemyItem(
                                         ((GameRoom)gwinfo.GetGameRoombyNumber(GameState.currentRoom)).GlobinList.Count +
                                          ((GameRoom)gwinfo.GetGameRoombyNumber(GameState.currentRoom)).MonsterList.Count);
                                    PlayUILeftPanel.UpdateTotalGlobinWItem(gwinfo.NumberOfGlobin);
                                    PlayUILeftPanel.UpdateTotalMonsterItem(gwinfo.NumberOfMonster);
                                    PlayUITopItem.UpdateHealthLevel(gwinfo.The_Player.GetHealthLevel());
                                    PlayUIRightPanel.PaintLegendOption(GameState.currentRoom);

                                  
                                    //*********************************************
                                }

                                if (((IWeapon)weapon).capableHit > 0)
                                {
                                    PlayUILeftPanel.UpdateWeaponTypeVal(weapon.descriptions);
                                    PlayUILeftPanel.UpdateWeaponrPowerLevel(
                                     Utility.getPercentageVal(((IWeapon)weapon).capableHit,400));

                                    PlayUILeftPanel.UpdateSwordItem(GameState.CountSwordInInventory());
                                }
                                else
                                {
                                    GameState.RemoveSwordFromInventory();

                                    PlayUILeftPanel.UpdateWeaponTypeVal("Not Found");
                                    PlayUILeftPanel.UpdateWeaponrPowerLevel(0);
                                    PlayUILeftPanel.UpdateSwordItem(GameState.CountSwordInInventory());
                                    PlayUITopItem.UpdateWeaponType("No");
                                    
                                }



                            }
                            toProcessing = false;
                            break;
                        /*
                        case 'Q':
                            //DEBUG
                            ScreenManager.WriteDebugLine("Quit Attack @@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            toProcessing = false;
                            GameState.setBatteOff();
                            //DEBUG
                            break;
                        */
                    }
                }


              
            }



        }

        private static void ReleaseEnemies(int currentRoomNo)
        {
            //int noOfMonster = 0;
            //int noOfGlobin = 0;

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


        private static void SuspendEnemies(int currentRoomNo)
        {
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

        private static void HandleGameOver()
        {
            PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.CLEAR);
            PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
            PlayUIBottomItem.updateMessageBox(
                 ConfigManager.GetStringResource(40) + "\r\n" +
                 ConfigManager.GetStringResource(41) + "\r\n" +
                 ConfigManager.GetStringResource(42)
                 );
            Console.ReadKey(true);
        }

        private static void HandleVictory()
        {
            PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.CLEAR);
            PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
            PlayUIBottomItem.updateMessageBox(
                 ConfigManager.GetStringResource(43) + "\r\n" +
                 ConfigManager.GetStringResource(44) + "\r\n" +
                 ConfigManager.GetStringResource(45) + GameState.score + "\r\n" +
                 ConfigManager.GetStringResource(41) + "\r\n" +
                 ConfigManager.GetStringResource(42) 
                 );
            Console.ReadKey(true);
        }

        private static void HandleWin()
        {
            PlayUIBottomItem.UpdateInstruction(
                PlayUIBottomItem.QUIT_INSTRUNCTION);

            PlayUIBottomItem.UpdateMenuOption(
              // PlayUIBottomItem.QUIT_INSTRUNCTION ^
              PlayUIBottomItem.UP_INSTRUNCTION ^
              PlayUIBottomItem.DOWN_INSTRUNCTION ^
              PlayUIBottomItem.RIGHT_INSTRUNCTION ^
              PlayUIBottomItem.LEFT_INSTRUNCTION
              );

            PlayUIBottomItem.updateMessageBox(
                 ConfigManager.GetStringResource(46) + "\r\n" +
                 ConfigManager.GetStringResource(25) + "\r\n" 
                 );
           
        }



        public static void Exit()
        {
            ScreenManager.ResetColor();
            GameDataFactory.ClearlWorldData();
        }
    }
}
