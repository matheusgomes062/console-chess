using System;
using console_chess.board;
using console_chess.board.chess;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.putPiece(new Tower(board, Color.Black), new Position(0, 0));

            Screen.printBoard(board);
        }
    }
}
