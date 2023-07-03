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

        Dictionary<Tuple<byte, byte>, Tile> tileSet = new Dictionary<Tuple<byte, byte>, Tile>();

        public maze() 
        {
            for(byte i = 0; i < 12; i++)
            {
                for(byte j = 0; j < 11; j++)
                {
                    tileSet.Add(new Tuple<byte, byte>(j,i), new Tile());
                }
            }
        }

        public void makePath(List<Tuple<byte, byte>> pathSpaces) //this is way easier than c++ LOL
        {
            foreach(Tuple<byte, byte> t in pathSpaces)
            {
                if(tileSet.ContainsKey(t))
                {
                    tileSet[t].setFace("[]");
                }
            }
        }

        public void printMaze()
        {

        }



        //12 wide 11 "tall"
        //X increases rightward, Y increases downward
        //132 items!
    }
}
