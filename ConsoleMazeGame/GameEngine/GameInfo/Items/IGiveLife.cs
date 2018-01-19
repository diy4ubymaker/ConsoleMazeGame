using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameInfo.Items
{
    public interface IGiveLife
    {
        int lifeLine { get; set; }  //No of Hit this shield can take
    }
}
