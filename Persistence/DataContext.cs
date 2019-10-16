using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext: IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Value> Values { get; set; }
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
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<UserGroup>().HasKey(x => new { x.UserId, x.GroupId });
            builder.Entity<ExerciseGroup>().HasKey(x => new { x.ExerciseId, x.GroupId });
            builder.Entity<ExerciseUser>().HasKey(x => new { x.ExerciseId, x.UserId });
            builder.Entity<Value>()
                .HasData(
                    new Value {Id = 1, Name = "Value 101"},
                    new Value {Id = 2, Name = "Value 1021"},
                    new Value {Id = 3, Name = "Value 1031"},
                    new Value {Id = 4, Name = "Value 1041"},
                    new Value {Id = 5, Name = "Value 1051"}
                );
        }
    }
}