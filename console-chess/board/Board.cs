﻿using System;
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
        public Piece piece(int row, int column)
        {
            return pieces[row, column];
        }

        public void putPiece(Piece p, Position pos)
        {
            pieces[pos.row, pos.column] = p;
            p.position = pos;
        }
    }
}
