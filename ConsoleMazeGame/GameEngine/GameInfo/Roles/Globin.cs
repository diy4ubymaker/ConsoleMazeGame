using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace DIY4UMazeGame.GameEngine.GameInfo.Roles
{
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.GameEngine.GameMaps;
    using DIY4UMazeGame.GameScreenLib.Screens;
    using DIY4UMazeGame.Utilities;
    using DIY4UMazeGame.GameEngine.GameInfo.Items;

    public class Globin : Enemy, ILivingOrganism, IPrintable, IStationary, IMovable, ITakable
    {
        static int instanceNo = 0;

        public int col { get; set; } = 10;
        public int row { get; set; } = 15;

        public char IconChar { get; set; } = 'Ê';

        public int HealthLevel { get; set; } = 0;
        public int NoOfLife { get; set; } = 1;

        //public int Price { get; set; } = 0;

        public ConsoleColor ItemColor { get; set; } = ConfigManager.GlobinColor;
        
        private Thread This_thread = null;
        private bool _pause = false;
        private object _threadLock = new object();
        private bool _started = false;

        private MOVE_DIRECTION curr_dir = MOVE_DIRECTION.DIR_EAST;
        private int explore_distance = ConfigManager.RandomGen.Next(10, 30);
        private int retreat_distance = ConfigManager.RandomGen.Next(1, 10);

        private GameWorldInfo gwinfo = null;
        private RoomMap roommap = null;


        public Globin(int col_in, int row_in, string key, RoomMap roommapin)
        {
            instanceNo++;
            InstanceName  = "Globin-" + instanceNo;
            col = col_in;
            row = row_in;
            InstanceKey = key;

            //Price = 20;

            roommap = roommapin;

            EnemyType = ENEMY_TYPE.GLOBIN;

            IconChar = ConfigManager.GlobinIcon;
            This_thread = new Thread(new ThreadStart(Engine));

            HealthLevel = 50;
            HitPower = 3;

        }

        public void SetLocation(int col_in, int row_in)
        {
            col = col_in;
            row = row_in;
        }

        public void DecreaseHealthLevel()
        {
            HealthLevel--;
        }

        public void DecreaseNoOfLife()
        {
            NoOfLife--;
        }

        public int GetHealthLevel()
        {
            return HealthLevel;
        }

        public Point GetLocation()
        {
            return new Point(col, row);
        }

        public int GetNoOfLife()
        {
            return NoOfLife;
        }

        public void IncreaseHealthLevel()
        {
            HealthLevel++;
        }

        public void IncreaseNoOfLife()
        {
            NoOfLife++;
        }

        public void SetHealthLevel(int healthValue)
        {
            HealthLevel = healthValue;
        }

        public void SetNoOfLife(int noOfLife)
        {
            NoOfLife = noOfLife;
        }

        public void Update()
        {
            if (HealthLevel > 0)
            {
                Console.SetCursorPosition(col, row);
                Console.ForegroundColor = ItemColor;
                Console.Write(IconChar);
            }
            else
            {
                Console.SetCursorPosition(col, row);
                Console.ForegroundColor = Console.BackgroundColor;
                Console.Write(" ");
            }

        }

        private MOVE_DIRECTION GetRandomDirection()
        {
            return (MOVE_DIRECTION)ConfigManager.RandomGen.Next((int)MOVE_DIRECTION.DIR_MIN, (int)MOVE_DIRECTION.DIR_MAX);
        }


        private MOVE_DIRECTION GetAlternativeDirection(MOVE_DIRECTION dir)
        {
            if (dir == MOVE_DIRECTION.DIR_NORTH)
            {
                return (MOVE_DIRECTION)ConfigManager.RandomGen.Next((int)MOVE_DIRECTION.DIR_EAST, (int)MOVE_DIRECTION.DIR_WEST);
            }
            else if (dir == MOVE_DIRECTION.DIR_SOUTH)
            {
                return (MOVE_DIRECTION)ConfigManager.RandomGen.Next((int)MOVE_DIRECTION.DIR_EAST, (int)MOVE_DIRECTION.DIR_WEST);
            }
            else if (dir == MOVE_DIRECTION.DIR_EAST)
            {
                return (MOVE_DIRECTION)ConfigManager.RandomGen.Next((int)MOVE_DIRECTION.DIR_NORTH, (int)MOVE_DIRECTION.DIR_SOUTH);
            }
            else if (dir == MOVE_DIRECTION.DIR_WEST)
            {
                return (MOVE_DIRECTION)ConfigManager.RandomGen.Next((int)MOVE_DIRECTION.DIR_NORTH, (int)MOVE_DIRECTION.DIR_SOUTH);
            }
            else
                return GetRandomDirection();

        }


        private MOVE_DIRECTION GetOppositeDirection(MOVE_DIRECTION dir)
        {
            if (dir == MOVE_DIRECTION.DIR_NORTH)
            {
                return MOVE_DIRECTION.DIR_SOUTH;
            }
            else if (dir == MOVE_DIRECTION.DIR_SOUTH)
            {
                return MOVE_DIRECTION.DIR_NORTH;
            }
            else if (dir == MOVE_DIRECTION.DIR_EAST)
            {
                return MOVE_DIRECTION.DIR_WEST;
            }
            else if (dir == MOVE_DIRECTION.DIR_WEST)
            {
                return MOVE_DIRECTION.DIR_EAST;
            }
            else
                return GetRandomDirection();

        }

        private void AttackPlayer()
        {
            bool attacking = true;

            //Scan For the Player
            if(gwinfo.IsPlayerWithinAttackRange(col, row))
            {
                GameState.SuspendEnemies(GameState.currentRoom, this);
                GameState.setBattleOn(this);
                GameState.enemyobj = this;  // Register Attacker

                //Launch indicator
                GameBattleIndicator gind = new GameBattleIndicator();
                gind.thisEnemy = this;
                gind.thisPlayer = gwinfo.The_Player;
                gind.WakeUp();
                GameState.gmind = gind;

                //************************************
                PlayUIRightPanel.UpdateEnemyTitle(InstanceName + " Health Level");
                PlayUIRightPanel.UpdateEnemyHealthLevel(HealthLevel);
                //************************************

                while (attacking && !_pause)
                {
                    Utility.Wait(500);

                    if(gwinfo.The_Player.HealthLevel<=0)
                    {
                        //Play health is Zero. I win
                        attacking = false;
                        GameState.setBatteOff();
                        Sleep();
                    }

                    GameDataFactory.curr_gwinfo.The_Player.getHit(HitPower);  //Attack

                    //#########################
                    PlayUILeftPanel.UpdateArmourPowerLevel(
                        ((IShield)GameState.GetFirstArmourInInventory()).shieldPower);
                    PlayUIRightPanel.UpdateHealthLevel(gwinfo.The_Player.HealthLevel);
                    PlayUITopItem.UpdateHealthLevel(gwinfo.The_Player.GetHealthLevel());
                    //#########################
                }

            }
            //Scan For the Player
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
            AttackPlayer();
        }

        public void Sleep()
        {
            _pause = true;
        }

        public void Kill()
        {
            _pause = true;
            This_thread.Abort();
        }

        private void PaintScreen(
            int col_prev, int row_pre,
            int col_new, int row_new)
        {
            lock (ScreenManager.LockSection)
            {
                Console.ForegroundColor = ItemColor;
                Console.SetCursorPosition(col_prev, row_pre);
                Console.Write(' ');

                if (HealthLevel > 0)
                {
                    Console.SetCursorPosition(col_new, row_new);
                    Console.Write(IconChar);
                }
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

        public void MoveNorth()
        {
            lock (ScreenManager.LockSection)
            {
                PaintScreen(col, row, col, --row);
            }
        }

        public void MoveSouth()
        {
            lock (ScreenManager.LockSection)
            {
                PaintScreen(col, row, col, ++row);
            }
        }

        public void MoveEast()
        {
            lock (ScreenManager.LockSection)
            {
                PaintScreen(col, row, ++col, row);
            }
        }

        public void MoveWest()
        {
            lock (ScreenManager.LockSection)
            {
                PaintScreen(col, row, --col, row);
            }
        }

       

        private void Engine()
        {
            RoomMap.MAPTYPE maptype;
            Object item = null;
          
            gwinfo = GameDataFactory.curr_gwinfo;
            
            int step_forward = 0;
            bool collision = false;

            curr_dir = GetRandomDirection();

            while (true)
            {
                CheckPause();

               

                //Pick a Random Direction First
                //While no collison move with explore distance
                //Hit somethin go the opposite way and Pick a Random Direction
                //Move Forward

                step_forward = 0;
                collision = false;

                while (!collision)
                {

                   
                    CheckPause();

                    if (step_forward <= explore_distance)
                    {
                        switch (curr_dir)
                        {
                            case MOVE_DIRECTION.DIR_NORTH:

                                maptype = roommap.ItemAtPostion(col - PlayUIWorldMap.mapBoxColPos, row - 1 - PlayUIWorldMap.mapBoxRowPos);
                                item = gwinfo.CheckRoomPointOccupied(col - PlayUIWorldMap.mapBoxColPos, row - 1 - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);

                                if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                                {
                                    MoveNorth();
                                }
                                else collision = true;

                                break;

                            case MOVE_DIRECTION.DIR_SOUTH:

                                maptype = roommap.ItemAtPostion(col - PlayUIWorldMap.mapBoxColPos, row + 1 - PlayUIWorldMap.mapBoxRowPos);
                                item = gwinfo.CheckRoomPointOccupied(col - PlayUIWorldMap.mapBoxColPos, row + 1 - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);

                                if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                                {
                                    MoveSouth();
                                }
                                else collision = true;

                                break;

                            case MOVE_DIRECTION.DIR_EAST:

                                maptype = roommap.ItemAtPostion(col + 1 - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos);
                                item = gwinfo.CheckRoomPointOccupied(col + 1 - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);
                                if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                                {
                                    MoveEast();
                                }
                                else collision = true;

                                break;

                            case MOVE_DIRECTION.DIR_WEST:

                                maptype = roommap.ItemAtPostion(col - 1 - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos);
                                item = gwinfo.CheckRoomPointOccupied(col - 1 - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);
                                if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                                {
                                    MoveWest();
                                }
                                else collision = true;

                                break;

                        }// Switch

                        if (collision)
                        {
                            //Break Switch
                            break;
                        }

                        CheckPause();
                        step_forward++;
                    }
                    else
                    {
                        CheckPause();
                        curr_dir = GetRandomDirection();
                        step_forward = 0;
                        break;
                    }

                    CheckPause();
                    Thread.Sleep(500);
                }

                CheckPause();
                Thread.Sleep(500);
                //************************************************************

                //Move Oppsite
                curr_dir = this.GetOppositeDirection(curr_dir);
                step_forward = 0;
                collision = false;
                while (!collision)
                {
                  
                    CheckPause();

                    if (step_forward <= retreat_distance)
                    {
                        switch (curr_dir)
                        {
                            case MOVE_DIRECTION.DIR_NORTH:

                                maptype = roommap.ItemAtPostion(col - PlayUIWorldMap.mapBoxColPos, row - 1 - PlayUIWorldMap.mapBoxRowPos);
                                item = gwinfo.CheckRoomPointOccupied(col - PlayUIWorldMap.mapBoxColPos, row - 1 - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);

                                if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                                {
                                    MoveNorth();
                                }
                                else collision = true;

                                break;

                            case MOVE_DIRECTION.DIR_SOUTH:

                                maptype = roommap.ItemAtPostion(col - PlayUIWorldMap.mapBoxColPos, row + 1 - PlayUIWorldMap.mapBoxRowPos);
                                item = gwinfo.CheckRoomPointOccupied(col - PlayUIWorldMap.mapBoxColPos, row + 1 - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);

                                if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                                {
                                    MoveSouth();
                                }
                                else collision = true;

                                break;

                            case MOVE_DIRECTION.DIR_EAST:

                                maptype = roommap.ItemAtPostion(col + 1 - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos);
                                item = gwinfo.CheckRoomPointOccupied(col + 1 - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);
                                if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                                {
                                    MoveEast();
                                }
                                else collision = true;

                                break;

                            case MOVE_DIRECTION.DIR_WEST:

                                maptype = roommap.ItemAtPostion(col - 1 - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos);
                                item = gwinfo.CheckRoomPointOccupied(col - 1 - PlayUIWorldMap.mapBoxColPos, row - PlayUIWorldMap.mapBoxRowPos, GameState.currentRoom);
                                if (maptype == RoomMap.MAPTYPE.SPACE && item == null)
                                {
                                    MoveWest();
                                }
                                else collision = true;

                                break;

                        }// Switch

                        if (collision)
                        {
                            //Break Switch
                            curr_dir = GetAlternativeDirection(curr_dir);
                            break;
                        }
                        CheckPause();
                        step_forward++;
                    }
                    else
                    {
                        CheckPause();
                        //curr_dir = GetRandomDirection();
                        curr_dir = GetAlternativeDirection(curr_dir);

                        collision = true;
                        break;
                    }

                    CheckPause();
                    Thread.Sleep(500);
                }

                CheckPause();
                Thread.Sleep(500);

              

            }//Main Engine Loop

        } //Engine Function

        public void getHit(int val)
        {
            if (HealthLevel >= 0)
                HealthLevel--;

            if (HealthLevel < 0)
                HealthLevel = 0;
        }

        public void Take()
        {
            lock (ScreenManager.LockSection)
            {
                Console.SetCursorPosition(col, row);
                Console.ForegroundColor = Console.BackgroundColor;
                Console.Write(" ");
            }
        }


    }
}
