using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Student.Data.Config;
using Student.Data.Entities;



namespace Student.Data.DBContext
{
    public class AppDBContext : DbContext
    {
        public DbSet<Studentt> Students { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentConfiguration).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            //var constr = configuration.GetSection("constr").Value;

            //optionsBuilder.UseSqlServer(constr);
        }
    }
}
