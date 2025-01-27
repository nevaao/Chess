using Chess.Board;
using Chess.ChessGame;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Match match = new Match();

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(match);

                        Console.WriteLine();

                        if (match.Check)
                        {
                            Console.WriteLine("CHECK!");

                            Console.WriteLine();
                        }

                        Console.Write("Origin: ");
                        ChessPosition origin = match.GetPosition(Console.ReadLine());

                        match.CheckOriginPosition(origin.ToPosition());

                        Console.Clear();

                        bool[,] possibleMovements = match.Board.GetPiece(origin.ToPosition()).PossibleMovements();

                        Screen.PrintBoard(match, possibleMovements);

                        Console.WriteLine();

                        if (match.Check)
                        {
                            Console.WriteLine("CHECK!");

                            Console.WriteLine();
                        }

                        Console.WriteLine($"Origin: {origin}");

                        Console.Write("Destination: ");
                        Position destination = match.GetPosition(Console.ReadLine()).ToPosition();

                        match.CheckDestinationPosition(destination, possibleMovements);

                        match.MakeMove(origin.ToPosition(), destination);
                    }
                    catch (BoardException exception)
                    {
                        Console.WriteLine();

                        Console.WriteLine(exception.Message);
                        Console.WriteLine("Press ENTER to continue...");

                        Console.ReadLine();
                    }
                }
            }
            catch (BoardException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
