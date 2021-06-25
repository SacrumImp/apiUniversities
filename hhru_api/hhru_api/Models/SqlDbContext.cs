using Microsoft.EntityFrameworkCore;

using hhru_api.Entities;

namespace hhru_api.Models
{
    public class SqlDbContext : DbContext
    {

        public DbSet<AreaEntity> Area { get; set; }
        public DbSet<UniversityEntity> Universities { get; set; }
        public DbSet<FacultyEntity> Faculties { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }

}