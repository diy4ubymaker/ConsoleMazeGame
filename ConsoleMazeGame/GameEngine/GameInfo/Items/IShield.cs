using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameInfo.Items
{
    public interface IShield
    {
        int shieldPower { get; set; }  //No of Hit this shield can take
        int shieldLevel { get; set; }  //The decrease in number of hit everytime it block a hit

        void takeHit(); // call this function when the Enemy the shield


    }
}
