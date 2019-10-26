using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Security;
using Application.Groups;
using Application.Groups.Dtos;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GroupController: BaseController
    {
        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpGet]
        public async Task<ActionResult<List<GroupDto>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpPost("{groupId}/user/{userId}")]
        public async Task<ActionResult<Unit>> AddMembers(Guid groupId, string userId)
        {
            return await Mediator.Send(new AddMember.Command { GroupId = groupId, UserId = userId });
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
            return await Mediator.Send(new GetByName.Query{Name = name});
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer, Role.Administrator)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDetailsDto>> Get(Guid id)
        {
            return await Mediator.Send(new Get.Query { Id = id });
        }
    }
}
