using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DIY4UMazeGame.GameEngine.GameMaps
{
    public class BoundaryPoint
    {
        public Point World { get; } = new Point();
        public Point Room { get; } = new Point();

        public BoundaryPoint(int wCol, int wRow, int rCol, int rRow)
        {
            World = new Point(wCol,wRow);
            Room = new Point(rCol,rRow);
        }

        public enum BOARDERPOINTYPE
        {
            BP_NORTH,
            BP_SOUTH,
            BP_EAST,
            BP_WEST
        };

    }
}
