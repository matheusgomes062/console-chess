using System;
using System.Collections.Generic;
using System.Text;
using console_chess.board;

namespace console_chess.chess
{
    class King : Piece
    {
        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "R";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }
        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];

            Position pos = new Position(0, 0);

            // north
            pos.defineValues(position.row - 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            // ne
            pos.defineValues(position.row - 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            // east
            pos.defineValues(position.row, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            // se
            pos.defineValues(position.row + 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            // south
            pos.defineValues(position.row + 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            // so
            pos.defineValues(position.row + 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            // west
            pos.defineValues(position.row, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            // no
            pos.defineValues(position.row - 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            
            return mat;
        }
    }
}
