using System.Collections.Generic;
using System.Linq;
using DraughtBoardPuzzle.Dlx;
using DraughtBoardPuzzle.Tests.Builders;
using NUnit.Framework;

namespace DraughtBoardPuzzle.Tests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    internal class DlxSolverTests
    {
        private bool[,] _matrix;

        [SetUp]
        public void SetUp()
        {
            _matrix = MatrixBuilder.Build();
        }

        [Test]
        public void Solve_GivenEmptyMatrix_Returns1EmptySolution()
        {
            // Arrange
            var dlxSolver = new DlxSolver();
            var emptyMatrix = new bool[0,0];

            // Act
            var actual = dlxSolver.Solve(emptyMatrix);

            // Assert
            Assert.That(actual, Has.Count.EqualTo(1));
            Assert.That(actual.First(), Has.Count.EqualTo(0));
        }

        [Test]
        public void Solve_GivenANonEmptyMatrix_CreatesARoot()
        {
            // Arrange
            var dlxSolver = new DlxSolver();

            // Act
            var actual = dlxSolver.Solve(_matrix);

            // Assert
            Assert.That(dlxSolver.Root, Is.Not.Null);
        }

        [Test]
        public void Solve_GivenANonEmptyMatrix_Creates4ColumnsLinkedViaNext()
        {
            // Arrange
            var dlxSolver = new DlxSolver();

            // Act
            var actual = dlxSolver.Solve(_matrix);

            // Assert
            var numColumns = 0;
            for (var columnHeader = dlxSolver.Root.NextColumnHeader; columnHeader != dlxSolver.Root; columnHeader = columnHeader.NextColumnHeader)
                numColumns++;
            Assert.That(numColumns, Is.EqualTo(4));
        }

        [Test]
        public void Solve_GivenANonEmptyMatrix_Creates4ColumnsLinkedViaPrevious()
        {
            // Arrange
            var dlxSolver = new DlxSolver();

            // Act
            var actual = dlxSolver.Solve(_matrix);

            // Assert
            var numColumns = 0;
            for (var columnHeader = dlxSolver.Root.PreviousColumnHeader; columnHeader != dlxSolver.Root; columnHeader = columnHeader.PreviousColumnHeader)
                numColumns++;
            Assert.That(numColumns, Is.EqualTo(4));
        }

        [Test]
        public void Solve_GivenANonEmptyMatrix_CreatesColumnsOfTheCorrectSize()
        {
            // Arrange
            var dlxSolver = new DlxSolver();

            // Act
            var actual = dlxSolver.Solve(_matrix);

            // Assert
            var columnHeader1 = dlxSolver.Root.NextColumnHeader;
            var columnHeader2 = columnHeader1.NextColumnHeader;
            var columnHeader3 = columnHeader2.NextColumnHeader;
            var columnHeader4 = columnHeader3.NextColumnHeader;
            Assert.That(columnHeader1.Size, Is.EqualTo(2));
            Assert.That(columnHeader2.Size, Is.EqualTo(2));
            Assert.That(columnHeader3.Size, Is.EqualTo(3));
            Assert.That(columnHeader4.Size, Is.EqualTo(2));
            Assert.That(columnHeader4.NextColumnHeader, Is.EqualTo(dlxSolver.Root));
            Assert.That(columnHeader1.PreviousColumnHeader, Is.EqualTo(dlxSolver.Root));
            Assert.That(dlxSolver.Root.PreviousColumnHeader, Is.EqualTo(columnHeader4));
        }

        [Test]
        public void Solve_GivenANonEmptyMatrix_CreatesFirstColumnWithCorrectLinks()
        {
            // Arrange
            var dlxSolver = new DlxSolver();

            // Act
            var actual = dlxSolver.Solve(_matrix);

            // Assert
            var firstColumnHeader = dlxSolver.Root.NextColumnHeader;
            var firstNodeInColumn = firstColumnHeader.Down;
            var secondNodeInColumn = firstNodeInColumn.Down;
            Assert.That(firstNodeInColumn.Up, Is.EqualTo(firstColumnHeader));
            Assert.That(secondNodeInColumn.Up, Is.EqualTo(firstNodeInColumn));
            Assert.That(secondNodeInColumn.Down, Is.EqualTo(firstColumnHeader));
            Assert.That(firstNodeInColumn.Left, Is.EqualTo(firstNodeInColumn));
            Assert.That(firstNodeInColumn.Right, Is.EqualTo(firstNodeInColumn));
            Assert.That(firstNodeInColumn.ColumnHeader, Is.EqualTo(firstColumnHeader));
            Assert.That(secondNodeInColumn.ColumnHeader, Is.EqualTo(firstColumnHeader));
        }

        [Test]
        public void Solve_GivenANonEmptyMatrix_CreatesSecondColumnWithCorrectLinks()
        {
            // Arrange
            var dlxSolver = new DlxSolver();

            // Act
            var actual = dlxSolver.Solve(_matrix);

            // Assert
            var secondColumnHeader = dlxSolver.Root.NextColumnHeader.NextColumnHeader;
            var firstNodeInColumn = secondColumnHeader.Down;
            var secondNodeInColumn = firstNodeInColumn.Down;
            Assert.That(firstNodeInColumn.Up, Is.EqualTo(secondColumnHeader));
            Assert.That(secondNodeInColumn.Up, Is.EqualTo(firstNodeInColumn));
            Assert.That(secondNodeInColumn.Down, Is.EqualTo(secondColumnHeader));
            Assert.That(firstNodeInColumn.Left.Left, Is.EqualTo(firstNodeInColumn));
            Assert.That(firstNodeInColumn.Right.Right, Is.EqualTo(firstNodeInColumn));
            Assert.That(firstNodeInColumn.ColumnHeader, Is.EqualTo(secondColumnHeader));
        }

        [Test]
        public void Solve_GivenTheMatrixFromJanMagneTjensvoldPaper_ReturnsSomeSolutions()
        {
            // Arrange
            var dlxSolver = new DlxSolver();

            // Act
            var actual = dlxSolver.Solve(_matrix);

            // Assert
            Assert.That(actual, Has.Count.GreaterThan(0));
        }

        [Test]
        public void Solve_GivenTheMatrixFromTheJanMagneTjensvoldPaper_ReturnsThe3ExpectedSolutions()
        {
            // Arrange
            var dlxSolver = new DlxSolver();

            // Act
            var actual = dlxSolver.Solve(_matrix);

            // Assert
            var solutions = actual.ToList();
            Assert.That(solutions, Has.Count.EqualTo(3));
            Assert.That(solutions, Has.Member(new[] { 0, 3, 4 }));
            Assert.That(solutions, Has.Member(new[] { 1, 2 }));
            Assert.That(solutions, Has.Member(new[] { 2, 4, 5 }));
        }
    }
}
