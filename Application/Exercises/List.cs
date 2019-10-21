using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Courses.Dtos;
using Application.Exercises.Dtos;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Exercises
{
    public class List
    {
        public class Query : IRequest<List<ExerciseDto>>
        {
        }

        public class Handler : IRequestHandler<Query, List<ExerciseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ExerciseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var exercises = await _context.Exercises.ToListAsync();
                return _mapper.Map<List<ExerciseDto>>(exercises);
            }
        }
    }
}
