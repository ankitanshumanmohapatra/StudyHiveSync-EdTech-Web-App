using Microsoft.EntityFrameworkCore; // Importing the Entity Framework Core library
using StudyHiveSync2.Model; // Importing the models from the StudyHiveSync2.Model namespace

namespace StudyHiveSync2.Data
{
    public class ApplicationDbContext : DbContext // Defining the ApplicationDbContext class that inherits from DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) //constructor injection
            : base(options) // Passing the options to the base DbContext class
        {
        }

        // Defining DbSet properties for each entity in the application
        public DbSet<User> Users { get; set; } // DbSet for User entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<RatingAndReview> RatingAndReviews { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureCourseEntity(modelBuilder);

            //below new
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18,2)");
            });
        }

        private void ConfigureCourseEntity(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Course and User (Instructor)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);
            // Specify precision and scale for the Price property
            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
