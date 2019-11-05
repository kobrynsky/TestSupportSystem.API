using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Courses.Dtos;
using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
                if (await _context.Groups.Where(x => x.Name == request.Name).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Nazwa = "Grupa o takiej nazwie już istnieje" });

                var course = await _context.Courses.Where(x => x.Id == request.Course.Id).FirstOrDefaultAsync();

                if(course == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Kurs = "Nie znaleziono kursu"});

                var currentUserName = _userAccessor.GetCurrentUsername();
                if (currentUserName == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var currentUser = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
                if (currentUser == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { Role = "Brak uprawnień" });

                var group = new Group
                {
                    Id = request.Id,
                    Name = request.Name,
                    Course = course
                };

                await _context.Groups.AddAsync(group);

                if (currentUser.Role == Role.MainLecturer || currentUser.Role == Role.Lecturer)
                {
                    var userGroup = new UserGroup()
                    {
                        GroupId = group.Id,
                        UserId = currentUser.Id,
                    };

                    await _context.UserGroups.AddAsync(userGroup);
                }

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem podczas zapisu");
            }
        }
    }
}
