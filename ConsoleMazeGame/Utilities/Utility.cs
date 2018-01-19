using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DIY4UMazeGame.Utilities
{
    /* Utility Class */

    class Utility
    {

        public static string[] ReadFromTextFile(String fileName)
        {
            List<string> tmplist = new List<string>();

            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.StreamReader sr = System.IO.File.OpenText(fileName))
                {
                    String input;
                    while ((input = sr.ReadLine()) != null)
                    {
                        tmplist.Add(input);
                    }
                }
            }
            return tmplist.ToArray();
        }
       
        public static void Wait(int waitTime)
        {
            System.Threading.Thread.Sleep(waitTime);
        }

        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        public static bool IsEven(int value)
        {
            return !IsOdd(value);
        }

        public static int getPercentageVal(int x, int y)
        {

            float val = ((float)x / (float)y)*100;
            return Convert.ToInt32(val);
        }
    }
}
