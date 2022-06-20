using AutoMapper;
using Event.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Event.Application.Events.Queries.GetEventById
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery,EventDetailsDto>
    {
        private readonly IEventDbContext _eventDb;
        private readonly IMapper _mapper;
        public GetEventByIdQueryHandler(IEventDbContext eventDb, IMapper mapper)
        {
            _eventDb = eventDb;
            _mapper = mapper;
        }
        public async Task<EventDetailsDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var eventById = await _eventDb.Events.Include(x=>x.Speaker).FirstOrDefaultAsync(ev =>
            ev.Id == request.Id, cancellationToken);
            if (eventById == null)
            {
                throw new Exception($"Event with Id {request.Id} not found");
            }
            return _mapper.Map<EventDetailsDto>(eventById);
        }
    }
}
