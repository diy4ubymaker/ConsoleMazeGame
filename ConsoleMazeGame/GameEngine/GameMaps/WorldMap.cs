using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameMaps
{
    using DIY4UMazeGame.Managers;

    public class WorldMap
    {
        public const int MapWidth = 120;
        public const int MapHeight = 25;

        public enum MAPTYPE
        {
            WALL,
            EXIT,
            SPACE
        };

        /* Object to store World and Room Info
         * 
        */
        public char[][] MapInfo { get; set; } = null;

        public WorldMap()
        {
            MapInfo = WorldMapFileReader.ReadMap(ConfigManager.WorldMapFile);
        }

        public MAPTYPE ItemAtPostion(int col, int row)
        {
            return GetItemType(MapInfo[row][col]);
        }

        private MAPTYPE GetItemType(char item)
        {
            if (item == '@') { return MAPTYPE.WALL; }
            if (item == 'E') { return MAPTYPE.EXIT; }
            
            return MAPTYPE.SPACE;
        }
    }
}
