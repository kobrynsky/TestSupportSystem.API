﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string MainLecturerId { get; set; }
        [ForeignKey("MainLecturerId")]
        public virtual ApplicationUser MainLecturer { get; set; }
        public virtual IList<Group> Groups { get; set; }
    }
}
