using System.Threading.Tasks;
using Application.Courses;
using API.Security;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CourseController: BaseController
    {
        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer)]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
