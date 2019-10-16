using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Value> Values { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
