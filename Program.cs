using System.Collections;

namespace P1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Maze myMaze = new Maze(11,12);
            
            //Goal state: current tile coordinates = (11,3)


            (byte,byte)[] pathSpaces = 
            {   (1,0), (1,1), (1,2), (1,3), (1,4), (1,5), (1,7), (1,8), (1,9),
                (2,1), (2,5), (2,6), (2,7), (2,9),
                (3,1), (3,3), (3,7), (3,9),
                (4,1), (4,2), (4,3), (4,5), (4,6), (4,7), (4,9), //1 2 3 5 6 7 9
                (5,5), (5,9), //5 9
                (6,1), (6,2), (6,3), (6,5), (6,7), (6,8), (6,9), //1 2 3 5 7 8 9
                (7,1), (7,3), (7,7), //1 3 7
                (8,1), (8,3), (8,4), (8,5), (8,6), (8,7), (8,8), (8,9), //1 3 4 5 6 7 8 9
                (9,1), (9,5), (9,9),//1 5 9
                (10,1), (10,2), (10,3), (10,5), (10,6), (10,7), (10,9), //1 2 3 5 6 7 9
                (11,3)  
            };  //didn't like writing this but what ever

            myMaze.makePath(pathSpaces);    //first entry in list set to maze's start. last entry set to exit.
            myMaze.printMaze();
            Agent agent1 = new Agent(myMaze);
            agent1.Brute(myMaze.GetPathStart());

            Console.WriteLine(pathSpaces.Length);
            Console.WriteLine("aeiou");
            myMaze.printMaze();

            

        }



/*  0  1  2  3  4  5  6  7  8  9  10 11 
 *  ## 00 ## ## ## ## ## ## ## ## ## ## 0
 *  ## 01 02 [] [] ## [] [] [] [] [] ## 1
 *  ## 03 ## ## [] ## [] ## ## ## [] ## 2
 *  ## [] ## [] [] ## [] [] [] ## [] [] 3
 *  ## [] ## ## ## ## ## ## [] ## ## ## 4
 *  ## [] [] ## [] [] [] ## [] [] [] ## 5
 *  ## ## [] ## [] ## ## ## [] ## [] ## 6
 *  ## [] [] [] [] ## [] [] [] ## [] ## 7
 *  ## [] ## ## ## ## [] ## [] ## ## ## 8
 *  ## [] [] [] [] [] [] ## [] [] [] ## 9
 *  ## ## ## ## ## ## ## ## ## ## ## ## 10
    seeking shortest path from 1,0 to 11,3
 */
    }
}