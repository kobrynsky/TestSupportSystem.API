using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<ApplicationUser> userManager)
        {
            await SeedUsers(userManager);
            await SeedCourses(context);
        }

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Id = "f71308b6-e185-44ff-997d-86bc23f849e9",
                        FirstName = "Admin",
                        LastName = "Admin",
                        UserName = "Admin",
                        Email = "admin@admin.com",
                        Role = "Administrator"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Haslo123!");
                }

            }
        }

        private static async Task SeedCourses(DataContext context)
        {
            var dbCourses = await context.Courses.ToListAsync();

            if (!dbCourses.Any())
            {
                var courses = new List<Course>
                {
                    new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Modelowanie i anal.sys.infor.",
                    },
                    new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Podstawy Programowania",
                    },
                    new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Programowanie Obiektowe",
                    },
                    new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Języki Programowania",
                    },
                };

                await context.Courses.AddRangeAsync(courses);
                await context.SaveChangesAsync();

            }
        }
    }
}