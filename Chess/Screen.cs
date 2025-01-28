using Chess.Board;
using Chess.ChessGame;
using Chess.ChessGame.ChessPieces;
using Chess.Enums;

namespace Chess
{
    public class Screen
    {
        public static void PrintBoard(Match match, bool[,] possibleMoviments = null)
        {
            Color squareColor = Color.White;

            Console.WriteLine("  ╔═════════════════╗");

            for (int line = 0; line < match.Board.Lines; line++)
            {
                Console.Write($"{8 - line} ║ ");

                for (int column = 0; column < match.Board.Columns; column++)
                {
                    if (match.Board.GetPiece(line, column) != null)
                    {
                        Piece piece = match.Board.GetPiece(line, column);

                        if (possibleMoviments != null)
                        {
                            if (possibleMoviments[line, column])
                            {
                                PrintPiece(piece, true, match.KingInCheck(piece.Color));
                            }
                            else
                            {
                                PrintPiece(piece, false, match.KingInCheck(piece.Color));
                            }
                        }
                        else
                        {
                            PrintPiece(piece, false, match.KingInCheck(piece.Color));
                        }
                    }
                    else
                    {
                        if (possibleMoviments != null)
                        {
                            if (possibleMoviments[line, column])
                            {
                                PrintSquare(squareColor, true);
                            }
                            else
                            {
                                PrintSquare(squareColor);
                            }
                        }
                        else
                        {
                            PrintSquare(squareColor);
                        }
                    }

                    if (column + 1 != match.Board.Columns)
                    {
                        Console.Write(' ');
                    }

                    squareColor = squareColor == Color.White ? Color.Black : Color.White;
                }

                squareColor = squareColor == Color.White ? Color.Black : Color.White;

                Console.WriteLine(" ║");
            }

            Console.WriteLine("  ╚═════════════════╝");
            Console.WriteLine("    a b c d e f g h");

            Console.WriteLine();

            PrintCapturedPieces(match);

            Console.WriteLine();

            if (!match.Finished)
            {
                PrintTurn(match);
            }
        }

        private static void PrintPiece(Piece piece, bool possibleMovement = false, bool inCheck = false)
        {
            if (possibleMovement)
            {
                ChangeColorAndPrint(piece.ToString(), ConsoleColor.Red);
            }
            else
            {
                if (piece is King && inCheck)
                {
                    ChangeColorAndPrint(piece.ToString(), ConsoleColor.Red);
                }
                else
                {
                    if (piece.Color == Color.White)
                    {
                        Console.Write(piece);
                    }
                    else
                    {
                        ChangeColorAndPrint(piece.ToString(), ConsoleColor.DarkGray);
                    }
                }
            }
        }

        private static void PrintSquare(Color squareColor, bool possibleMovement = false)
        {
            if (possibleMovement)
            {
                ChangeColorAndPrint("■", ConsoleColor.Yellow);
            }
            else
            {
                if (squareColor == Color.White)
                {
                    Console.Write("■");
                }
                else
                {
                    ChangeColorAndPrint("■", ConsoleColor.DarkGray);
                }
            }
        }

        private static void PrintTurn(Match match)
        {
            Console.WriteLine($"Turn: {match.Turn}");
            Console.WriteLine($"Actual player: {match.ActualPlayer}");
        }

        private static void PrintCapturedPieces(Match match)
        {
            Console.Write("White:");
            foreach (Piece piece in match.CapturedWhitePieces)
            {
                PrintPiece(piece);
            }

            Console.WriteLine();

            Console.Write("Black:");
            foreach (Piece piece in match.CapturedBlackPieces)
            {
                PrintPiece(piece);
            }

            Console.WriteLine();
        }

        private static void ChangeColorAndPrint(string content, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.Write(content);

            Console.ForegroundColor = originalColor;
        }
    }
}
