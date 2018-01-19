using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine
{

    using static GameStateTable;
    using static GameStateTable.GAMESTATE_ENUM;

    class GameStateManager
    {

        //Empty constructor
        private GameStateManager() { }

        struct GameStateIndices
        {
            public GAMESTATE_ENUM current; // Index of the current game state
            public GAMESTATE_ENUM next;    // Index of the next game state
        }

        static GameStateIndices gameStateIndices;

        //Initalize the game state manager
        public static void Init()
        {
            gameStateIndices.current = GsInvalid;
            gameStateIndices.next = GsInitial;
        }

        //Update the game state manager
        public static void Update(float dt)
        {
            if (IsGameStateChanging())
            {
                ExecuteGameStateExit(gameStateIndices.current);

                gameStateIndices.current = gameStateIndices.next;

                ExecuteGameStateInit(gameStateIndices.current);
            }

            ExecuteGameStateUpdate(gameStateIndices.current, dt);
        }

        public static void Exit()
        {

        }

        //If the game is running
        public static bool IsRunning()
        {
            //And the next game state isn't a quit
            if (gameStateIndices.next != GsQuit)
            {
                return true;
            }
            return false;
        }

        //Function to change the game state to the next state
        public static void SetNextState(GAMESTATE_ENUM nextGameState)
        {
            if (IsGameStateValid(nextGameState) || IsGameStateSpecial(nextGameState))
            {
                gameStateIndices.next = nextGameState;
            }
            else
            {
                
            }
        }

        //Check if the game state is changing
        private static bool IsGameStateChanging()
        {
            //If the current game state is different from the next game state
            if (gameStateIndices.current != gameStateIndices.next)
            {
                return true;
            }
            return false;
        }

       

    }
}
