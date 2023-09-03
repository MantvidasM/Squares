using SquaresApi.Dto;
using SquaresApi.Interfaces;
using SquaresApi.Models;

namespace SquaresApi.Services
{
	public class SquaresService : ISquaresService
    {
        private readonly IPointRepository _pointRepository;

        public SquaresService(IPointRepository pointRepository)
		{
            _pointRepository = pointRepository;
        }

        public Task<bool> ImportPoints(IFormFile file)
        {
            List<PointDto> points = new();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var pointValues = line.Split(',');
                        if (pointValues.Length == 2)
                        {
                            if (int.TryParse(pointValues[0], out int x)
                             && int.TryParse(pointValues[1], out int y))
                            {
                                points.Add(new PointDto(x, y));
                            }
                        }
                    }
                }
            }
            return Task.FromResult(CreatePoints(points));
        }

        public bool CreatePoint(PointDto point)
        {
            if (_pointRepository.GetPoint(point.X, point.Y) != null)
            {
                return true;
            }

            return _pointRepository.CreatePoint(new Point { X = point.X, Y = point.Y });
        }

        public bool DeletePoint(PointDto point)
        {
            var pointEntity = _pointRepository.GetPoint(point.X, point.Y);
            if (pointEntity == null)
            {
                return false;
            }

            return _pointRepository.DeletePoint(pointEntity);
        }

        public IEnumerable<IEnumerable<PointDto>> GetSquares()
        {
            var points = _pointRepository.GetPoints().Select(p => new PointDto(p.X, p.Y)).ToList();

            List<List<PointDto>> squares = FindSquares(points.ToList());

            return squares;
        }

        private bool CreatePoints(IEnumerable<PointDto> points)
        {
            var pointEntities = _pointRepository.GetPoints();
            var pointsToCreate = new List<Point>();

            foreach (var pointDto in points)
            {
                if (!pointEntities.Any(p => p.X == pointDto.X && p.Y == pointDto.Y))
                {
                    pointsToCreate.Add(new Point { X = pointDto.X, Y = pointDto.Y });
                }
            }

            return _pointRepository.CreatePoints(pointsToCreate);
        }

        private static List<List<PointDto>> FindSquares(List<PointDto> points)
        {
            List<List<PointDto>> squares = new();

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    PointDto point1 = points[i];
                    PointDto point2 = points[j];

                    int distanceX = point2.X - point1.X;
                    int distanceY = point2.Y - point1.Y;

                    PointDto point3 = new(point1.X - distanceY, point1.Y + distanceX);
                    PointDto point4 = new(point2.X - distanceY, point2.Y + distanceX);

                    if (points.Any(p => p.X == point3.X && p.Y == point3.Y) &&
                        points.Any(p => p.X == point4.X && p.Y == point4.Y))
                    {
                        List<PointDto> square = new() { point1, point2, point3, point4 };
                        if (!squares.Any(s => AreSquaresEqual(s, square)))
                        {
                            squares.Add(square);
                        }
                    }
                }
            }

            return squares;
        }

        private static bool AreSquaresEqual(IEnumerable<PointDto> square1, IEnumerable<PointDto> square2)
        {
            foreach (var point in square1)
            {
                if (!square2.Any(p => p.X == point.X && p.Y == point.Y))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
