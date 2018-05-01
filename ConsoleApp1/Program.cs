using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuLib;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[,] theFullMap = SudukoMap.GenerateFullMap();
            //Console.WriteLine(SudukoMap.PrintMap(theFullMap));

            //// make sparse wholes
            //theFullMap[3, 6] = 0;
            //theFullMap[2, 3] = 0;
            //theFullMap[8, 0] = 0;
            //theFullMap[0, 8] = 0;
            //theFullMap[4, 4] = 0;
            //theFullMap[6, 1] = 0;
            //Console.WriteLine(SudukoMap.PrintMap(theFullMap));

            //SudukoSolver solver = new SudukoSolver(theFullMap);
            //Console.WriteLine(solver.ComputerSolution());
            //Console.WriteLine(SudukoMap.PrintMap(theFullMap));


            int[,] theMap = SudukoMap.GenerateMap(17);
            Console.WriteLine(SudukoMap.PrintMap(theMap));

            SudukoSolver solver2 = new SudukoSolver(theMap);
            Console.WriteLine(solver2.ComputerSolution());
            Console.WriteLine(SudukoMap.PrintMap(theMap));




            Console.ReadLine();
        }
    }
}
