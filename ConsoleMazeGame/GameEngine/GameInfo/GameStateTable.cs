using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine
{
    using DIY4UMazeGame.GameEngine.GamePhases;

    class GameStateTable
    {
        public enum GAMESTATE_ENUM
        {
            GsQuit = -3,
            GsRestart = -2,
            GsInvalid = -1,
            GsDIY4ULogo,  //This will be 0
            GsGamePlay,
            GsGameOver,
            GsCredits,
            GsMax,
            GsLimit = GsMax - 1,
            GsInitial = GsDIY4ULogo,
        };

        delegate void GameStateVoidFunction();
        delegate void GameStateDtFunction(float dt);

        struct GameStateTableEntry
        {
            public GameStateVoidFunction gameStateInIt;
            public GameStateDtFunction gameStateUpdate;
            public GameStateVoidFunction gameStateExit;

            public GameStateTableEntry(GameStateVoidFunction inItFunction, GameStateDtFunction updateFunction, GameStateVoidFunction exitFunction)
            {
                gameStateInIt = inItFunction;
                gameStateUpdate = updateFunction;
                gameStateExit = exitFunction;
            }
        };

       static readonly GameStateTableEntry[] GameStateTab =
       {
            //Setup functions Pointers
            new GameStateTableEntry(GamePhaseDIY4ULogo.Init, GamePhaseDIY4ULogo.Update, GamePhaseDIY4ULogo.Exit),
            new GameStateTableEntry(GamePhasePlay.Init, GamePhasePlay.Update, GamePhasePlay.Exit),
            new GameStateTableEntry(GamePhaseGameOver.Init, GamePhaseGameOver.Update, GamePhaseGameOver.Exit),
            new GameStateTableEntry(GamePhaseCredits.Init, GamePhaseCredits.Update, GamePhaseCredits.Exit),

        };

        public static bool IsGameStateValid(GAMESTATE_ENUM gameState)
        {
            if ((0 <= gameState) && (gameState < GAMESTATE_ENUM.GsMax))
            {
                return true;
            }

            return false;
        }

        public static bool IsGameStateSpecial(GAMESTATE_ENUM gameState)
        {
            if (gameState == GAMESTATE_ENUM.GsRestart || gameState == GAMESTATE_ENUM.GsQuit)
            {
                return true;
            }
            return false;
        }

        public static void ExecuteGameStateInit(GAMESTATE_ENUM gameState)
        {
            if (IsGameStateValid(gameState) && ((GameStateTab[(int)gameState].gameStateInIt) != null))
            {
                (GameStateTab[(int)gameState].gameStateInIt)();
            }
        }

        public static void ExecuteGameStateUpdate(GAMESTATE_ENUM gameState, float dt)
        {
            if (IsGameStateValid(gameState) && ((GameStateTab[(int)gameState].gameStateUpdate) != null))
            {
                (GameStateTab[(int)gameState].gameStateUpdate)(dt);
            }
        }

        public static void ExecuteGameStateExit(GAMESTATE_ENUM gameState)
        {
            if (IsGameStateValid(gameState) && ((GameStateTab[(int)gameState].gameStateExit) != null))
            {
                (GameStateTab[(int)gameState].gameStateExit)();
            }
        }
    }
}
