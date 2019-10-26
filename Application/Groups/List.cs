using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Groups.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Groups
{
    public class List
    {
        public class Query : IRequest<List<GroupDto>>
        {
        }

        public class Handler : IRequestHandler<List.Query, List<GroupDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
                _userManager = userManager;
            }

            public async Task<List<GroupDto>> Handle(List.Query request, CancellationToken cancellationToken)
            {
                var currentUserName = _userAccessor.GetCurrentUsername();
                if (currentUserName == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var currentUser = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
                if (currentUser == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var groups = new System.Collections.Generic.List<Group>();

                switch (currentUser.Role)
                {
                    case Role.Administrator:
                        groups = await _context.Groups
                            .Include(x => x.Course)
                            .Include(x => x.UserGroups)
                            .ThenInclude(y => y.User)
                            .ToListAsync();
                        break;

                    case Role.MainLecturer:
                        if (currentUser.MainLecturerCourse == null)
                            throw new RestException(HttpStatusCode.BadRequest, new { Kursy = "Główny prowadzący nie jest przypisany do żadnego kursu" });

                        groups = await _context.Groups.Where(x => x.Course.Id == currentUser.MainLecturerCourse.Id)
                            .Include(x => x.Course)
                            .Include(x => x.UserGroups)
                            .ThenInclude(y => y.User)
                            .ToListAsync();
                        break;

                    case Role.Lecturer:
                        groups = await _context.Groups
                            .Where(x => currentUser.UserGroups
                                .Any(y => y.GroupId == x.Id))
                            .Include(x => x.Course)
                            .Include(x => x.UserGroups)
                            .ThenInclude(y => y.User)
                            .ToListAsync();
                        break;

                    case Role.Student:
                        groups = await _context.Groups
                            .Where(x => currentUser.UserGroups
                                .Any(y => y.GroupId == x.Id))
                            .Include(x => x.Course)
                            .ToListAsync();
                        break;
                }
                return _mapper.Map<List<GroupDto>>(groups);
            }
        }
    }
}
