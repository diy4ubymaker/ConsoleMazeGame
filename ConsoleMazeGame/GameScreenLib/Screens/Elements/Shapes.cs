using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace DIY4UMazeGame.GameScreenLib.Screens.Elements
{
    using DIY4UMazeGame.Managers;

    public class Shapes
    {
        public enum BORDER_TYPE
        {
            Single = 1,
            Double
        };

        public Point Position { get; set; }
        public Size ShapeSize { get; set; }

        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public BORDER_TYPE BorderType { get; set; } = BORDER_TYPE.Double;

        public virtual void Create(Point pos, Size size, ConsoleColor fcolor, ConsoleColor bcolor)
        {
            Position = pos;
            ShapeSize = size;
            ForegroundColor = fcolor;
            BackgroundColor = bcolor;
        }

        public virtual void draw()
        {

        }

    }
}
