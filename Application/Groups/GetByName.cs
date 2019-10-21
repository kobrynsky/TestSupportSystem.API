using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exercises.Dtos;
using Application.Groups.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Groups
{
    public class GetByName
    {
        public class Query : IRequest<GroupDto>
        {
            public string Name { get; set; }
        }

        public class Handler : IRequestHandler<Query, GroupDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GroupDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var group = await _context.Groups.Where(x => x.Name == request.Name).Include(x => x.Course).FirstOrDefaultAsync();
                return _mapper.Map<GroupDto>(group);
            }
        }
    }
}
