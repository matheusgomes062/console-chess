using System;
using System.Collections.Generic;
using System.Text;
using console_chess.board;

namespace console_chess.chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        private int round;
        private Color thisPlayer;
        public bool finished { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            round = 1;
            thisPlayer = Color.White;
            finished = false;
            putPieces();
        }

        public void executeMovement(Position from, Position to)
        {
            Piece p = board.removePiece(from);
            p.incrementQtyMvmt();
            Piece capturedPiece = board.removePiece(to);
            board.putPiece(p, to);
        }

        private void putPieces()
        {
            board.putPiece(new Tower(board, Color.Black), new ChessPosition('c', 1).toPosition());
        }
    }
}
