using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Courses.Dtos;
using Application.Errors;
using Application.Groups.Dtos;
using AutoMapper;
using Castle.Core.Internal;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Groups
{
    public class AddMember
    {
        public class Command : IRequest
        {
            public Guid GroupId { get; set; }
            public string UserId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.GroupId).NotEmpty().NotNull().WithMessage("Brak Id grupy");
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
                var group = await _context.Groups.Where(x => x.Id == request.GroupId).FirstOrDefaultAsync();

                if (group == null)
                    throw new RestException(HttpStatusCode.BadRequest, new {Grupa = "Nie znaleziono grupy"});

                var user = await _context.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync();

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