using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HighLander
{
    public class ConsoleApp
    {
        private List<Highlander> _highlanderList = new List<Highlander>(); // list of highlanders
        private int _gridRowDimension; // num of rows for the grid where highlander game happen
        private int _gridColumnDimension;// num of columns for the grid where highlander game happen
        private int[,] _grid; //two dimensional grid

        public ConsoleApp(int gridRowDimension, int gridColumnDimension)
        {
            _gridRowDimension = gridRowDimension;
            _gridColumnDimension = gridColumnDimension;
            _grid = new int[_gridRowDimension, _gridColumnDimension];
        }

        public List<Highlander> HighlanderList
        {
            get { return _highlanderList; }
            set
            {
                _highlanderList = value;
            }
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

        /*Add a highlander to the highlander list*/
        public void addHighlander(Highlander highlander)
        {
            bool exist = false;
            foreach (Highlander item in HighlanderList)
            {
                if (item.Name.Equals(highlander.Name)) exist = true;
            }
            if (!exist) { HighlanderList.Add(highlander); }
            else { throw new Exception("highlander with the same name already exist"); }
        }

        /*Remove a highlander to the highlander list*/
        public void removeHighlander(Highlander highlander)
        {
            foreach (Highlander item in HighlanderList)
            {
                if (item.Id == highlander.Id) { HighlanderList.Remove(item); }
            }
        }

        /*Implement the logic of the Highlander's behavior, i.e. random moving, escape, fight, */
        public void playGame(bool option1, bool option2)
        {
            Fight fight = new Fight();
            /*option1: Game ends only when there is 1 highlander left
            * option2: Game ends after certain rounds of simulation specified by user
            */
            //Game logic implementation for option1
            if (option1)
            {
                while (HighlanderList.Count(h => h.IsAlive) > 1)
                {
                    // Update position for each highlander after the game begins
                    foreach (Highlander highlander in HighlanderList.Where(h => h.IsAlive))
                    {
                        if (highlander.IsAlive)
                        {
                            highlander.Behavior = new RandomMove();
                            highlander.Behavior.execute(this, highlander);

                            //Check for collisions
                            var opponentsInCell = HighlanderList
                                    .Where(h => h.IsAlive &&
                                           h != highlander &&
                                           h.Row == highlander.Row &&
                                           h.Column == highlander.Column)
                                    .ToList();
                            

                            while (opponentsInCell.Count > 0)
                            {
                                Highlander opponent = opponentsInCell.First();
                                highlander.Behavior = fight;
                                highlander.Behavior.execute(this, highlander, opponent);

                                //Remove defeated highlanders
                                opponentsInCell = opponentsInCell.Where(h => h.IsAlive).ToList();
                            }
                        }
                    }
                }
            }

            //Game logic implementation for option2
            if (option2)
            {
                Console.WriteLine("Input how many rounds of simulation you wanna run?");
                int rounds = Convert.ToInt32(Console.ReadLine());
                Logger.Log($"User started {rounds} rounds.");
                for (int i = 1; i <= rounds; i++)
                {
                    foreach (Highlander highlander in HighlanderList.Where(h => h.IsAlive))
                    {
                        if (highlander.PowerLevel < 20)
                        {
                                highlander.Behavior = new Escape();
                        }
                        else
                        {
                            highlander.Behavior = new RandomMove();
                        }
                        
                        highlander.Behavior.execute(this, highlander);

                        //Check for collisions
                        var opponentsInCell = HighlanderList
                                    .Where(h => h.IsAlive &&
                                           h != highlander &&
                                           h.Row == highlander.Row &&
                                           h.Column == highlander.Column)
                                    .ToList();


                        while (opponentsInCell.Count > 0)
                        {
                            Highlander opponent = opponentsInCell.First();
                            highlander.Behavior = fight;
                            highlander.Behavior.execute(this, highlander, opponent);

                            //Remove defeated highlanders
                            opponentsInCell = opponentsInCell.Where(h => h.IsAlive).ToList();
                        }
                    }

                    Logger.Log($"Round {i} completed.");
                    HighlanderList.RemoveAll(h => !h.IsAlive);
                }

                Logger.Log("Simulation Complete.");
            }
        }
    }
}
