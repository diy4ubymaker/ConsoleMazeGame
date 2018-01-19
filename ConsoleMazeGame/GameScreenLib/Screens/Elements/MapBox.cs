using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameScreenLib.Screens.Elements
{
    using DIY4UMazeGame.Managers;

    public class MapBox : TextBox
    {
        public char[][] MapInfo { get; set; } = null;

        public MapBox()
        {}

        public MapBox(char[][] mapInfo)
        {
            MapInfo = mapInfo;
        }

        private void clearCurrentline()
        {

             Console.Write(string.Concat(Enumerable.Repeat(" ", ShapeSize.Width)));
         }

        public override void draw()
        {
            int numOfRow = 0;
            int displayrow = 0;

            lock (ScreenManager.LockSection)
            {
                Console.ForegroundColor = ForegroundColor;
                Console.BackgroundColor = BackgroundColor;

                List<string> lineItem = new List<string>();

                if (MapInfo != null)
                {
                    numOfRow = MapInfo.GetLength(0);
                    if (numOfRow > 0)
                    {
                        for (int row = 0; row < numOfRow; row++)
                            lineItem.Add(new string(MapInfo[row], 0, (MapInfo[row].Count())));

                        foreach (string item in lineItem)
                        {
                            Console.SetCursorPosition(Position.X, Position.Y + displayrow);
                            clearCurrentline();
                            Console.SetCursorPosition(Position.X, Position.Y + displayrow++);
                            Console.WriteLine(item);
                        }
                    }
                }
            }
        }
    }
}
