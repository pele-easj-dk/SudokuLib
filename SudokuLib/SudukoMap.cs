using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuLib
{
    public class SudukoMap
    {
        private static Random rand = new Random(DateTime.Now.Millisecond);
        private static int[] numberRow = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private static int[] AddNumber = { 0, 3, 6 };
        private static List<Position> AllPosistions = new List<Position>();

        static SudukoMap()
        {
            for (int r = 0; r < 9; r++)
            for (int c = 0; c < 9; c++)
            {
                AllPosistions.Add(new Position() {row = r, col = c});
            }
        }




        public static int[,] GenerateFullMap()
        {
            int[,] tempMap = new int[9,9];

            int[] basicRow = GenerateRow();

            int[] shuffleAdds = ShuffleRows(AddNumber);
            InsertRowInMap(tempMap, 0, CycleRight(basicRow, 0 + shuffleAdds[0]));
            InsertRowInMap(tempMap, 1, CycleRight(basicRow, 0 + shuffleAdds[1]));
            InsertRowInMap(tempMap, 2, CycleRight(basicRow, 0 + shuffleAdds[2]));

            shuffleAdds = ShuffleRows(AddNumber);
            InsertRowInMap(tempMap, 3, CycleRight(basicRow, 1 + shuffleAdds[0]));
            InsertRowInMap(tempMap, 4, CycleRight(basicRow, 1 + shuffleAdds[1]));
            InsertRowInMap(tempMap, 5, CycleRight(basicRow, 1 + shuffleAdds[2]));

            shuffleAdds = ShuffleRows(AddNumber);
            InsertRowInMap(tempMap, 6, CycleRight(basicRow, 2 + shuffleAdds[0]));
            InsertRowInMap(tempMap, 7, CycleRight(basicRow, 2 + shuffleAdds[1]));
            InsertRowInMap(tempMap, 8, CycleRight(basicRow, 2 + shuffleAdds[2]));

            ShuffleColumns(tempMap, 0, 2);
            ShuffleColumns(tempMap, 3, 5);
            ShuffleColumns(tempMap, 6, 8);

            return tempMap;
        }

        private static void ShuffleColumns(int[,] tempMap, int from, int to)
        {
            List<int> columns = new List<int>() { 0, 1, 2 };

            int[,] tempColumns = new int[9,3];

            for (int i = from; i <= to; i++)
            {
                for (int row = 0; row < 9; row++)
                {
                    tempColumns[row, (i - from)] = tempMap[row, i];
                }
            }


            for (int i = 0; i < 3; i++)
            {
                int ix = rand.Next(columns.Count);
                MoveColumn(tempMap, tempColumns, from + i, columns[ix]);
                columns.RemoveAt(ix);
            }

        }

        private static void MoveColumn(int[,] tempMap, int[,] tempColumns, int tempMapIx, int tempColumnIx)
        {
            for (int row = 0; row < 9; row++)
            {
                tempMap[row, tempMapIx] = tempColumns[row, tempColumnIx];
            }
        }


        private static int[] ShuffleRows(int[] addNumber)
        {
            int[] numbers = new int[3];

            List<int> list = new List<int>(addNumber);

            for (int i = 0; i < 3; i++)
            {
                int ix = rand.Next(list.Count);
                numbers[i] = list[ix];
                list.RemoveAt(ix);
            }

            return numbers;
        }

        private static int[] CycleRight(int[] basicRow, int v)
        {
            int[] row = new int[9];

            for (int i = 0; i < 9; i++)
            {
                row[i] = basicRow[(i + v) % 9];
            }

            return row;
        }

        private static void InsertRowInMap(int[,] tempMap, int rowIx, int[] basicRow)
        {
            for (int i = 0; i < 9; i++)
            {
                tempMap[rowIx, i] = basicRow[i];
            }
        }

        private static int[] GenerateRow()
        {
            int[] row = new int[9];

            List<int> list = new List<int>(numberRow);

            for (int i = 0; i < 9; i++)
            {
                int ix = rand.Next(list.Count);
                row[i] = list[ix];
                list.RemoveAt(ix);
            }

            return row;
        }

        public static bool IsFull(int[,] tempMap)
        {
            bool full = true;
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    if (tempMap[r,c] == 0)
                        full = false;

                }
            }
            return full;
        }


        public static String PrintMap(int[,] tempMap)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Suduko map\n");
            sb.Append("\n");
            sb.Append("   1 2 3 4 5 6 7 8 9 \n");
            sb.Append("  -------------------\n");
            for (int i = 0; i < 9; i++)
            {
                sb.Append(i + "| ");
                for (int j = 0; j < 9; j++)
                {
                    sb.Append(tempMap[i, j] + " ");
                }
                sb.Append("\n");
            }
            sb.Append("\n");

            return sb.ToString();
        }

        public static int[,] GenerateMap(int NoOfNumbersBack)
        {
            if (NoOfNumbersBack <= 17)
                throw new ArgumentException("Can not have a Suduko with less than 18 number try with " + NoOfNumbersBack );

            

            bool solution = false;
            int[,] tempMap = null;

            while (!solution)
            {
                tempMap = GenerateFullMap();

                List<Position> restPositions = new List<Position>(AllPosistions);
                while (restPositions.Count > NoOfNumbersBack)
                {
                    // remove next
                    int ix = rand.Next(restPositions.Count);
                    Position pos = restPositions[ix];
                    tempMap[pos.row, pos.col] = 0;
                    restPositions.RemoveAt(ix);
                }


                solution = CheckIfValid(tempMap);
            }
            return tempMap;
        }

        private static bool CheckIfValid(int[,] tempMap)
        {
            int[,] copyMap = new int[9,9];
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    copyMap[r, c] = tempMap[r, c];
                }
            }

            SudukoSolver solver = new SudukoSolver(copyMap);
            return solver.ComputerSolution();
        }
    }
}
