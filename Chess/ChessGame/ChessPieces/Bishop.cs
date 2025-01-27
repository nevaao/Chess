using Chess.Board;
using Chess.Enums;

namespace Chess.ChessGame.ChessPieces
{
    public class Bishop : Piece
    {
        public Bishop(Color color, ChessBoard board) : base(color, board) { }

        public override bool[,] PossibleMovements()
        {
            bool[,] possibleMovements = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            #region Top Diagonal Right
            position.SetPosition(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;

                if (Board.PieceExists(position) && Board.GetPiece(position).Color != Color)
                {
                    break;
                }

                position.Line--;
                position.Column++;
            }
            #endregion

            #region Bottom Diagonal Right
            position.SetPosition(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;

                if (Board.PieceExists(position) && Board.GetPiece(position).Color != Color)
                {
                    break;
                }

                position.Line++;
                position.Column++;
            }
            #endregion

            #region Bottom Diagonal Left
            position.SetPosition(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;

                if (Board.PieceExists(position) && Board.GetPiece(position).Color != Color)
                {
                    break;
                }

                position.Line++;
                position.Column--;
            }
            #endregion

            #region Top Diagonal Left
            position.SetPosition(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;

                if (Board.PieceExists(position) && Board.GetPiece(position).Color != Color)
                {
                    break;
                }

                position.Line--;
                position.Column--;
            }
            #endregion

            return possibleMovements;
        }

        public override string ToString()
        {
            return "♝";
        }
    }
}
