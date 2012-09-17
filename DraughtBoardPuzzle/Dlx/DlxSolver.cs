using System.Collections.Generic;
using System.Linq;

namespace DraughtBoardPuzzle.Dlx
{
    public class DlxSolver
    {
        private IList<IList<int>> _solutions;
        private Stack<int> _solution;

        public IEnumerable<IEnumerable<int>> Solve(bool[,] matrix)
        {
            BuildInternalStructure(matrix);
            _solutions = new List<IList<int>>();
            _solution = new Stack<int>();
            Search();
            return _solutions;
        }

        internal ColumnHeader Root { get; private set; }

        private void BuildInternalStructure(bool[,] matrix)
        {
            var numRows = matrix.GetLength(0);
            var numCols = matrix.GetLength(1);

            Root = new ColumnHeader();

            for (var colIndex = 0; colIndex < numCols; colIndex++)
            {
                var columnHeader = new ColumnHeader();
                Root.AppendColumnHeader(columnHeader);
            }

            for (var rowIndex = 0; rowIndex < numRows; rowIndex++)
            {
                // We are starting a new row so indicate that this row is currently empty.
                Node lastNodeInThisRow = null;

                for (var colIndex = 0; colIndex < numCols; colIndex++)
                {
                    // We are only interested in the '1's ('true's) in the matrix.
                    // We create a node for each '1'. We ignore all the '0's ('false's).
                    if (!matrix[rowIndex, colIndex]) continue;

                    // Add the node to the appropriate column header.
                    var columnHeader = FindColumnHeader(colIndex);
                    var node = new Node(columnHeader, rowIndex);

                    // Append this node to the last node in this row (if there is one).
                    if (lastNodeInThisRow != null)
                        lastNodeInThisRow.AppendRowNode(node);

                    // This node is now the last node in this row.
                    lastNodeInThisRow = node;
                }
            }
        }

        private ColumnHeader FindColumnHeader(int colIndexToFind)
        {
            var columnHeader = Root;
            for (var colIndex = 0; colIndex <= colIndexToFind; colIndex++)
                columnHeader = columnHeader.NextColumnHeader;
            return columnHeader;
        }

        private void Search()
        {
            if (Root.NextColumnHeader == Root)
            {
                var reorderedSolution = (from x in _solution orderby x ascending select x).ToList();
                _solutions.Add(reorderedSolution);
                return;
            }

            // I have used variable names c, r and j here to make it easy to
            // relate this code to the original "Dancing Links" paper.

            var c = ChooseColumnWithLeastRows();
            CoverColumn(c);

            for (var r = c.Down; r != c; r = r.Down)
            {
                _solution.Push(r.RowIndex);

                for (var j = r.Right; j != r; j = j.Right)
                    CoverColumn(j.ColumnHeader);

                Search();

                for (var j = r.Left; j != r; j = j.Left)
                    UncoverColumn(j.ColumnHeader);

                _solution.Pop();
            }

            UncoverColumn(c);
        }

        private ColumnHeader ChooseColumnWithLeastRows()
        {
            ColumnHeader chosenColumn = null;

            var smallestNumberOfRows = int.MaxValue;
            for (var columnHeader = Root.NextColumnHeader; columnHeader != Root; columnHeader = columnHeader.NextColumnHeader)
            {
                if (columnHeader.Size < smallestNumberOfRows)
                {
                    chosenColumn = columnHeader;
                    smallestNumberOfRows = columnHeader.Size;
                }
            }

            return chosenColumn;
        }

        private static void CoverColumn(ColumnHeader c)
        {
            c.UnlinkColumnHeader();

            // I have used variable names c, i and j here to make it easy to
            // relate this code to the original "Dancing Links" paper.

            for (var i = c.Down; i != c; i = i.Down)
                for (var j = i.Right; j != i; j = j.Right)
                    j.ColumnHeader.UnlinkNode(j);
        }

        private static void UncoverColumn(ColumnHeader c)
        {
            // I have used variable names c, i and j here to make it easy to
            // relate this code to the original "Dancing Links" paper.

            for (var i = c.Up; i != c; i = i.Up)
                for (var j = i.Left; j != i; j = j.Left)
                    j.ColumnHeader.RelinkNode(j);

            c.RelinkColumnHeader();
        }
    }
}
