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

        internal Column Root { get; private set; }

        private void BuildInternalStructure(bool[,] matrix)
        {
            var numRows = matrix.GetLength(0);
            var numCols = matrix.GetLength(1);

            Root = new Column();

            for (var col = 0; col < numCols; col++)
            {
                var columnHeader = new Column();
                Root.AppendColumnHeader(columnHeader);
            }

            Node lastNodeAddedToThisRow = null;

            for (var row = 0; row < numRows; row++)
            {
                for (var col = 0; col < numCols; col++)
                {
                    if (!matrix[row, col]) continue;
                    var columnHeader = FindColumnHeader(col);
                    var node = new Node(columnHeader, row);
                    if (lastNodeAddedToThisRow != null)
                        lastNodeAddedToThisRow.AppendRowNode(node);
                    lastNodeAddedToThisRow = node;
                }

                lastNodeAddedToThisRow = null;
            }
        }

        private Column FindColumnHeader(int zeroBasedColumnIndex)
        {
            var result = Root;
            for (var col = 0; col <= zeroBasedColumnIndex; col++)
                result = result.Next;
            return result;
        }

        private void Search()
        {
            if (Root.Next == Root)
            {
                var reorderedSolution = (from x in _solution orderby x ascending select x).ToList();
                _solutions.Add(reorderedSolution);
                return;
            }

            var c = ChooseColumnWithLeastRows();
            CoverColumn(c);

            for (var r = c.Node.Down; r != c.Node; r = r.Down)
            {
                _solution.Push(r.RowIndex);

                for (var j = r.Right; j != r; j = j.Right)
                    CoverColumn(j.Column);

                Search();

                for (var j = r.Left; j != r; j = j.Left)
                    UncoverColumn(j.Column);

                _solution.Pop();
            }

            UncoverColumn(c);
        }

        private Column ChooseColumnWithLeastRows()
        {
            Column chosenColumn = null;

            var smallestNumberOfRows = int.MaxValue;
            for (var columnHeader = Root.Next; columnHeader != Root; columnHeader = columnHeader.Next)
            {
                if (columnHeader.Size < smallestNumberOfRows)
                {
                    chosenColumn = columnHeader;
                    smallestNumberOfRows = columnHeader.Size;
                }
            }

            return chosenColumn;
        }

        private static void CoverColumn(Column c)
        {
            c.UnlinkColumnHeader();

            for (var i = c.Node.Down; i != c.Node; i = i.Down)
                for (var j = i.Right; j != i; j = j.Right)
                    j.Column.UnlinkNode(j);
        }

        private static void UncoverColumn(Column c)
        {
            for (var i = c.Node.Up; i != c.Node; i = i.Up)
                for (var j = i.Left; j != i; j = j.Left)
                    j.Column.RelinkNode(j);

            c.RelinkColumnHeader();
        }
    }
}
