using SquaresApi.Data;
using SquaresApi.Interfaces;
using SquaresApi.Models;

namespace SquaresApi.Repository
{
	public class PointRepository : IPointRepository
	{
		private readonly ApiDbContext _context;

		public PointRepository(ApiDbContext context)
		{
			_context = context;
		}

        public ICollection<Point> GetPoints()
		{
			return _context.Points.OrderBy(p => p.Id).ToList();
		}

        public Point? GetPoint(int x, int y)
        {
            return _context.Points.Where(p => p.X == x && p.Y == y).FirstOrDefault();
        }

        public bool CreatePoint(Point point)
        {
			_context.Points.Add(point);
            return Save();
        }

        public bool CreatePoints(IEnumerable<Point> points)
        {
            _context.Points.AddRange(points);
            return Save();
        }

        public bool DeletePoint(Point point)
        {
            _context.Points.Remove(point);
            return Save();
        }

        private bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}

