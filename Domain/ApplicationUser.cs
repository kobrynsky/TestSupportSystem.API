using System.Collections.Generic;
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
    }
}
