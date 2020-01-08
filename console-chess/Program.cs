using System;
using console_chess.board;
using console_chess.board.chess;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                Screen.printBoard(match.board);
            }
            catch (boardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
