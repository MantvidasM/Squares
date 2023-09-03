using SquaresApi.Models;

namespace SquaresApi.Interfaces
{
	public interface IPointRepository
	{
		ICollection<Point> GetPoints();
        Point? GetPoint(int x, int y);
        bool CreatePoint(Point point);
        bool CreatePoints(IEnumerable<Point> point);
        bool DeletePoint(Point point);

    }
}

