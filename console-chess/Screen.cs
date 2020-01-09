using System;
using System.Collections.Generic;
using System.Text;
using console_chess.board;
using console_chess.chess;

namespace console_chess
{
    class Screen
    {
        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void printBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBg = Console.BackgroundColor;
            ConsoleColor alteredBg = ConsoleColor.DarkGray;

            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if(possiblePositions[i, j])
                    {
                        Console.BackgroundColor = alteredBg;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBg;
                    }
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBg;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBg;
        }

        public static ChessPosition readChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + ""); //force the number to become a string
            return new ChessPosition(column, row);
        }

        public static void printPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
