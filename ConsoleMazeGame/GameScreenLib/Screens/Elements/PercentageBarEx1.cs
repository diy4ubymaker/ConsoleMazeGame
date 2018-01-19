using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameScreenLib.Screens.Elements
{
    using DIY4UMazeGame.Managers;

    public class PercentageBarEx1 : Shapes
    {

        public int Percent { get; set; } = 100;
        public char icon { get; set; } = '█';

        public int BarWidth { get; set; } = 10;
        public int BarHeight { get; set; } = 1;

        public ConsoleColor Normal { get; set; } = ConsoleColor.Green;
        public ConsoleColor Amble { get; set; }  = ConsoleColor.Yellow;
        public ConsoleColor Critical { get; set; }  = ConsoleColor.Red;


        private void clearAll()
        {
            lock (ScreenManager.LockSection)
            {
                if (ShapeSize.Width > 0)
                {
                    for (int row = 0; row < BarHeight; row++)
                    {
                        Console.SetCursorPosition(Position.X, Position.Y + row++);
                        Console.Write
                        (
                        string.Concat(Enumerable.Repeat(" ", ShapeSize.Width))
                        );
                    }
                }
            }
        }

        private void clearLine()
        {
            lock (ScreenManager.LockSection)
            {
                if (ShapeSize.Width > 0)
                {

                    Console.Write
                    (
                    string.Concat(Enumerable.Repeat(" ", ShapeSize.Width))
                    );

                }
            }
        }

        public override void draw()
        {
            int row = 0;
            int coltodraw = 0;
            float val;

            lock (ScreenManager.LockSection)
            {
               
                Console.BackgroundColor = BackgroundColor;

                val = ((float)Percent / (float)100.0) * BarWidth;
                coltodraw = Convert.ToInt32(val);

                if (Percent > 75)
                    Console.ForegroundColor = Normal;
                else if (Percent < 75 && Percent > 35)
                    Console.ForegroundColor = Amble;
                else
                    Console.ForegroundColor = Critical;

                Console.SetCursorPosition(Position.X, Position.Y);
                clearLine();

                for (row = 0; row < BarHeight; row++)
                {
                    Console.SetCursorPosition(Position.X, Position.Y+ row);

                    Console.Write
                    (
                    string.Concat(Enumerable.Repeat(icon, coltodraw))
                    );

                    if(row == 0)
                    {
                        Console.ForegroundColor = ForegroundColor;
                        Console.SetCursorPosition(Position.X + coltodraw + 1, Position.Y + row);

                        if(Percent >0)
                            Console.Write(Percent + "%");
                    }

                }


            }
        }
    }
}
