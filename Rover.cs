namespace PlutoRover
{
    using System;

    public class Rover
    {
        private int x;
        private int y;
        private Orientation orientation;
        private Pluto pluto;

        public Rover(int x, int y, Orientation orientation)
        {
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
            foreach (var commandChar in commands)
            {
                var command = InputParser.ParseCommand(commandChar);
                if (pluto == null)
                {
                    Move(command, null, null);
                }
                else
                {
                    Move(command, pluto.Width, pluto.Length);
                }
            }
        }

        private void Move(Command command, int? maxX, int? maxY)
        {
            switch (command)
            {
                case Command.Forward:
                    if (orientation == Orientation.North) y = y + 1;
                    else if (orientation == Orientation.South) y = y - 1;
                    else if (orientation == Orientation.East) x = x + 1;
                    else x = x - 1;
                    break;
                case Command.Backward:
                    if (orientation == Orientation.North) y = y - 1;
                    else if (orientation == Orientation.South) y = y + 1;
                    else if (orientation == Orientation.East) x = x - 1;
                    else x = x + 1;
                    break;
                case Command.Left:
                    if (orientation == Orientation.North) orientation = Orientation.West;
                    else if (orientation == Orientation.South) orientation = Orientation.East;
                    else if (orientation == Orientation.East) orientation = Orientation.North;
                    else orientation = Orientation.South;
                    break;
                case Command.Right:
                    if (orientation == Orientation.North) orientation = Orientation.East;
                    else if (orientation == Orientation.South) orientation = Orientation.West;
                    else if (orientation == Orientation.East) orientation = Orientation.South;
                    else orientation = Orientation.North;
                    break;
                default:
                    throw new Exception($"Command '{command}' is not valid.");
            }

            if (maxX.HasValue)
            {
                if (x > maxX.Value) x = x - maxX.Value - 1;
                if (x < 0) x = maxX.Value + x + 1;
            }

            if (maxY.HasValue)
            {
                if (y > maxY.Value) y = y - maxY.Value - 1;
                if (y < 0) y = maxY.Value + y + 1;
            }
        }

        public void DeployTo(Pluto pluto)
        {
            this.pluto = pluto;
        }
    }
}