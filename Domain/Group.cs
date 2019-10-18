using System;
using System.Collections.Generic;

namespace Domain
{
    public class Group
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<UserGroup> UserGroups { get; set; }
    }
}
