using Chess.Enums;

namespace Chess.Board
{
    public class ChessBoard
    {
        public int Lines { get; set; }
        public int Columns { get; set; }

        private Piece[,] Pieces;

        public ChessBoard(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[Lines, Columns];
        }

        public Piece GetPiece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece GetPiece(Position position)
        {
            return Pieces[position.Line, position.Column];
        }

        public List<Piece> GetPiecesFromColor(Color color)
        {
            List<Piece> pieces = new List<Piece>();

            for (int line = 0; line < Lines; line++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    if (PieceExists(new Position(line, column)) && Pieces[line, column].Color == color)
                    {
                        pieces.Add(GetPiece(line, column));
                    }
                }
            }

            return pieces;
        }

        public void PlacePiece(Piece piece, Position position)
        {
            if (PieceExists(position))
            {
                throw new BoardException("There is already a piece in this position!");
            }

            Pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (!PieceExists(position))
            {
                return null;
            }

            Piece piece = GetPiece(position);

            Pieces[position.Line, position.Column] = null;
            piece.Position = null;

            return piece;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Line < 0 || position.Line >= Lines || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }

            return true;
        }

        public void CheckPosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid position!");
            }
        }

        public bool PieceExists(Position position)
        {
            CheckPosition(position);
            return GetPiece(position) != null;
        }
    }
}
