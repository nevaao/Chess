using Chess.Board;

namespace Chess.ChessGame
{
    public class ChessPosition
    {
        public int Line { get; set; }
        public string Column { get; set; }

        public ChessPosition(string column, int line)
        {
            Column = column;
            Line = line;
        }

        public Position ToPosition()
        {
            return new Position(8 - Line, char.Parse(Column.ToLower()) - 'a');
        }

        public override string ToString()
        {
            return (Column + Line.ToString()).ToUpper();
        }
    }
}
