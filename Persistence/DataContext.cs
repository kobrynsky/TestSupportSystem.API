using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseGroup> ExerciseGroups { get; set; }
        public DbSet<ExerciseUser> ExerciseUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasKey(x => x.Id);
            builder.Entity<UserGroup>().HasKey(x => new { x.UserId, x.GroupId });
            builder.Entity<ExerciseGroup>().HasKey(x => new { x.ExerciseId, x.GroupId });
            builder.Entity<ExerciseUser>().HasKey(x => new { x.ExerciseId, x.UserId });
            builder.Entity<Group>()
                .HasOne<Course>(x => x.Course)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.CourseId);

            builder.Entity<ApplicationUser>().HasMany(x => x.UserGroups).WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Group>().HasMany(x => x.UserGroups).WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Exercise>().HasMany(x => x.ExerciseGroups).WithOne(x => x.Exercise)
                .HasForeignKey(x => x.ExerciseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Group>().HasMany(x => x.ExerciseGroups).WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>().HasMany(x => x.ExerciseUsers).WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Exercise>().HasMany(x => x.ExerciseUsers).WithOne(x => x.Exercise)
                .HasForeignKey(x => x.ExerciseId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}