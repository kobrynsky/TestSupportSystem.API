using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class UserGroup
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public Guid GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
    }
}
