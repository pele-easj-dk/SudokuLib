using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public class SudukoSolver
    {
        private static int[] numberRow = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private int[,] sudukoMap;

        public SudukoSolver(int[,] sudukoMap)
        {
            this.sudukoMap = sudukoMap;
        }

        public bool ComputerSolution()
        {
            bool NoSolution = false;

            while (!(IsFinished() || NoSolution))
            {
                NoSolution = !ComputerNextSolution();
            }
            return IsFinished();
        }


        public bool ComputerNextSolution()
        {

            if (IsFinished())
            {
                return true;
            }

            // next iteration
            PositionValues val = FindNext();
            int[] ints = val.Values;

            if (ints.Length == 1)
            {
                // only one choice insert value - this solution is a success
                sudukoMap[val.Pos.row, val.Pos.col] = ints[0];
                return true;
            }
            
            // more than one solution - try from the start
            for (int i = 0; i < ints.Length; i++)
            {
                sudukoMap[val.Pos.row, val.Pos.col] = ints[i];

                if (ComputerNextSolution())
                {
                    return true;
                }

                // else delete and try next
                sudukoMap[val.Pos.row, val.Pos.col] = 0;
            }

            return false;
        }

        public PositionValues FindNext()
        {
            PositionValues returnVar = new PositionValues(new Position(), null);
            int minNo = 999; // number of possible values

            Position pos = new Position();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    pos.row = row;
                    pos.col = col;
                    int val = sudukoMap[row,col];
                    
                    
                    if (val == 0)
                    { // no value in this posistion

                        List<int> missingNumbers = this.GetMissingNumbers(pos);
                        if (missingNumbers.Count < minNo)
                        { // find the one with fewest alternatives optimal only one 
                            minNo = missingNumbers.Count;
                            returnVar.Pos = pos;
                            returnVar.Values = missingNumbers.ToArray();
                        } 
                    }
                    
                    // if one posistion with only one posssible value - pick that
                    if (minNo == 1) break;
                }
                if (minNo == 1) break;
            }
            return returnVar;
        }

        
        public bool IsFinished()
        {
            return SudukoMap.IsFull(sudukoMap);
        }

        
        //public bool hasNext()
        //{
        //    return true;
        //}

        public List<int> GetMissingNumbers(Position pos)
        {
            // initial all numbers are possible 
            HashSet<int> missingNumbers = new HashSet<int>(numberRow);
            int posRow = pos.row;
            int posCol = pos.col;
            
            // remove from column
            for (int r = 0; r < 9; r++)
            {
                int value = sudukoMap[r, posCol];
                if (value != 0)
                {
                    missingNumbers.Remove(value);
                }
            }

            // remove from row
            if (missingNumbers.Count > 1)
            for (int c = 0; c < 9; c++)
            {
                int value = sudukoMap[posRow, c];
                if (value != 0)
                {
                    missingNumbers.Remove(value);
                }
            }

            // remove from square
            int lowerRow = (posRow / 3) * 3;
            int lowerCol = (posCol / 3) * 3;

            if (missingNumbers.Count > 1)
                for (int r = lowerRow; r < (lowerRow + 3); r++)
                for (int c = lowerCol; c < (lowerCol + 3); c++)
                {
                    int value = sudukoMap[r, c];
                    if (value != 0)
                    {
                        missingNumbers.Remove(value);
                    }
                }

            return missingNumbers.ToList();
        }

    }
}
