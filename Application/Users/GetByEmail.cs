using Application.Users.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users
{
    public class GetByEmail
    {
        public class Query : IRequest<UserDto>
        {
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
                return _mapper.Map<UserDto>(user);
            }
        }
    }
}