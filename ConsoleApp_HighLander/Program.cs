using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp_HighLander
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name;
            int age, powerLevel, rowPosition, columnPosition;
            int[] position;
            bool isAlive;
            bool exit = false;
            ConsoleApp highlanderApp = new ConsoleApp(5, 5);

            Console.WriteLine("dimension of highlander app is {0} x {1}", highlanderApp.GridRowDimension, highlanderApp.GridColumnDimension);
            while (!exit){
                Console.WriteLine("Input the name of highlander");
                name = Console.ReadLine();
                Console.WriteLine("Input the age of highlander");
                age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input the power level of highlander");
                powerLevel = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input the row position of highlander");
                rowPosition = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input the column position of highlander");
                columnPosition = Convert.ToInt32(Console.ReadLine());
                position = new int[] { rowPosition, columnPosition };
                Console.WriteLine("Input whether the highlander is alive: 1 for alive, 0 for dead");
                isAlive = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                highlanderApp.HighlanderList.Add(new GoodHighlander(name, age, powerLevel, position, isAlive));

                Console.WriteLine("highlander successfully added");
                Console.WriteLine("Do you wanna add another highlander? y or n");
                string ans = Console.ReadLine();
                if(ans == "n") exit = true;

            }

            Console.WriteLine("Select from Option 1 or 2 for the Game /n Option1: Game end until there is only 1 highlander /n Option2: Game ends after certain rounds of play: ");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Logger.Log("User chose option 1.");
                    highlanderApp.playGame(true,false) ;
                    break;
                case 2:
                    Logger.Log("User chose option 2.");
                    highlanderApp.playGame(false, true);
                    break;
            }
            
            Console.Read();
        }
    }
}
