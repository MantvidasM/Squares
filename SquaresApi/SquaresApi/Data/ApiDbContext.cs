using Microsoft.EntityFrameworkCore;
using SquaresApi.Models;

namespace SquaresApi.Data
{
	public class ApiDbContext : DbContext
	{
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
		{
		}

		public DbSet<Point> Points { get; set; }
    }
}

