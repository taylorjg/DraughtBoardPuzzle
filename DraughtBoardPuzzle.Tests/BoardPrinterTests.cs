using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DraughtBoardPuzzle.Tests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    internal class BoardPrinterTests
    {
        private const int TestBoardSize = 4;

        private Board _board;
        private MockPrintTarget _mockPrintTarget;
        private BoardPrinter _boardPrinter;
        private int _expectedNumberOfOutputLines;

        [SetUp]
        public void SetUp()
        {
            _board = new Board(TestBoardSize);
            _mockPrintTarget = new MockPrintTarget();
            _boardPrinter = new BoardPrinter(_mockPrintTarget);
            _expectedNumberOfOutputLines = _board.BoardSize * 2 + 1;
        }

        [Test]
        public void Print_GivenEmptyBoard_PrintsCorrectNumberOfLines()
        {
            // Arrange, Act
            _boardPrinter.Print(_board);

            // Assert
            Assert.That(_mockPrintTarget.Lines, Has.Length.EqualTo(_expectedNumberOfOutputLines));
        }

        [Test]
        public void Print_GivenEmptyBoard_PrintsRowDividersCorrectly()
        {
            // Arrange
            var expectedRowDivider = _boardPrinter.GetRowDivider(_board);

            // Act
            _boardPrinter.Print(_board);

            // Assert
            for (int i = 0; i < _expectedNumberOfOutputLines; i += 2)
            {
                Assert.That(_mockPrintTarget.Lines[i], Is.EqualTo(expectedRowDivider));
            }
        }

        [Test]
        public void Print_GivenEmptyBoard_PrintsRowDataCorrectly()
        {
            // Arrange
            var expectedLineForEmptyRow = CreateExpectedLineForEmptyRow();

            // Act
            _boardPrinter.Print(_board);

            // Assert
            for (int i = 1; i < _expectedNumberOfOutputLines; i += 2) {
                Assert.That(_mockPrintTarget.Lines[i], Is.EqualTo(expectedLineForEmptyRow));
            }
        }

        [Test]
        public void Print_GivenBoardContainingASinglePiece_PrintsRowDataCorrectly()
        {
            // Arrange
            var squares =
                new[]
                    {
                        // WBW
                        new Square(0, 0, Colour.White),
                        new Square(1, 0, Colour.Black),
                        new Square(2, 0, Colour.White)
                    };
            var piece = new Piece(squares, 'A');
            _board.PlacePieceAt(piece, 0, 0);

            var expectedLineForEmptyRow = CreateExpectedLineForEmptyRow();

            var expectedLastLine = "| Aw | Ab | Aw ";
            for (int i = 3; i < _board.BoardSize; i++) {
                expectedLastLine += "|";
                expectedLastLine += new string(' ', 4);
            }
            expectedLastLine += "|";

            // Act
            _boardPrinter.Print(_board);

            // Assert
            Assert.That(_mockPrintTarget.Lines, Has.Length.EqualTo(_expectedNumberOfOutputLines));

            Assert.That(_mockPrintTarget.Lines[1], Is.EqualTo(expectedLineForEmptyRow));
            Assert.That(_mockPrintTarget.Lines[3], Is.EqualTo(expectedLineForEmptyRow));
            Assert.That(_mockPrintTarget.Lines[5], Is.EqualTo(expectedLineForEmptyRow));
            Assert.That(_mockPrintTarget.Lines[7], Is.EqualTo(expectedLastLine));
        }

        private string CreateExpectedLineForEmptyRow()
        {
            var expectedLineForEmptyRow = string.Empty;

            for (int i = 0; i < _board.BoardSize; i++)
            {
                expectedLineForEmptyRow += "|";
                expectedLineForEmptyRow += new string(' ', 4);
            }

            expectedLineForEmptyRow += "|";

            return expectedLineForEmptyRow;
        }
    }

    internal class MockPrintTarget : IPrintTarget
    {
        private readonly IList<string> _lines = new List<string>();

        public void PrintLine(string line)
        {
            _lines.Add(line);
        }

        public string[] Lines { get { return _lines.ToArray(); } }
    }

    // ReSharper restore InconsistentNaming
}
