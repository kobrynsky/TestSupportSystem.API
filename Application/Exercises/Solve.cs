using Application.Errors;
using Application.Exercises.Models;
using Application.Interfaces;
using Domain;
using FluentValidation;
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
    public class Solve
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid GroupId { get; set; }
            public string Code { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.GroupId).NotEmpty();
                RuleFor(x => x.Code).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IApiCompiler _apiCompiler;
            public Handler(DataContext context, IUserAccessor userAccessor, UserManager<ApplicationUser> userManager, IApiCompiler apiCompiler)
            {
                _context = context;
                _userAccessor = userAccessor;
                _userManager = userManager;
                _apiCompiler = apiCompiler;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUserName = _userAccessor.GetCurrentUsername();
                if (currentUserName == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var currentUser = await _userManager.FindByNameAsync(currentUserName);
                if (currentUser == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                if (currentUser.Role != Role.Student)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Tylko Student może rozwiązywać zadania" });

                var exercise = await _context.Exercises.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

                var tests = await _context.CorrectnessTests
                    .Where(x => x.ExerciseId == exercise.Id)
                    .Include(x => x.Inputs)
                    .Include(x => x.Outputs)
                    .ToListAsync();

                if (exercise == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Exercise = "Nie znaleziono zadania" });

                var correctnessTestsResults = new List<CorrectnessTestResult>();

                foreach (var test in tests)
                {
                    var submission = ConvertToSubmission(request.Code,
                        exercise.ProgrammingLanguage,
                        test.Inputs.Select(x => x.Content).ToArray(),
                        test.Outputs.Select(x => x.Content).ToArray());

                    var response = await _apiCompiler.SendSubmission(submission);

                    var result = new CorrectnessTestResult()
                    {
                        CorrectnessTestId = test.Id,
                        Memory = response.memory ?? 0,
                        CompileOutput = response.compile_output,
                        Error = response.stderr,
                        Message = response.message,
                        Status = response.status.description,
                        Time = response.time ?? "0  ",
                    };
                    correctnessTestsResults.Add(result);
                }

                var exerciseResult = new ExerciseResult()
                {
                    Code = request.Code,
                    StudentId = currentUser.Id,
                    GroupId = request.GroupId,
                    CorrectnessTestResults = correctnessTestsResults,
                };

                await _context.ExerciseResults.AddAsync(exerciseResult);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem podczas zapisu");
            }

            private Submission ConvertToSubmission(string code, string programmingLanguage, string[] inputs, string[] outputs)
            {
                return new Submission()
                {
                    language_id = GetProgrammingLanguageId(programmingLanguage),
                    source_code = code,
                    stdin = string.Join("\n", inputs),
                    expected_output = string.Join("\n", outputs),
                };
            }

            private int GetProgrammingLanguageId(string programmingLanguage)
            {
                return programmingLanguage switch
                {
                    "C++" => 54,
                    "C#" => 51,
                    "Java" => 62,
                    "Python" => 71,
                    _ => 0
                };
            }
        }
    }
}