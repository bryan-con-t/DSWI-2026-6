using cibertec_demo.Models;
using Microsoft.EntityFrameworkCore;

namespace cibertec_demo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
