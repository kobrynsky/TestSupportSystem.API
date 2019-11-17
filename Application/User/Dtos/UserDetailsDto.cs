using Application.Groups.Dtos;
using System.Collections.Generic;

namespace Application.User.Dtos
{
    public class UserDetailsDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<UserGroupDetailsDto> Groups { get; set; }

    }
}
