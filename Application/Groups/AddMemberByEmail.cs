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
    public class AddMemberByEmail
    {
        public class Command : IRequest
        {
            public Guid GroupId { get; set; }
            public string Email { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.GroupId).NotEmpty().NotNull().WithMessage("Brak Id grupy");
                RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Brak emaila użytkownika");
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

                var user = await _context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();

                if (user == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Użytkownik = "Nie znaleziono użytkownika" });

                var userGroup = await _context.UserGroups.Where(x => x.GroupId == group.Id && x.UserId == user.Id).FirstOrDefaultAsync();

                if (userGroup != null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Użytkownik = "Użytkownik jest już w grupie" });

                userGroup = new UserGroup()
                {
                    GroupId = group.Id,
                    UserId = user.Id
                };

                _context.UserGroups.Add(userGroup);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem podczas dodawania");
            }
        }
    }
}