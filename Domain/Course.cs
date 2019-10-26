using System;
using System.Collections.Generic;

namespace Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Group> Groups { get; set; }
        public virtual IList<CourseMainLecturer> CourseMainLecturers { get; set; }
        public virtual IList<ExerciseCourse> ExerciseCourses { get; set; }
    }
}
