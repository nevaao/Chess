using Chess.Board;
using Chess.Enums;

namespace Chess.ChessGame.ChessPieces
{
    public class Knight : Piece
    {
        public Knight(Color color, ChessBoard board) : base(color, board) { }

        public override bool[,] PossibleMovements()
        {
            bool[,] possibleMovements = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            #region Up Left

            position.SetPosition(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }

            #endregion

            #region Up Right

            position.SetPosition(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }

            #endregion

            #region Right Up

            position.SetPosition(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }

            #endregion

            #region Right Down

            position.SetPosition(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }

            #endregion

            #region Down Right

            position.SetPosition(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }

            #endregion

            #region Down Left

            position.SetPosition(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }

            #endregion

            #region Left Down

            position.SetPosition(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }

            #endregion

            #region Left Up

            position.SetPosition(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }

            #endregion

            return possibleMovements;
        }

        public override string ToString()
        {
            return "♞";
        }
    }
}
