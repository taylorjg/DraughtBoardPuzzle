using System;
using NUnit.Framework;

namespace DraughtBoardPuzzle.Tests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    internal class BoardTests
    {
        private const int TestBoardSize = 4;

        [Test]
        public void Constructor_WithNoParameters_CreatesBoardOfTheCorrectSize()
        {
            // Arrange, Act
            var board = new Board(TestBoardSize);

            // Assert
            Assert.That(board.BoardSize, Is.EqualTo(TestBoardSize));
            //Assert.That(board.Width, Is.EqualTo(Board.BoardSize));
            //Assert.That(board.Height, Is.EqualTo(Board.BoardSize));
        }

        [Test]
        public void PlacePiece_GivenAValidPieceAtAValidLocation_ReturnsTrue()
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
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act
            var actual = board.PlacePieceAt(piece, 0, 0);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        [TestCase(-1, 0)]
        [TestCase(TestBoardSize, 0)]
        [TestCase(TestBoardSize + 1, 0)]
        [TestCase(0, -1)]
        [TestCase(0, TestBoardSize)]
        [TestCase(0, TestBoardSize + 1)]
        public void PlacePiece_GivenInvalidCoordinates_ThrowsException(int x, int y)
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
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => board.PlacePieceAt(piece, x, y));
        }

        [Test]
        public void PlacePiece_GivenAnXValueAndPieceWidthCombinationThatExceedBoardSize_ReturnsFalse()
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
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act
            var actual = board.PlacePieceAt(piece, board.BoardSize - piece.Width + 1, 0);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void PlacePiece_GivenAnXValueAndPieceWidthCombinationThatEqualsBoardSize_ReturnsTrue()
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
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act
            var actual = board.PlacePieceAt(piece, board.BoardSize - piece.Width, 0);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void PlacePiece_GivenAYValueAndPieceHeightCombinationThatExceedBoardSize_ReturnsFalse()
        {
            // Arrange
            var squares =
                new[]
                    {
                        // W
                        // B
                        // W
                        new Square(0, 0, Colour.White),
                        new Square(0, 1, Colour.Black),
                        new Square(0, 2, Colour.White)
                    };
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act
            var actual = board.PlacePieceAt(piece, 0, board.BoardSize - piece.Height + 1);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void PlacePiece_GivenAYValueAndPieceHeightCombinationThatEqualsBoardSize_ReturnsTrue()
        {
            // Arrange
            var squares =
                new[]
                    {
                        // W
                        // B
                        // W
                        new Square(0, 0, Colour.White),
                        new Square(0, 1, Colour.Black),
                        new Square(0, 2, Colour.White)
                    };
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act
            var actual = board.PlacePieceAt(piece, 0, board.BoardSize - piece.Height);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void PlacePiece_GivenALocationAndPieceShapeThatCausesAnOverlapWithAnExistingPiece_ReturnsFalse()
        {
            // Arrange
            var squares1 =
                new[]
                    {
                        // WBW
                        new Square(0, 0, Colour.White),
                        new Square(1, 0, Colour.Black),
                        new Square(2, 0, Colour.White)
                    };
            var piece1 = new Piece(squares1);

            var squares2 =
                new[]
                    {
                        // BWB
                        new Square(0, 0, Colour.Black),
                        new Square(1, 0, Colour.White),
                        new Square(2, 0, Colour.Black)
                    };
            var piece2 = new Piece(squares2);

            var board = new Board(TestBoardSize);

            // Act
            var actual1 = board.PlacePieceAt(piece1, 1, 0);
            var actual2 = board.PlacePieceAt(piece2, 0, 0);

            // Assert
            Assert.That(actual1, Is.True);
            Assert.That(actual2, Is.False);
            Assert.That(board.PieceAt(0, 0), Is.Null);
            Assert.That(board.PieceAt(1, 0), Is.SameAs(piece1));
            Assert.That(board.PieceAt(2, 0), Is.SameAs(piece1));
            Assert.That(board.PieceAt(3, 0), Is.SameAs(piece1));
        }

        [Test]
        [TestCase(-1, 0)]
        [TestCase(TestBoardSize, 0)]
        [TestCase(TestBoardSize + 1, 0)]
        [TestCase(0, -1)]
        [TestCase(0, TestBoardSize)]
        [TestCase(0, TestBoardSize + 1)]
        public void PieceAt_GivenInvalidCoordinates_ThrowsException(int x, int y)
        {
            // Arrange
            var board = new Board(TestBoardSize);

            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => board.PieceAt(x, y));
        }

        [Test]
        public void PieceAt_GivenOccupiedLocation_ReturnsPieceAtThatLocation()
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
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act
            var actual = board.PlacePieceAt(piece, 1, 0);

            // Assert
            Assert.That(actual, Is.True);
            Assert.That(board.PieceAt(1, 0), Is.SameAs(piece));
            Assert.That(board.PieceAt(2, 0), Is.SameAs(piece));
            Assert.That(board.PieceAt(3, 0), Is.SameAs(piece));
        }

        [Test]
        public void PieceAt_GivenUnoccupiedLocation_ReturnsNull()
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
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act
            var actual = board.PlacePieceAt(piece, 1, 0);

            // Assert
            Assert.That(actual, Is.True);
            Assert.That(board.PieceAt(0, 0), Is.Null);
            Assert.That(board.PieceAt(0, 1), Is.Null);
        }

        [Test]
        public void PlacePiece_GivenASecondPieceAtAValidLocation_ReturnsTrue()
        {
            // Arrange
            var squares1 =
                new[]
                    {
                        // WBW
                        new Square(0, 0, Colour.White),
                        new Square(1, 0, Colour.Black),
                        new Square(2, 0, Colour.White)
                    };
            var piece1 = new Piece(squares1);

            var squares2 =
                new[]
                    {
                        // BWB
                        new Square(0, 0, Colour.Black),
                        new Square(1, 0, Colour.White),
                        new Square(2, 0, Colour.Black)
                    };
            var piece2 = new Piece(squares2);

            var board = new Board(TestBoardSize);

            // Act
            var actual1 = board.PlacePieceAt(piece1, 0, 0);
            var actual2 = board.PlacePieceAt(piece2, 0, 1);

            // Assert
            Assert.That(actual1, Is.True);
            Assert.That(actual2, Is.True);
        }

        [Test]
        public void PlacePiece_GivenASecondPieceAtAValidLocationButWhereASquareHasTheWrongColour_ReturnsFalse()
        {
            // Arrange
            var squares1 =
                new[]
                    {
                        // WBW
                        new Square(0, 0, Colour.White),
                        new Square(1, 0, Colour.Black),
                        new Square(2, 0, Colour.White)
                    };
            var piece1 = new Piece(squares1);

            var squares2 =
                new[]
                    {
                        // BWB
                        new Square(0, 0, Colour.Black),
                        new Square(1, 0, Colour.White),
                        new Square(2, 0, Colour.Black)
                    };
            var piece2 = new Piece(squares2);

            var board = new Board(TestBoardSize);

            // Act
            board.PlacePieceAt(piece1, 0, 0);
            var actual = board.PlacePieceAt(piece2, 1, 1);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void Reset_GivenThatTheBoardContainsAtLeastOnePiece_ClearsAllSqaures()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            var squares =
                new[]
                    {
                        // WBW
                        new Square(0, 0, Colour.White),
                        new Square(1, 0, Colour.Black),
                        new Square(2, 0, Colour.White)
                    };
            var piece = new Piece(squares);
            board.PlacePieceAt(piece, 0, 0);

            // Act
            board.Reset();

            // Assert
            for (int x = 0; x < board.BoardSize; x++) {
                for (int y = 0; y < board.BoardSize; y++) {
                    Assert.That(board.PieceAt(x, y), Is.Null);
                }
            }
        }

        [Test]
        public void Reset_GivenThatTheBoardContainsAtLeastOnePiece_SetsColourOfSquareZeroZeroToNull()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            var squares =
                new[]
                    {
                        // WBW
                        new Square(0, 0, Colour.White),
                        new Square(1, 0, Colour.Black),
                        new Square(2, 0, Colour.White)
                    };
            var piece = new Piece(squares);
            board.PlacePieceAt(piece, 0, 0);

            // Act
            board.Reset();

            // Assert
            Assert.That(board.ColourOfSquareZeroZero, Is.Null);
        }

        [Test]
        public void PlacePiece_WhenPlacingTheFirstPieceAtLocationZeroZero_SetsColourOfSquareZeroZeroToCorrectValue()
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
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act
            board.PlacePieceAt(piece, 0, 0);

            // Assert
            Assert.That(board.ColourOfSquareZeroZero, Is.Not.Null);
            Assert.That(board.ColourOfSquareZeroZero, Is.EqualTo(Colour.White));
        }

        [Test]
        public void PlacePiece_WhenPlacingTheFirstPieceAtALocationOtherThanZeroZero_SetsColourOfSquareZeroZeroToCorrectValue()
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
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act
            board.PlacePieceAt(piece, 1, 2);

            // Assert
            Assert.That(board.ColourOfSquareZeroZero, Is.Not.Null);
            Assert.That(board.ColourOfSquareZeroZero, Is.EqualTo(Colour.Black));
        }

        [Test]
        public void PlacePiece_WhenPlacingTheFirstPieceAtLocationZeroZeroWithOrientationEast_ReturnsTrue()
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
            var piece = new Piece(squares);
            var board = new Board(TestBoardSize);

            // Act
            var actual = board.PlacePieceAt(new RotatedPiece(piece, Orientation.North), 0, 0);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void IsSolved_WhenSolved_ReturnsTrue()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            board.PlacePieceAt(new RotatedPiece(Piece.TestPieceA, Orientation.North), 0, 0);
            board.PlacePieceAt(new RotatedPiece(Piece.TestPieceB, Orientation.North), 0, 2);
            board.PlacePieceAt(new RotatedPiece(Piece.TestPieceC, Orientation.North), 1, 1);
            board.PlacePieceAt(new RotatedPiece(Piece.TestPieceD, Orientation.North), 2, 1);
            
            // Act
            var actual = board.IsSolved();

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void IsSolved_WhenOnlyPartiallySolved_ReturnsFalse()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            board.PlacePieceAt(new RotatedPiece(Piece.TestPieceA, Orientation.North), 0, 0);
            board.PlacePieceAt(new RotatedPiece(Piece.TestPieceB, Orientation.North), 0, 2);

            // Act
            var actual = board.IsSolved();

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void LayoutNextPiece_GivenThatPieceFirstPieceIsPlacedAndNextPieceIsCompatible_ReturnsTrue()
        {
            // Arrange
            var board = new Board(TestBoardSize);
            board.PlacePieceAt(new RotatedPiece(Piece.TestPieceA, Orientation.North), 0, 0);

            // Act
            var actual = board.LayoutNextPiece(new RotatedPiece(Piece.TestPieceC, Orientation.North));

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void LayoutNextPiece_GivenAllFourPiecesInTheCorrectOrderAndOrientation_ReturnsTrueForEachPiece()
        {
            // Arrange
            var board = new Board(TestBoardSize);

            // Act
            var actual1 = board.LayoutNextPiece(new RotatedPiece(Piece.TestPieceA, Orientation.North));
            var actual2 = board.LayoutNextPiece(new RotatedPiece(Piece.TestPieceC, Orientation.North));
            var actual3 = board.LayoutNextPiece(new RotatedPiece(Piece.TestPieceD, Orientation.North));
            var actual4 = board.LayoutNextPiece(new RotatedPiece(Piece.TestPieceB, Orientation.North));

            // Assert
            Assert.That(actual1, Is.True);
            Assert.That(actual2, Is.True);
            Assert.That(actual3, Is.True);
            Assert.That(actual4, Is.True);
        }

        // TODO: check whether the we have hit a dead end e.g. left a hole of size 1 or 2 squares that is impossible to fill
    }

    // TODO: separate class re solving the puzzle
    // need a collection of pieces to place to feed into a laying out algorithm
    // each item in the collection will be a piece + orientation i.e. an instance of RotatedPiece
    // e.g. AN, BN, CN, DN
    // need an outer algorithm that changes the collection of pieces
    // need an inner algorithm that tries to lay out the collection of pieces
    // if the inner algorithm successfully adds all pieces to the board then i guess it must have solved the puzzle => complete
    // otherwise, get the next collection of pieces to try

    // AN, BN, CN, DN
    // AE, BN, CN, DN
    // AW, BN, CN, DN
    // AS, BN, CN, DN

    // AN, BN, CN, DN

    // ReSharper restore InconsistentNaming
}
