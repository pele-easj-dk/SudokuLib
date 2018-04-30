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
            int[,] theMap = SudokuMap.GenerateFullMap();
            Console.WriteLine(SudokuMap.PrintMap(theMap));
            
            Console.ReadLine();
        }
    }
}
