using Chess.Board;
using Chess.Enums;

namespace Chess.ChessGame.ChessPieces
{
    public class King : Piece
    {
        public King(Color color, ChessBoard board) : base(color, board) { }

        public override bool[,] PossibleMovements()
        {
            bool[,] possibleMovements = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            #region Up
            position.SetPosition(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            { 
                possibleMovements[position.Line, position.Column] = true;
            }
            #endregion

            #region Top Diagonal Right
            position.SetPosition(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }
            #endregion

            #region Right
            position.SetPosition(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }
            #endregion

            #region Bottom Diagonal Right
            position.SetPosition(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }
            #endregion

            #region Down
            position.SetPosition(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }
            #endregion

            #region Bottom Diagonal Left
            position.SetPosition(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }
            #endregion

            #region Left
            position.SetPosition(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }
            #endregion

            #region Top Diagonal Left
            position.SetPosition(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && MovementPermitted(position))
            {
                possibleMovements[position.Line, position.Column] = true;
            }
            #endregion

            return possibleMovements;
        }

        public override string ToString()
        {
            return "♚";
        }
    }
}
