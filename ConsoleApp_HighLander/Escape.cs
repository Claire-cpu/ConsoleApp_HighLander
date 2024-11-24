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
        if (opponent == null)
        {
        Console.WriteLine($"{self.GetName()} has no opponent nearby to escape from.");
        return;
        }

        int[] selfPos = self.GetPosition();
        int[] opponentPos = opponent.GetPosition();

         int newRow = selfPos[0] + (selfPos[0] - opponentPos[0]);
         int newCol = selfPos[1] + (selfPos[1] - opponentPos[1]);

         newRow = Math.Clamp(newRow, 0, app.GridRow - 1);
         newCol = Math.Clamp(newCol, 0, app.GridColumn - 1); // Assuming app.GridColumn defines the grid width
         
         self.UpdatePosition(new int[] { newRow, newCol });

         Console.WriteLine($"{self.GetName()} escaped from {opponent.GetName()} to position ({newRow}, {newCol}).");

        }
    }
}
