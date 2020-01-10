using System;
using System.Collections.Generic;
using System.Text;
using console_chess.board;

namespace console_chess.chess
{
    class Pawn : Piece
    {

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        private bool hasEnemy(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }

        private bool free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];

            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.defineValues(position.row - 1, position.column);
                if (board.validPosition(pos) && free(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row - 2, position.column);
                Position p2 = new Position(position.row - 1, position.column);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && noMvmt == 0)
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row - 1, position.column - 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row - 1, position.column + 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                // #jogadaespecial en passant
                //if (position.row == 3)
                //{
                //    Position west = new Position(position.row, position.column - 1);
                //    if (board.validPosition(west) && hasEnemy(west) && board.piece(west) == match.vulneravelEnPassant)
                //    {
                //        mat[west.row - 1, west.column] = true;
                //    }
                //    Position east = new Position(position.row, position.column + 1);
                //    if (board.validPosition(east) && hasEnemy(east) && board.piece(east) == match.vulneravelEnPassant)
                //    {
                //        mat[east.row - 1, east.column] = true;
                //    }
                //}
            }
            else
            {
                pos.defineValues(position.row + 1, position.column);
                if (board.validPosition(pos) && free(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row + 2, position.column);
                Position p2 = new Position(position.row + 1, position.column);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && noMvmt == 0)
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row + 1, position.column - 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row + 1, position.column + 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                //// #jogadaespecial en passant
                //if (position.row == 4)
                //{
                //    Position west = new Position(position.row, position.column - 1);
                //    if (board.validPosition(west) && hasEnemy(west) && board.piece(west) == match.vulneravelEnPassant)
                //    {
                //        mat[west.row + 1, west.column] = true;
                //    }
                //    Position east = new Position(position.row, position.column + 1);
                //    if (board.validPosition(east) && hasEnemy(east) && board.piece(east) == match.vulneravelEnPassant)
                //    {
                //        mat[east.row + 1, east.column] = true;
                //    }
                //}
            }

            return mat;
        }
    }
}
