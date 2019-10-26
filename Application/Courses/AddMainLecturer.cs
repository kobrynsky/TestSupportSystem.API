using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Courses
{
    public class AddMainLecturer
    {
        public class Command : IRequest
        {
            public Guid CourseId { get; set; }
            public string UserId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CourseId).NotEmpty().NotNull().WithMessage("Brak Id kursu");
                RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("Brak Id użytkownika");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var course = await _context.Courses.Where(x => x.Id == request.CourseId).FirstOrDefaultAsync();

                if (course == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Grupa = "Nie znaleziono kursu" });

                var user = await _context.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync();

                if (user == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Użytkownik = "Nie znaleziono użytkownika" });

                var courseMainLecturer = await _context.CourseMainLecturers
                    .Where(x => x.CourseId == course.Id && x.MainLecturerId == user.Id)
                    .FirstOrDefaultAsync();

                if (courseMainLecturer != null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Użytkownik = "Użytkownik jest już głównym prowadzącym tego kursu" });

                courseMainLecturer = new CourseMainLecturer()
                {
                    CourseId = course.Id,
                    MainLecturerId = user.Id
                };

                _context.CourseMainLecturers.Add(courseMainLecturer);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem podczas dodawania");
            }
        }
    }
}