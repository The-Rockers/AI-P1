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
        Maze rootNode;
        Hashtable exploredSet = new Hashtable();
        PriorityQueue<Maze, int> frontierSet = new PriorityQueue<Maze, int>();
        Tile currentTile, nextTile;

        List<Tile>testSet = new List<Tile>();

        byte f, g, h;
        //f = total. g = cost so far. h = "remaining" cost.

        public void agentMain()
        {
            exploredSet.Add(currentTile, 0);
            //test
            //Brute(currentTile);
        }

        public bool Brute(Tile tgtTile)  //BFS - testSet, FIFO
        {
            byte validNeighbors = 0;
            currentTile= tgtTile;
            Console.WriteLine(Convert.ToString(tgtTile.GetCoords()));
            currentTile.SetFace("00");

            if (currentTile.GetCoords() != rootNode.end)
            {
                if (!exploredSet.ContainsKey(currentTile))
                {
                    exploredSet.Add(currentTile, 0);
                    testSet.Remove(currentTile);
                }

                //WNES order
                nextTile = rootNode.GetTile(((byte, byte))(currentTile.x - 1, currentTile.y));
                if (rootNode.isLegalMove(currentTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        testSet.Add(nextTile);
                        validNeighbors++;
                    }
                }

                nextTile = rootNode.GetTile(((byte, byte))(currentTile.x, currentTile.y - 1));
                if (rootNode.isLegalMove(currentTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        testSet.Add(nextTile);
                        validNeighbors++;
                    }
                }

                nextTile = rootNode.GetTile(((byte, byte))(currentTile.x + 1, currentTile.y));
                if (rootNode.isLegalMove(currentTile, nextTile))
                {
                    if (!exploredSet.ContainsKey(nextTile))
                    {
                        testSet.Add(nextTile);
                        validNeighbors++;
                    }
                }

                nextTile = rootNode.GetTile(((byte,byte))(currentTile.x, currentTile.y + 1));
                if(rootNode.isLegalMove(currentTile, nextTile))
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

        public Agent(Maze x) 
        {
            this.rootNode = x;
            currentTile = rootNode.GetPathStart(); 
        }

    }
}
