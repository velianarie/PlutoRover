namespace PlutoRover
{
    using System;

    public class Rover
    {
        private int x;
        private int y;
        private readonly char orientation;

        public Rover(int x, int y, char orientation)
        {
            if (orientation != 'N' && orientation != 'n'
                && orientation != 'S' && orientation != 's'
                && orientation != 'E' && orientation != 'e'
                && orientation != 'W' && orientation != 'w')
            {
                throw new ArgumentException($"Orientation '{orientation}' is not valid.");
            }

            this.x = x;
            this.y = y;
            this.orientation = orientation;
        }

        public Position Position
        {
            get { return new Position(x, y, orientation); }
        }

        public void Move(string commands)
        {
            foreach (var command in commands)
            {
                switch (command)
                {
                    case 'F':
                    case 'f':
                        if (orientation == 'N') y = y + 1;
                        else if (orientation == 'S') y = y - 1;
                        else if (orientation == 'E') x = x + 1;
                        else x = x - 1;
                        break;
                    case 'B':
                    case 'b':
                        if (orientation == 'N') y = y - 1;
                        else if (orientation == 'S') y = y + 1;
                        else if (orientation == 'E') x = x - 1;
                        else x = x + 1;
                        break;
                    default:
                        throw new Exception($"Command '{command}' is not valid.");
                }
            }
        }
    }
}