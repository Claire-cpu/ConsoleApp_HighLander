using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HighLander
{
    public class RandomMove : BehaviorStrategy
    {
        /*1 represent moving direction of north
          2 represent moving direction of northEast
          3 represent moving direction of east
          4 represent moving direction of southEast
          5 represent moving direction of south
          6 represent moving direction of southWest
          7 represent moving direction of west
          8 represent moving direction of northWest
        In total 8 moving directions as requried by the project
         */
        Dictionary<int, int[]> step = new Dictionary<int, int[]> {
            { 1, new int[2]{0,1} },
            { 2, new int[2]{1,1} },
            { 3, new int[2]{1,0} },
            { 4, new int[2]{1,-1} },
            { 5, new int[2]{0,-1} },
            { 6, new int[2]{-1,-1} },
            { 7, new int[2]{-1,0} },
            { 8, new int[2]{-1,1} },
        };
        public void execute(ConsoleApp app, Highlander self, Highlander opponent=null) {
            Random rand = new Random();
            int[] newDir = step[rand.Next(1, 9)]; //randomly generate number from 1 to 8, each represent a unique moving direction
            Console.WriteLine("new move direction is {0} {1}", newDir[0], newDir[1]);
            while (self.Position[0] + newDir[0]>app.GridColumnDimension-1 || self.Position[0] + newDir[0] < 0)
            {
                newDir = step[rand.Next(1, 9)]; //If the randomly genrated moving direction causes the highlander to move outside the boundary, regenerate a direction
            }
            self.Position[0] += newDir[0]; // update position in the row index
            self.Position[1] += newDir[1]; // update position in the column index

            Console.WriteLine("Test: {0}'s updated position is ({1}, {2})", self.Name, self.Position[0], self.Position[1]);
        }
    }
}
