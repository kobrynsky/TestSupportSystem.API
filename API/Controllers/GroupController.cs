using API.Security;
using Application.Groups;
using Application.Groups.Dtos;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class GroupController : BaseController
    {
        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator, Role.Student)]
        [HttpGet]
        public async Task<ActionResult<List<GroupDto>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpPost("{groupId}/user/{userId}")]
        public async Task<ActionResult<Unit>> AddMember(Guid groupId, string userId)
        {
            return await Mediator.Send(new AddMember.Command { GroupId = groupId, UserId = userId });
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpPost("{groupId}/userEmail/{email}")]
        public async Task<ActionResult<Unit>> AddMemberByEmail(Guid groupId, string email)
        {
            return await Mediator.Send(new AddMemberByEmail.Command { GroupId = groupId, Email = email });
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpDelete("{groupId}/user/{userId}")]
        public async Task<ActionResult<Unit>> DeleteMember(Guid groupId, string userId)
        {
            return await Mediator.Send(new DeleteMember.Command { GroupId = groupId, UserId = userId });
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpGet("getByName/{name}")]
        public async Task<ActionResult<GroupDto>> GetByName(string name)
        {
            return await Mediator.Send(new GetByName.Query { Name = name });
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator, Role.Student)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDetailsDto>> Get(Guid id)
        {
            return await Mediator.Send(new Get.Query { Id = id });
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpPost("{groupId}/exercise/{exerciseId}")]
        public async Task<ActionResult<Unit>> AddExercise(Guid groupId, Guid exerciseId)
        {
            return await Mediator.Send(new AddExercise.Command { GroupId = groupId, ExerciseId = exerciseId });
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpPost("{groupId}/exerciseName/{exerciseName}")]
        public async Task<ActionResult<Unit>> AddExerciseByName(Guid groupId, string exerciseName)
        {
            return await Mediator.Send(new AddExerciseByName.Command { GroupId = groupId, ExerciseName = exerciseName });
        }
    }
}
