namespace PlutoRover
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class RoverTest
    {
        private static IEnumerable<TestCaseData> SingleCommandFTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, Orientation.North), new Position(5, 6, Orientation.North));
                yield return new TestCaseData(new Rover(5, 5, Orientation.South), new Position(5, 4, Orientation.South));
                yield return new TestCaseData(new Rover(5, 5, Orientation.East), new Position(6, 5, Orientation.East));
                yield return new TestCaseData(new Rover(5, 5, Orientation.West), new Position(4, 5, Orientation.West));
            }
        }

        private static IEnumerable<TestCaseData> SingleCommandBTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, Orientation.North), new Position(5, 4, Orientation.North));
                yield return new TestCaseData(new Rover(5, 5, Orientation.South), new Position(5, 6, Orientation.South));
                yield return new TestCaseData(new Rover(5, 5, Orientation.East), new Position(4, 5, Orientation.East));
                yield return new TestCaseData(new Rover(5, 5, Orientation.West), new Position(6, 5, Orientation.West));
            }
        }

        private static IEnumerable<TestCaseData> SingleCommandLTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, Orientation.North), new Position(5, 5, Orientation.West));
                yield return new TestCaseData(new Rover(5, 5, Orientation.South), new Position(5, 5, Orientation.East));
                yield return new TestCaseData(new Rover(5, 5, Orientation.East), new Position(5, 5, Orientation.North));
                yield return new TestCaseData(new Rover(5, 5, Orientation.West), new Position(5, 5, Orientation.South));
            }
        }

        private static IEnumerable<TestCaseData> SingleCommandRTestData
        {
            get
            {
                yield return new TestCaseData(new Rover(5, 5, Orientation.North), new Position(5, 5, Orientation.East));
                yield return new TestCaseData(new Rover(5, 5, Orientation.South), new Position(5, 5, Orientation.West));
                yield return new TestCaseData(new Rover(5, 5, Orientation.East), new Position(5, 5, Orientation.South));
                yield return new TestCaseData(new Rover(5, 5, Orientation.West), new Position(5, 5, Orientation.North));
            }
        }

        [Test]
        public void NewRoverInstanceShouldSetItsPosition()
        {
            var rover = new Rover(0, 0, Orientation.North);
            var expectedPosition = new Position(0, 0, Orientation.North);
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test, TestCaseSource(nameof(SingleCommandFTestData))]
        public void SingleCommandFShouldMoveRoverForwardByOneGridPoint(Rover rover, Position expectedPosition)
        {
            rover.Move("F");
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test, TestCaseSource(nameof(SingleCommandBTestData))]
        public void SingleCommandBShouldMoveRoverBackwardByOneGridPoint(Rover rover, Position expectedPosition)
        {
            rover.Move("B");
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void MovingRoverWithUnrecognizedCommandShouldThrowException()
        {
            var rover = new Rover(0, 0, Orientation.North);
            Assert.Throws<Exception>(() => rover.Move("x")).Message.Equals("Command 'x' is not valid.");
        }

        [Test, TestCaseSource(nameof(SingleCommandLTestData))]
        public void SingleCommandLShouldTurnRover90DegreesLeft(Rover rover, Position expectedPosition)
        {
            rover.Move("L");
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test, TestCaseSource(nameof(SingleCommandRTestData))]
        public void SingleCommandRShouldTurnRover90DegreesRight(Rover rover, Position expectedPosition)
        {
            rover.Move("R");
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void ChainOfCommands()
        {
            var rover = new Rover(0, 0, Orientation.North);
            rover.Move("FFRFF");
            var expectedPosition = new Position(2, 2, Orientation.East);
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }
    }
}
