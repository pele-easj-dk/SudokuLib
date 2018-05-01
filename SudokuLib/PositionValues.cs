using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public struct Position
    {
        public int row;
        public int col;
    }

    public class PositionValues
    {
        public Position Pos { get; set; }
        public int[] Values { get; set; }

        public PositionValues(Position pos, int[] values)
        {
            this.Pos = pos;
            if (values == null)
                values = new int[0];

            this.Values = values;
        }

    }
}
