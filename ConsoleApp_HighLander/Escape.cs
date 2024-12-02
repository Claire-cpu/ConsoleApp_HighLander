using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HighLander
{
    public class Escape : BehaviorStrategy
    {
        public void execute(ConsoleApp app, Highlander self, Highlander opponent = null)
        {
            string message;
            if (opponent == null)
            {
                Console.WriteLine($"{self.Name} has no opponent nearby to escape from.");
                return;
            }

            // here Checking the type of the opponent
            if (opponent.IsBad && self.IsGood)
            {
             
                int[] selfPos = self.GetPosition();
                int[] opponentPos = opponent.GetPosition();

            
                int[] newPos = CalculateEscapePosition(selfPos, opponentPos, app.GridRowDimension - 1, app.GridColumnDimension - 1);
                self.UpdatePosition(newPos);

                message = $"{self.Name} escaped from {opponent.Name} to position ({newPos[0]}, {newPos[1]}).";
                Console.WriteLine(message);
                Logger.Log(message);
            }
            else
            {
                Console.WriteLine($"{self.Name} did not escape from {opponent.Name}.");
            }
        }

    
        private int[] CalculateEscapePosition(int[] selfPos, int[] opponentPos, int maxRow, int maxCol)
        {
            int newRow = Clamp(selfPos[0] + Math.Sign(selfPos[0] - opponentPos[0]), 0, maxRow);
            int newCol = Clamp(selfPos[1] + Math.Sign(selfPos[1] - opponentPos[1]), 0, maxCol);
            return new int[] { newRow, newCol };
        }


        private int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}
