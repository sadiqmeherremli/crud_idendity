using back_end_projects.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end_projects.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
