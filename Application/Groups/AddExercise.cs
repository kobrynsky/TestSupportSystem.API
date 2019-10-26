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

namespace Application.Groups
{
    public class AddExercise
    {
        public class Command : IRequest
        {
            public Guid GroupId { get; set; }
            public Guid ExerciseId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.GroupId).NotEmpty().NotNull().WithMessage("Brak Id grupy");
                RuleFor(x => x.ExerciseId).NotEmpty().NotNull().WithMessage("Brak Id zadania");
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
                var group = await _context.Groups.Where(x => x.Id == request.GroupId).FirstOrDefaultAsync();

                if (group == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Grupa = "Nie znaleziono grupy" });

                var exercise = await _context.Exercises.Where(x => x.Id == request.ExerciseId).FirstOrDefaultAsync();

                if (exercise == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Zadanie = "Nie znaleziono zadania" });

                var exerciseGroup = await _context.ExerciseGroups.Where(x => x.GroupId == group.Id && x.ExerciseId == exercise.Id).FirstOrDefaultAsync();

                if (exerciseGroup != null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Zadanie = "Zadanie już jest przypisane do grupy" });

                exerciseGroup = new ExerciseGroup()
                {
                    GroupId = group.Id,
                    ExerciseId = exercise.Id
                };

                _context.ExerciseGroups.Add(exerciseGroup);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem podczas dodawania");
            }
        }
    }
}