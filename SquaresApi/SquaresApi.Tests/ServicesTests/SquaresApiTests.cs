using SquaresApi.Interfaces;
using SquaresApi.Models;
using SquaresApi.Services;
using FluentAssertions;
using Moq;
using SquaresApi.Dto;

namespace SquaresApi.Tests
{
    public class SquaresServiceTests
    {
        private Mock<IPointRepository> _pointRepositoryMock;
        private SquaresService _squaresService;

        public SquaresServiceTests()
        {
            _pointRepositoryMock = new Mock<IPointRepository>();
            _squaresService = new SquaresService(_pointRepositoryMock.Object);
        }

        [Fact]
        public void GetSquares_ReturnsListOfSquares()
        {
            // Arrange
            var square1 = new List<PointDto>
            {
                new PointDto(-1, 1),
                new PointDto(-1, -1),
                new PointDto(1, -1),
                new PointDto(1, 1)
            };
            var square2 = new List<PointDto>
            {
                new PointDto(5, 2),
                new PointDto(5, 4),
                new PointDto(7, 4),
                new PointDto(7, 2)
            };
            var square3 = new List<PointDto>
            {
                new PointDto(9, 1),
                new PointDto(9, 3),
                new PointDto(11, 3),
                new PointDto(11, 1)
            };
            var points = new List<Point>
            {
                new Point { X = -1, Y = 1 },
                new Point { X = -1, Y = -1 },
                new Point { X = 1, Y = -1 },
                new Point { X = 1, Y = 1 },

                new Point { X = 5, Y = 2 },
                new Point { X = 5, Y = 4 },
                new Point { X = 7, Y = 4 },
                new Point { X = 7, Y = 2 },

                new Point { X = 9, Y = 1 },
                new Point { X = 9, Y = 3 },
                new Point { X = 11, Y = 3 },
                new Point { X = 11, Y = 1 },

                new Point { X = -30, Y = 21 }
            };
            _pointRepositoryMock.Setup(p => p.GetPoints()).Returns(points);

            // Act
            var result = _squaresService.GetSquares();

            // Assert
            result.Should().HaveCount(3);
            result.Should().Contain(item => AreSquaresEqual(item, square1));
            result.Should().Contain(item => AreSquaresEqual(item, square2));
            result.Should().Contain(item => AreSquaresEqual(item, square3));
        }

        [Fact]
        public void GetSquares_ReturnsNoSquaresForGivenInput()
        {
            var points = new List<Point>
            {
                new Point { X = -1, Y = 1 },
                new Point { X = -1, Y = -1 },
                new Point { X = 1, Y = -1 },
                new Point { X = -30, Y = 21 }
            };
            _pointRepositoryMock.Setup(p => p.GetPoints()).Returns(points);

            // Act
            var result = _squaresService.GetSquares();

            // Assert
            result.Should().HaveCount(0);
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
