using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HighLander
{
    public class Escape : BehaviorStrategy
    {
        public void execute(ConsoleApp app, Highlander self, Highlander opponent=null) {

        if (self == null)
         {
                Console.WriteLine("Error: Self cannot be null.");
                return;
         }
        
        if (opponent == null)
        {
        Console.WriteLine($"{self.Name} has no opponent nearby to escape from.");
        return;
        }

        int[] selfPos = self.GetPosition();
        int[] opponentPos = opponent.GetPosition();

         int newRow = selfPos[0] + Math.Sign(selfPos[0] - opponentPos[0]);
         int newCol = selfPos[1] + Math.Sign(selfPos[1] - opponentPos[1]);


         newRow = Math.Clamp(newRow, 0, app.GridRowDimension - 1);
         newCol = Math.Clamp(newCol, 0, app.GridColumnDimension - 1); // Assuming app.GridColumn defines the grid width
         
         self.UpdatePosition(new int[] { newRow, newCol });

         Console.WriteLine($"{self.Name} escaped from {opponent.Name} to position ({newRow}, {newCol}).");

        }
    }
}
