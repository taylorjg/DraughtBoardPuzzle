namespace DraughtBoardPuzzle
{
    public class BoardPrinter
    {
        private readonly IPrintTarget _printTarget;

        public BoardPrinter(IPrintTarget printTarget)
        {
            _printTarget = printTarget;
        }

        public void Print(Board board)
        {
            // Note that the outer loop iterates over the rows rather than the columns
            // because it is easier to build the lines this way. Also, we regard (0,0)
            // to be in the bottom left corner. Hence, we print the highest numbered
            // row first so that the last row printed will be row 0.

            var rowDivider = GetRowDivider(board);

            for (var y = board.BoardSize - 1; y >= 0; y--) {

                _printTarget.PrintLine(rowDivider);

                string line = string.Empty;

                // The inner loop iterates over the columns.
                for (var x = 0; x < board.BoardSize; x++) {
                    line += "|";

                    var piece = board.PieceAt(x, y);
                    if (piece != null) {
                        var square = board.SquareAt(x, y);
                        line += string.Format(
                            " {0}{1} ",
                            piece.Name,
                            square.Colour == Colour.Black ? "b" : "w");
                    }
                    else {
                        line += new string(' ', 4);
                    }
                }
                line += "|";

                _printTarget.PrintLine(line);
            }

            _printTarget.PrintLine(rowDivider);
        }

        public string GetRowDivider(Board board)
        {
            var rowDivider = string.Empty;

            for (int x = 0; x < board.BoardSize; x++)
            {
                rowDivider += "+";
                rowDivider += new string('-', 4);
            }
            rowDivider += "+";

            return rowDivider;
        }
    }
}
