using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameInfo.Roles
{
    public interface ILivingOrganism
    {
        int HealthLevel { get; set; } 
        int NoOfLife { get; set; }

        int GetHealthLevel();
        void SetHealthLevel(int healthValue);
        void IncreaseHealthLevel();
        void DecreaseHealthLevel();

        int GetNoOfLife();
        void IncreaseNoOfLife();
        void DecreaseNoOfLife();
        void SetNoOfLife(int noOfLife);

        void WakeUp();
        void Sleep();
        void Kill();

        void getHit(int val);

    }
}
