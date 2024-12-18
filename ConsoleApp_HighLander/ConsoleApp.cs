﻿using System;
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
            _grid = new int[_gridRowDimension, _gridColumnDimension];//dimension of the play grid
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
                    ExecuteRound();
                }
                Console.WriteLine("The game has ended. Winner is {0}!", _highlanderList[0].Name);
            }

        
            if (option2)
            {
                Console.WriteLine("Input how many rounds of simulation you want to run:");
                int rounds = Convert.ToInt32(Console.ReadLine());

                for (int round = 1; round <= rounds; round++)
                {
                    Console.WriteLine($"Round {round} begins.");
                    ExecuteRound();
                    Console.WriteLine($"Round {round} ends. Remaining Highlanders: {_highlanderList.Count(h => h.IsAlive)}");
                }

                Console.WriteLine("Simulation complete.");
            }
        }


        private void ExecuteRound()
        {
            var liveHighlanders = _highlanderList.Where(h => h.IsAlive).ToList(); // Snapshot of live highlanders
            foreach (Highlander highlander in liveHighlanders)
            {
                // Checking for collisions
                var opponentsInCell = _highlanderList
                    .Where(h => h.IsAlive && h != highlander && h.Row == highlander.Row && h.Column == highlander.Column)
                    .ToList();
                bool hasBadHighlander = opponentsInCell.Any(h => !h.IsGood);
                if (highlander.IsGood && hasBadHighlander)
                {
                    /*Each bad highlander in the opponents list will try to fight with 
                     * good highlander in the first place*/
                    foreach (Highlander oppo in opponentsInCell)
                    {
                        if (!oppo.IsGood)
                        {
                            oppo.Behavior = new Fight();
                            oppo.ExecuteBehavior(this, highlander);
                            if (highlander.IsAlive)
                            {
                                continue;
                            }
                        }
                    }
                    if (highlander.IsAlive)
                    {
                        highlander.Behavior = new Escape();
                        highlander.ExecuteBehavior(this, highlander);
                      
                    }
                  

                }
                else if (!highlander.IsGood && opponentsInCell.Count > 0)
                {
                    foreach (Highlander oppo in opponentsInCell)
                    {
                        highlander.Behavior = new Fight();
                        highlander.ExecuteBehavior(this, oppo);
                        if (highlander.IsAlive)
                        {
                            continue;
                        }
                    }
                    if (highlander.IsAlive)
                    {
                        highlander.Behavior = new RandomMove();
                        highlander.ExecuteBehavior(this, highlander);
                    }


                }
                else
                {
                    //when the highlander is good and all highlanders in the opponent list in cell is good
                    highlander.Behavior = new RandomMove();
                    highlander.ExecuteBehavior(this, highlander);
                }
            }
            // Remove dead Highlanders after the round
            _highlanderList.RemoveAll(h => !h.IsAlive);
            

        }


    }
}
