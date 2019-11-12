using Application.Courses.Dtos;
using Application.Errors;
using Application.Exercises.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Exercises
{
    public class GetSolved
    {
        public class Query : IRequest<SolvedExerciseDetailsDto>
        {
            public Guid Id { get; set; }
            public Guid GroupId { get; set; }
        }

        public class Handler : IRequestHandler<Query, SolvedExerciseDetailsDto>
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

            public async Task<SolvedExerciseDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentUserName = _userAccessor.GetCurrentUsername();
                if (currentUserName == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var currentUser = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
                if (currentUser == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var exercise = await _context.Exercises.Include(x => x.Course).Where(x => x.Id == request.Id).FirstAsync();

                var exerciseResult = await _context.ExerciseResults
                    .Where(x => x.StudentId == currentUser.Id &&
                                x.GroupId == request.GroupId &&
                                x.CorrectnessTestResults
                                    .All(y => y.CorrectnessTest.ExerciseId == exercise.Id))
                    .Include(x => x.CorrectnessTestResults)
                    .FirstAsync();

                var dto = new SolvedExerciseDetailsDto()
                {
                    Id = exercise.Id,
                    Code = exerciseResult.Code,
                    Course = _mapper.Map<CourseDto>(exercise.Course),
                    Content = exercise.Content,
                    Name = exercise.Name,
                    ProgrammingLanguage = exercise.ProgrammingLanguage,
                    CorrectnessTestsResults = _mapper.Map<List<CorrectnessTestResultDto>>(exerciseResult.CorrectnessTestResults),
                };

                return dto;
            }
        }
    }
}