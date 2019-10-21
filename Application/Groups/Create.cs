using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Courses;
using Application.Courses.Dtos;
using Application.Errors;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Groups
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public CourseDto Course { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Course.Id).NotEmpty();
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
                if (await _context.Groups.Where(x => x.Name == request.Name).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Nazwa = "Grupa o takiej nazwie już istnieje" });

                var course = await _context.Courses.Where(x => x.Id == request.Course.Id).FirstOrDefaultAsync();

                if(course == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Kurs = "Nie znaleziono kursu"});

                var group = new Group
                {
                    Id = request.Id,
                    Name = request.Name,
                    Course = course
                };

                _context.Groups.Add(group);


                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem podczas zapisu");
            }
        }
    }
}
