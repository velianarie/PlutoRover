namespace PlutoRover
{
    using NUnit.Framework;

    [TestFixture]
    public class RoverTest
    {
        [Test]
        public void NewRoverInstanceShouldSetItsPosition()
        {
            var rover = new Rover(0, 0, 'N');
            var expectedPosition = new Position(0, 0, 'N');
            Assert.That(rover.Position, Is.EqualTo(expectedPosition));
        }
    }
}
