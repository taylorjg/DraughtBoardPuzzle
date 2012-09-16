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
            for (var colHeader = dlxSolver.Root.Next; colHeader != dlxSolver.Root; colHeader = colHeader.Next)
            {
                numColumns++;
            }
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
            for (var colHeader = dlxSolver.Root.Previous; colHeader != dlxSolver.Root; colHeader = colHeader.Previous)
            {
                numColumns++;
            }
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
            var colHeader1 = dlxSolver.Root.Next;
            var colHeader2 = colHeader1.Next;
            var colHeader3 = colHeader2.Next;
            var colHeader4 = colHeader3.Next;
            Assert.That(colHeader1.Size, Is.EqualTo(2));
            Assert.That(colHeader2.Size, Is.EqualTo(2));
            Assert.That(colHeader3.Size, Is.EqualTo(3));
            Assert.That(colHeader4.Size, Is.EqualTo(2));
        }

        [Test]
        public void Solve_GivenANonEmptyMatrix_CreatesFirstColumnWithCorrectLinks()
        {
            // Arrange
            var dlxSolver = new DlxSolver();

            // Act
            var actual = dlxSolver.Solve(_matrix);

            // Assert
            var firstColHeader = dlxSolver.Root.Next;
            var firstNodeInColumn = firstColHeader.Node.Down;
            var secondNodeInColumn = firstNodeInColumn.Down;
            Assert.That(firstNodeInColumn.Up, Is.EqualTo(firstColHeader.Node));
            Assert.That(secondNodeInColumn.Up, Is.EqualTo(firstNodeInColumn));
            Assert.That(secondNodeInColumn.Down, Is.EqualTo(firstColHeader.Node));
            Assert.That(firstNodeInColumn.Left, Is.EqualTo(firstNodeInColumn));
            Assert.That(firstNodeInColumn.Right, Is.EqualTo(firstNodeInColumn));
            Assert.That(firstNodeInColumn.Column, Is.EqualTo(firstColHeader));
            Assert.That(secondNodeInColumn.Column, Is.EqualTo(firstColHeader));
        }

        [Test]
        public void Solve_GivenANonEmptyMatrix_CreatesSecondColumnWithCorrectLinks()
        {
            // Arrange
            var dlxSolver = new DlxSolver();

            // Act
            var actual = dlxSolver.Solve(_matrix);

            // Assert
            var secondColHeader = dlxSolver.Root.Next.Next;
            var firstNodeInColumn = secondColHeader.Node.Down;
            var secondNodeInColumn = firstNodeInColumn.Down;
            Assert.That(firstNodeInColumn.Up, Is.EqualTo(secondColHeader.Node));
            Assert.That(secondNodeInColumn.Up, Is.EqualTo(firstNodeInColumn));
            Assert.That(secondNodeInColumn.Down, Is.EqualTo(secondColHeader.Node));
            Assert.That(firstNodeInColumn.Left.Left, Is.EqualTo(firstNodeInColumn));
            Assert.That(firstNodeInColumn.Right.Right, Is.EqualTo(firstNodeInColumn));
            Assert.That(firstNodeInColumn.Column, Is.EqualTo(secondColHeader));
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
