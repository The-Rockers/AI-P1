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

        public void agentMain()
        {
            exploredSet.Add(currentTile, 0);
        }

        public void brute(Maze maze, Tile tgtTile)
        {
            while(currentTile.GetCoords() != maze.end)
            {
                nextTile = maze.GetTile(((byte, byte))(currentTile.x - 1, currentTile.y));
                if (maze.isLegalMove(currentTile, nextTile))
                {
                    testSet.Add(nextTile);
                }
                nextTile = maze.GetTile(((byte,byte))(currentTile.x, currentTile.y + 1));
                if(maze.isLegalMove(currentTile, nextTile))
                {
                    testSet.Add(nextTile);
                }
                nextTile = maze.GetTile(((byte, byte))(currentTile.x + 1, currentTile.y));
                if (maze.isLegalMove(currentTile, nextTile))
                {
                    testSet.Add(nextTile);
                }
                nextTile = maze.GetTile(((byte, byte))(currentTile.x, currentTile.y - 1));
                if (maze.isLegalMove(currentTile, nextTile))
                {
                    testSet.Add(nextTile);
                }
                foreach(Tile t in testSet)
                {
                    if (exploredSet.ContainsKey(t))
                    {
                        testSet.Remove(t);
                    }
                }

            }
        }

        public Agent(Maze x) 
        {
            this.rootNode = x;
            currentTile = rootNode.GetPathStart(); 
        }

    }
}
