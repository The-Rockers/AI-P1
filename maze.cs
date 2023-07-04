using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    internal class Maze
    {
        /*
         *  ## 00 ## ## ## ## ## ## ## ## ## ##
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
        //12 wide 11 "tall"
        //X increases rightward, Y increases downward
        //132 items!

        Dictionary<(byte, byte), Tile> tileSet = new Dictionary<(byte, byte), Tile>(); //dict of all tiles. maps coords to tile objects
        (byte,byte)[] path = new (byte,byte)[56];//list of path spaces.

        public Maze() 
        {
            for(byte i = 0; i < 11; i++)
            {
                for(byte j = 0; j < 12; j++)
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
                    tileSet[t].SetPath(true);
                    tileSet[t].SetFace("[]");
                }
            }
            tileSet[path[0]].SetFace("00");
        }

        public Tile GetPathStart()
        {
            return tileSet[path[0]];
        }

        public bool isLegalMove(Tile a, Tile b)
        {
            //a is current, b is target
            //Check: if obstacle, if OOB, *then* if valid.
            //Consider moving this to agent - create dict of discovered illegal moves to avoid repeat?
            if(b.face == "##" || !b.isPath) { return false; }
            if(b.x > 11 || b.x < 0) { return false; }
            if(b.y > 10 || b.y < 0) { return false; }
            if(b.x == (a.x + 1) || b.x == (a.x - 1))
            {
                if(b.y == a.y)
                {
                    return true;
                }
            }
            else if(b.x == a.x)
            {
                if (b.y == (a.y + 1) || b.y == (a.y - 1))
                {
                    return true;
                }

            }

            return false;
        }

        public void printMaze()
        {
            for (byte i = 0; i < 11; i++)
            {
                for (byte j = 0; j < 12; j++)
                {
                    try 
                    { 
                        tileSet.TryGetValue((j, i), out Tile ex);
                        Console.Write(" ");
                        Console.Write(ex.face);
                    }
                    catch(NullReferenceException)
                    {
                        Console.WriteLine(String.Format("Tile not found. Targeted Coordinate: {0},{1}", j, i));
                        continue;
                    }
                }
                Console.Write(Environment.NewLine);
            }
        }




    }
}
