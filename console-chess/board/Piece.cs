using System;
using System.Collections.Generic;
using System.Text;

namespace console_chess.board
{
    class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int noMvmt { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Position position, Board board, Color color)
        {
            this.position = position;
            this.board = board;
            this.color = color;
            this.noMvmt = 0;
        }
    }
}
