namespace PlutoRover
{
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
}