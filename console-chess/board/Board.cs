using System;
using System.Collections.Generic;
using System.Text;

namespace console_chess.board
{
    class Board
    {
        public int rows { get; set; }
        public int columns { get; set; }
        public Piece[,] pieces;

        public Board(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            pieces = new Piece[rows, columns];
        }
    }
}
