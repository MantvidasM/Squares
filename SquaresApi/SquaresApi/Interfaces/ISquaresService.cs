using SquaresApi.Dto;

namespace SquaresApi.Interfaces
{
	public interface ISquaresService
	{
        IEnumerable<IEnumerable<PointDto>> GetSquares();
        Task<bool> ImportPoints(IFormFile file);
        bool CreatePoint(PointDto point);
        bool DeletePoint(PointDto point);
    }
}

