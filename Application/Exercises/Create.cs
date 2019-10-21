using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Courses.Dtos;
using Application.Errors;
using Application.ProgrammingLanguages.Dtos;
using FluentValidation;
using MediatR;
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
            public ProgrammingLanguageDto ProgrammingLanguage { get; set; }  
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Content).NotEmpty();
                RuleFor(x => x.Course.Id).NotEmpty();
                RuleFor(x => x.ProgrammingLanguage.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Exercises.Where(x => x.Name == request.Name && x.ProgrammingLanguage.Id == request.ProgrammingLanguage.Id).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Nazwa = "Zadanie o takiej nazwie w tym języku już istnieje" });

                var course = await _context.Courses.Where(x => x.Id == request.Course.Id).FirstOrDefaultAsync();

                if(course == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Kurs = "Kurs o zadanym Id nie istnieje" });

                var programmingLanguage = await _context.ProgrammingLanguages.Where(x => x.Id == request.ProgrammingLanguage.Id).FirstOrDefaultAsync();

                if (programmingLanguage == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Język = "Język programowania o zadanym Id nie istnieje" });

                var exercise = new Domain.Exercise()
                {
                    Content = request.Content,
                    InitialCode = request.InitialCode,
                    Name = request.Name,
                    ProgrammingLanguage = programmingLanguage,
                    Course = course,
                };

                _context.Exercises.Add(exercise);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem podczas zapisu");
            }
        }
    }
}