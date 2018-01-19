using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameInfo.Roles
{
    //using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;

    //Sample Class
    public class Enemy
    {
        public string InstanceName { get; set; } = "Enemy-";
        public string InstanceKey { get; set; } = "";
        public ENEMY_TYPE EnemyType { get; set; } = ENEMY_TYPE.UNKNOW;

        public int HitPower { get; set; } = 3;


        public enum MOVE_DIRECTION
        {
            DIR_NORTH = 1,
            DIR_SOUTH = 2,
            DIR_EAST = 3,
            DIR_WEST = 4,
            DIR_MIN = DIR_NORTH,
            DIR_MAX = DIR_WEST
        };

        public enum ENEMY_TYPE
        {
            MONSTER = (int)Item.ITEM_TYPE.Gold << 9,
            GLOBIN = (int)Item.ITEM_TYPE.Gold << 10,
            UNKNOW
        };
    }
}
