using System;
using System.Collections.Generic;
using System.Text;
using console_chess.board;

namespace console_chess.chess
{
    class ChessPosition
    {
        public char column { get; set; }
        public int row { get; set; }

        public ChessPosition(char column, int row)
        {
            this.column = column;
            this.row = row;
        }

        public Position toPosition()
        {
            return new Position(8 - row, column - 'a');
        }

        public override string ToString()
        {
            return "" + column + row;
        }
    }
}
