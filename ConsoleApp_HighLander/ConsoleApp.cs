using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HighLander
{
    public class ConsoleApp
    {
        private List<Highlander> _highlanderList = new List<Highlander>();
        private int _gridRowDimension;
        private int _gridColumnDimension;
        private int[,] _grid; 

        public ConsoleApp(int gridRowDimension, int gridColumnDimension)
        {
            _gridRowDimension = gridRowDimension;
            _gridColumnDimension = gridColumnDimension;
            _grid = new int[_gridRowDimension, _gridColumnDimension];
        }

        public List<Highlander> HighlanderList
        {
            get { return _highlanderList; }
            set { _highlanderList = value; }
        }

        public int GridRowDimension
        {
            get { return _gridRowDimension; }
            set { _gridRowDimension = value; }
        }

        public int GridColumnDimension
        {
            get { return _gridColumnDimension; }
            set { _gridColumnDimension = value; }
        }

        public void AddHighlander(Highlander highlander)
        {
            if (_highlanderList.Any(h => h.Name.Equals(highlander.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("A Highlander with the same name already exists.");
            }
            _highlanderList.Add(highlander);
        }

        public void RemoveHighlander(Highlander highlander)
        {
            _highlanderList.RemoveAll(h => h.Id == highlander.Id);
        }


        public void PlayGame(bool option1, bool option2)
        {
            Fight fight = new Fight();

        
            if (option1)
            {
                while (_highlanderList.Count(h => h.IsAlive) > 1)
                {
                    ExecuteRound(fight);
                }
                Console.WriteLine("The game has ended. Only one Highlander remains!");
            }

        
            if (option2)
            {
                Console.WriteLine("Input how many rounds of simulation you want to run:");
                int rounds = Convert.ToInt32(Console.ReadLine());

                for (int round = 1; round <= rounds; round++)
                {
                    Console.WriteLine($"Round {round} begins.");
                    ExecuteRound(fight);
                    Console.WriteLine($"Round {round} ends. Remaining Highlanders: {_highlanderList.Count(h => h.IsAlive)}");
                }

                Console.WriteLine("Simulation complete.");
            }
        }

    
        private void ExecuteRound(Fight fight)
        {
            foreach (Highlander highlander in _highlanderList.Where(h => h.IsAlive))
            {
                // Checking for collisions
                var opponentsInCell = _highlanderList
                    .Where(h => h.IsAlive && h != highlander && h.Row == highlander.Row && h.Column == highlander.Column)
                    .ToList();

                if (highlander.IsGood && opponentsInCell.Any())
                {
                    // Determining here whether to escape or fight
                    bool shouldEscape = opponentsInCell.Any(oppo =>
                        (!oppo.IsGood));

                    if (shouldEscape)
                    {
                        highlander.Behavior = new Escape();
                        highlander.Behavior.Execute(this, highlander);
                        Console.WriteLine($"{highlander.Name} escaped.");
                        continue; // Skip further logic for this Highlander in this round
                    }

                    // Otherwise, fight the first opponent in the cell
                    Highlander opponent = opponentsInCell.First();
                    highlander.Behavior = fight;
                    highlander.Behavior.Execute(this, highlander, opponent);

                    if (!opponent.IsAlive)
                    {
                        Console.WriteLine($"{highlander.Name} defeated {opponent.Name}.");
                    }
                }
                else
                {
               
                    highlander.Behavior = new RandomMove();
                    highlander.Behavior.Execute(this, highlander);
                }
            }

            // Remove dead Highlanders after the round
            _highlanderList.RemoveAll(h => !h.IsAlive);
        }
    }
}
