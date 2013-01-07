using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
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

        public void WriteSolution(int[] solutionRowIndexes)
        {
            for (var i = 0; i < solutionRowIndexes.Length; i++)
            {
                var solutionRowIndex = solutionRowIndexes[i];
                var tuple = _dictionary[solutionRowIndex];
                var rotatedPiece = tuple.Item1;
                var x = tuple.Item2;
                var y = tuple.Item3;
                Console.WriteLine("Piece {0} - Orientation: {1}; Location: ({2}, {3})", rotatedPiece.Piece.Name, rotatedPiece.Orientation, x, y);
            }
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

        public void WritePropertyList(string fileName, IEnumerable<Piece> pieces, IEnumerable<IEnumerable<int>> solutions)
        {
            var doc = new XDocument();
            var documentType = new XDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
            doc.Add(documentType);
            var plist = new XElement("plist", new XAttribute("version", "1.0"));
            plist.Add(CreateDict(
                new[] {"Pieces", "Solutions"},
                new[] {CreatePieces(pieces), CreateSolutions(solutions)}));
            doc.Add(plist);
            //var stringBuilder = new StringBuilder();
            //var stringWriter = new StringWriter(stringBuilder);
            //doc.Save(stringWriter);
            //var text = stringBuilder.ToString();
            doc.Save(fileName);
        }

        private XElement CreatePieces(IEnumerable<Piece> pieces)
        {
            var array = CreateArray();
            foreach (var piece in pieces)
            {
                array.Add(CreatePiece(piece));
            }
            return array;
        }

        private object CreatePiece(Piece piece)
        {
            var initStrings = CreateArray();
            for (var y = piece.Height - 1; y >= 0; y--)
            {
                var initString = string.Empty;
                for (var x = 0; x < piece.Width; x++)
                {
                    var square = piece.SquareAt(x, y);
                    if (square != null)
                        initString += (square.Colour == Colour.Black) ? "B" : "W";
                    else
                        initString += " ";
                }
                initStrings.Add(CreateString(initString));
            }
            var name = CreateString(piece.Name.ToString(CultureInfo.InvariantCulture));
            return CreateDict(
                new[] {"InitStrings", "Name"},
                new[] {initStrings, name});
        }

        private XElement CreateSolutions(IEnumerable<IEnumerable<int>> solutions)
        {
            var array = CreateArray();
            foreach (var solution in solutions)
            {
                array.Add(CreateSolution(solution));
            }
            return array;
        }

        private XElement CreateSolution(IEnumerable<int> solutionRowIndexes)
        {
            var dict = new XElement("dict");
            foreach (var solutionRowIndex in solutionRowIndexes)
            {
                var tuple = _dictionary[solutionRowIndex];
                var rotatedPiece = tuple.Item1;
                var x = tuple.Item2;
                var y = tuple.Item3;
                var innerDict = CreateDict(
                    new[] { "Orientation", "X", "Y" },
                    new[] { CreateInteger((int)rotatedPiece.Orientation), CreateInteger(x), CreateInteger(y) });
                AddKeyAndValue(dict, rotatedPiece.Piece.Name.ToString(CultureInfo.InvariantCulture), innerDict);
            }
            return dict;
        }

        private XElement CreateArray(params XElement[] elements)
        {
            var array = new XElement("array");
            foreach (var e in elements)
            {
                array.Add(e);
            }
            return array;
        }

        private XElement CreateDict(IList<string> keys, IList<XElement> values)
        {
            var dict = new XElement("dict");
            for (var i = 0; i < keys.Count; i++)
            {
                dict.Add(CreateKey(keys[i]));
                dict.Add(values[i]);
            }
            return dict;
        }

        private XElement CreateString(string value)
        {
            return new XElement("string", value);
        }

        private XElement CreateKey(string value)
        {
            return new XElement("key", value);
        }

        private XElement CreateInteger(int value)
        {
            return new XElement("integer", value);
        }

        private void AddKeyAndValue(XElement e, string k, string v)
        {
            e.Add(CreateKey(k));
            e.Add(CreateString(v));
        }

        private void AddKeyAndValue(XElement e, string k, XElement v)
        {
            e.Add(CreateKey(k));
            e.Add(v);
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
