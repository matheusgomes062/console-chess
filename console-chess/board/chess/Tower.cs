using System;
using System.Collections.Generic;
using System.Text;

namespace console_chess.board.chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "T";
        }
    }
}
