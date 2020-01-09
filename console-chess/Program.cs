using System;
using console_chess.board;
using console_chess.chess;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.finished)
                {
                    Console.Clear();
                    Screen.printBoard(match.board);

                    Console.Write("\nOrigem: ");
                    Position from = Screen.readChessPosition().toPosition();

                    bool[,] possiblePositions = match.board.piece(from).possibleMoves();

                    Console.Clear();
                    Screen.printBoard(match.board, possiblePositions);

                    Console.Write("Destino: ");
                    Position to = Screen.readChessPosition().toPosition();

                    match.executeMovement(from, to);
                }

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
