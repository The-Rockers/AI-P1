using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    internal class Agent
    {
        Maze referenceMaze;
        Hashtable exploredSet = new Hashtable();
        PriorityQueue<Tile, int> frontierSet = new PriorityQueue<Tile, int>();
        Tile currentTile, nextTile;

        List<Tile>testSet = new List<Tile>();

        byte f, g, h;
        byte exp = 00;
        
        // f = total. g = cost so far. h = "remaining" cost.
        // to get h for a given tile, need to calculate distance from it to end.
        // fortunately things can be either up or down not both. there fore we can do easy math.
        // diff [(y1,y2) * (if positive, 1, if negative, 3)] + diff (x1,x2) * 2 -- REMBER Y UPSIDE DOWN. 
        // IF y(Tile1) < y(tile2) THEN multiply |yDiff| by 3 ELSE multiply |yDiff| by 1
        // Multiply |xDiff| by 2. add outcomes together. this is h.
        // g is cost from prev tile.

        public bool Brute(Tile tgtTile)  //BFS - testSet, FIFO
        {
            byte validNeighbors = 0;
            currentTile= tgtTile;
            Console.WriteLine(Convert.ToString(tgtTile.GetCoords()));
            exp++;
            string expStr;
            if (exp < 10) { expStr = "0" + Convert.ToString(exp); }
            else { expStr = Convert.ToString(exp); }
            tgtTile.SetFace(expStr);

            if (currentTile.GetCoords() != referenceMaze.end)
            {
                if (!exploredSet.ContainsKey(currentTile))
                {
                    exploredSet.Add(currentTile, 0);
                    testSet.Remove(currentTile);
                }

                //WNES order
                nextTile = referenceMaze.GetTile(((byte, byte))(currentTile.x - 1, currentTile.y));  //WEST
                if (referenceMaze.isLegalMove(currentTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        testSet.Add(nextTile);
                        validNeighbors++;
                    }
                }

                nextTile = referenceMaze.GetTile(((byte, byte))(currentTile.x, currentTile.y - 1));  //NORTH
                if (referenceMaze.isLegalMove(currentTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        testSet.Add(nextTile);
                        validNeighbors++;
                    }
                }

                nextTile = referenceMaze.GetTile(((byte, byte))(currentTile.x + 1, currentTile.y));  //EAST
                if (referenceMaze.isLegalMove(currentTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        testSet.Add(nextTile);
                        validNeighbors++;
                    }
                }

                nextTile = referenceMaze.GetTile(((byte,byte))(currentTile.x, currentTile.y + 1));   //SOUTH
                if(referenceMaze.isLegalMove(currentTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        testSet.Add(nextTile);
                        validNeighbors++;
                    }
                }

                /*
                foreach(Tile t in testSet)
                {
                    if (exploredSet.ContainsKey(t))
                    {
                        testSet.Remove(t);  didnt know this was illegal LOL
                    }
                }*/

                if (validNeighbors == 0)
                {
                    Console.WriteLine("DEAD END");
                    return false;
                }
                else
                {
                    Brute(testSet[0]);
                }
                return false;
                //if no immedaite paths, go back out the recursive chain
            }
            else
            {
                Console.WriteLine(String.Format("Arrived at {0}", Convert.ToString(tgtTile.GetCoords())));
                return true;
            }
        }

        public void ASTAR(Tile tgtTile)  //A-STAR - heuristic function f = g + h
        {
            byte validNeighbors = 0;
            exp++;
            string expStr;
            if(exp < 10) { expStr = "0" + Convert.ToString(exp); }
            else { expStr =  Convert.ToString(exp); }

            Console.WriteLine(Convert.ToString(tgtTile.GetCoords()));
            tgtTile.SetFace(expStr);

            if (tgtTile.GetCoords() != referenceMaze.end)
            {
                if (!exploredSet.ContainsKey(tgtTile))
                {
                    exploredSet.Add(tgtTile, 0);
                }

                //WNES order
                nextTile = referenceMaze.GetTile(((byte, byte))(tgtTile.x - 1, tgtTile.y));  //WEST
                if (referenceMaze.isLegalMove(tgtTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        nextTile.g = (byte)(currentTile.g + 2);
                        nextTile.h = referenceMaze.GetH(nextTile);
                        f = (byte)(nextTile.g + nextTile.h);
                        frontierSet.Enqueue(nextTile, f);
                        validNeighbors++;
                    }
                }

                nextTile = referenceMaze.GetTile(((byte, byte))(tgtTile.x, tgtTile.y - 1));  //NORTH
                if (referenceMaze.isLegalMove(tgtTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        nextTile.g = (byte)(currentTile.g + 1);
                        nextTile.h = referenceMaze.GetH(nextTile);
                        f = (byte)(nextTile.g + nextTile.h);
                        frontierSet.Enqueue(nextTile, f);
                        validNeighbors++;
                    }
                }

                nextTile = referenceMaze.GetTile(((byte, byte))(tgtTile.x + 1, tgtTile.y));  //EAST
                if (referenceMaze.isLegalMove(tgtTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        nextTile.g = (byte)(currentTile.g + 2);
                        nextTile.h = referenceMaze.GetH(nextTile);
                        f = (byte)(nextTile.g + nextTile.h);
                        frontierSet.Enqueue(nextTile, f);
                        validNeighbors++;
                    }
                }

                nextTile = referenceMaze.GetTile(((byte, byte))(tgtTile.x, tgtTile.y + 1));   //SOUTH
                if (referenceMaze.isLegalMove(tgtTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        nextTile.g = (byte)(currentTile.g + 3);
                        nextTile.h = referenceMaze.GetH(nextTile);
                        f = (byte)(nextTile.g + nextTile.h);
                        frontierSet.Enqueue(nextTile, f);
                        validNeighbors++;
                    }
                }


                if (validNeighbors == 0)
                {
                    Console.WriteLine("DEAD END");
                }
                else
                { }
                
                ASTAR(frontierSet.Dequeue());
                
                
                //if no immedaite paths, go back out the recursive chain
            }
            else
            {
                Console.WriteLine(String.Format("Arrived at {0}", Convert.ToString(tgtTile.GetCoords())));
                return;
            }
        }

        public byte getHeuristic()
        {
            return 1;
        }

        public Agent(Maze x) 
        {
            this.referenceMaze = x;
            currentTile = referenceMaze.GetPathStart(); 
        }

    }
}
