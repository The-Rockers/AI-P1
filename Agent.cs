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

        public void agentMain()
        {
            exploredSet.Add(currentTile, 0);
        }

        public Agent(Maze x) 
        {
            this.rootNode = x;
            currentTile = rootNode.GetPathStart(); 
        }

    }
}
