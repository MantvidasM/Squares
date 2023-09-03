namespace SquaresApi.Dto
{
	public class PointDto
	{
        public PointDto(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}

