using Chess.Board;
using Chess.Enums;

namespace Chess.ChessGame.ChessPieces
{
    public class Pawn : Piece
    {
        public Pawn(Color color, ChessBoard board) : base(color, board) { }

        public override bool[,] PossibleMovements()
        {
            bool[,] possibleMovements = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            #region Up
            position.SetPosition(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(position) && MovementPermitted(position) && Color == Color.White)
            {
                possibleMovements[position.Line, position.Column] = true;

                if (Movements == 0)
                {
                    possibleMovements[position.Line - 1, position.Column] = true;
                }
            }
            #endregion

            #region Top Diagonal Right
            position.SetPosition(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && MovementPermitted(position) && Color == Color.White && Board.PieceExists(position))
            {
                if (Board.GetPiece(position).Color != Color)
                {
                    possibleMovements[position.Line, position.Column] = true;
                }
            }
            #endregion

            #region Bottom Diagonal Right
            position.SetPosition(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && MovementPermitted(position) && Color == Color.Black && Board.PieceExists(position))
            {
                if (Board.GetPiece(position).Color != Color)
                {
                    possibleMovements[position.Line, position.Column] = true;
                }
            }
            #endregion

            #region Down
            position.SetPosition(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(position) && MovementPermitted(position) && Color == Color.Black)
            {
                possibleMovements[position.Line, position.Column] = true;

                if (Movements == 0)
                {
                    possibleMovements[position.Line + 1, position.Column] = true;
                }
            }
            #endregion

            #region Bottom Diagonal Left
            position.SetPosition(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && MovementPermitted(position) && Color == Color.Black && Board.PieceExists(position))
            {
                if (Board.GetPiece(position).Color != Color)
                {
                    possibleMovements[position.Line, position.Column] = true;
                }
            }
            #endregion

            #region Top Diagonal Left
            position.SetPosition(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && MovementPermitted(position) && Color == Color.White && Board.PieceExists(position))
            {
                if (Board.GetPiece(position).Color != Color)
                {
                    possibleMovements[position.Line, position.Column] = true;
                }
            }
            #endregion

            return possibleMovements;
        }

        public override string ToString()
        {
            return "♙";
        }
    }
}
