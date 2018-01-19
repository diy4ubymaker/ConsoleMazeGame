using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameInfo.Items
{
    public interface IWeapon
    {
        int hitPower { get; set; }  // The Hit that can be deducted from the shield
        int capableHit { get; set; }   //Th total number of hit it can take.

        void doHit(); //call this when the weapon is use to attack an enemy .
    }
}
