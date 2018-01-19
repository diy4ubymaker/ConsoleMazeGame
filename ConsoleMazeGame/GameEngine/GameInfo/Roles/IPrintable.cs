using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameInfo.Roles
{
    public interface IPrintable
    {
        char IconChar { get; set; }
        ConsoleColor ItemColor { get; set; }

        void Update();


}
}
