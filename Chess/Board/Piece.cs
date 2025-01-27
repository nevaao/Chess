using Chess.Enums;

namespace Chess.Board
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int Movements { get; set; }
        public ChessBoard Board { get; set; }

        public Piece(Color color, ChessBoard board)
        {
            Position = null;
            Color = color;
            Board = board;
            Movements = 0;
        }

        public bool MovementPermitted(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        public bool HasPossibleMovements()
        {
            bool[,] possiblemMovements = PossibleMovements();

            for (int line = 0; line < Board.Lines; line++)
            {
                for (int column = 0; column < Board.Columns; column++)
                {
                    if (possiblemMovements[line, column])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public abstract bool[,] PossibleMovements();

        public void IncreaseMoviments()
        {
            Movements++;
        }
    }
}
