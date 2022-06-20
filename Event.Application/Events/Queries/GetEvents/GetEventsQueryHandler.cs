using AutoMapper;
using AutoMapper.QueryableExtensions;
using Event.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Event.Application.Events.Queries.GetEvents
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, List<EventLookupDto>>
    {
        private readonly IEventDbContext _eventsDb;
        private readonly IMapper _mapper;
        public GetEventsQueryHandler(IEventDbContext eventsDb, IMapper mapper)
        {
            _eventsDb = eventsDb;
            _mapper = mapper;
        }

        public async Task<List<EventLookupDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var posts = _eventsDb.Events
                .ProjectTo<EventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return await posts;
        }
    }
}
