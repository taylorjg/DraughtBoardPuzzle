using NUnit.Framework;

namespace DraughtBoardPuzzle.Tests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    internal class RotatedPieceTests
    {
        private Piece _piece;

        [SetUp]
        public void SetUp()
        {
            var squares =
                new[]
                    {
                        // WBWB
                        // B
                        new Square(0, 0, Colour.Black),
                        new Square(0, 1, Colour.White),
                        new Square(1, 1, Colour.Black),
                        new Square(2, 1, Colour.White),
                    };
            _piece = new Piece(squares);
        }

        [Test]
        [TestCase(Orientation.North, 3, 2)]
        [TestCase(Orientation.South, 3, 2)]
        [TestCase(Orientation.East, 2, 3)]
        [TestCase(Orientation.West, 2, 3)]
        public void RotatedPiece_ForTestPieceWithGivenOrientation_HasCorrectWidthAndHeight(Orientation orientation, int expectedWidth, int expectedHeight)
        {
            // Arrange, Act
            var rotatedPiece = new RotatedPiece(_piece, orientation);

            // Assert
            Assert.That(rotatedPiece.Width, Is.EqualTo(expectedWidth));
            Assert.That(rotatedPiece.Height, Is.EqualTo(expectedHeight));
        }

        [Test]
        public void SquareAt_ForTestPieceWithNorthOrientation_ReturnsCorrectSquareDetailsForAllValidCoordinates()
        {
            // Arrange, Act
            var rotatedPiece = new RotatedPiece(_piece, Orientation.North);

            // Assert
            Assert.That(VerifySquareDetails(rotatedPiece, 0, 0, Colour.Black), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 0, 1, Colour.White), Is.True);
            Assert.That(VerifySquareIsNull(rotatedPiece, 1, 0), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 1, 1, Colour.Black), Is.True);
            Assert.That(VerifySquareIsNull(rotatedPiece, 2, 0), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 2, 1, Colour.White), Is.True);
        }

        [Test]
        public void SquareAt_ForTestPieceWithEastOrientation_ReturnsCorrectSquareDetailsForAllValidCoordinates()
        {
            // Arrange, Act
            var rotatedPiece = new RotatedPiece(_piece, Orientation.East);

            // Assert
            Assert.That(VerifySquareIsNull(rotatedPiece, 0, 0), Is.True);
            Assert.That(VerifySquareIsNull(rotatedPiece, 0, 1), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 0, 2, Colour.Black), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 1, 0, Colour.White), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 1, 1, Colour.Black), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 1, 2, Colour.White), Is.True);
        }

        [Test]
        public void SquareAt_ForTestPieceWithSouthOrientation_ReturnsCorrectSquareDetailsForAllValidCoordinates()
        {
            // Arrange, Act
            var rotatedPiece = new RotatedPiece(_piece, Orientation.South);

            // Assert
            Assert.That(VerifySquareDetails(rotatedPiece, 0, 0, Colour.White), Is.True);
            Assert.That(VerifySquareIsNull(rotatedPiece, 0, 1), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 1, 0, Colour.Black), Is.True);
            Assert.That(VerifySquareIsNull(rotatedPiece, 1, 1), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 2, 0, Colour.White), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 2, 1, Colour.Black), Is.True);
        }

        [Test]
        public void SquareAt_ForTestPieceWithWestOrientation_ReturnsCorrectSquareDetailsForAllValidCoordinates()
        {
            // Arrange, Act
            var rotatedPiece = new RotatedPiece(_piece, Orientation.West);

            // Assert
            Assert.That(VerifySquareDetails(rotatedPiece, 0, 0, Colour.White), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 0, 1, Colour.Black), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 0, 2, Colour.White), Is.True);
            Assert.That(VerifySquareDetails(rotatedPiece, 1, 0, Colour.Black), Is.True);
            Assert.That(VerifySquareIsNull(rotatedPiece, 1, 1), Is.True);
            Assert.That(VerifySquareIsNull(rotatedPiece, 1, 2), Is.True);
        }

        private bool VerifySquareDetails(RotatedPiece rotatedPiece, int x, int y, Colour colour)
        {
            var square = rotatedPiece.SquareAt(x, y);

            return
                square != null &&
                square.X == x &&
                square.Y == y &&
                square.Colour == colour;
        }

        private bool VerifySquareIsNull(RotatedPiece rotatedPiece, int x, int y)
        {
            return rotatedPiece.SquareAt(x, y) == null;
        }
    }

    // ReSharper restore InconsistentNaming
}
