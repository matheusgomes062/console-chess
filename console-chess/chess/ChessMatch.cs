using System;
using System.Collections.Generic;
using System.Text;
using console_chess.board;

namespace console_chess.chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public int round { get; private set; }
        public Color thisPlayer { get; private set; }
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

        public void makeAMove(Position from, Position to)
        {
            executeMovement(from, to);
            round++;
            changePlayer();
        }

        public void validateOriginPosition(Position pos)
        {
            if(board.piece(pos) == null)
            {
                throw new boardException("Não existe peça na posição de origem escolhida!");
            }
            if(thisPlayer != board.piece(pos).color)
            {
                throw new boardException("A peça de origem escolhida não é sua!");
            }
            if(!board.piece(pos).hasPossibleMoves())
            {
                throw new boardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validateDestination(Position from, Position to)
        {
            if(!board.piece(from).canMoveTo(to))
            {
                throw new boardException("Posição de destino inválida!");
            }
        }

        private void changePlayer()
        {
            if(thisPlayer == Color.White)
            {
                thisPlayer = Color.Black;
            }
            else
            {
                thisPlayer = Color.White;
            }
        }

        private void putPieces()
        {
            board.putPiece(new Tower(board, Color.Black), new ChessPosition('c', 1).toPosition());
        }
    }
}
