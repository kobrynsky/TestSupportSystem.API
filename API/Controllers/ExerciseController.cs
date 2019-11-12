using API.Security;
using Application.Exercises;
using Application.Exercises.Dtos;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Create = Application.Exercises.Create;
using List = Application.Exercises.List;


namespace API.Controllers
{
    public class ExerciseController : BaseController
    {
        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator, Role.Student)]
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

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator, Role.Student)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDetailsDto>> Get(Guid id)
        {
            return await Mediator.Send(new Get.Query { Id = id });
        }

        [AuthorizeRoles(Role.Student)]
        [HttpPost("solve")]
        public async Task<ActionResult<Unit>> Solve(Solve.Command command)
        {
            return await Mediator.Send(command);
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator, Role.Student)]
        [HttpGet("getSolved/{id}/group/{groupId}")]
        public async Task<ActionResult<SolvedExerciseDetailsDto>> GetSolved(Guid id, Guid groupId)
        {
            return await Mediator.Send(new GetSolved.Query { Id = id, GroupId = groupId });
        }
    }
}
