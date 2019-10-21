using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Groups.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Groups
{
    public class Get
    {
        public class Query : IRequest<GroupDetailsDto>
        {
            public Guid Id{ get; set; }
        }

        public class Handler : IRequestHandler<Query, GroupDetailsDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GroupDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var group = await _context.Groups.Where(x => x.Id == request.Id)
                    .Include(x => x.UserGroups).ThenInclude(x => x.User)
                    .Include(x => x.Course)
                    .FirstOrDefaultAsync();

                var dto = _mapper.Map<GroupDetailsDto>(group);

                return dto;
            }
        }
    }
}
