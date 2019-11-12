using Application.Errors;
using Application.Groups.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Groups
{
    public class Get
    {
        public class Query : IRequest<GroupDetailsDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, GroupDetailsDto>
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

            public async Task<GroupDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentUserName = _userAccessor.GetCurrentUsername();
                if (currentUserName == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var currentUser = await _userManager.FindByNameAsync(currentUserName);
                if (currentUser == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var group = await _context.Groups.Where(x => x.Id == request.Id)
                    .Include(x => x.UserGroups)
                    .ThenInclude(x => x.User)
                    .Include(x => x.Course)
                    .Include(x => x.ExerciseGroups)
                    .ThenInclude(x => x.Exercise)
                    .FirstOrDefaultAsync();

                if (currentUser.Role == Role.Student)
                {
                    var userGroups = await _context.UserGroups.Where(x => x.UserId == currentUser.Id).ToListAsync();
                    if (userGroups.All(x => x.GroupId != @group.Id))
                        throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                    var exercises = await _context.Exercises
                        .Include(x => x.Course)
                        .Include(x => x.Author)
                        .ToListAsync();

                    var exercisesResults = await _context.ExerciseResults
                        .Where(x => x.StudentId == currentUser.Id && x.GroupId == request.Id)
                        .Include(x => x.CorrectnessTestResults)
                        .ThenInclude(x => x.CorrectnessTest)
                        .ToListAsync();

                    var studentGroupDto = _mapper.Map<GroupDetailsDto>(group);


                    foreach (var exercise in studentGroupDto.Exercises)
                    {
                        var solved = exercisesResults
                            .Any(x => x.CorrectnessTestResults
                                          .First().CorrectnessTest.ExerciseId == exercise.Id);
                        exercise.Solved = solved;
                    }

                    return studentGroupDto;
                }

                var dto = _mapper.Map<GroupDetailsDto>(group);
                return dto;
            }
        }
    }
}
