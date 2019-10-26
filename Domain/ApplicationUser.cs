using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public virtual IList<UserGroup> UserGroups { get; set; }
        public virtual IList<ExerciseUser> ExerciseStudents { get; set; }
        public virtual IList<ExerciseUser> ExerciseLecturers { get; set; }

        public Guid MainLecturerCourseId { get; set; }
        [ForeignKey("MainLecturerCourseId")]
        public virtual Course MainLecturerCourse { get; set; }
    }
}
