﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UserGroup
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
