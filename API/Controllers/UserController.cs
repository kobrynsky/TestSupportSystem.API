using API.Security;
using Application.User;
using Application.User.Dtos;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using List = Application.User.List;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(Register.Command command)
        {
            return await Mediator.Send(command);
        }


        [HttpGet]
        public async Task<ActionResult<User>> CurrentUser()
        {
            return await Mediator.Send(new CurrentUser.Query());
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpGet("all")]
        public async Task<ActionResult<List<UserDto>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpGet("getByEmail/{email}")]
        public async Task<ActionResult<UserDto>> GetByEmail(string email)
        {
            return await Mediator.Send(new GetByEmail.Query { Email = email });
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpGet("getDetailsByEmail/{email}")]
        public async Task<ActionResult<UserDetailsDto>> GetDetailsByEmail(string email)
        {
            return await Mediator.Send(new GetDetailsByEmail.Query { Email = email });
        }


        //endpoint for testing Roles
        [Authorize(Roles = Role.Student)]
        [HttpPost("test")]
        public async Task<ActionResult<User>> Test(Login.Query query)
        {
            return await Mediator.Send(new CurrentUser.Query());
        }
    }
}