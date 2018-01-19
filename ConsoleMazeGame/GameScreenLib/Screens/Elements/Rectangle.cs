using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameScreenLib.Screens.Elements
{
    using DIY4UMazeGame.Managers;

    public class Rectangle : Shapes
    {
        public override void draw()
        {
            lock (ScreenManager.LockSection)
            {
                Console.ForegroundColor = ForegroundColor;
                Console.BackgroundColor = BackgroundColor;

                for (int w = 0; w < ShapeSize.Width; w++)
                {
                    for (int h = 0; h < ShapeSize.Height; h++)
                    {
                        Console.SetCursorPosition(Position.X + w, Position.Y + h);

                        if (w == 0 && h == 0)
                        {
                            if (BorderType == BORDER_TYPE.Double)
                                Console.Write("╔");
                            else
                                Console.Write("┌");
                        }
                        else if (w == ShapeSize.Width - 1 && h == 0)
                        {
                            if (BorderType == BORDER_TYPE.Double)
                                Console.Write("╗");
                            else
                                Console.Write("┐");
                        }
                        else if (w == 0 && h == ShapeSize.Height - 1)
                        {
                            if (BorderType == BORDER_TYPE.Double)
                                Console.Write("╚");
                            else
                                Console.Write("└");
                        }
                        else if (w == ShapeSize.Width - 1 && h == ShapeSize.Height - 1)
                        {
                            if (BorderType == BORDER_TYPE.Double)
                                Console.Write("╝");
                            else
                                Console.Write("┘");
                        }
                        else if ((w == 0 || w == ShapeSize.Width - 1) && (h > 0 && h < ShapeSize.Height - 1))
                        {
                            if (BorderType == BORDER_TYPE.Double)
                                Console.Write("║");
                            else
                                Console.Write("│");
                        }
                        else if ((w > 0 && w < ShapeSize.Width - 1) && (h == 0 || h == ShapeSize.Height - 1))
                        {
                            if (BorderType == BORDER_TYPE.Double)
                                Console.Write("═");
                            else
                                Console.Write("─");
                        }
                        else
                        {
                            /*
                            if (BorderType == BORDER_TYPE.Double)
                                Console.Write(" ");
                            else
                                Console.Write(" ");
                            */
                        }
                    }
                }
            }
        }
    }
}
