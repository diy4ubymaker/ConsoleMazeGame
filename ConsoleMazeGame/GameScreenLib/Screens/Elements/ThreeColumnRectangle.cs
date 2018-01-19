using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameScreenLib.Screens.Elements
{
    using DIY4UMazeGame.Managers;

    public class ThreeColumnRectangle : Rectangle
    {
        public override void draw()
        {
            lock (ScreenManager.LockSection)
            {
                //Call Base class method
                base.draw();

                int firstX = 0;
                int secondX = 0;

                firstX = ShapeSize.Width / 3;
                secondX = firstX * 2;

                Console.SetCursorPosition(Position.X + firstX, Position.Y);
                Console.Write("╦");

                Console.SetCursorPosition(Position.X + secondX, Position.Y);
                Console.Write("╦");

                Console.SetCursorPosition(Position.X + firstX, Position.Y + ShapeSize.Height - 1);
                Console.Write("╩");

                Console.SetCursorPosition(Position.X + secondX, Position.Y + ShapeSize.Height - 1);
                Console.Write("╩");

                for (int i = Position.Y + 1; i < Position.Y + ShapeSize.Height - 1; i++)
                {
                    Console.SetCursorPosition(Position.X + firstX, i);
                    Console.Write("║");
                    Console.SetCursorPosition(Position.X + secondX, i);
                    Console.Write("║");
                }

                //╚ ╝ ╬ ═ ╩ ╠ ╣ ╦ ╔ ╗ ║"


            }
        }
    }
}
