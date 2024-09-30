using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        private readonly DbContextOptions options;

        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Database Seeding - dummy data upon initial setup of an application
            modelBuilder.Entity<City>().HasData
                (
                    new City("New York")
                    {
                        Id = 1,
                        Description = "The One with that big park."
                    },
                    new City("Antwerp")
                    {
                        Id = 2,
                        Description = "The One with the Cathedral that was never really finished."
                    },
                    new City("Paris")
                    {
                        Id = 3,
                        Description = "The One with that big tower."
                    }

                );
            modelBuilder.Entity<PointOfInterest>().HasData
                (
                    new PointOfInterest("Central Park")
                    {
                        Id = 1,
                        CityId = 1,
                        Description = "The most visted urban park."
                    },
                    new PointOfInterest("Empire State Building")
                    {
                        Id = 2,
                        CityId = 1,
                        Description = "A 102-story skyscrapper located in Midtown Manhattan."
                    },
                    new PointOfInterest("Cathedral")
                    {
                        Id = 3,
                        CityId = 2,
                        Description = "A Gothic style cathedral."
                    }
                );
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source = CityInfo.db");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
