using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameScreenLib.Screens.Elements
{
    using DIY4UMazeGame.Managers;

    //Single Line box
    public class TextBox : Shapes
    {

        public enum ALIGN_ENUM
        {
            Left_Justify = 1,
            Centre_Justify,
            Right_Justify
        }

        public string textItem { get; set; } = " ";
        public ALIGN_ENUM alighnment { get; set; } = ALIGN_ENUM.Left_Justify;


        private void clearline()
        {
            lock (ScreenManager.LockSection)
            {
                if (textItem != null && ShapeSize.Width > 0)
                {
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.Write
                        (
                        string.Concat(Enumerable.Repeat(" ", ShapeSize.Width))
                        );
                }
            }
        }

        public override void draw()
        {
            lock (ScreenManager.LockSection)
            {
                Console.ForegroundColor = ForegroundColor;
                Console.BackgroundColor = BackgroundColor;


                if (alighnment == ALIGN_ENUM.Left_Justify)
                {
                    clearline();
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.Write(textItem);
                }
                else if (alighnment == ALIGN_ENUM.Centre_Justify)
                {
                    clearline();
                    Console.SetCursorPosition(Position.X + (ShapeSize.Width - textItem.Length) / 2, Position.Y);
                    Console.Write(textItem);
                }
                else
                {
                    clearline();
                    Console.SetCursorPosition(Position.X + (ShapeSize.Width - textItem.Length), Position.Y);
                    Console.Write(textItem);
                }
            }
  
        }

    }
}
