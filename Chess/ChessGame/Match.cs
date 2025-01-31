using Chess.Board;
using Chess.ChessGame.ChessPieces;
using Chess.Enums;

namespace Chess.ChessGame
{
    public class Match
    {
        public ChessBoard Board { get; private set; }
        public bool Finished { get; set; }
        public int Turn { get; private set; }
        public Color ActualPlayer { get; private set; }
        public List<Piece> CapturedWhitePieces { get; private set; }
        public List<Piece> CapturedBlackPieces { get; private set; }
        public bool Check { get; set; }

        public Match()
        {
            Board = new ChessBoard(8, 8);
            Turn = 1;
            ActualPlayer = Color.White;
            PlacePieces();
            CapturedWhitePieces = new List<Piece>();
            CapturedBlackPieces = new List<Piece>();
            Finished = false;
            Check = false;
        }

        public ChessPosition GetPosition(string position)
        {
            string column = position[0].ToString();
            int line = int.Parse(position[1].ToString());

            return new ChessPosition(column, line);
        }

        public void CheckOriginPosition(Position position)
        {
            Piece piece = Board.GetPiece(position);

            if (piece == null)
            {
                throw new BoardException("There is no piece in this position!");
            }

            if (piece.Color != ActualPlayer)
            {
                throw new BoardException($"It's {ActualPlayer} Player turn!");
            }

            if (!piece.HasPossibleMovements())
            {
                throw new BoardException("There are no possible movements for this piece!");
            }

        }

        public void CheckDestinationPosition(Position position, bool[,] possibleMovements)
        {
            if (!possibleMovements[position.Line, position.Column])
            {
                throw new BoardException("Invalid movement!");
            }
        }

        public void MakeMove(Position origin, Position destination)
        {
            Piece piece = Board.GetPiece(origin);

            #region Special Move - Castle

            Piece rook = Board.GetPiece(GetRookPositionForCastle(piece.Color, origin, destination));

            bool isCastle = false;

            if (piece is King && piece.Movements == 0 && rook.Movements == 0)
            {
                isCastle = IsCastle(piece, origin, destination);
            }

            if (isCastle)
            {
                MovePiece(origin, destination);
                MovePiece(rook.Position, GetRookDestinationForCastle(rook.Color, origin, destination));

                Check = KingInCheck(GetEnemyColor());

                if (Checkmate(GetEnemyColor()))
                {
                    Finished = true;
                }
                else
                {
                    Turn++;
                    RemoveAllEnPassant(GetEnemyColor());
                    ChangeActualPlayer();
                }

                return;
            }

            #endregion

            #region Special Move - En Passant

            Position pawnPosition = GetPawnPositionForEnPassant(origin, destination);
            Piece pawn;

            bool isEnPassant = false;

            if (Board.PieceExists(pawnPosition))
            {
                pawn = Board.GetPiece(pawnPosition);

                if (pawn is Pawn)
                {
                    isEnPassant = IsEnPassant(destination, pawnPosition);
                }
            }

            if (isEnPassant)
            {
                MovePiece(origin, destination);
                CapturePiece(Board.RemovePiece(pawnPosition));

                Check = KingInCheck(GetEnemyColor());

                if (Checkmate(GetEnemyColor()))
                {
                    Finished = true;
                }
                else
                {
                    Turn++;
                    RemoveAllEnPassant(GetEnemyColor());
                    ChangeActualPlayer();
                }

                return;
            }

            #endregion

            Piece capturedPiece = MovePiece(origin, destination);

            if (KingInCheck(ActualPlayer))
            {
                UndoMove(destination, origin);

                throw new BoardException($"The {ActualPlayer} Player king is in check!");
            }
            else
            {
                CapturePiece(capturedPiece);

                Check = KingInCheck(GetEnemyColor());

                if (Checkmate(GetEnemyColor()))
                {
                    Finished = true;
                }
                else
                {
                    Turn++;
                    RemoveAllEnPassant(GetEnemyColor());
                    ChangeActualPlayer();
                }
            }
        }

        public bool KingInCheck(Color color)
        {
            Color enemyColor = color == ActualPlayer ? GetEnemyColor() : ActualPlayer;

            foreach (Piece piece in Board.GetPiecesFromColor(enemyColor))
            {
                bool[,] possibleMovements = piece.PossibleMovements();

                for (int line = 0; line < Board.Lines; line++)
                {
                    for (int column = 0; column < Board.Columns; column++)
                    {
                        if (possibleMovements[line, column] && Board.GetPiece(line, column) is King && Board.GetPiece(line, column).Color != enemyColor)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool Checkmate(Color color)
        {
            if (!KingInCheck(color))
            {
                return false;
            }

            foreach (Piece piece in Board.GetPiecesFromColor(color))
            {
                bool[,] possibleMovements = piece.PossibleMovements();

                for (int line = 0; line < Board.Lines; line++)
                {
                    for (int column = 0; column < Board.Columns; column++)
                    {
                        if (possibleMovements[line, column])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(line, column);

                            Piece capturedPiece = MovePiece(piece.Position, destination);

                            bool inCheck = KingInCheck(color);

                            UndoMove(destination, origin);

                            if (!inCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public Color GetEnemyColor()
        {
            if (ActualPlayer == Color.White)
            {
                return Color.Black;
            }

            return Color.White;
        }

        private Piece MovePiece(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            Piece capturedPiece = Board.RemovePiece(destination);

            Board.PlacePiece(piece, destination);

            if (piece is Pawn && (destination.Line == origin.Line + 2 || destination.Line == origin.Line - 2))
            {
                Pawn pawn = piece as Pawn;
                pawn.EnPassant = true;
            }

            piece.IncreaseMoviments();

            return capturedPiece;
        }

        private void UndoMove(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            Piece capturedPiece = Board.RemovePiece(destination);

            Board.PlacePiece(piece, destination);

            if (piece is Pawn)
            {
                Pawn pawn = piece as Pawn;
                pawn.EnPassant = false;
            }

            piece.DecreaseMovements();
        }

        private bool IsCastle(Piece piece, Position origin, Position destination)
        {
            #region Did the King move two positions to the side?

            if (!(destination.Column == origin.Column + 2 || destination.Column == origin.Column - 2))
            {
                return false;
            }

            #endregion

            #region Is the King in check?

            if (KingInCheck(ActualPlayer))
            {
                return false;
            }

            #endregion

            #region Will the King be in check?

            MovePiece(origin, destination);

            if (KingInCheck(ActualPlayer))
            {
                UndoMove(destination, origin);

                return false;
            }

            UndoMove(destination, origin);

            #endregion

            #region Is the Rook position being controlled by the enemy?

            if (piece is King)
            {
                Position position;

                if (destination.Column == origin.Column + 2)
                {
                    position = new Position(piece.Position.Line, piece.Position.Column + 1);
                }
                else
                {
                    position = new Position(piece.Position.Line, piece.Position.Column - 1);
                }

                foreach (Piece enemyPiece in Board.GetPiecesFromColor(GetEnemyColor()))
                {
                    bool[,] possibleMovements = enemyPiece.PossibleMovements();

                    for (int line = 0; line < Board.Lines; line++)
                    {
                        for (int column = 0; column < Board.Columns; column++)
                        {
                            if (possibleMovements[position.Line, position.Column])
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            #endregion

            return true;
        }

        private bool IsEnPassant(Position destination, Position pawnPosition)
        {
            if (!((destination.Line == pawnPosition.Line + 1 || destination.Line == pawnPosition.Line - 1) && pawnPosition.Column == destination.Column))
            {
                return false;
            }

            return true;
        }

        private void RemoveAllEnPassant(Color color)
        {
            List<Piece> enemyPieces = Board.GetPiecesFromColor(color);

            foreach (Piece piece in enemyPieces)
            {
                if (piece is Pawn)
                {
                    Pawn pawn = piece as Pawn;
                    pawn.EnPassant = false;
                }
            }
        }

        private Position GetRookPositionForCastle(Color color, Position origin, Position destination)
        {
            int line = color == Color.White ? 7 : 0;
            int column = destination.Column > origin.Column ? 7 : 0;

            return new Position(line, column);
        }

        private Position GetRookDestinationForCastle(Color color, Position origin, Position destination)
        {
            int line = color == Color.White ? 7 : 0;
            int column = destination.Column > origin.Column ? origin.Column + 1 : origin.Column - 1;

            return new Position(line, column);
        }

        private Position GetPawnPositionForEnPassant(Position origin, Position destination)
        {
            int line = origin.Line;
            int column = destination.Column > origin.Column ? origin.Column + 1 : origin.Column - 1;

            if (column < 0 || column > Board.Columns)
            {
                return origin;
            }

            return new Position(line, column);
        }

        private void ChangeActualPlayer()
        {
            if (ActualPlayer == Color.White)
            {
                ActualPlayer = Color.Black;
            }
            else
            {
                ActualPlayer = Color.White;
            }
        }

        private void CapturePiece(Piece piece)
        {
            if (piece != null)
            {
                if (piece.Color == Color.White)
                {
                    CapturedWhitePieces.Add(piece);
                }
                else
                {
                    CapturedBlackPieces.Add(piece);
                }
            }
        }

        private void PlacePieces()
        {
            #region Black Player

            Board.PlacePiece(new Rook(Color.Black, Board), new ChessPosition("A", 8).ToPosition());
            Board.PlacePiece(new Knight(Color.Black, Board), new ChessPosition("B", 8).ToPosition());
            Board.PlacePiece(new Bishop(Color.Black, Board), new ChessPosition("C", 8).ToPosition());
            Board.PlacePiece(new Queen(Color.Black, Board), new ChessPosition("D", 8).ToPosition());
            Board.PlacePiece(new King(Color.Black, Board, this), new ChessPosition("E", 8).ToPosition());
            Board.PlacePiece(new Bishop(Color.Black, Board), new ChessPosition("F", 8).ToPosition());
            Board.PlacePiece(new Knight(Color.Black, Board), new ChessPosition("G", 8).ToPosition());
            Board.PlacePiece(new Rook(Color.Black, Board), new ChessPosition("H", 8).ToPosition());

            for (int column = 0; column < Board.Columns; column++)
            {
                Board.PlacePiece(new Pawn(Color.Black, Board, this), new Position(1, column));
            }

            #endregion

            #region White Player

            Board.PlacePiece(new Rook(Color.White, Board), new ChessPosition("A", 1).ToPosition());
            Board.PlacePiece(new Knight(Color.White, Board), new ChessPosition("B", 1).ToPosition());
            Board.PlacePiece(new Bishop(Color.White, Board), new ChessPosition("C", 1).ToPosition());
            Board.PlacePiece(new King(Color.White, Board, this), new ChessPosition("D", 1).ToPosition());
            Board.PlacePiece(new Queen(Color.White, Board), new ChessPosition("E", 1).ToPosition());
            Board.PlacePiece(new Bishop(Color.White, Board), new ChessPosition("F", 1).ToPosition());
            Board.PlacePiece(new Knight(Color.White, Board), new ChessPosition("G", 1).ToPosition());
            Board.PlacePiece(new Rook(Color.White, Board), new ChessPosition("H", 1).ToPosition());

            for (int column = 0; column < Board.Columns; column++)
            {
                Board.PlacePiece(new Pawn(Color.White, Board, this), new Position(6, column));
            }

            #endregion
        }
    }
}
