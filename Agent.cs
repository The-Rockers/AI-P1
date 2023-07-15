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
        Hashtable exploredSet = new Hashtable(); //A hash table for the explored set 
        PriorityQueue<Tile, int> frontierSet = new PriorityQueue<Tile, int>();  //Priority queue for frontier set
        Tile? nextTile;

        byte f;
        byte exp = 00;

        Tile BFSOnlyTile; //BFSOnly is only used in my (sort of?) BFS implementation. I just don't have the heart to get rid of it.
        List<Tile>BFSSet = new List<Tile>();
        
        // f = total. g = cost so far. h = "remaining" cost.
        // to get h for a given tile, need to calculate distance from it to end.
        // fortunately things can be either up or down not both. there fore we can do easy math.
        // diff [(y1,y2) * (if positive, 1, if negative, 3)] + diff (x1,x2) * 2 -- REMBER Y UPSIDE DOWN. 
        // IF y(Tile1) < y(tile2) THEN multiply |yDiff| by 3 ELSE multiply |yDiff| by 1
        // Multiply |xDiff| by 2. add outcomes together. this is h.
        // g is cost from prev tile.

        public bool PseudoBFS(Tile tgtTile)  //BFS - BFSSet, FIFO
        {
            byte validNeighbors = 0;
            BFSOnlyTile= tgtTile;
            Console.WriteLine(Convert.ToString(tgtTile.GetCoords()));
            exp++;
            string expStr;
            if (exp < 10) { expStr = "0" + Convert.ToString(exp); }
            else { expStr = Convert.ToString(exp); }
            tgtTile.SetFace(expStr);

            if (BFSOnlyTile.GetCoords() != referenceMaze.end)
            {
                if (!exploredSet.ContainsKey(BFSOnlyTile))
                {
                    exploredSet.Add(BFSOnlyTile, 0);
                    BFSSet.Remove(BFSOnlyTile);
                }

                //WNES order
                nextTile = referenceMaze.GetTile(((byte, byte))(BFSOnlyTile.x - 1, BFSOnlyTile.y));  //WEST
                if (referenceMaze.isLegalMove(BFSOnlyTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        BFSSet.Add(nextTile);
                        validNeighbors++;
                    }
                }

                nextTile = referenceMaze.GetTile(((byte, byte))(BFSOnlyTile.x, BFSOnlyTile.y - 1));  //NORTH
                if (referenceMaze.isLegalMove(BFSOnlyTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        BFSSet.Add(nextTile);
                        validNeighbors++;
                    }
                }

                nextTile = referenceMaze.GetTile(((byte, byte))(BFSOnlyTile.x + 1, BFSOnlyTile.y));  //EAST
                if (referenceMaze.isLegalMove(BFSOnlyTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        BFSSet.Add(nextTile);
                        validNeighbors++;
                    }
                }

                nextTile = referenceMaze.GetTile(((byte,byte))(BFSOnlyTile.x, BFSOnlyTile.y + 1));   //SOUTH
                if(referenceMaze.isLegalMove(BFSOnlyTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        BFSSet.Add(nextTile);
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
                }
                PseudoBFS(BFSSet[0]);
                
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

            string expStr;
            if(tgtTile.order < 10) { expStr = "0" + Convert.ToString(tgtTile.order); }
            else { expStr =  Convert.ToString(tgtTile.order); }
            //string testOutPutString = expStr + Convert.ToString(tgtTile.GetCoords()) + Convert.ToString(tgtTile.g) + "-" + Convert.ToString(tgtTile.h);
            //Console.WriteLine(testOutPutString);
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
                        exp++;
                        nextTile.order = exp;
                        nextTile.g = (byte)(tgtTile.g + 2);
                        nextTile.h = referenceMaze.GetH(nextTile);
                        f = (byte)(nextTile.g + nextTile.h);
                        frontierSet.Enqueue(nextTile, f);
                        validNeighbors++;
                        //nextTile.SetFace("--");     //DEBUG ONLY
                        //referenceMaze.printMaze();      //DEBUG ONLY
                    }
                    
                }

                nextTile = referenceMaze.GetTile(((byte, byte))(tgtTile.x, tgtTile.y - 1));  //NORTH
                if (referenceMaze.isLegalMove(tgtTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        exp++;
                        nextTile.order = exp;
                        nextTile.g = (byte)(tgtTile.g + 1);
                        nextTile.h = referenceMaze.GetH(nextTile);
                        f = (byte)(nextTile.g + nextTile.h);
                        frontierSet.Enqueue(nextTile, f);
                        validNeighbors++;
                        //nextTile.SetFace("--");     //DEBUG ONLY
                        //referenceMaze.printMaze();      //DEBUG ONLY
                    }
                   
                }

                nextTile = referenceMaze.GetTile(((byte, byte))(tgtTile.x + 1, tgtTile.y));  //EAST
                if (referenceMaze.isLegalMove(tgtTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        exp++;
                        nextTile.order = exp;
                        nextTile.g = (byte)(tgtTile.g + 2);
                        nextTile.h = referenceMaze.GetH(nextTile);
                        f = (byte)(nextTile.g + nextTile.h);
                        frontierSet.Enqueue(nextTile, f);
                        validNeighbors++;
                        //nextTile.SetFace("--");     //DEBUG ONLY
                        //referenceMaze.printMaze();      //DEBUG ONLY
                    }
                    
                }

                nextTile = referenceMaze.GetTile(((byte, byte))(tgtTile.x, tgtTile.y + 1));   //SOUTH
                if (referenceMaze.isLegalMove(tgtTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        exp++;
                        nextTile.order = exp;
                        nextTile.g = (byte)(tgtTile.g + 3);
                        nextTile.h = referenceMaze.GetH(nextTile);
                        f = (byte)(nextTile.g + nextTile.h);
                        frontierSet.Enqueue(nextTile, f);
                        validNeighbors++;
                        //nextTile.SetFace("--");     //DEBUG ONLY
                        //referenceMaze.printMaze();      //DEBUG ONLY
                    }
                }

                List<(Tile, int)> tieSet = new List<(Tile, int)>();
                for(int i = 0; i <= frontierSet.Count; i++)
                {
                    frontierSet.TryDequeue(out Tile x, out int priority);
                    tieSet.Add((x, priority));
                }
                
                for(int i = 0; i < tieSet.Count; i++)
                {
                    for(int j = i+1; j < tieSet.Count; j++)
                    {
                        if (tieSet[i].Item2 == tieSet[j].Item2)
                        {
                            if (tieSet[j].Item1.order < tieSet[i].Item1.order)
                            {
                                (Tile, int) myItem = tieSet[i];
                                tieSet[i] = tieSet[j];      //swap them! If they have equal priority values, put the lower order one first.
                                tieSet[j] = myItem;         //order = order they were added to frontier set. so equal priority items are tiebroken by FIFO!
                            }
                        }
                    }
                }
                foreach (var node in tieSet)
                {
                    frontierSet.Enqueue(node.Item1, node.Item2);
                }
                
                ASTAR(frontierSet.Dequeue());
                
            }
            else
            {
                Console.WriteLine(String.Format("Arrived at {0} in {1} moves", Convert.ToString(tgtTile.GetCoords()), Convert.ToString(tgtTile.order)));
                return;
            }
        }

        public Agent(Maze x) 
        {
            this.referenceMaze = x;
            BFSOnlyTile = referenceMaze.GetPathStart();
        }

    }
}
