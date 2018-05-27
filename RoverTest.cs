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

    public class Rover
    {
        private readonly int x;
        private readonly int y;
        private readonly char orientation;

        public Rover(int x, int y, char orientation)
        {
            this.x = x;
            this.y = y;
            this.orientation = orientation;
        }

        public Position Position
        {
            get { return new Position(x, y, orientation); }
        }
    }

    public class Position
    {
        private readonly int x;
        private readonly int y;
        private readonly char orientation;

        public Position(int x, int y, char orientation)
        {
            this.x = x;
            this.y = y;
            this.orientation = orientation;
        }

        protected bool Equals(Position other)
        {
            return x == other.x && y == other.y && orientation == other.orientation;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = x;
                hashCode = (hashCode * 397) ^ y;
                hashCode = (hashCode * 397) ^ orientation.GetHashCode();
                return hashCode;
            }
        }
    }
}
