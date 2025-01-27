using Chess.Board;
using Chess.Enums;

namespace Chess.ChessGame.ChessPieces
{
    public class Rook : Piece
    {
        public Rook(Color color, ChessBoard board) : base(color, board) { }

        public override bool[,] PossibleMovements()
        {
            bool[,] possibleMovements = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            #region Up
            position.SetPosition(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;

                if (Board.PieceExists(position) && Board.GetPiece(position).Color != Color)
                {
                    break;
                }

                position.Line--;
            }
            #endregion

            #region Right
            position.SetPosition(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;

                if (Board.PieceExists(position) && Board.GetPiece(position).Color != Color)
                {
                    break;
                }

                position.Column++;
            }
            #endregion

            #region Down
            position.SetPosition(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;

                if (Board.PieceExists(position) && Board.GetPiece(position).Color != Color)
                {
                    break;
                }

                position.Line++;
            }
            #endregion

            #region Left
            position.SetPosition(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;

                if (Board.PieceExists(position) && Board.GetPiece(position).Color != Color)
                {
                    break;
                }

                position.Column--;
            }
            #endregion

            return possibleMovements;
        }

        public override string ToString()
        {
            return "♜";
        }
    }
}
