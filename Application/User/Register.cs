using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User
{
    public class Register
    {
        public class Command : IRequest<User>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public string RolePassword { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
                RuleFor(x => x.Role).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly DataContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
            {
                _jwtGenerator = jwtGenerator;
                _userAccessor = userAccessor;
                _userManager = userManager;
                _context = context;
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email już istnieje" });

                if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Nick = "Nick już istnieje" });

                if (request.Role == Role.MainLecturer || request.Role == Role.Lecturer || request.Role == Role.Administrator)
                {
                    var currentUserName = _userAccessor.GetCurrentUsername();

                    if (currentUserName == null)
                        throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień do rejestracji konta z taką rolą" });

                    var currentUser = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
                    if (currentUser == null || currentUser.Role != Role.Administrator)
                        throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień do rejestracji konta z taką rolą" });
                }

                var user = new ApplicationUser()
                {
                    LastName = request.LastName,
                    FirstName = request.FirstName,
                    Email = request.Email,
                    UserName = request.UserName,
                    Role = request.Role
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        UserName = user.UserName,
                        Role = user.Role,
                        Token = _jwtGenerator.CreateToken(user)
                    };
                }

                throw new Exception("Wystąpił błąd podczas tworzenia konta");
            }
        }
    }
}