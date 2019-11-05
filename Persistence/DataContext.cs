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
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseGroup> ExerciseGroups { get; set; }
        public DbSet<ExerciseUser> ExerciseUsers { get; set; }
        public DbSet<CourseMainLecturer> CourseMainLecturers { get; set; }
        public DbSet<ExerciseCourse> ExerciseCourses { get; set; }
        public DbSet<CorrectnessTestInput> CorrectnessTestInputs { get; set; }
        public DbSet<CorrectnessTestOutput> CorrectnessTestOutputs { get; set; }
        public DbSet<CorrectnessTest> CorrectnessTests { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasKey(x => x.Id);

            builder.Entity<UserGroup>()
                .HasKey(x => new { x.UserId, x.GroupId });

            builder.Entity<ExerciseGroup>()
                .HasKey(x => new { x.ExerciseId, x.GroupId });

            builder.Entity<Group>()
                .HasOne<Course>(x => x.Course)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.CourseId);

            builder.Entity<CourseMainLecturer>()
                .HasKey(x => new { x.CourseId, x.MainLecturerId });

            builder.Entity<ExerciseCourse>()
                .HasKey(x => new { x.CourseId, x.ExerciseId });

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.UserGroups)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Group>()
                .HasMany(x => x.UserGroups)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Exercise>()
                .HasMany(x => x.ExerciseGroups)
                .WithOne(x => x.Exercise)
                .HasForeignKey(x => x.ExerciseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Group>()
                .HasMany(x => x.ExerciseGroups)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ExerciseUser>()
                .HasOne(x => x.Student)
                .WithMany(x => x.ExerciseStudents)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ExerciseUser>()
                .HasOne(x => x.Lecturer)
                .WithMany(x => x.ExerciseLecturers)
                .HasForeignKey(x => x.LecturerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Exercise>()
                .HasMany(x => x.ExerciseUsers)
                .WithOne(x => x.Exercise)
                .HasForeignKey(x => x.ExerciseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ExerciseCourse>()
                .HasOne(x => x.Exercise)
                .WithMany(x => x.ExerciseCourses)
                .HasForeignKey(x => x.ExerciseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ExerciseCourse>()
                .HasOne(x => x.Course)
                .WithMany(x => x.ExerciseCourses)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Exercise>()
                .HasMany(x => x.ExerciseCourses)
                .WithOne(x => x.Exercise)
                .HasForeignKey(x => x.ExerciseId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}