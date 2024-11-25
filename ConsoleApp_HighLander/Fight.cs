using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HighLander
{
    public class Fight : BehaviorStrategy
    {
        public void execute(ConsoleApp app, Highlander self, Highlander opponent)
        {
            if (self == null || opponent == null)
            {
                Console.WriteLine("Error: both parties must be valid highlanders");
                return;
            
            }

            Console.WriteLine($"{self.Name} (Power: {self.PowerLevel}) is fighting {opponent.Name} (Power: {opponent.PowerLevel})");

            //Find stronger highlander
            Random rand = new Random();
            int totalPower = self.PowerLevel + opponent.PowerLevel;
            int chance = rand.Next(1, totalPower + 1);

            if (chance <= self.PowerLevel)
            {
                Console.WriteLine($"{self.Name} wins against {opponent.Name} and absorbs {opponent.PowerLevel} power!");
                self.PowerLevel += opponent.PowerLevel;
                opponent.IsAlive = false;
            }
            else
            {
                Console.WriteLine($"{opponent.Name} wins against {self.Name} and absorbs {self.PowerLevel} power!");
                opponent.PowerLevel += self.PowerLevel;
                self.IsAlive = false;
            }

        }
    }
}
