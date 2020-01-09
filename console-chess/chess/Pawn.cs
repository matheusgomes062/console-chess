using System;
using System.Collections.Generic;
using System.Text;
using console_chess.board;

namespace console_chess.chess
{
    class Pawn : Piece
    {

        public Pawn(Board board, Color color) : base(board, color)
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
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && qteMovimentos == 0)
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
                if (position.row == 3)
                {
                    Position esquerda = new Position(position.row, position.column - 1);
                    if (board.validPosition(esquerda) && hasEnemy(esquerda) && board.piece(esquerda) == match.vulneravelEnPassant)
                    {
                        mat[esquerda.row - 1, esquerda.column] = true;
                    }
                    Position direita = new Position(position.row, position.column + 1);
                    if (board.validPosition(direita) && hasEnemy(direita) && board.piece(direita) == match.vulneravelEnPassant)
                    {
                        mat[direita.row - 1, direita.column] = true;
                    }
                }
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
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && qteMovimentos == 0)
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

                // #jogadaespecial en passant
                if (position.row == 4)
                {
                    Position esquerda = new Position(position.row, position.column - 1);
                    if (board.validPosition(esquerda) && hasEnemy(esquerda) && board.piece(esquerda) == match.vulneravelEnPassant)
                    {
                        mat[esquerda.row + 1, esquerda.column] = true;
                    }
                    Position direita = new Position(position.row, position.column + 1);
                    if (board.validPosition(direita) && hasEnemy(direita) && board.piece(direita) == match.vulneravelEnPassant)
                    {
                        mat[direita.row + 1, direita.column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
