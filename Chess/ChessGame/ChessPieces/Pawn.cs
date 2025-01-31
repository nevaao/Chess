using Chess.Board;
using Chess.Enums;

namespace Chess.ChessGame.ChessPieces
{
    public class Pawn : Piece
    {
        public Match Match { get; set; }
        public bool EnPassant { get; set; }

        public Pawn(Color color, ChessBoard board, Match match) : base(color, board)
        {
            Match = match;
            EnPassant = false;
        }

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
            if (Board.ValidPosition(position) && MovementPermitted(position) && Color == Color.White)
            {
                if (Board.PieceExists(position))
                {
                    if (Board.GetPiece(position).Color != Color)
                    {
                        possibleMovements[position.Line, position.Column] = true;
                    }
                }

                Position enemyPosition = new Position(Position.Line, Position.Column + 1);

                if (Board.PieceExists(enemyPosition))
                {
                    Piece enemyPiece = Board.GetPiece(enemyPosition);

                    if (enemyPiece is Pawn && enemyPiece.Color == Match.GetEnemyColor() && enemyPiece.Movements == 1)
                    {
                        Pawn pawn = enemyPiece as Pawn;

                        if (pawn.EnPassant)
                        {
                            possibleMovements[position.Line, position.Column] = true;
                        }                        
                    }
                }
            }

            #endregion

            #region Bottom Diagonal Right

            position.SetPosition(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && MovementPermitted(position) && Color == Color.Black)
            {
                if(Board.PieceExists(position))
                {
                    if (Board.GetPiece(position).Color != Color)
                    {
                        possibleMovements[position.Line, position.Column] = true;
                    }
                }

                Position enemyPosition = new Position(Position.Line, Position.Column + 1);

                if (Board.PieceExists(enemyPosition))
                {
                    Piece enemyPiece = Board.GetPiece(enemyPosition);

                    if (enemyPiece is Pawn && enemyPiece.Color == Match.GetEnemyColor() && enemyPiece.Movements == 1)
                    {
                        Pawn pawn = enemyPiece as Pawn;

                        if (pawn.EnPassant)
                        {
                            possibleMovements[position.Line, position.Column] = true;
                        }
                    }
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
            if (Board.ValidPosition(position) && MovementPermitted(position) && Color == Color.Black)
            {
                if (Board.PieceExists(position))
                {
                    if (Board.GetPiece(position).Color != Color)
                    {
                        possibleMovements[position.Line, position.Column] = true;
                    }
                }

                Position enemyPosition = new Position(Position.Line, Position.Column - 1);

                if (Board.PieceExists(enemyPosition))
                {
                    Piece enemyPiece = Board.GetPiece(enemyPosition);

                    if (enemyPiece is Pawn && enemyPiece.Color == Match.GetEnemyColor() && enemyPiece.Movements == 1)
                    {
                        Pawn pawn = enemyPiece as Pawn;

                        if (pawn.EnPassant)
                        {
                            possibleMovements[position.Line, position.Column] = true;
                        }
                    }
                }
            }

            #endregion

            #region Top Diagonal Left

            position.SetPosition(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && MovementPermitted(position) && Color == Color.White)
            {
                if (Board.PieceExists(position))
                {
                    if (Board.GetPiece(position).Color != Color)
                    {
                        possibleMovements[position.Line, position.Column] = true;
                    }
                }

                Position enemyPosition = new Position(Position.Line, Position.Column - 1);

                if (Board.PieceExists(enemyPosition))
                {
                    Piece enemyPiece = Board.GetPiece(enemyPosition);

                    if (enemyPiece is Pawn && enemyPiece.Color == Match.GetEnemyColor() && enemyPiece.Movements == 1)
                    {
                        Pawn pawn = enemyPiece as Pawn;

                        if (pawn.EnPassant)
                        {
                            possibleMovements[position.Line, position.Column] = true;
                        }
                    }
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
