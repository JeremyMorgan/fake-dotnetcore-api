using Microsoft.EntityFrameworkCore;
using fake_dotnetcore_api.Models;

namespace fake_dotnetcore_api.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }
 
        public DbSet<Movie> Movies { get; set; }
    }
}