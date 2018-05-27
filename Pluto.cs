namespace PlutoRover
{
    public class Pluto
    {
        private readonly int width;
        private readonly int length;

        public Pluto(int width, int length)
        {
            this.width = width;
            this.length = length;
        }

        public int Width
        {
            get { return width; }
        }

        public int Length
        {
            get { return length; }
        }
    }
}