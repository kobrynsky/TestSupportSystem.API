using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Security;
using Application.Exercises.Dtos;
using Application.Groups;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Create = Application.Exercises.Create;
using List = Application.Exercises.List;


namespace API.Controllers
{
    public class ExerciseController: BaseController
    {
        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpGet]
        public async Task<ActionResult<List<ExerciseDto>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpGet("/group/{groupId}")]
        public async Task<ActionResult<List<ExerciseDto>>> ListByGroup(Guid groupId)
        {
            return await Mediator.Send(new ListByGroup.Query { GroupId = groupId });
        }
    }
}
