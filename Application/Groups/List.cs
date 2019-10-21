using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Courses;
using Application.Groups.Dtos;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Groups
{
    public class List
    {
        public class Query : IRequest<List<GroupDto>>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<GroupMemberDto> Members { get; set; }
        }

        public class QueryValidator : AbstractValidator<List.Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<List.Query, List<GroupDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GroupDto>> Handle(List.Query request, CancellationToken cancellationToken)
            {
                var groups = await _context.Groups.Include(x => x.UserGroups).ThenInclude(y => y.User).ToListAsync();
                return _mapper.Map<List<GroupDto>>(groups);
            }
        }
    }
}
