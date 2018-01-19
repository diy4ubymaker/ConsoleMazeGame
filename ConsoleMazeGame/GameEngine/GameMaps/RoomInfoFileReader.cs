using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DIY4UMazeGame.GameEngine.GameMaps
{

    using DIY4UMazeGame.Utilities;

    public class RoomInfoFileReader
    {

        public static List<RoomMap> ReadRoomInfo(String pathFilname)
        {
            String input = null;
            int outint = 0;
            List<RoomMap> roomlist = null;
            char[] delimiterChars = { ','};
           

            String filepah = null;
            int TopLeftCol = 0;
            int TopLeftRow = 0;
            int TopBottomCol = 0;
            int TopBottomRow  = 0;
            int StartCol = 0;
            int SratRow = 0;

            int outintVal = 0;
          
            RoomMap rMap = null;
           
            if (System.IO.File.Exists(pathFilname))
            {
                using (System.IO.StreamReader sr = System.IO.File.OpenText(pathFilname))
                {
                    input = sr.ReadLine(); //Read first Line 

                    if (int.TryParse(input, out outint))
                    {
                        roomlist = new List<RoomMap>();

                        for (int row =0; row < outint; row++)
                        {
                            input = sr.ReadLine(); //Read first Line 
                            string[] words = input.Split(delimiterChars);

                            if (words.Length == 7)
                            {
                                filepah = words[0];
                                int.TryParse(words[1], out outintVal);
                                TopLeftCol = outintVal;

                                int.TryParse(words[2], out outintVal);
                                TopLeftRow = outintVal;

                                int.TryParse(words[3], out outintVal);
                                TopBottomCol = outintVal;

                                int.TryParse(words[4], out outintVal);
                                TopBottomRow = outintVal;

                                int.TryParse(words[5], out outintVal);
                                StartCol = outintVal;

                                int.TryParse(words[6], out outintVal);
                                SratRow = outintVal;

                                rMap = new RoomMap(filepah, TopLeftCol, TopLeftRow, TopBottomCol, TopBottomRow);

                                roomlist.Add(rMap);
                            }

                           
                        }

                    }
                }

                return roomlist;
            }
            else
            {
                return null;
            }

        }

        public static RoomMapFileInfo ReadRoomMapInfo(String pathFilname)
        {
            String input = null;
            int outint = 0;
            List<RoomMap> roomlist = null;
            char[] delimiterChars = { ',' };

            String filepah = null;
            int TopLeftCol = 0;
            int TopLeftRow = 0;
            int TopBottomCol = 0;
            int TopBottomRow = 0;
            //int StartCol = 0;
            //int SratRow = 0;

            int outintVal = 0;
            int outintVal2 = 0;
            int outintVal3 = 0;
            int outintVal4 = 0;

            RoomMap rMap = null;
            List<Point> dropPointlist = new List<Point>(); //Drop in point for World Map
            List<BoundaryPoint>[] boundaryPointlist = null;

            if (System.IO.File.Exists(pathFilname))
            {
                using (System.IO.StreamReader sr = System.IO.File.OpenText(pathFilname))
                {
                    input = sr.ReadLine(); //Read first Line 

                    if (int.TryParse(input, out outint))
                    {
                        roomlist = new List<RoomMap>();

                        for (int row = 0; row < outint; row++)
                        {
                            input = sr.ReadLine(); //Read first Line 
                            string[] words = input.Split(delimiterChars);

                            if (words.Length == 5 && words !=null)
                            {
                                filepah = words[0];
                                int.TryParse(words[1], out outintVal);
                                TopLeftCol = outintVal;

                                int.TryParse(words[2], out outintVal);
                                TopLeftRow = outintVal;

                                int.TryParse(words[3], out outintVal);
                                TopBottomCol = outintVal;

                                int.TryParse(words[4], out outintVal);
                                TopBottomRow = outintVal;

                                rMap = new RoomMap(filepah, TopLeftCol, TopLeftRow, TopBottomCol, TopBottomRow);
                                roomlist.Add(rMap);
                            }
                        }
                        // Read Boudary Value. 

                        boundaryPointlist = new List<BoundaryPoint>[outint];

                        for (int row = 0; row < outint; row++)
                        {
                            input = sr.ReadLine(); //Read first Line 
                            string[] words = input.Split(delimiterChars);
                            
                            BoundaryPoint bpoint = null;

                            if (words.Length == 16 && words!=null)
                            {
                                boundaryPointlist[row] = new List<BoundaryPoint>();

                                int.TryParse(words[0], out outintVal);
                                int.TryParse(words[1], out outintVal2);
                                int.TryParse(words[2], out outintVal3);
                                int.TryParse(words[3], out outintVal4);
                                bpoint = new BoundaryPoint(outintVal, outintVal2, outintVal3, outintVal4);
                                boundaryPointlist[row].Add(bpoint);

                                int.TryParse(words[4], out outintVal);
                                int.TryParse(words[5], out outintVal2);
                                int.TryParse(words[6], out outintVal3);
                                int.TryParse(words[7], out outintVal4);
                                bpoint = new BoundaryPoint(outintVal, outintVal2, outintVal3, outintVal4);
                                boundaryPointlist[row].Add(bpoint);

                                int.TryParse(words[8], out outintVal);
                                int.TryParse(words[9], out outintVal2);
                                int.TryParse(words[10], out outintVal3);
                                int.TryParse(words[11], out outintVal4);
                                bpoint = new BoundaryPoint(outintVal, outintVal2, outintVal3, outintVal4);
                                boundaryPointlist[row].Add(bpoint);

                                int.TryParse(words[12], out outintVal);
                                int.TryParse(words[13], out outintVal2);
                                int.TryParse(words[14], out outintVal3);
                                int.TryParse(words[15], out outintVal4);
                                bpoint = new BoundaryPoint(outintVal, outintVal2, outintVal3, outintVal4);
                                boundaryPointlist[row].Add(bpoint);
                            }
                        }

                        //Red Drop Point List
                        input = sr.ReadLine(); //Read anotherline
                        string[] wordin = input.Split(delimiterChars);
                        int index = 0;

                        while (wordin != null && Utility.IsEven(wordin.Length) &&
                                                index < wordin.Length)
                        {
                            int.TryParse(wordin[index++], out outintVal);
                            int.TryParse(wordin[index++], out outintVal2);
                            dropPointlist.Add(new Point(outintVal, outintVal2));
                        }
                    }
                }

                return new RoomMapFileInfo(roomlist, dropPointlist, boundaryPointlist);
            }
            else
            {
                return null;
            }

        }
    }

}
