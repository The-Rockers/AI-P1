using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    internal class maze
    {
        /*
            ## 00 ## ## ## ## ## ## ## ## ## ##
         *  ## 01 02 [] [] ## [] [] [] [] [] ##
         *  ## 03 ## ## [] ## [] ## ## ## [] ##
         *  ## [] ## [] [] ## [] [] [] ## [] []
         *  ## [] ## ## ## ## ## ## [] ## ## ##
         *  ## [] [] ## [] [] [] ## [] [] [] ##
         *  ## ## [] ## [] ## ## ## [] ## [] ##
         *  ## [] [] [] [] ## [] [] [] ## [] ##
         *  ## [] ## ## ## ## [] ## [] ## ## ##
         *  ## [] [] [] [] [] [] ## [] [] [] ##
         *  ## ## ## ## ## ## ## ## ## ## ## ##
         */

        Dictionary<(byte, byte), Tile> tileSet = new Dictionary<(byte, byte), Tile>(); //dict of all tiles. maps coords to tile objects
        (byte,byte)[] path = new (byte,byte)[132];//list of path spaces.

        public maze() 
        {
            for(byte i = 0; i < 12; i++)
            {
                for(byte j = 0; j < 11; j++)
                { 
                    tileSet.Add((j,i), new Tile(j,i));
                }
            }
        }

        public void makePath((byte,byte)[] pathSpaces)
        {
            this.path = pathSpaces;
            foreach((byte, byte) t in pathSpaces)
            {
                if(tileSet.ContainsKey(t))
                {
                    tileSet[t].SetFace("[]");
                }
            }
        }
        public void setStart((byte, byte) startTile)
        {
            if (tileSet.ContainsKey(startTile))
            {
                tileSet[startTile].SetFace("00");
            }
        }

        public void printMaze()
        {
            for (byte i = 0; i < 12; i++)
            {
                for (byte j = 0; j < 11; j++)
                {
                    try 
                    { 
                        tileSet.TryGetValue((j, i), out Tile ex);
                        Console.WriteLine(ex.face);
                    }
                    catch(NullReferenceException)
                    {
                        Console.WriteLine(String.Format("Tile not found. Targeted Coordinate: {0},{1}", j, i));
                        continue;
                    }
                    
                    
                }
            }
        }



        //12 wide 11 "tall"
        //X increases rightward, Y increases downward
        //132 items!
    }
}
