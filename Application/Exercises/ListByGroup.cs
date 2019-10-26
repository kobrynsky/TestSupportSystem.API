using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Exercises.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Exercises
{
    public class ListByGroup
    {
        public class Query : IRequest<List<ExerciseDto>>
        {
            public Guid GroupId { get; set; }
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
                var group = await _context.Groups.Where(x => x.Id == request.GroupId).FirstOrDefaultAsync();

                if (group == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Grupa = "Nie znaleziono grupy" });

                var exercises = await _context.Exercises.Where(x => x.CourseId == group.CourseId)
                    .FirstOrDefaultAsync();

                return _mapper.Map<List<ExerciseDto>>(exercises);
            }
        }
    }
}