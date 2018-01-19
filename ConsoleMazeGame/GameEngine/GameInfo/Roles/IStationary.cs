using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DIY4UMazeGame.GameEngine.GameInfo.Roles
{
    public interface IStationary
    {
        int col { get; set; }
        int row { get; set; }
               
        Point GetLocation();
        void SetLocation(int col_in, int row_in);
        //void Update();



    }
}
