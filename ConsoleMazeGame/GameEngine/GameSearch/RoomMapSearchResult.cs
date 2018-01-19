using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameSearch
{
    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;

    public class RoomMapSearchResult
    {
        public Object North_Item { get; set; } = null;
        public Object South_Item { get; set; } = null;
        public Object East_Item { get; set; } = null;
        public Object West_Item { get; set; } = null;
        public Object NorthEast_Item { get; set; } = null;
        public Object NorthWest_Item{ get; set; } = null;
        public Object SouthEast_Item { get; set; } = null;
        public Object SouthWest_Item { get; set; } = null;
        public int ItemFound { get; set; } = 0;

        public List<Object> AutoCollectedList { get; set; } = new List<Object>();   // item that can be collected.
        public List<Object> CollectedList { get; set; } = new List<Object>();
       

        public enum SEARCH_TYPE
        {
            NORTH_ONLY = 0,
            SOUTH_ONLY = 1,
            EAST_ONLY = 2,
            WEST_ONLY = 3,
            NORTH_BOUND,
            SOUTH_BOUND,
            EAST_BOUND,
            WEST_BOUND, 
            ALL_DIRECTION,
        };

    }
}
