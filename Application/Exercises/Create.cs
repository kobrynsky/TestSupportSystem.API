using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Courses.Dtos;
using Application.Errors;
using Application.Exercises.Dtos;
using Application.Interfaces;
using Application.ProgrammingLanguages.Dtos;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Exercises
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Content { get; set; }
            public string InitialCode { get; set; }
            public CourseDto Course { get; set; }
            public string ProgrammingLanguage { get; set; }
            public List<CorrectnessTestsDto> CorrectnessTests { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Content).NotEmpty();
                RuleFor(x => x.Course.Id).NotEmpty();
                RuleFor(x => x.ProgrammingLanguage).NotEmpty();
                RuleFor(x => x.CorrectnessTests).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(DataContext context, IUserAccessor userAccessor, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _userAccessor = userAccessor;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Exercises.Where(x => x.Name == request.Name && x.ProgrammingLanguage == request.ProgrammingLanguage).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Nazwa = "Zadanie o takiej nazwie w tym języku już istnieje" });

                var course = await _context.Courses.Where(x => x.Id == request.Course.Id).FirstOrDefaultAsync();

                if(course == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Kurs = "Kurs o zadanym Id nie istnieje" });

                var currentUserName = _userAccessor.GetCurrentUsername();
                if (currentUserName == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var currentUser = await _userManager.FindByNameAsync(currentUserName);
                if (currentUser == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });


                var exercise = new Domain.Exercise()
                {
                    Content = request.Content,
                    InitialCode = request.InitialCode,
                    Name = request.Name,
                    ProgrammingLanguage = request.ProgrammingLanguage,
                    Course = course,
                    AuthorId = currentUser.Id,
                };

                _context.Exercises.Add(exercise);

                var correctnessTests = new List<CorrectnessTest>();

                foreach (var correctnessTest in request.CorrectnessTests)
                {
                    var inputs = new List<CorrectnessTestInput>();
                    var outputs = new List<CorrectnessTestOutput>();

                    foreach (var input in correctnessTest.Inputs)
                    {
                        inputs.Add(new CorrectnessTestInput()
                        {
                            Content = input
                        });
                    }

                    foreach (var output in correctnessTest.Outputs)
                    {
                        outputs.Add(new CorrectnessTestOutput()
                        {
                            Content = output
                        });
                    }

                    correctnessTests.Add(new CorrectnessTest()
                    {
                        ExerciseId = exercise.Id,
                        Inputs = inputs,
                        Outputs = outputs,
                    });
                };

                _context.CorrectnessTests.AddRange(correctnessTests);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem podczas zapisu");
            }
        }
    }
}