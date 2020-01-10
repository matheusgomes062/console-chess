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
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool checkMate { get; private set; }
        public Piece vulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            round = 1;
            thisPlayer = Color.White;
            finished = false;
            checkMate = false;
            vulnerableEnPassant = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            putPieces();
        }

        public Piece executeMovement(Position from, Position to)
        {
            Piece p = board.removePiece(from);
            p.incrementQtyMvmt();
            Piece capturedPiece = board.removePiece(to);
            board.putPiece(p, to);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            // #jogadaespecial roque pequeno
            if (p is King && to.column == from.column + 2)
            {
                Position origemT = new Position(from.row, from.column + 3);
                Position destinoT = new Position(from.row, from.column + 1);
                Piece T = board.removePiece(origemT);
                T.incrementQtyMvmt();
                board.putPiece(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is King && to.column == from.column - 2)
            {
                Position origemT = new Position(from.row, from.column - 4);
                Position destinoT = new Position(from.row, from.column - 1);
                Piece T = board.removePiece(origemT);
                T.incrementQtyMvmt();
                board.putPiece(T, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Pawn)
            {
                if (from.column != to.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(to.row + 1, to.column);
                    }
                    else
                    {
                        posP = new Position(to.row - 1, to.column);
                    }
                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }


            return capturedPiece;
        }

        public void undoMovement(Position from, Position to, Piece capturedPiece)
        {
            Piece p = board.removePiece(to);
            p.decrementQtyMvmt();
            if (capturedPiece != null)
            {
                board.putPiece(capturedPiece, to);
                captured.Remove(capturedPiece);
            }

            // #jogadaespecial roque pequeno
            if (p is King && to.column == from.column + 2)
            {
                Position origemT = new Position(from.row, from.column + 3);
                Position destinoT = new Position(from.row, from.column + 1);
                Piece T = board.removePiece(destinoT);
                T.decrementQtyMvmt();
                board.putPiece(T, origemT);
            }

            // #jogadaespecial roque grande
            if (p is King && to.column == from.column - 2)
            {
                Position origemT = new Position(from.row, from.column - 4);
                Position destinoT = new Position(from.row, from.column - 1);
                Piece T = board.removePiece(destinoT);
                T.decrementQtyMvmt();
                board.putPiece(T, origemT);
            }

            //# jogadaespecial en passant
            if (p is Pawn)
            {
                if (from.column != to.column && capturedPiece == vulnerableEnPassant)
                {
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(to.row + 1, to.column);
                    }
                    else
                    {
                        posP = new Position(to.row - 1, to.column);
                    }
                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }

        }

        public void makeAMove(Position from, Position to)
        {
            Piece capturedPiece = executeMovement(from, to);

            if (isCheckmate(thisPlayer))
            {
                undoMovement(from, to, capturedPiece);
                throw new boardException("Você não pode se colocar em xeque!");
            }

            if (isCheckmate(adversary(thisPlayer)))
            {
                checkMate = true;
            }
            else
            {
                checkMate = false;
            }

            if (testCheckmate(adversary(thisPlayer)))
            {
                finished = true;
            }
            else
            {
                round++;
                changePlayer();
            }

            Piece p = board.piece(to);

            // #jogadaespecial promocao
            if (p is Pawn)
            {
                if ((p.color == Color.White && to.row == 0) || (p.color == Color.Black && to.row == 7))
                {
                    p = board.removePiece(to);
                    pieces.Remove(p);
                    Piece queen = new Queen(board, p.color);
                    board.putPiece(queen, to);
                    pieces.Add(queen);
                }
            }

            // #jogadaespecial en passant
            if (p is Pawn && (to.row == from.row - 2 || to.row == from.row + 2))
            {
                vulnerableEnPassant = p;
            }
            else
            {
                vulnerableEnPassant = null;
            }

        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        private Color adversary(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in piecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool isCheckmate(Color color)
        {
            Piece R = king(color);
            if (R == null)
            {
                throw new boardException("Não tem rei da color " + color + " no tabuleiro!");
            }
            foreach (Piece x in piecesInGame(adversary(color)))
            {
                bool[,] mat = x.possibleMoves();
                if (mat[R.position.row, R.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }


        public void validateOriginPosition(Position pos)
        {
            if (board.piece(pos) == null)
            {
                throw new boardException("Não existe peça na posição de from escolhida!");
            }
            if (thisPlayer != board.piece(pos).color)
            {
                throw new boardException("A peça de from escolhida não é sua!");
            }
            if (!board.piece(pos).hasPossibleMoves())
            {
                throw new boardException("Não há movimentos possíveis para a peça de from escolhida!");
            }
        }

        public void validateDestination(Position from, Position to)
        {
            if (!board.piece(from).possibleMove(to))
            {
                throw new boardException("Posição de to inválida!");
            }
        }

        private void changePlayer()
        {
            if (thisPlayer == Color.White)
            {
                thisPlayer = Color.Black;
            }
            else
            {
                thisPlayer = Color.White;
            }
        }

        public bool testCheckmate(Color color)
        {
            if (!isCheckmate(color))
            {
                return false;
            }
            foreach (Piece x in piecesInGame(color))
            {
                bool[,] mat = x.possibleMoves();
                for (int i = 0; i < board.rows; i++)
                {
                    for (int j = 0; j < board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position from = x.position;
                            Position to = new Position(i, j);
                            Piece capturedPiece = executeMovement(from, to);
                            bool testCheckmate = isCheckmate(color);
                            undoMovement(from, to, capturedPiece);
                            if (!testCheckmate)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void putNewPiece(char column, int row, Piece piece)
        {
            board.putPiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {
            putNewPiece('a', 1, new Tower(board, Color.White));
            putNewPiece('b', 1, new Horse(board, Color.White));
            putNewPiece('c', 1, new Bishop(board, Color.White));
            putNewPiece('d', 1, new Queen(board, Color.White));
            putNewPiece('e', 1, new King(board, Color.White, this));
            putNewPiece('f', 1, new Bishop(board, Color.White));
            putNewPiece('g', 1, new Horse(board, Color.White));
            putNewPiece('h', 1, new Tower(board, Color.White));
            putNewPiece('a', 2, new Pawn(board, Color.White, this));
            putNewPiece('b', 2, new Pawn(board, Color.White, this));
            putNewPiece('c', 2, new Pawn(board, Color.White, this));
            putNewPiece('d', 2, new Pawn(board, Color.White, this));
            putNewPiece('e', 2, new Pawn(board, Color.White, this));
            putNewPiece('f', 2, new Pawn(board, Color.White, this));
            putNewPiece('g', 2, new Pawn(board, Color.White, this));
            putNewPiece('h', 2, new Pawn(board, Color.White, this));

            putNewPiece('a', 8, new Tower(board, Color.Black));
            putNewPiece('b', 8, new Horse(board, Color.Black));
            putNewPiece('c', 8, new Bishop(board, Color.Black));
            putNewPiece('d', 8, new Queen(board, Color.Black));
            putNewPiece('e', 8, new King(board, Color.Black, this));
            putNewPiece('f', 8, new Bishop(board, Color.Black));
            putNewPiece('g', 8, new Horse(board, Color.Black));
            putNewPiece('h', 8, new Tower(board, Color.Black));
            putNewPiece('a', 7, new Pawn(board, Color.Black, this));
            putNewPiece('b', 7, new Pawn(board, Color.Black, this));
            putNewPiece('c', 7, new Pawn(board, Color.Black, this));
            putNewPiece('d', 7, new Pawn(board, Color.Black, this));
            putNewPiece('e', 7, new Pawn(board, Color.Black, this));
            putNewPiece('f', 7, new Pawn(board, Color.Black, this));
            putNewPiece('g', 7, new Pawn(board, Color.Black, this));
            putNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}
