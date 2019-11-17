using Application.Groups.Dtos;
using Application.User.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class GetDetailsByEmail
    {
        public class Query : IRequest<UserDetailsDto>
        {
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<Query, UserDetailsDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();


                var groups = await _context.UserGroups
                    .Where(x => x.UserId == user.Id)
                    .Include(x => x.Group.ExerciseGroups)
                    .ThenInclude(x => x.Exercise)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.Group.Course)
                    .Select(x => x.Group)
                    .ToListAsync();




                var exercisesResults = await _context.ExerciseResults
                    .Where(x => x.StudentId == user.Id)
                    .Include(x => x.CorrectnessTestResults)
                    .ThenInclude(x => x.CorrectnessTest)
                    .ToListAsync();

                var studentGroupsDto = _mapper.Map<List<UserGroupDetailsDto>>(groups);
                var userDetailsDto = _mapper.Map<UserDetailsDto>(user);

                foreach (var studentGroupDto in studentGroupsDto)
                {
                    foreach (var exercise in studentGroupDto.Exercises)
                    {
                        try
                        {
                            var solved = exercisesResults
                                .Any(x => x.CorrectnessTestResults
                                              .First(y => y.ExerciseResult.GroupId == studentGroupDto.Id).CorrectnessTest.ExerciseId == exercise.Id);
                            exercise.Solved = solved;
                        }
                        catch
                        {
                            exercise.Solved = false;
                        }
                    }
                }

                userDetailsDto.Groups = studentGroupsDto;


                return userDetailsDto;
            }
        }
    }
}