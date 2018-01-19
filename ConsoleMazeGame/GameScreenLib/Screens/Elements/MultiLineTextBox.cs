using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameScreenLib.Screens.Elements
{
    using DIY4UMazeGame.Managers;

    public class MultiLineTextBox : TextBox
    {
        //bool init = false;
        List<string> lineItem = null;

        List<string> Wrap(string text, int margin)
        {
            int start = 0, end;
            List<string> lines = new List<string>();
            text = Regex.Replace(text, @"\s", " ").Trim();

            while ((end = start + margin) < text.Length)
            {
                while (text[end] != ' ' && end > start)
                    end -= 1;

                if (end == start)
                    end = start + margin;

                lines.Add(text.Substring(start, end - start));
                start = end + 1;
            }

            if (start < text.Length)
                lines.Add(text.Substring(start));

            return lines;
        }

        List<string> Wrap1(string text, int margin)
        {
            int pos = 0;
            string str = null;
            bool toContinue = true;

            List<string> lines = new List<string>();
            List<string> lines2 = new List<string>();
            List<string> lines3 = new List<string>();
            List<string> tmplist = null;

            //text = Regex.Replace(text, @"\s", " ").Trim();
            text = new Regex(@" {2,}").Replace(text.Trim(), @" ");
            int len = text.Length;
            //int index = 0;

            while (toContinue)
            {
                //pos = text.IndexOf("\n");
                pos = text.IndexOf(Environment.NewLine);

                if (pos >= 0)
                {
                    str = text.Substring(0, pos);
                    len = text.Length;
                    text = text.Substring(pos + 1, len - str.Length - 1).Trim();
                    lines.Add(str);
                    lines.Add(" ");
                }
                else
                {
                    lines.Add(text);
                    toContinue = false;
                }

            }


            //Pass two 
            pos = 0;
            //index = 0;
            toContinue = true;
            String tmpstr;

            foreach (string item in lines)
            {
                tmpstr = item;
                len = tmpstr.Length;
                while (toContinue)
                {
                    pos = tmpstr.IndexOf("\n");

                    if (pos >= 0)
                    {
                        str = tmpstr.Substring(0, pos);
                        len = tmpstr.Length;
                        tmpstr = tmpstr.Substring(pos + 1, len - str.Length - 1).Trim();
                        lines2.Add(str);
                    }
                    else
                    {
                        lines2.Add(tmpstr);
                        toContinue = false;
                    }
                }

                toContinue = true;
            }

           //Pass 3
            foreach (string item in lines2)
            {
                tmpstr = item;

                if (tmpstr.Length > 1)
                {
                    tmplist = Wrap(tmpstr, ShapeSize.Width);
                    if (tmplist != null)
                    {
                        foreach (string item1 in tmplist)
                            lines3.Add(item1);
                    }
                }
                else
                    lines3.Add(tmpstr);

            }
            return lines3;
            
        }

        public MultiLineTextBox()
        {
            alighnment = ALIGN_ENUM.Left_Justify;  // force to left justify
        }

        public override void draw()
        {
            int row = 0;
            lock (ScreenManager.LockSection)
            {

                Console.ForegroundColor = ForegroundColor;
                Console.BackgroundColor = BackgroundColor;

                //if (!init)
                //{
                lineItem = Wrap1(textItem, ShapeSize.Width);
                //init = true;
                //}

                clearline();

                foreach (string item in lineItem)
                {
                    Console.SetCursorPosition(Position.X, Position.Y + row++);
                    Console.WriteLine(item);
                }
            }
           
        }

        private void clearline()
        {
            lock (ScreenManager.LockSection)
            {
                if (textItem != null && ShapeSize.Width > 0)
                {
                    for (int row = 0; row < ShapeSize.Height; row++)
                    {
                        Console.SetCursorPosition(Position.X, Position.Y + row);
                        Console.Write
                            (
                            string.Concat(Enumerable.Repeat(" ", ShapeSize.Width))
                            );
                    }
                }
            }
        }
    }
}
