using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject
{
    public class Item
    {
        public string itemname { get; set; } = null;
        public string descriptions { get; set; } = null;
        public ITEM_TYPE itemtype { get; set; } = ITEM_TYPE.Unknow;
        public string InstanceKey { get; set; } = "";

        public enum ITEM_TYPE
        {
            Gold = 1,
            Silver = Gold <<1,
            Wooden_Sword  = Gold <<2,
            Iron_Sword = Gold << 3,
            Wooden_Armour = Gold << 4,
            Iron_Armour = Gold << 5,
            Chest = Gold << 6,
            Chest_Master_Key = Gold << 7,
            Health_Potion = Gold << 8,
            Unknow //Not init properly
        }


    }
}
