using System;
using System.Collections.Generic;
using System.Text;
using console_chess.board;
using console_chess.chess;

namespace console_chess
{
    class Screen
    {
        public static void printMatch(ChessMatch match)
        {
            printBoard(match.board);
            printCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turno: " + match.round);
            if (!match.finished)
            {
                Console.WriteLine("Aguardando jogada: " + match.thisPlayer);
                if (match.checkMate)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + match.thisPlayer);
            }
        }

        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            printGroup(match.capturedPieces(Color.Branca));
            Console.Write("\nPretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printGroup(match.capturedPieces(Color.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void printGroup(HashSet<Piece> group)
        {
            Console.Write("[");
            foreach (Piece x in group)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
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
                    if (possiblePositions[i, j])
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
                if (piece.color == Color.Branca)
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
