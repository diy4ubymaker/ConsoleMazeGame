using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameScreenLib.Screens.Elements
{
    using DIY4UMazeGame.Managers;

    public class MenuTextBox : TextBox
    {


        public List<string> lineItem { get; set; } = null;

        public MenuTextBox()
        {
            alighnment = ALIGN_ENUM.Left_Justify;  // force to left justify
        }

        public override void draw()
        {
            int row = 0;

            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;

            if (lineItem == null)
            {
                return;
            }

            clearline();
            foreach (string item in lineItem)
            {
                Console.SetCursorPosition(Position.X, Position.Y + row++);
                Console.WriteLine(item);
            }


        }

        private void clearline()
        {
            // if (textItem != null && ShapeSize.Width > 0)
            //{
            for (int row = 0; row < ShapeSize.Height; row++)
            {
                Console.SetCursorPosition(Position.X, Position.Y + row);
                Console.Write
                    (
                    string.Concat(Enumerable.Repeat(" ", ShapeSize.Width))
                    );
            }
            //}
        }


        
        public void drawwithColorOption(List<Char> iconOptions, List<ConsoleColor> colorOptions)
        {
            int row = 0;

            Console.BackgroundColor = BackgroundColor;

            /*

            if (lineItem == null)
            {
                return;
            }
            */

            clearline();
            foreach (string item in lineItem)
            {
                Console.SetCursorPosition(Position.X, Position.Y + row);
                Console.ForegroundColor = colorOptions.ElementAt(row);
                Console.WriteLine(iconOptions.ElementAt(row));
                Console.SetCursorPosition(Position.X+1, Position.Y + row);
                Console.ForegroundColor = ForegroundColor;
                Console.WriteLine(item);
                row++;
            }

        
        }



    }
}
