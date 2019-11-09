using Application.Exercises.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Exercises
{
    public class Get
    {
        public class Query : IRequest<ExerciseDetailsDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ExerciseDetailsDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ExerciseDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var exercise = await _context.Exercises.Where(x => x.Id == request.Id)
                    .Include(x => x.Author)
                    .Include(x => x.Course)
                    .FirstOrDefaultAsync();

                var correctnessTests = await _context.CorrectnessTests
                    .Where(x => x.ExerciseId == exercise.Id)
                    .Include(x => x.Inputs)
                    .Include(x => x.Outputs)
                    .ToListAsync();

                var dto = _mapper.Map<ExerciseDetailsDto>(exercise);
                dto.CorrectnessTests = _mapper.Map<List<CorrectnessTestDto>>(correctnessTests);

                return dto;
            }
        }
    }
}
