using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameMaps
{
   

    public class RoomMap
    {
        public const int RoomWidth = 120;
        public const int RoomHeight = 25;
        public const int RoomEffectiveWidth = 60;
       
        public int TopLeftCol { get; set; } = 0;
        public int TopLeftRow { get; set; } = 0;

        public int BottomRightCol { get; set; } = 0;
        public int BottomRightRow { get; set; } = 0;

        public enum MAPTYPE
        {
            WALL,
            EXIT,
            SPACE
        };

        /* Object to store World and Room Info
         * 
        */
        public char[][] RoomInfo { get; set; } = null;

        public RoomMap(string filePath, int topLeftCol, int topLeftRow, int bottomRightCol, int bottomRightRow)
        {
            RoomInfo = RooMapFileReader.ReadMap(filePath);
            TopLeftCol = topLeftCol;
            TopLeftRow = topLeftRow;
            BottomRightCol = bottomRightCol;
            BottomRightRow = bottomRightRow;
           
        }


        public MAPTYPE ItemAtPostion(int col, int row)
        {
            return GetItemType(RoomInfo[row][col]);
        }

        public bool IsEmpty(int col, int row)
        {
            if(GetItemType(RoomInfo[row][col]) == MAPTYPE.SPACE)
            {
                return true;
            }
            return false;
        }

        public bool InsertItem(int col, int row, char itemicon)
        {
            if(IsEmpty(col,row))
            {
                RoomInfo[row][col] = itemicon;
                return true;
            }
            return false;
        }

        private MAPTYPE GetItemType(char item)
        {
            if (item == '@') { return MAPTYPE.WALL; }
            if (item == 'E') { return MAPTYPE.EXIT; }

            return MAPTYPE.SPACE;
        }

        
    }
}
