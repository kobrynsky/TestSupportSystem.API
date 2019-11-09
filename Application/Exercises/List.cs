using Application.Errors;
using Application.Exercises.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Exercises
{
    public class List
    {
        public class Query : IRequest<List<ExerciseDto>>
        {
        }

        public class Handler : IRequestHandler<Query, List<ExerciseDto>>
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

            public async Task<List<ExerciseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentUserName = _userAccessor.GetCurrentUsername();
                if (currentUserName == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var currentUser = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
                if (currentUser == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var exercises = new System.Collections.Generic.List<Exercise>();

                switch (currentUser.Role)
                {
                    case Role.Administrator:
                        exercises = await _context.Exercises
                            .Include(x => x.Course)
                            .Include(x => x.Author)
                            .ToListAsync();
                        break;

                    case Role.MainLecturer:
                        if (currentUser.CourseMainLecturers == null || !currentUser.CourseMainLecturers.Any())
                            throw new RestException(HttpStatusCode.BadRequest, new { Kursy = "Główny prowadzący nie jest przypisany do żadnego kursu" });

                        var courseMainLecturers = _context.CourseMainLecturers.Where(x => x.MainLecturerId == currentUser.Id);
                        exercises = await _context.Exercises
                            .Where(x => courseMainLecturers
                                .Any(y => y.CourseId == x.CourseId))
                            .Include(x => x.Course)
                            .Include(x => x.Author)
                            .ToListAsync();
                        break;

                    case Role.Lecturer:
                        exercises = await _context.Exercises
                            .Where(x => x.AuthorId == currentUser.Id)
                            .Include(x => x.Course)
                            .Include(x => x.Author)
                            .ToListAsync();
                        break;

                    case Role.Student:
                        var exerciseUsers = _context.ExerciseUsers.Where(x => x.StudentId == currentUser.Id);
                        exercises = await _context.Exercises
                            .Where(x => exerciseUsers
                                .Any(y => y.ExerciseId == x.Id))
                            .Include(x => x.Course)
                            .Include(x => x.Author)
                            .ToListAsync();
                        break;
                }

                var dtos = _mapper.Map<List<ExerciseDto>>(exercises);

                return dtos;
            }
        }
    }
}
