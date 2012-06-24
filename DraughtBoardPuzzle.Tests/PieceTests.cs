using System;
using NUnit.Framework;

namespace DraughtBoardPuzzle.Tests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    internal class PieceTests
    {
        // X
        [Test]
        public void Constructor_ForPieceWithOneSquare_CreatesPieceWithWidthAndHeightOfOne()
        {
            // Arrange
            var squares =
                new[]
                    {
                        new Square(0, 0, Colour.White), 
                    };
            var piece = new Piece(squares);

            // Act
            int width = piece.Width;
            int height = piece.Height;

            // Assert
            Assert.That(width, Is.EqualTo(1));
            Assert.That(height, Is.EqualTo(1));
        }

        // XX
        [Test]
        public void Constructor_ForPieceWithTwoSquaresInXDirection_CreatesPieceWithWidthOfTwoAndHeightOfOne()
        {
            // Arrange
            var squares =
                new[]
                    {
                        new Square(0, 0, Colour.White), 
                        new Square(1, 0, Colour.White)
                    };
            var piece = new Piece(squares);

            // Act
            int width = piece.Width;
            int height = piece.Height;

            // Assert
            Assert.That(width, Is.EqualTo(2));
            Assert.That(height, Is.EqualTo(1));
        }

        // X
        // XXXX
        [Test]
        public void Constructor_ForPieceWithLShape_CreatesPieceWithWidthOfFourAndHeightOfTwo()
        {
            // Arrange
            var squares =
                new[]
                    {
                        new Square(0, 1, Colour.White), 
                        new Square(0, 0, Colour.White),
                        new Square(1, 0, Colour.White),
                        new Square(2, 0, Colour.White),
                        new Square(3, 0, Colour.White)
                    };
            var piece = new Piece(squares);

            // Act
            int width = piece.Width;
            int height = piece.Height;

            // Assert
            Assert.That(width, Is.EqualTo(4));
            Assert.That(height, Is.EqualTo(2));
        }

        [Test]
        public void Constructor_ForPieceWithOneWhiteSquare_CreatesPieceWithOneWhiteSquare()
        {
            // Arrange
            var squares =
                new[]
                    {
                        new Square(0, 0, Colour.White)
                    };
            var piece = new Piece(squares);

            // Act
            var square = piece.SquareAt(0, 0);

            // Assert
            Assert.That(square.Colour, Is.EqualTo(Colour.White));
        }

        [Test]
        public void Constructor_GivenInitStrings_CreatesPieceWithCorrectSquares()
        {
            // Arrange
            var initStrings =
                new[]
                    {
                        " WB",
                        " B ",
                        "BW ",
                    };

            // Act
            var piece = new Piece(initStrings);

            // Assert
            Assert.That(piece.SquareAt(0, 0).Colour, Is.EqualTo(Colour.Black));
            Assert.That(piece.SquareAt(0, 1), Is.Null);
            Assert.That(piece.SquareAt(0, 2), Is.Null);
            Assert.That(piece.SquareAt(1, 0).Colour, Is.EqualTo(Colour.White));
            Assert.That(piece.SquareAt(1, 1).Colour, Is.EqualTo(Colour.Black));
            Assert.That(piece.SquareAt(1, 2).Colour, Is.EqualTo(Colour.White));
            Assert.That(piece.SquareAt(2, 0), Is.Null);
            Assert.That(piece.SquareAt(2, 1), Is.Null);
            Assert.That(piece.SquareAt(2, 2).Colour, Is.EqualTo(Colour.Black));
        }

        [Test]
        public void IsValid_ForPieceWithConsistentSquareColours_ReturnsTrue()
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

            // Act
            var isValid = piece.IsValid();

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void IsValid_ForPieceWithInconsistentSquareColours_ReturnsFalse()
        {
            // Arrange
            var squares =
                new[]
                    {
                        // WWW
                        new Square(0, 0, Colour.White),
                        new Square(1, 0, Colour.White),
                        new Square(2, 0, Colour.White)
                    };
            var piece = new Piece(squares);

            // Act
            var isValid = piece.IsValid();

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void IsValid_ForComplexPieceWithConsistentSquareColours_ReturnsTrue()
        {
            // Arrange
            var squares =
                new[]
                    {
                        // BWB
                        //  B
                        new Square(1, 0, Colour.Black),
                        new Square(0, 1, Colour.Black),
                        new Square(1, 1, Colour.White),
                        new Square(2, 1, Colour.Black)
                    };
            var piece = new Piece(squares);

            // Act
            var isValid = piece.IsValid();

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [TestCase(-1, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 0)]
        [TestCase(0, -1)]
        [TestCase(0, 2)]
        [TestCase(0, 3)]
        public void SquareAt_GivenInvalidCoordinates_ThrowsException(int x, int y)
        {
            // Arrange
            var squares =
                new[]
                    {
                        // BWB
                        //  B
                        new Square(1, 0, Colour.Black),
                        new Square(0, 1, Colour.Black),
                        new Square(1, 1, Colour.White),
                        new Square(2, 1, Colour.Black)
                    };
            var piece = new Piece(squares);

            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => piece.SquareAt(x, y));
        }

        // TODO: IsValid() - Test for 0 squares
        // TODO: IsValid() - Test for negative x/y values
        // TODO: ISValid() - Test for x/y values >= width/height
        // TODO: IsValid() - Test for duplicate x/y values
    }

    // ReSharper restore InconsistentNaming
}
