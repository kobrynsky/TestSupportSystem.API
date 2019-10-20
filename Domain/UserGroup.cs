using System;

namespace Domain
{
    public class UserGroup
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
