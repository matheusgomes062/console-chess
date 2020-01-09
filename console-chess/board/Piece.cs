using System;
using System.Collections.Generic;
using System.Text;

namespace console_chess.board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }

        //noMvmt = numberOfMovements
        public int noMvmt { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            this.noMvmt = 0;
        }

        public void incrementQtyMvmt()
        {
            noMvmt++;

        }
        public void decrementQtyMvmt()
        {
            noMvmt++;

        }

        public bool hasPossibleMoves()
        {
            bool[,] mat = possibleMoves();
            for (int i = 0; i < board.rows; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool possibleMove(Position pos)
        {
            return possibleMoves()[pos.row, pos.column];
        }

        public bool canMoveTo(Position pos)
        {
            return possibleMoves()[pos.row, pos.column];
        }

        public abstract bool[,] possibleMoves();
    }
}
