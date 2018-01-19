using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameMaps
{
    public class RooMapFileReader
    {
        private const long  MapFileSize = 3297;
        private const int   MapWidth    = 120;
        private const int   MapHeight   = 25;

        public static char[][] ReadMap(String pathFilname)
        {
            long length = 0;
            String input = null;
            char[][] returnMap = new char[25][];
            int maprow = 0;

            if (System.IO.File.Exists(pathFilname))
            {
                length = new System.IO.FileInfo(pathFilname).Length;
                if(length == MapFileSize)
                {
                    using (System.IO.StreamReader sr = System.IO.File.OpenText(pathFilname))
                    {
                        input = sr.ReadLine(); // Skip first line 
                        while(maprow < MapHeight)
                        {
                            input = sr.ReadLine(); // Skip first line
                            input = Regex.Replace(input, "#", " ");
                            returnMap[maprow++] = input.ToCharArray();
                        }
                    }
                }
            }
            else
                return null;

            return returnMap;
        }

    }
}
