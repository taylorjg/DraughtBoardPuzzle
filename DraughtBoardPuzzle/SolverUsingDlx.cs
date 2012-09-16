using System;
using System.Collections.Generic;
using System.Linq;
using DraughtBoardPuzzle.Dlx;

namespace DraughtBoardPuzzle
{
    public class SolverUsingDlx
    {
        private readonly Piece[] _pieces;
        private readonly Board _board;

        // Maps from matrix row number (zero-based) to a tuple containing RotatedPiece + (x,y) board location.
        private readonly IDictionary<int, Tuple<RotatedPiece, int, int>> _dictionary;

        private bool[,] _matrix;

        public SolverUsingDlx(IEnumerable<Piece> pieces)
        {
            _pieces = pieces.ToArray();
            _dictionary = new Dictionary<int, Tuple<RotatedPiece, int, int>>();

            var numSquares = 0;

            foreach (var piece in _pieces)
            {
                for (var x = 0; x < piece.Width; x++)
                {
                    for (var y = 0; y < piece.Height; y++)
                    {
                        if (piece.SquareAt(x, y) != null)
                            numSquares++;
                    }
                }
            }

            var boardSize = Convert.ToInt32(Math.Sqrt(numSquares));
            _board = new Board(boardSize);
            _board.ForceColourOfSquareZeroZeroToBeBlack();
        }

        public IEnumerable<IEnumerable<int>> FindAllSolutions()
        {
            BuildMatrixAndDictionary();
            var dlxSolver = new DlxSolver();
            return dlxSolver.Solve(_matrix);
        }

        public Board PopulateBoardWithSolution(int[] solutionRowIndexes)
        {
            var board = new Board(_board.BoardSize);

            for (var i = 0; i < solutionRowIndexes.Length; i++)
            {
                var solutionRowIndex = solutionRowIndexes[i];
                var tuple = _dictionary[solutionRowIndex];
                var rotatedPiece = tuple.Item1;
                var x = tuple.Item2;
                var y = tuple.Item3;
                board.PlacePieceAt(rotatedPiece, x, y);
            }

            return board;
        }

        private void BuildMatrixAndDictionary()
        {
            IList<IList<bool>> data = new List<IList<bool>>();

            for (var pieceIndex = 0; pieceIndex < _pieces.Length; pieceIndex++)
            {
                var piece = _pieces[pieceIndex];
                AddDataItemsForPieceWithSpecificOrientation(data, pieceIndex, piece, Orientation.North);
                var isFirstPiece = (pieceIndex == 0);
                if (!isFirstPiece)
                {
                    AddDataItemsForPieceWithSpecificOrientation(data, pieceIndex, piece, Orientation.South);
                    AddDataItemsForPieceWithSpecificOrientation(data, pieceIndex, piece, Orientation.East);
                    AddDataItemsForPieceWithSpecificOrientation(data, pieceIndex, piece, Orientation.West);
                }
            }

            var numColumns = _pieces.Length + _board.BoardSize * _board.BoardSize;
            _matrix = new bool[data.Count, numColumns];
            for (var row = 0; row < data.Count; row++)
            {
                for (var col = 0; col < numColumns; col++)
                {
                    _matrix[row, col] = data[row][col];
                }
            }
        }

        private void AddDataItemsForPieceWithSpecificOrientation(IList<IList<bool>> data, int pieceIndex, Piece piece, Orientation orientation)
        {
            var rotatedPiece = new RotatedPiece(piece, orientation);

            for (var x = 0; x < _board.BoardSize; x++)
            {
                for (var y = 0; y < _board.BoardSize; y++)
                {
                    _board.Reset();
                    _board.ForceColourOfSquareZeroZeroToBeBlack();
                    if (!_board.PlacePieceAt(rotatedPiece, x, y)) continue;
                    var dataItem = BuildDataItem(pieceIndex, rotatedPiece, x, y);
                    data.Add(dataItem);
                    _dictionary.Add(data.Count - 1, Tuple.Create(rotatedPiece, x, y));
                }
            }
        }

        private IList<bool> BuildDataItem(int pieceIndex, RotatedPiece rotatedPiece, int x, int y)
        {
            var numColumns = _pieces.Length + _board.BoardSize * _board.BoardSize;
            var dataItem = new bool[numColumns];

            dataItem[pieceIndex] = true;

            var w = rotatedPiece.Width;
            var h = rotatedPiece.Height;

            for (var pieceX = 0; pieceX < w; pieceX++)
            {
                for (var pieceY = 0; pieceY < h; pieceY++)
                {
                    if (rotatedPiece.SquareAt(pieceX, pieceY) == null) continue;
                    var boardX = x + pieceX;
                    var boardY = y + pieceY;
                    var boardLocationColumnIndex = _pieces.Length + (_board.BoardSize * boardX) + boardY;
                    dataItem[boardLocationColumnIndex] = true;
                }
            }

            return dataItem;
        }
    }
}
