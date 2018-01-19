using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DIY4UMazeGame.GameEngine.GameMaps
{
    public class RoomMapFileInfo
    {
        public List<RoomMap> roomaplist { get;} = null;
        public List<Point> dropPointlist { get;} = null;
        //public List<BoundaryPoint> boundaryPointlist { get; } = null;
        public List<BoundaryPoint>[] boundaryPointlist = null;

        public RoomMapFileInfo(List<RoomMap> roomaplist_in, List<Point> dropPointlist_in)
        {
            roomaplist = roomaplist_in;
            dropPointlist = dropPointlist_in;
        }

        public RoomMapFileInfo(List<RoomMap> roomaplist_in, List<Point> dropPointlist_in,
            List<BoundaryPoint>[] boundaryPointlist_in)
        {
            roomaplist = roomaplist_in;
            dropPointlist = dropPointlist_in;
            boundaryPointlist = boundaryPointlist_in;
        }

    }
}
