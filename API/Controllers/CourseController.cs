using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Security;
using Application.Courses;
using Application.Courses.Dtos;
using Application.Groups;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Create = Application.Courses.Create;
using List = Application.Courses.List;

namespace API.Controllers
{
    public class CourseController: BaseController
    {
        [AuthorizeRoles(Role.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpGet]
        public async Task<ActionResult<List<CourseDto>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [AuthorizeRoles(Role.Administrator)]
        [HttpPost("{courseId}/mainlecturer/{userId}")]
        public async Task<ActionResult<Unit>> AddMainLecturer(Guid courseId, string userId)
        {
            return await Mediator.Send(new AddMainLecturer.Command { CourseId = courseId, UserId = userId });
        }
    }
}
