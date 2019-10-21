using System;
using System.Collections.Generic;
using System.Linq;
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
        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer)]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer)]
        [HttpGet]
        public async Task<ActionResult<List<GroupDto>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer)]
        [HttpPost("{groupId}/user/{userId}")]
        public async Task<ActionResult<Unit>> AddMembers(Guid groupId, string userId)
        {
            return await Mediator.Send(new AddMember.Command { GroupId = groupId, UserId = userId });
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer)]
        [HttpDelete("{groupId}/user/{userId}")]
        public async Task<ActionResult<Unit>> DeleteMember(Guid groupId, string userId)
        {
            return await Mediator.Send(new DeleteMember.Command { GroupId = groupId, UserId = userId });
        }

        [AuthorizeRoles(Role.Lecturer, Role.MainLecturer)]
        [HttpGet("getByName/{name}")]
        public async Task<ActionResult<GroupDto>> GetByName(string name)
        {
            return await Mediator.Send(new GetByName.Query{Name = name});
        }
    }
}
