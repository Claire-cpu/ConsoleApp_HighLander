using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp_HighLander
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input the name of highlander");
            string name = Console.ReadLine();

            Highlander h1 = new GoodHighlander(1, name, 25, 10, new int[] {2,1}, true);

            Console.WriteLine("the created highlander h1 with name {0} and initial position {1}:{2}",name, h1.Position[0], h1.Position[1]);
            ConsoleApp highlanderApp = new ConsoleApp(5,5);

            Console.WriteLine("dimension of highlander app is {0} x {1}", highlanderApp.GridRowDimension, highlanderApp.GridColumnDimension);
            highlanderApp.HighlanderList.Add(h1);

            Console.WriteLine("highlander successfully added");
            Console.WriteLine("Select from Option 1 or 2 for the Game /n Option1: Game end until there is only 1 highlander /n Option2: Game ends after certain rounds of play: ");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1: 
                    highlanderApp.playGame(true,false) ;
                    break;
                case 2: 
                    highlanderApp.playGame(false, true);
                    break;
            }
            
            Console.Read();
        }
    }
}
